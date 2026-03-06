//-----------------------------------------------------------------------
// <copyright file="TelegramWorkerService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.ObjectModel;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TacosPortal.Services.Telegram
{
    public class TelegramWorkerService(ILogger<TelegramWorkerService> logger, ITelegramBotClient bot, ITacosApi tacosApi)
    {
        private async Task UpdateChats(CancellationToken cancellationToken)
        {
            try
            {
                var Chats = tacosApi.GetChats(cancellationToken);
                ArgumentNullException.ThrowIfNull(Chats);
                ObservableCollection<ChatFullInfo> chatFullInfos = new ObservableCollection<ChatFullInfo>();
                foreach (var chat in Chats)
                {
                    try
                    {
                        var chatFullInfo = await bot.GetChat(chat.ChatId, cancellationToken).ConfigureAwait(false);
                        ArgumentNullException.ThrowIfNull(chatFullInfo);
                        chatFullInfos.Add(chatFullInfo);
                        await Task.Delay(50, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException ex)
                    {
                    }
                    catch (ApiRequestException ex)
                    {
                        logger.LogError(ex, $"ApiRequestException in get so set Chat to ignore for {chat}: {ex.ToString()}");
                        tacosApi.IgnoreChat(chat, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error in get ChatFullInfo for {chat}: {ex.ToString()}");
                    }
                }
                var chatFullInfosInDb = tacosApi.GetMostRecentChatFullInfos(cancellationToken);
                foreach (var chatFullInfo in chatFullInfos.Where(chatFullInfoFromTg => chatFullInfosInDb.Count(
                    chatfullInfoFromDb => chatfullInfoFromDb.Chat.ChatId == chatFullInfoFromTg.Id
                    && chatfullInfoFromDb.DateCreated == DateOnly.FromDateTime(DateTime.Today)) <= 0))
                {
                    try
                    {
                        await tacosApi.NewFullChatInfo(chatFullInfo, cancellationToken).ConfigureAwait(false);
                        var me = await bot.GetMe(cancellationToken).ConfigureAwait(false);
                        ArgumentNullException.ThrowIfNull(me);
                        tacosApi.AddOrUpdateUser(me, cancellationToken);
                        bool isMyChat = tacosApi.TelegramChatBotRightsUser(me, chatFullInfo, cancellationToken);
                        await Task.Delay(50, cancellationToken).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error in trying update ChatFullInfo: for {chatFullInfo}: {ex.ToString()}");
                    }
                }

            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UpdateChats: {ex.ToString()}");
            }
        }
        private async Task UpdateMe(CancellationToken cancellationToken)
        {
            try
            {
                var me = await bot.GetMe(cancellationToken).ConfigureAwait(false);
                ArgumentNullException.ThrowIfNull(me);
                tacosApi.AddOrUpdateUser(me, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UpdateMe: {ex.ToString()}");
            }
        }

        private async Task UpdateUserInChats(CancellationToken cancellationToken)
        {
            try
            {
                var chatsFullInfos = tacosApi.GetMostRecentChatFullInfos(cancellationToken);
                ArgumentNullException.ThrowIfNull(chatsFullInfos);
                var users = tacosApi.GetTelegramUsers(cancellationToken);
                ArgumentNullException.ThrowIfNull(chatsFullInfos);
                ObservableCollection<ChatMember> chatMembers = new ObservableCollection<ChatMember>();

                foreach (var chatFullInfo in chatsFullInfos)
                {
                    try
                    {
                        ArgumentNullException.ThrowIfNull(chatFullInfo.Chat);
                        var telegramChatFullInfo = await bot.GetChat(chatFullInfo.Chat.ChatId, cancellationToken).ConfigureAwait(false);
                        if (telegramChatFullInfo.ActiveUsernames != null && telegramChatFullInfo.ActiveUsernames.LongLength > 0)
                        {
                            foreach (var member in telegramChatFullInfo.ActiveUsernames)
                            {
                                try
                                {
                                    long? tgChatId = telegramChatFullInfo.Id;
                                    ArgumentNullException.ThrowIfNull(tgChatId);
                                    ChatId chatId = new(tgChatId.Value);
                                    foreach (var user in users.Where(x => x.Username == member))
                                    {
                                        var chatMember = await bot.GetChatMember(chatId, user.UserId, cancellationToken).ConfigureAwait(false);
                                        if (chatMember != null)
                                        {
                                            tacosApi.AddOrUpdateUser(chatMember.User, cancellationToken);
                                            tacosApi.AddOrUpdateUserChats(chatMember, telegramChatFullInfo, chatFullInfo, cancellationToken);

                                        }
                                        await Task.Delay(50, cancellationToken);
                                    }
                                }
                                catch (OperationCanceledException ex)
                                {
                                }
                                catch (Exception ex)
                                {
                                    logger.LogWarning(ex, $"Error in Get and Update ChatMember: {ex.ToString()}");
                                }
                            }
                        }
                        if (chatFullInfo.Chat.Type != ChatType.Private)
                        {
                            var telegramAdminsOfChat = await bot.GetChatAdministrators(chatFullInfo.Chat.ChatId, cancellationToken).ConfigureAwait(false);
                            foreach (var telegramAdminOfChat in telegramAdminsOfChat)
                            {
                                try
                                {
                                    long? tgChatId = telegramChatFullInfo.Id;
                                    ArgumentNullException.ThrowIfNull(tgChatId);
                                    ChatId chatId = new(tgChatId.Value);
                                    var telegramChatMember = await bot.GetChatMember(chatId, telegramAdminOfChat.User.Id, cancellationToken).ConfigureAwait(false);
                                    if (telegramChatMember != null)
                                    {
                                        tacosApi.AddOrUpdateUser(telegramChatMember.User, cancellationToken);
                                        tacosApi.AddOrUpdateUserChats(telegramChatMember, telegramChatFullInfo, chatFullInfo, cancellationToken);

                                    }
                                    await Task.Delay(50, cancellationToken);
                                }
                                catch (OperationCanceledException ex)
                                {
                                }
                                catch (Exception ex)
                                {
                                    logger.LogWarning(ex, $"Error in processing telegramAdminOfChat {ex.ToString()}");
                                }
                            }
                        }



                        var telegramUsersOfChat = tacosApi.GetUsersAppeardInRoom(chatFullInfo, cancellationToken);

                        foreach (var telegramUserOfChat in telegramUsersOfChat)
                        {
                            try
                            {
                                long? tgChatId = telegramChatFullInfo.Id;
                                ArgumentNullException.ThrowIfNull(tgChatId);
                                ChatId chatId = new(tgChatId.Value);
                                var telegramChatMember = await bot.GetChatMember(chatId, telegramUserOfChat.UserId, cancellationToken).ConfigureAwait(false);
                                if (telegramChatMember != null)
                                {
                                    tacosApi.AddOrUpdateUser(telegramChatMember.User, cancellationToken);
                                    tacosApi.AddOrUpdateUserChats(telegramChatMember, telegramChatFullInfo, chatFullInfo, cancellationToken);
                                }
                                await Task.Delay(50, cancellationToken);
                            }
                            catch (OperationCanceledException ex)
                            {
                            }
                            catch (Exception ex)
                            {
                                logger.LogWarning(ex, $"Error in processing telegramUsersOfChat {ex.ToString()}");
                            }
                        }
                    }
                    catch (OperationCanceledException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Error in processing ChatFullInfo while updating users: {ex.ToString()}");
                    }

                }
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in UpdateChats: {ex.ToString()}");
            }
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await UpdateMe(stoppingToken).ConfigureAwait(false);
                    await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
                    await UpdateChats(stoppingToken).ConfigureAwait(false);
                    await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
                    await UpdateUserInChats(stoppingToken).ConfigureAwait(false);
                    await Task.Delay(60000, stoppingToken).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException ex)
            {
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in ExecuteAsync: {ex.ToString()}");
            }
        }
    }
}
