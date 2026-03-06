//-----------------------------------------------------------------------
// <copyright file="TacosPortalApiService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Data.Filtering;
using DevExpress.EntityFrameworkCore.Security;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;
using TacosPortal.BusinessObjects;
using TacosPortal.Helper;
using TacosPortal.Hubs;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using ChatFullInfo = Telegram.Bot.Types.ChatFullInfo;
using Message = Telegram.Bot.Types.Message;

namespace TacosPortal.Services.Telegram
{
    public interface ITacosApi : IDisposable
    {
        bool AddOrUpdateUser(User user, CancellationToken? externalToken = null);
        bool AddOrUpdateUserChats(ChatMember chatMember, ChatFullInfo telegramChatFullInfo, TelegramChatFullInfo chatFullInfo,
            CancellationToken? externalToken = null);
        IList<TelegramChat> GetChats(CancellationToken? externalToken = null);
        IList<TelegramChatFullInfo> GetMostRecentChatFullInfos(CancellationToken? externalToken = null);
        IList<TelegramUser> GetTelegramUsers(CancellationToken? externalToken = null);
        IList<TelegramUser> GetUsersAppeardInRoom(TelegramChatFullInfo chatFullInfo, CancellationToken? cancellationToken = null);
        bool IgnoreChat(TelegramChat telegramChat, CancellationToken? externalToken = null);
        Task<string> NewFullChatInfo(ChatFullInfo item, CancellationToken? externalToken = null);
        Task<string> NewMessage(Message item, CancellationToken? externalToken = null);
        Task<string> NewPoll(Poll item, CancellationToken? externalToken = null);
        Task<string> NewPollAnswer(PollAnswer item, CancellationToken? externalToken = null);
        Task<string> NewUpdate(Update item, CancellationToken? externalToken = null);
        bool TelegramChatBotRightsUser(User me, ChatFullInfo chatFullInfo, CancellationToken? externalToken = null);
    }

    public class TacosPortalApiService(ILogger<TacosPortalApiService> logger, IConfiguration configuration, IHubContext<ChatHub> hubContext, AuditSaveChangesInterceptor auditSaveChangesInterceptor, EfCommandDiagnosticsInterceptor efCommandDiagnosticsInterceptor, IAmbientUserContext ambientUserContext) : ITacosApi
    {
        private bool disposed;

        private SecuredEFCoreObjectSpaceProvider<TacoContext> CreateServiceObjectSpaceProvider()
        {
            try
            {
                var authentication = new AuthenticationStandard();
                var security = new SecurityStrategyComplex(typeof(TacoPermissionPolicyUser), typeof(TacoPermissionPolicyRole), authentication)
                {
                    SupportNavigationPermissionsForTypes = false
                };



                var objectSpaceProvider = new SecuredEFCoreObjectSpaceProvider<TacoContext>(
                    security,
                    (builder, _) => builder.UseSqlServer(ConnectionStrings.ConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseChangeTrackingProxies().AddInterceptors(auditSaveChangesInterceptor, efCommandDiagnosticsInterceptor));

                var loginParameters = new AuthenticationStandardLogonParameters(
                    ServiceConfigurationOptions.ApiUser,
                    ServiceConfigurationOptions.ApiPassword);

                var loginObjectSpace = objectSpaceProvider.CreateNonsecuredObjectSpace();
                authentication.SetLogonParameters(loginParameters);
                security.Logon(loginObjectSpace);
                try
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        CriteriaOperator userCriteria = CriteriaOperator.FromLambda<ApplicationUser>(
                                                     x => x.UserName == ServiceConfigurationOptions.ApiUser);
                        var user = securedObjectSpace.FindObject<ApplicationUser>(userCriteria);
                        if (user != null)
                        {
                            ambientUserContext.UserId = user.ID; ambientUserContext.UserName = user.UserName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, $"Warning: Problems in setting AmbientUserContext values in TacosPortalApiService ex: {ex.ToString()}");
                }


                return objectSpaceProvider;
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in LoginServieUser {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in LoginServieUser {ex.ToString()}");
                throw;
            }

        }
        private void MapTelegramAnimationToMessage(IObjectSpace securedObjectSpace, Animation animation, out TelegramAnimation? databaseAnimation)
        {
            try
            {
                CriteriaOperator animationCriteria = CriteriaOperator.FromLambda<TelegramAnimation>(
                                animation => animation.FileUniqueId == animation.FileUniqueId);
                databaseAnimation = securedObjectSpace.FindObject<TelegramAnimation>(
                    animationCriteria);
                if (databaseAnimation == null)
                {
                    databaseAnimation = securedObjectSpace.CreateObject<TelegramAnimation>(
                        );

                }
                databaseAnimation.FileId = animation.FileId;
                databaseAnimation.FileUniqueId = animation.FileUniqueId;
                databaseAnimation.FileName = animation.FileName ?? string.Empty;
                databaseAnimation.MimeType = animation.MimeType ?? string.Empty;
                databaseAnimation.FileSize = animation.FileSize;
                databaseAnimation.Width = animation.Width;
                databaseAnimation.Height = animation.Height;
                databaseAnimation.Duration = animation.Duration;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramAnimationToMessage {animation.ToString()}");
                throw;
            }
        }
        private void MapTelegramAudioToMessage(IObjectSpace securedObjectSpace, Audio audio, out TelegramAudio? databaseAudio)
        {
            try
            {
                CriteriaOperator audioCriteria = CriteriaOperator.FromLambda<TelegramAudio>(
                                               audio => audio.FileUniqueId == audio.FileUniqueId);
                databaseAudio = securedObjectSpace.FindObject<TelegramAudio>(
                    audioCriteria);
                if (databaseAudio == null)
                {
                    databaseAudio = securedObjectSpace.CreateObject<TelegramAudio>(
                        );

                }
                databaseAudio.FileId = audio.FileId;
                databaseAudio.FileUniqueId = audio.FileUniqueId;
                databaseAudio.FileName = audio.FileName ?? string.Empty;
                databaseAudio.MimeType = audio.MimeType ?? string.Empty;
                databaseAudio.FileSize = audio.FileSize;
                databaseAudio.Duration = audio.Duration;
                databaseAudio.Title = audio.Title ?? string.Empty;
                databaseAudio.Performer = audio.Performer ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramAudioToMessage {audio.ToString()}");
                throw;
            }
        }

        private void MapTelegramBoost(IObjectSpace securedObjectSpace, ChatBoost telegramChatBoost, out TelegramChatBoost databaseChatBoost)
        {
            try
            {
                databaseChatBoost = securedObjectSpace.CreateObject<TelegramChatBoost>();
                databaseChatBoost.AddDate = telegramChatBoost.AddDate;
                databaseChatBoost.BoostId = telegramChatBoost.BoostId;
                databaseChatBoost.ExpirationDate = telegramChatBoost.ExpirationDate;
                MapTelegramChatBoostSource(securedObjectSpace, telegramChatBoost.Source, out TelegramChatBoostSource databaseChatBoostSource);
                if (databaseChatBoostSource != null)
                    databaseChatBoost.Source = databaseChatBoostSource;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramBoost {telegramChatBoost.ToString()}");
                throw;
            }
        }

        private void MapTelegramBusinessBotRights(IObjectSpace securedObjectSpace, BusinessBotRights rights, out TelegramBusinessBotRights? databaseTelegramBusinessBotRights)
        {
            try
            {
                databaseTelegramBusinessBotRights = securedObjectSpace.CreateObject<TelegramBusinessBotRights>();
                databaseTelegramBusinessBotRights.CanChangeGiftSettings = rights.CanChangeGiftSettings;
                databaseTelegramBusinessBotRights.CanConvertGiftsToStars = rights.CanConvertGiftsToStars;
                databaseTelegramBusinessBotRights.CanDeleteAllMessages = rights.CanDeleteAllMessages;
                databaseTelegramBusinessBotRights.CanDeleteSentMessages = rights.CanDeleteSentMessages;
                databaseTelegramBusinessBotRights.CanEditBio = rights.CanEditBio;
                databaseTelegramBusinessBotRights.CanEditName = rights.CanEditName;
                databaseTelegramBusinessBotRights.CanEditProfilePhoto = rights.CanEditProfilePhoto;
                databaseTelegramBusinessBotRights.CanEditUsername = rights.CanEditUsername;
                databaseTelegramBusinessBotRights.CanManageStories = rights.CanManageStories;
                databaseTelegramBusinessBotRights.CanReadMessages = rights.CanReadMessages;
                databaseTelegramBusinessBotRights.CanReply = rights.CanReply;
                databaseTelegramBusinessBotRights.CanTransferAndUpgradeGifts = rights.CanTransferAndUpgradeGifts;
                databaseTelegramBusinessBotRights.CanTransferStars = rights.CanTransferStars;
                databaseTelegramBusinessBotRights.CanViewGiftsAndStars = rights.CanViewGiftsAndStars;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramBusinessBotRights {rights.ToString()}");
                throw;
            }
        }


        private void MapTelegramBusinessConnectionToMessage(IObjectSpace securedObjectSpace, BusinessConnection telegramBusinessConnection, out TelegramBusinessConnection finalBusinessConnection)
        {
            try
            {
                CriteriaOperator businessConnectionCriteria = CriteriaOperator.FromLambda<TelegramBusinessConnection>(
                               x => x.BusinessConnectionId == telegramBusinessConnection.Id);
                finalBusinessConnection = securedObjectSpace.FindObject<TelegramBusinessConnection>(
                    businessConnectionCriteria);
                if (finalBusinessConnection == null)
                {
                    finalBusinessConnection = securedObjectSpace.CreateObject<TelegramBusinessConnection>();

                }
                finalBusinessConnection.Date = telegramBusinessConnection.Date;
                finalBusinessConnection.IsEnabled = telegramBusinessConnection.IsEnabled;

                if (telegramBusinessConnection.Rights != null)
                {
                    MapTelegramBusinessBotRights(securedObjectSpace, telegramBusinessConnection.Rights, out TelegramBusinessBotRights? databaseTelegramBusinessBotRights);
                    if (databaseTelegramBusinessBotRights != null)
                    {
                        finalBusinessConnection.Rights = databaseTelegramBusinessBotRights;
                    }
                }
                finalBusinessConnection.UpdateType = UpdateType.BusinessConnection;

                MapTelegramUserToMessage(securedObjectSpace, telegramBusinessConnection.User, out TelegramUser? databaseTelegramUser);
                if (databaseTelegramUser != null)
                    finalBusinessConnection.User = databaseTelegramUser;

                finalBusinessConnection.UserChatId = telegramBusinessConnection.UserChatId;

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MapTelegramBusinessConnectionToMessage {telegramBusinessConnection.ToString()}");
                throw;
            }
        }

        private void MapTelegramBusinessMessagesDeleted(IObjectSpace securedObjectSpace, BusinessMessagesDeleted telegramBusinessMessagesDeleted, out TelegramBusinessMessagesDeleted finalBusinessMessagesDeleted)
        {
            try
            {
                finalBusinessMessagesDeleted = securedObjectSpace.CreateObject<TelegramBusinessMessagesDeleted>();
                if (finalBusinessMessagesDeleted.MessageIds == null)
                    finalBusinessMessagesDeleted.MessageIds = new ObservableCollection<int>();
                foreach (int messageId in telegramBusinessMessagesDeleted.MessageIds)
                {
                    finalBusinessMessagesDeleted.MessageIds.Add(messageId);
                }
                finalBusinessMessagesDeleted.BusinessConnectionId = telegramBusinessMessagesDeleted.BusinessConnectionId;
                MapTelegramChatToMessage(securedObjectSpace, telegramBusinessMessagesDeleted.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    finalBusinessMessagesDeleted.Chat = databaseChat;
                finalBusinessMessagesDeleted.UpdateType = UpdateType.DeletedBusinessMessages;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MapTelegramBusinessMessagesDeleted {telegramBusinessMessagesDeleted.ToString()}");
                throw;
            }
        }
        private void MapTelegramCallbackGameToMessage(IObjectSpace securedObjectSpace, CallbackGame callbackGame, out TelegramCallbackGame? databaseCallbackGame)
        {
            try
            {
                databaseCallbackGame = securedObjectSpace.CreateObject<TelegramCallbackGame>();


            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramCallbackGameToMessage {callbackGame.ToString()}");
                throw;
            }
        }

        private void MaptelegramCallbackQuery(IObjectSpace securedObjectSpace, CallbackQuery telegramCallbackQuery, out TelegramCallbackQuery? finaltelegramCallbackQuery)
        {
            try
            {
                finaltelegramCallbackQuery = securedObjectSpace.CreateObject<TelegramCallbackQuery>();
                if (telegramCallbackQuery.Message != null)
                {
                    MapTelegramMessageToDatabase(telegramCallbackQuery.Message, securedObjectSpace, out TelegramMessage? telegramMessage);
                    if (telegramMessage != null)
                    {
                        finaltelegramCallbackQuery.Message = telegramMessage;
                    }
                }
                finaltelegramCallbackQuery.ChatInstance = telegramCallbackQuery.ChatInstance;
                finaltelegramCallbackQuery.Data = telegramCallbackQuery.Data;
                if (telegramCallbackQuery.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramCallbackQuery.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramCallbackQuery.From = databaseUser;
                    }

                }
                if (telegramCallbackQuery.GameShortName != null)
                    finaltelegramCallbackQuery.GameShortName = telegramCallbackQuery.GameShortName;
                finaltelegramCallbackQuery.InlineMessageId = telegramCallbackQuery.InlineMessageId;
                finaltelegramCallbackQuery.UpdateType = UpdateType.CallbackQuery;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramCallbackQuery {telegramCallbackQuery.ToString()}");
                throw;
            }
        }

        private void MaptelegramChatBoostRemoved(IObjectSpace securedObjectSpace, ChatBoostRemoved telegramChatBoostRemoved, out TelegramChatBoostRemoved? databaseTelegramChatBoostRemoved)
        {
            try
            {
                databaseTelegramChatBoostRemoved = securedObjectSpace.CreateObject<TelegramChatBoostRemoved>();

                databaseTelegramChatBoostRemoved.BoostId = telegramChatBoostRemoved.BoostId;

                MapTelegramChatToMessage(securedObjectSpace, telegramChatBoostRemoved.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    databaseTelegramChatBoostRemoved.Chat = databaseChat;

                databaseTelegramChatBoostRemoved.RemoveDate = telegramChatBoostRemoved.RemoveDate;
                MapTelegramChatBoostSource(securedObjectSpace, telegramChatBoostRemoved.Source, out TelegramChatBoostSource? databaseChatBoostSource);
                if (databaseChatBoostSource != null)
                    databaseTelegramChatBoostRemoved.Source = databaseChatBoostSource;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MaptelegramChatBoostRemoved {telegramChatBoostRemoved.ToString()}");
                throw;
            }
        }

        private void MapTelegramChatBoostSource(IObjectSpace securedObjectSpace, ChatBoostSource telegramChatBoostSource, out TelegramChatBoostSource databaseChatBoostSource)
        {
            try
            {
                switch (telegramChatBoostSource)
                {
                    case ChatBoostSourceGiftCode telegramChatBoostSourceGiftCode:
                        var tmpChatBoostSourceGiftCode = securedObjectSpace.CreateObject<TelegramChatBoostSourceGiftCode>();

                        if (telegramChatBoostSourceGiftCode.User != null)
                        {
                            MapTelegramUserToMessage(securedObjectSpace, telegramChatBoostSourceGiftCode.User, out TelegramUser? databaseGiftCodeUser);
                            if (databaseGiftCodeUser != null)
                            {
                                tmpChatBoostSourceGiftCode.User = databaseGiftCodeUser;
                            }

                        }
                        databaseChatBoostSource = tmpChatBoostSourceGiftCode;
                        break;
                    case ChatBoostSourceGiveaway telegramChatBoostSourceGiveAway:
                        var tempChatBoostSourceGiveaway = securedObjectSpace.CreateObject<TelegramChatBoostSourceGiveaway>();
                        if (telegramChatBoostSourceGiveAway.User != null)
                        {
                            MapTelegramUserToMessage(securedObjectSpace, telegramChatBoostSourceGiveAway.User, out TelegramUser? databaseGiveawayUser);
                            if (databaseGiveawayUser != null)
                            {
                                tempChatBoostSourceGiveaway.User = databaseGiveawayUser;
                            }

                        }
                        tempChatBoostSourceGiveaway.GiveawayMessageId = telegramChatBoostSourceGiveAway.GiveawayMessageId;
                        tempChatBoostSourceGiveaway.IsUnclaimed = telegramChatBoostSourceGiveAway.IsUnclaimed;
                        tempChatBoostSourceGiveaway.PrizeStarCount = telegramChatBoostSourceGiveAway.PrizeStarCount;

                        databaseChatBoostSource = tempChatBoostSourceGiveaway;
                        break;
                    case ChatBoostSourcePremium telegramChatBoostSourcePremium:
                        var tempChatBoostSourcePremium = securedObjectSpace.CreateObject<TelegramChatBoostSourcePremium>();
                        if (tempChatBoostSourcePremium.User != null)
                        {
                            MapTelegramUserToMessage(securedObjectSpace, telegramChatBoostSourcePremium.User, out TelegramUser? databasePremiumUser);
                            if (databasePremiumUser != null)
                            {
                                tempChatBoostSourcePremium.User = databasePremiumUser;
                            }

                        }

                        databaseChatBoostSource = tempChatBoostSourcePremium;
                        break;
                    default:
                        var temp = securedObjectSpace.CreateObject<TelegramChatBoostSource>();
                        databaseChatBoostSource = temp;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramChatBoostSource {telegramChatBoostSource.ToString()}");
                throw;
            }
        }

        private void MaptelegramChatBoostUpdated(IObjectSpace securedObjectSpace, ChatBoostUpdated telegramChatBoostUpdated, out TelegramChatBoostUpdated? databaseChatBoostUpdated)
        {
            try
            {
                databaseChatBoostUpdated = securedObjectSpace.CreateObject<TelegramChatBoostUpdated>();
                MapTelegramBoost(securedObjectSpace, telegramChatBoostUpdated.Boost, out TelegramChatBoost databaseChatBoost);
                if (databaseChatBoost != null)
                    databaseChatBoostUpdated.Boost = databaseChatBoost;

                MapTelegramChatToMessage(securedObjectSpace, telegramChatBoostUpdated.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    databaseChatBoostUpdated.Chat = databaseChat;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MaptelegramChatBoostUpdated {telegramChatBoostUpdated.ToString()}");
                throw;
            }
        }

        private void MapTelegramChatFullInfoSetToDatabase(ChatFullInfo telegramChatFullInfo, SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider)
        {
            try
            {
                using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                {
                    if (telegramChatFullInfo != null)
                    {
                        try
                        {
                            MapTelegramChatFullInfoToMessage(securedObjectSpace, telegramChatFullInfo, out var databaseChatFullInfo);

                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Error while adding telegramChatFullInfo to DB {ex.ToString()}");
                        }
                    }
                    securedObjectSpace.CommitChanges();
                    logger.LogInformation($"updated telegramChatFullInfo {telegramChatFullInfo.ToString()}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MaptelegramChatFullInfoToDatabase {telegramChatFullInfo.ToString()}");
            }
        }

        private void MapTelegramChatFullInfoToMessage(IObjectSpace securedObjectSpace, ChatFullInfo chatFullInfo, out TelegramChatFullInfo? databaseChatFullInfo)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfZero(chatFullInfo.Id);
                var dateNow = DateOnly.FromDateTime(DateTime.Today);
                CriteriaOperator chatCriteria = CriteriaOperator.FromLambda<TelegramChatFullInfo>(
                                         x => x.Chat.ChatId == chatFullInfo.Id && x.DateCreated == dateNow);
                databaseChatFullInfo = securedObjectSpace.FindObject<TelegramChatFullInfo>(
                    chatCriteria);
                if (databaseChatFullInfo == null)
                {
                    databaseChatFullInfo = securedObjectSpace.CreateObject<TelegramChatFullInfo>(
                        );
                    databaseChatFullInfo.DateCreated = dateNow;
                }
                databaseChatFullInfo.AccentColorId = chatFullInfo.AccentColorId;
                if (chatFullInfo.BackgroundCustomEmojiId != null)
                    databaseChatFullInfo.BackgroundCustomEmojiId = chatFullInfo.BackgroundCustomEmojiId;
                if (chatFullInfo.Bio != null)
                    databaseChatFullInfo.Bio = chatFullInfo.Bio;
                databaseChatFullInfo.CanSendPaidMedia = chatFullInfo.CanSendPaidMedia;
                databaseChatFullInfo.CanSetStickerSet = chatFullInfo.CanSetStickerSet;
                if (chatFullInfo.Description != null)
                    databaseChatFullInfo.Description = chatFullInfo.Description;
                databaseChatFullInfo.EmojiStatusExpirationDate = chatFullInfo.EmojiStatusExpirationDate;

                databaseChatFullInfo.HasAggressiveAntiSpamEnabled = chatFullInfo.HasAggressiveAntiSpamEnabled;
                databaseChatFullInfo.HasHiddenMembers = chatFullInfo.HasHiddenMembers;
                databaseChatFullInfo.HasPrivateForwards = chatFullInfo.HasPrivateForwards;
                databaseChatFullInfo.HasProtectedContent = chatFullInfo.HasProtectedContent;
                databaseChatFullInfo.HasRestrictedVoiceAndVideoMessages = chatFullInfo.HasRestrictedVoiceAndVideoMessages;
                databaseChatFullInfo.HasVisibleHistory = chatFullInfo.HasVisibleHistory;

                databaseChatFullInfo.JoinByRequest = chatFullInfo.JoinByRequest;
                databaseChatFullInfo.JoinToSendMessages = chatFullInfo.JoinToSendMessages;

                databaseChatFullInfo.LinkedChatId = chatFullInfo.LinkedChatId;
                databaseChatFullInfo.MaxReactionCount = chatFullInfo.MaxReactionCount;
                databaseChatFullInfo.MessageAutoDeleteTime = chatFullInfo.MessageAutoDeleteTime;
                databaseChatFullInfo.ProfileAccentColorId = chatFullInfo.ProfileAccentColorId;
                databaseChatFullInfo.SlowModeDelay = chatFullInfo.SlowModeDelay;
                if (chatFullInfo.StickerSetName != null)
                    databaseChatFullInfo.StickerSetName = chatFullInfo.StickerSetName;
                databaseChatFullInfo.UnrestrictBoostCount = chatFullInfo.UnrestrictBoostCount;

                if (chatFullInfo.ActiveUsernames != null)
                    foreach (var activeUser in chatFullInfo.ActiveUsernames)
                    {
                        databaseChatFullInfo.ActiveUsernames.Add(activeUser);
                    }

                MapTelegramChatToMessage(securedObjectSpace, chatFullInfo, out TelegramChat? databaseChat);
                {
                    if (databaseChat != null)
                    {
                        databaseChatFullInfo.Chat = databaseChat;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramChatFullInfoToMessage {chatFullInfo.ToString()}");
                throw;
            }
        }

        private void MaptelegramChatJoinRequest(IObjectSpace securedObjectSpace, ChatJoinRequest telegramChatJoinRequest, out TelegramChatJoinRequest? finalChatJoinRequest)
        {
            try
            {
                finalChatJoinRequest = securedObjectSpace.CreateObject<TelegramChatJoinRequest>();
                if (telegramChatJoinRequest.Bio != null)
                    finalChatJoinRequest.Bio = telegramChatJoinRequest.Bio;
                MapTelegramChatToMessage(securedObjectSpace, telegramChatJoinRequest.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    finalChatJoinRequest.Chat = databaseChat;
                finalChatJoinRequest.Date = telegramChatJoinRequest.Date;
                if (telegramChatJoinRequest.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramChatJoinRequest.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finalChatJoinRequest.From = databaseUser;
                    }

                }
                if (telegramChatJoinRequest.InviteLink != null)
                {
                    MapTelegramInviteLink(securedObjectSpace, telegramChatJoinRequest.InviteLink, out TelegramChatInviteLink? databaseChatInviteLink);
                    if (databaseChatInviteLink != null)
                        finalChatJoinRequest.InviteLink = databaseChatInviteLink;
                }
                finalChatJoinRequest.UserChatId = telegramChatJoinRequest.UserChatId;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MaptelegramChatJoinRequest {telegramChatJoinRequest.ToString()}");
                throw;
            }
        }

        private void MapTelegramChatMember(IObjectSpace securedObjectSpace, ChatMember telegramNewChatMember, bool? isNew, out TelegramChatMember? databaseChatMember)
        {
            try
            {

                switch (telegramNewChatMember)
                {
                    case ChatMemberOwner chatMemberOwner:
                        var tmpOwner = securedObjectSpace.CreateObject<TelegramChatMemberOwner>();
                        if (chatMemberOwner.CustomTitle != null)
                            tmpOwner.CustomTitle = chatMemberOwner.CustomTitle;
                        tmpOwner.IsAdmin = chatMemberOwner.IsAdmin;
                        tmpOwner.IsAnonymous = chatMemberOwner.IsAnonymous;
                        tmpOwner.IsInChat = chatMemberOwner.IsInChat;
                        MapTelegramUserToMessage(securedObjectSpace, chatMemberOwner.User, out TelegramUser? databaseOwnerUser);
                        if (databaseOwnerUser != null)
                            tmpOwner.User = databaseOwnerUser;
                        databaseChatMember = tmpOwner;
                        break;
                    case ChatMemberAdministrator chatMemberAdministrator:
                        var tmpChatMemberAdministrator = securedObjectSpace.CreateObject<TelegramChatMemberAdministrator>();
                        tmpChatMemberAdministrator.CanBeEdited = chatMemberAdministrator.CanBeEdited;
                        tmpChatMemberAdministrator.CanChangeInfo = chatMemberAdministrator.CanChangeInfo;
                        tmpChatMemberAdministrator.CanDeleteMessages = chatMemberAdministrator.CanDeleteMessages;
                        tmpChatMemberAdministrator.CanDeleteStories = chatMemberAdministrator.CanDeleteStories;
                        tmpChatMemberAdministrator.CanEditMessages = chatMemberAdministrator.CanEditMessages;
                        tmpChatMemberAdministrator.CanEditStories = chatMemberAdministrator.CanEditStories;
                        tmpChatMemberAdministrator.CanInviteUsers = chatMemberAdministrator.CanInviteUsers;
                        tmpChatMemberAdministrator.CanManageChat = chatMemberAdministrator.CanManageChat;
                        tmpChatMemberAdministrator.CanManageTopics = chatMemberAdministrator.CanManageTopics;
                        tmpChatMemberAdministrator.CanManageVideoChats = chatMemberAdministrator.CanManageVideoChats;
                        tmpChatMemberAdministrator.CanPinMessages = chatMemberAdministrator.CanPinMessages;
                        tmpChatMemberAdministrator.CanPostMessages = chatMemberAdministrator.CanPostMessages;
                        tmpChatMemberAdministrator.CanPostStories = chatMemberAdministrator.CanPostStories;
                        tmpChatMemberAdministrator.CanPromoteMembers = chatMemberAdministrator.CanPromoteMembers;
                        tmpChatMemberAdministrator.CanRestrictMembers = chatMemberAdministrator.CanRestrictMembers;
                        if (chatMemberAdministrator.CustomTitle != null)
                            tmpChatMemberAdministrator.CustomTitle = chatMemberAdministrator.CustomTitle;
                        tmpChatMemberAdministrator.IsAdmin = chatMemberAdministrator.IsAdmin;
                        tmpChatMemberAdministrator.IsInChat = chatMemberAdministrator.IsInChat;
                        MapTelegramUserToMessage(securedObjectSpace, chatMemberAdministrator.User, out TelegramUser? databaseAdminUser);
                        if (databaseAdminUser != null)
                            tmpChatMemberAdministrator.User = databaseAdminUser;

                        databaseChatMember = tmpChatMemberAdministrator;
                        break;
                    case ChatMemberBanned chatMemberBanned:
                        var tmpChatMemberBanned = securedObjectSpace.CreateObject<TelegramChatMemberBanned>();
                        tmpChatMemberBanned.IsAdmin = chatMemberBanned.IsAdmin;
                        tmpChatMemberBanned.IsInChat = chatMemberBanned.IsInChat;
                        if (chatMemberBanned.UntilDate != null)
                            tmpChatMemberBanned.UntilDate = chatMemberBanned.UntilDate;

                        MapTelegramUserToMessage(securedObjectSpace, chatMemberBanned.User, out TelegramUser? databaseChatMemberBanned);
                        if (databaseChatMemberBanned != null)
                            tmpChatMemberBanned.User = databaseChatMemberBanned;
                        databaseChatMember = tmpChatMemberBanned;
                        break;
                    case ChatMemberLeft chatMemberLeft:
                        var tmpChatMemberLeft = securedObjectSpace.CreateObject<TelegramChatMemberLeft>();
                        tmpChatMemberLeft.IsAdmin = chatMemberLeft.IsAdmin;
                        tmpChatMemberLeft.IsInChat = chatMemberLeft.IsInChat;
                        MapTelegramUserToMessage(securedObjectSpace, chatMemberLeft.User, out TelegramUser? databaseChatMemberLeft);
                        if (databaseChatMemberLeft != null)
                            tmpChatMemberLeft.User = databaseChatMemberLeft;

                        databaseChatMember = tmpChatMemberLeft;
                        break;
                    case ChatMemberMember chatMemberMember:
                        var tmpChatMemberMember = securedObjectSpace.CreateObject<TelegramChatMemberMember>();
                        tmpChatMemberMember.IsAdmin = chatMemberMember.IsAdmin;
                        tmpChatMemberMember.IsInChat = chatMemberMember.IsInChat;
                        if (chatMemberMember.UntilDate != null)
                            tmpChatMemberMember.UntilDate = chatMemberMember.UntilDate;

                        MapTelegramUserToMessage(securedObjectSpace, chatMemberMember.User, out TelegramUser? databaseChatMemberMember);
                        if (databaseChatMemberMember != null)
                            tmpChatMemberMember.User = databaseChatMemberMember;

                        databaseChatMember = tmpChatMemberMember;
                        break;
                    case ChatMemberRestricted chatMemberRestricted:
                        var tmpChatMemberRestricted = securedObjectSpace.CreateObject<TelegramChatMemberRestricted>();

                        tmpChatMemberRestricted.CanAddWebPagePreviews = chatMemberRestricted.CanAddWebPagePreviews;
                        tmpChatMemberRestricted.CanChangeInfo = chatMemberRestricted.CanChangeInfo;
                        tmpChatMemberRestricted.CanInviteUsers = chatMemberRestricted.CanInviteUsers;
                        tmpChatMemberRestricted.CanManageTopics = chatMemberRestricted.CanManageTopics;
                        tmpChatMemberRestricted.CanPinMessages = chatMemberRestricted.CanPinMessages;
                        tmpChatMemberRestricted.CanSendAudios = chatMemberRestricted.CanSendAudios;
                        tmpChatMemberRestricted.CanSendDocuments = chatMemberRestricted.CanSendDocuments;
                        tmpChatMemberRestricted.CanSendMessages = chatMemberRestricted.CanSendMessages;
                        tmpChatMemberRestricted.CanSendOtherMessages = chatMemberRestricted.CanSendOtherMessages;
                        tmpChatMemberRestricted.CanSendPhotos = chatMemberRestricted.CanSendPhotos;
                        tmpChatMemberRestricted.CanSendPolls = chatMemberRestricted.CanSendPolls;
                        tmpChatMemberRestricted.CanSendVideoNotes = chatMemberRestricted.CanSendVideoNotes;
                        tmpChatMemberRestricted.IsAdmin = chatMemberRestricted.IsAdmin;
                        tmpChatMemberRestricted.CanSendVideos = chatMemberRestricted.CanSendVideos;
                        tmpChatMemberRestricted.CanSendVoiceNotes = chatMemberRestricted.CanSendVoiceNotes;
                        tmpChatMemberRestricted.IsInChat = chatMemberRestricted.IsInChat;
                        tmpChatMemberRestricted.IsMember = chatMemberRestricted.IsMember;
                        if (chatMemberRestricted.UntilDate != null)
                            tmpChatMemberRestricted.UntilDate = chatMemberRestricted.UntilDate;
                        MapTelegramUserToMessage(securedObjectSpace, chatMemberRestricted.User, out TelegramUser? databaseChatMemberRestricted);
                        if (databaseChatMemberRestricted != null)
                            tmpChatMemberRestricted.User = databaseChatMemberRestricted;

                        databaseChatMember = tmpChatMemberRestricted;
                        break;
                    default:
                        var tmpChatMember = securedObjectSpace.CreateObject<TelegramChatMember>();
                        MapTelegramUserToMessage(securedObjectSpace, telegramNewChatMember.User, out TelegramUser? databasetelegramNewChatMember);
                        if (databasetelegramNewChatMember != null)
                            tmpChatMember.User = databasetelegramNewChatMember;
                        tmpChatMember.IsAdmin = telegramNewChatMember.IsAdmin;
                        tmpChatMember.IsInChat = telegramNewChatMember.IsInChat;

                        databaseChatMember = tmpChatMember;
                        break;
                }
                if (isNew != null)
                    databaseChatMember.IsNew = isNew;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramChatMember {telegramNewChatMember.ToString()}");
                throw;
            }
        }

        private void MaptelegramChatMemberUpdated(IObjectSpace securedObjectSpace, ChatMemberUpdated telegramChatMemberUpdated, out TelegramChatMemberUpdated? finaltelegramChatMemberUpdated)
        {
            try
            {
                finaltelegramChatMemberUpdated = securedObjectSpace.CreateObject<TelegramChatMemberUpdated>();
                MapTelegramChatToMessage(securedObjectSpace, telegramChatMemberUpdated.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    finaltelegramChatMemberUpdated.Chat = databaseChat;

                finaltelegramChatMemberUpdated.Date = telegramChatMemberUpdated.Date;
                if (telegramChatMemberUpdated.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramChatMemberUpdated.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramChatMemberUpdated.From = databaseUser;
                    }

                }
                if (telegramChatMemberUpdated.InviteLink != null)
                {

                    MapTelegramInviteLink(securedObjectSpace, telegramChatMemberUpdated.InviteLink, out TelegramChatInviteLink databaseInviteLink);
                    if (databaseInviteLink != null)
                        finaltelegramChatMemberUpdated.InviteLink = databaseInviteLink;
                }
                MapTelegramChatMember(securedObjectSpace, telegramChatMemberUpdated.NewChatMember, true, out TelegramChatMember? databaseChatMember);
                if (databaseChatMember != null)
                    finaltelegramChatMemberUpdated.NewChatMember = databaseChatMember;
                MapTelegramChatMember(securedObjectSpace, telegramChatMemberUpdated.OldChatMember, false, out TelegramChatMember? databaseOldChatMember);
                finaltelegramChatMemberUpdated.ViaChatFolderInviteLink = telegramChatMemberUpdated.ViaChatFolderInviteLink;
                finaltelegramChatMemberUpdated.ViaJoinRequest = telegramChatMemberUpdated.ViaJoinRequest;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUpdateToDatabase {telegramChatMemberUpdated.ToString()}");
                throw;
            }
        }

        private void MapTelegramChatSharedToMessage(IObjectSpace securedObjectSpace, ChatShared chatShared, out TelegramChatShared? databaseChatShared)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfZero(chatShared.ChatId);
                CriteriaOperator chatCriteria = CriteriaOperator.FromLambda<TelegramChatShared>(
     chat => chat.ChatId == chatShared.ChatId);
                databaseChatShared = securedObjectSpace.FindObject<TelegramChatShared>(
                    chatCriteria);
                if (databaseChatShared == null)
                {
                    databaseChatShared = securedObjectSpace.CreateObject<TelegramChatShared>(
                        );

                }
                databaseChatShared.ChatId = chatShared.ChatId;
                databaseChatShared.RequestId = chatShared.RequestId;
                if (chatShared.Title != null)
                    databaseChatShared.Title = chatShared.Title;
                databaseChatShared.Username = chatShared.Username ?? string.Empty;
                if (chatShared.Photo != null && chatShared.Photo.Length > 0)
                {
                    foreach (var photo in chatShared.Photo)
                    {
                        var photoToAddCriteria = CriteriaOperator.FromLambda<TelegramPhotoSize>(
                            p => p.FileUniqueId == photo.FileUniqueId);
                        var photoSize = securedObjectSpace.FindObject<TelegramPhotoSize>(
                            photoToAddCriteria);
                        if (photoSize == null)
                        {

                            photoSize = securedObjectSpace.CreateObject<TelegramPhotoSize>(
                                );

                        }
                        photoSize.FileId = photo.FileId;
                        photoSize.FileUniqueId = photo.FileUniqueId;
                        photoSize.Width = photo.Width;
                        photoSize.Height = photo.Height;
                        photoSize.FileSize = photo.FileSize;
                        databaseChatShared.Photo.Add(photoSize);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramChatSharedToMessage {chatShared.ToString()}");
                throw;
            }
        }
        private void MapTelegramChatToMessage(IObjectSpace securedObjectSpace, Chat chat, out TelegramChat? databaseChat)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfZero(chat.Id);
                CriteriaOperator chatCriteria = CriteriaOperator.FromLambda<TelegramChat>(
                                             x => x.ChatId == chat.Id);
                databaseChat = securedObjectSpace.FindObject<TelegramChat>(
                    chatCriteria);
                if (databaseChat == null)
                {
                    databaseChat = securedObjectSpace.CreateObject<TelegramChat>(
                        );

                }
                databaseChat.ChatId = chat.Id;
                databaseChat.Type = chat.Type;
                databaseChat.Title = chat.Title ?? string.Empty;
                databaseChat.Username = chat.Username ?? string.Empty;
                databaseChat.FirstName = chat.FirstName ?? string.Empty;
                databaseChat.LastName = chat.LastName ?? string.Empty;
                databaseChat.IsForum = chat.IsForum;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramChatToMessage {chat.ToString()}");
                throw;
            }
        }

        private void MaptelegramChosenInlineResult(IObjectSpace securedObjectSpace, ChosenInlineResult telegramChosenInlineResult, out TelegramChosenInlineResult? finaltelegramChosenInlineResult)
        {
            try
            {
                finaltelegramChosenInlineResult = securedObjectSpace.CreateObject<TelegramChosenInlineResult>();
                if (telegramChosenInlineResult.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramChosenInlineResult.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramChosenInlineResult.From = databaseUser;
                    }

                }
                if (telegramChosenInlineResult.InlineMessageId != null)
                    finaltelegramChosenInlineResult.InlineMessageId = telegramChosenInlineResult.InlineMessageId;
                if (telegramChosenInlineResult.Location != null)
                {
                    MapTelegramLocationToMessage(securedObjectSpace, telegramChosenInlineResult.Location, out TelegramLocation? telegramLocation);
                    if (telegramLocation != null)
                        finaltelegramChosenInlineResult.Location = telegramLocation;
                }
                finaltelegramChosenInlineResult.Query = telegramChosenInlineResult.Query;
                finaltelegramChosenInlineResult.ResultId = telegramChosenInlineResult.ResultId;
                finaltelegramChosenInlineResult.UpdateType = UpdateType.ChosenInlineResult;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramChosenInlineResult {telegramChosenInlineResult.ToString()}");
                throw;
            }
        }
        private void MapTelegramContactToMessage(IObjectSpace securedObjectSpace, Contact contact, out TelegramContact? databaseContact)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(contact.UserId);
                ArgumentOutOfRangeException.ThrowIfZero((long)contact.UserId);
                CriteriaOperator contactCriteria = CriteriaOperator.FromLambda<TelegramContact>(
                              contact => contact.PhoneNumber == contact.PhoneNumber &&
                                  contact.UserId == contact.UserId);
                databaseContact = securedObjectSpace.FindObject<TelegramContact>(
                    contactCriteria);
                if (databaseContact == null)
                {
                    databaseContact = securedObjectSpace.CreateObject<TelegramContact>(
                        );

                }
                databaseContact.PhoneNumber = contact.PhoneNumber ?? string.Empty;
                databaseContact.FirstName = contact.FirstName ?? string.Empty;
                databaseContact.LastName = contact.LastName ?? string.Empty;
                databaseContact.UserId = contact.UserId;
                databaseContact.Vcard = contact.Vcard ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramContactToMessage {contact.ToString()}");
                throw;
            }
        }
        private void MapTelegramCopyTextButtonToMessage(IObjectSpace securedObjectSpace, CopyTextButton copyTextButton, out TelegramCopyTextButton? databaseCopyTextButton)
        {
            try
            {
                databaseCopyTextButton = securedObjectSpace.CreateObject<TelegramCopyTextButton>();
                databaseCopyTextButton.Text = copyTextButton.Text ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramCopyTextButtonToMessage {copyTextButton.ToString()}");
                throw;
            }
        }
        private void MapTelegramDiceToMessage(IObjectSpace securedObjectSpace, Dice dice, out TelegramDice? databaseDice)
        {
            try
            {
                CriteriaOperator diceCriteria = CriteriaOperator.FromLambda<TelegramDice>(
                    dice => dice.Emoji == dice.Emoji && dice.Value == dice.Value);
                databaseDice = securedObjectSpace.FindObject<TelegramDice>(
                    diceCriteria);
                if (databaseDice == null)
                {
                    databaseDice = securedObjectSpace.CreateObject<TelegramDice>(
                        );

                }
                databaseDice.Emoji = dice.Emoji ?? string.Empty;
                databaseDice.Value = dice.Value;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramDiceToMessage {dice.ToString()}");
                throw;
            }
        }

        private void MapTelegramDocumentToMessage(IObjectSpace securedObjectSpace, Document document, out TelegramDocument? databaseDocument)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(document.FileUniqueId);
                CriteriaOperator documentCriteria = CriteriaOperator.FromLambda<TelegramDocument>(
                    document => document.FileUniqueId == document.FileUniqueId);
                databaseDocument = securedObjectSpace.FindObject<TelegramDocument>(
                    documentCriteria);
                if (databaseDocument == null)
                {
                    databaseDocument = securedObjectSpace.CreateObject<TelegramDocument>(
                        );

                }
                databaseDocument.FileId = document.FileId;
                databaseDocument.FileUniqueId = document.FileUniqueId;
                databaseDocument.FileName = document.FileName ?? string.Empty;
                databaseDocument.MimeType = document.MimeType ?? string.Empty;
                databaseDocument.FileSize = document.FileSize;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramDocumentToMessage {document.ToString()}");
                throw;
            }
        }
        private void MapTelegramEncryptedCredentialsToMessage(IObjectSpace securedObjectSpace, EncryptedCredentials encryptedCredentials, out TelegramEncryptedCredentials? databaseEncryptedCredentials)
        {
            try
            {
                CriteriaOperator encryptedCredentialsCriteria = CriteriaOperator.FromLambda<TelegramEncryptedCredentials>(
                    ec => ec.Hash == encryptedCredentials.Hash && ec.Secret == encryptedCredentials.Secret && ec.Data == encryptedCredentials.Data);
                databaseEncryptedCredentials = securedObjectSpace.FindObject<TelegramEncryptedCredentials>(
                    encryptedCredentialsCriteria);
                if (databaseEncryptedCredentials == null)
                {
                    databaseEncryptedCredentials = securedObjectSpace.CreateObject<TelegramEncryptedCredentials>();

                }
                databaseEncryptedCredentials.Data = encryptedCredentials.Data ?? string.Empty;
                databaseEncryptedCredentials.Hash = encryptedCredentials.Hash ?? string.Empty;
                databaseEncryptedCredentials.Secret = encryptedCredentials.Secret ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramEncryptedCredentialsToMessage {encryptedCredentials.ToString()}");
                throw;
            }
        }
        private void MapTelegramExternalReplyInfoToMessage(IObjectSpace securedObjectSpace, ExternalReplyInfo externalReplyInfo, out TelegramExternalReplyInfo? databaseExternalReplyInfo)
        {
            try
            {


                databaseExternalReplyInfo = securedObjectSpace.CreateObject<TelegramExternalReplyInfo>(
                    );
                if (externalReplyInfo.Invoice != null)
                {
                    MapTelegramInvoiceToMessage(securedObjectSpace, externalReplyInfo.Invoice, out var databaseInvoice);
                    if (databaseInvoice != null)
                        databaseExternalReplyInfo.Invoice = databaseInvoice;
                }
                if (externalReplyInfo.Animation != null)
                {
                    MapTelegramAnimationToMessage(
                        securedObjectSpace,
                        externalReplyInfo.Animation,
                        out var databaseAnimation);
                    if (databaseAnimation != null)
                        databaseExternalReplyInfo.Animation = databaseAnimation;
                }
                if (externalReplyInfo.Audio != null)
                {
                    MapTelegramAudioToMessage(securedObjectSpace, externalReplyInfo.Audio, out var databaseAudio);
                    if (databaseAudio != null)
                        databaseExternalReplyInfo.Audio = databaseAudio;
                }
                if (externalReplyInfo.Document != null)
                {
                    MapTelegramDocumentToMessage(
                        securedObjectSpace,
                        externalReplyInfo.Document,
                        out var databaseDocument);
                    if (databaseDocument != null)
                        databaseExternalReplyInfo.Document = databaseDocument;
                }
                if (externalReplyInfo.Video != null)
                {
                    MapTelegramVideoToMessage(securedObjectSpace, externalReplyInfo.Video, out var databaseVideo);
                    if (databaseVideo != null)
                        databaseExternalReplyInfo.Video = databaseVideo;
                }
                if (externalReplyInfo.VideoNote != null)
                {
                    MapTelegramVideoNoteToMessage(
                        securedObjectSpace,
                        externalReplyInfo.VideoNote,
                        out var databaseVideoNote);
                    if (databaseVideoNote != null)
                        databaseExternalReplyInfo.VideoNote = databaseVideoNote;
                }
                if (externalReplyInfo.Voice != null)
                {
                    MapTelegramVoiceToMessage(securedObjectSpace, externalReplyInfo.Voice, out var databaseVoice);
                    if (databaseVoice != null)
                        databaseExternalReplyInfo.Voice = databaseVoice;
                }
                if (externalReplyInfo.Chat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, externalReplyInfo.Chat, out var databaseChat);
                    if (databaseChat != null)
                        databaseExternalReplyInfo.Chat = databaseChat;
                }
                if (externalReplyInfo.Contact != null)
                {
                    MapTelegramContactToMessage(securedObjectSpace, externalReplyInfo.Contact, out var databaseContact);
                    if (databaseContact != null)
                        databaseExternalReplyInfo.Contact = databaseContact;
                }
                if (externalReplyInfo.Dice != null)
                {
                    MapTelegramDiceToMessage(securedObjectSpace, externalReplyInfo.Dice, out var databaseDice);
                    if (databaseDice != null)
                        databaseExternalReplyInfo.Dice = databaseDice;
                }
                if (externalReplyInfo.Game != null)
                {
                    MapTelegramGameToMessage(securedObjectSpace, externalReplyInfo.Game, out var databaseGame);
                    if (databaseGame != null)
                        databaseExternalReplyInfo.Game = databaseGame;
                }
                if (externalReplyInfo.Giveaway != null)
                {
                    MapTelegramGiveawayToMessage(
                        securedObjectSpace,
                        externalReplyInfo.Giveaway,
                        out var databaseGiveaway);
                    if (databaseGiveaway != null)
                        databaseExternalReplyInfo.Giveaway = databaseGiveaway;
                }
                if (externalReplyInfo.GiveawayWinners != null)
                {
                    MapTelegramGiveawayWinnersToMessage(
                        securedObjectSpace,
                        externalReplyInfo.GiveawayWinners,
                        out var databaseGiveawayWinners);
                    if (databaseGiveawayWinners != null)
                        databaseExternalReplyInfo.GiveawayWinners = databaseGiveawayWinners;
                }
                databaseExternalReplyInfo.HasMediaSpoiler = externalReplyInfo.HasMediaSpoiler;
                if (externalReplyInfo.LinkPreviewOptions != null)
                {
                    MapTelegramLinkPreviewOptionsToMessage(
                        securedObjectSpace,
                        externalReplyInfo.LinkPreviewOptions,
                        out var databaseLinkPreviewOptions);
                    if (databaseLinkPreviewOptions != null)
                        databaseExternalReplyInfo.LinkPreviewOptions = databaseLinkPreviewOptions;
                }
                if (externalReplyInfo.Location != null)
                {
                    MapTelegramLocationToMessage(
                        securedObjectSpace,
                        externalReplyInfo.Location,
                        out var databaseLocation);
                    if (databaseLocation != null)
                        databaseExternalReplyInfo.Location = databaseLocation;
                }
                if (externalReplyInfo.MessageId != null)
                {
                    databaseExternalReplyInfo.UniqueMessageId = externalReplyInfo.MessageId;
                }
                if (externalReplyInfo.Origin != null)
                {
                    MapTelegramMessageOrigin(
                        securedObjectSpace,
                        externalReplyInfo.Origin,
                        out var databaseMessageOrigin);
                    if (databaseMessageOrigin != null)
                        databaseExternalReplyInfo.Origin = databaseMessageOrigin;
                }
                if (externalReplyInfo.PaidMedia != null)
                {
                    MapTelegramPaidMediaInfoToMessage(
                        securedObjectSpace,
                        externalReplyInfo.PaidMedia,
                        out var databasePaidMedia);
                    if (databasePaidMedia != null)
                        databaseExternalReplyInfo.PaidMedia = databasePaidMedia;
                }
                if (externalReplyInfo.Photo != null)
                {
                    MapTelegramPhotoSizeArrayToMessage(
                        securedObjectSpace,
                        externalReplyInfo.Photo,
                        out var databasePhoto);
                    if (databasePhoto != null)
                        foreach (var pho in databasePhoto)
                            databaseExternalReplyInfo.Photo.Add(pho);
                }
                if (externalReplyInfo.Poll != null)
                {
                    MapTelegramPollToMessage(securedObjectSpace, externalReplyInfo.Poll, out var databasePoll);
                    if (databasePoll != null)
                        databaseExternalReplyInfo.Poll = databasePoll;
                }
                if (externalReplyInfo.Sticker != null)
                {
                    MapTelegramStickerToMessage(securedObjectSpace, externalReplyInfo.Sticker, out var databaseSticker);
                    if (databaseSticker != null)
                        databaseExternalReplyInfo.Sticker = databaseSticker;
                }
                if (externalReplyInfo.Story != null)
                {
                    MapTelegramStoryToMessage(securedObjectSpace, externalReplyInfo.Story, out var databaseStory);
                    if (databaseStory != null)
                        databaseExternalReplyInfo.Story = databaseStory;
                }
                if (externalReplyInfo.Venue != null)
                {
                    MapTelegramVenueToMessage(securedObjectSpace, externalReplyInfo.Venue, out var databaseVenue);
                    if (databaseVenue != null)
                        databaseExternalReplyInfo.Venue = databaseVenue;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramExternalReplyInfoToMessage {externalReplyInfo.ToString()}");
                throw;
            }
        }
        private void MapTelegramForumTopicCreated(IObjectSpace securedObjectSpace, ForumTopicCreated forumTopicCreated, out TelegramForumTopicCreated? databaseForumTopicCreated)
        {
            try
            {
                databaseForumTopicCreated = securedObjectSpace.CreateObject<TelegramForumTopicCreated>(
                    );
                databaseForumTopicCreated.Name = forumTopicCreated.Name ?? string.Empty;
                databaseForumTopicCreated.IconColor = forumTopicCreated.IconColor;
                databaseForumTopicCreated.IconCustomEmojiId = forumTopicCreated.IconCustomEmojiId ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramForumTopicCreated {forumTopicCreated.ToString()}");
                throw;
            }
        }

        private void MapTelegramForumTopicEdited(IObjectSpace securedObjectSpace, ForumTopicEdited forumTopicEdited, out TelegramForumTopicEdited? databaseForumTopicEdited)
        {
            try
            {
                databaseForumTopicEdited = securedObjectSpace.CreateObject<TelegramForumTopicEdited>(
                    );
                databaseForumTopicEdited.Name = forumTopicEdited.Name ?? string.Empty;
                databaseForumTopicEdited.IconCustomEmojiId = forumTopicEdited.IconCustomEmojiId ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramForumTopicEdited {forumTopicEdited.ToString()}");
                throw;
            }
        }
        private void MapTelegramGameToMessage(IObjectSpace securedObjectSpace, Game game, out TelegramGame? databaseMessageGame)
        {
            try
            {
                databaseMessageGame = securedObjectSpace.CreateObject<TelegramGame>();
                databaseMessageGame.Title = game.Title ?? string.Empty;
                databaseMessageGame.Description = game.Description ?? string.Empty;
                if (game.Photo != null && game.Photo.Length > 0)
                {
                    MapTelegramPhotoSizeArrayToMessage(securedObjectSpace, game.Photo, out var databasePhotoSizes);
                    if (databasePhotoSizes != null)
                        foreach (var entry in databasePhotoSizes)
                            databaseMessageGame.Photo.Add(entry);
                }
                if (game.Text != null)
                {
                    databaseMessageGame.Text = game.Text;
                }
                if (game.TextEntities != null && game.TextEntities.Length > 0)
                {
                    MapTelegramMessageEntitiesToMessage(securedObjectSpace, game.TextEntities, out var databaseMessageEntities);
                    if (databaseMessageEntities != null && databaseMessageGame.TextEntities != null)
                        foreach (var entry in databaseMessageEntities)
                            databaseMessageGame.TextEntities.Add(entry);
                }
                if (game.Animation != null)
                {
                    MapTelegramAnimationToMessage(securedObjectSpace, game.Animation, out var databaseAnimation);
                    if (databaseAnimation != null)
                        databaseMessageGame.Animation = databaseAnimation;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGameToMessage {game.ToString()}");
                throw;
            }
        }

        private void MapTelegramGiftInfoToMessage(IObjectSpace securedObjectSpace, GiftInfo giftinfo, out TelegramGiftInfo? databaseGiftInfo)
        {
            try
            {
                databaseGiftInfo = securedObjectSpace.CreateObject<TelegramGiftInfo>();
                databaseGiftInfo.Text = giftinfo.Text ?? string.Empty;
                databaseGiftInfo.OwnedGiftId = giftinfo.OwnedGiftId ?? string.Empty;
                databaseGiftInfo.CanBeUpgraded = giftinfo.CanBeUpgraded;
                databaseGiftInfo.ConvertStarCount = giftinfo.ConvertStarCount;
                databaseGiftInfo.PrepaidUpgradeStarCount = giftinfo.PrepaidUpgradeStarCount;
                databaseGiftInfo.IsPrivate = giftinfo.IsPrivate;

                if (databaseGiftInfo.Gift != null)
                {
                    MapTelegramGiftToMessage(securedObjectSpace, giftinfo.Gift, out var databaseGift);
                    if (databaseGift != null)
                        databaseGiftInfo.Gift = databaseGift;
                }
                if (databaseGiftInfo.Entities != null && giftinfo.Entities != null && giftinfo.Entities.Length > 0)
                {
                    MapTelegramMessageEntitiesToMessage(securedObjectSpace, giftinfo.Entities, out var databaseMessageEntities);
                    if (databaseMessageEntities != null)
                        foreach (var entry in databaseMessageEntities)
                            databaseGiftInfo.Entities.Add(entry);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGiftInfoToMessage {giftinfo.ToString()}");
                throw;
            }
        }

        private void MapTelegramGiftToMessage(IObjectSpace securedObjectSpace, Gift gift, out TelegramGift? databaseGift)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(gift.Id);
                CriteriaOperator giftCriteria = CriteriaOperator.FromLambda<TelegramGift>(
            dbgift => dbgift.GiftId == gift.Id);
                databaseGift = securedObjectSpace.FindObject<TelegramGift>(
                    giftCriteria);
                if (databaseGift == null)
                {
                    databaseGift = securedObjectSpace.CreateObject<TelegramGift>(
                        );


                }
                databaseGift.GiftId = gift.Id;
                if (gift.Sticker != null)
                {
                    MapTelegramStickerToMessage(securedObjectSpace, gift.Sticker, out var databaseSticker);
                    if (databaseSticker != null)
                        databaseGift.Sticker = databaseSticker;
                }

                databaseGift.StarCount = gift.StarCount;
                databaseGift.UpgradeStarCount = gift.UpgradeStarCount;
                databaseGift.TotalCount = gift.TotalCount;
                databaseGift.RemainingCount = gift.RemainingCount;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGiftToMessage {gift.ToString()}");
                throw;
            }
        }

        private void MapTelegramGiveawayCompletedToMessage(IObjectSpace securedObjectSpace, GiveawayCompleted giveawayCompleted, out TelegramGiveawayCompleted? databaseGiveawayCompleted)
        {
            try
            {
                databaseGiveawayCompleted = securedObjectSpace.CreateObject<TelegramGiveawayCompleted>();
                if (giveawayCompleted.GiveawayMessage != null)
                {



                    ArgumentNullException.ThrowIfNull(giveawayCompleted.GiveawayMessage.Chat);
                    var findExistingGiveawayMessageMessage = CriteriaOperator.FromLambda<TelegramMessage>(
  m => m.Message_ID == giveawayCompleted.GiveawayMessage.MessageId);
                    var existingGiveawayMessage = securedObjectSpace.FindObject<TelegramMessage>(findExistingGiveawayMessageMessage);

                    if (existingGiveawayMessage != null)
                    {
                        databaseGiveawayCompleted.GiveawayMessage = existingGiveawayMessage;

                        logger.LogInformation($"Pinned MapTelegramGiveawayCompletedToMessage {giveawayCompleted.ToString()} already exists in database");
                    }
                    else
                    {
                        try
                        {
                            MapTelegramMessageToDatabase(giveawayCompleted.GiveawayMessage, securedObjectSpace, out var GiveawayMessage);

                            GiveawayMessage = securedObjectSpace.FindObject<TelegramMessage>(findExistingGiveawayMessageMessage);
                            databaseGiveawayCompleted.GiveawayMessage = GiveawayMessage;
                            logger.LogInformation($"Nested MapTelegramGiveawayCompletedToMessage {giveawayCompleted.ToString()} saved to database");

                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Error while adding nested MapTelegramGiveawayCompletedToMessage  to DB {ex.ToString()}");
                        }
                    }
                }
                databaseGiveawayCompleted.IsStarGiveaway = giveawayCompleted.IsStarGiveaway;
                databaseGiveawayCompleted.UnclaimedPrizeCount = giveawayCompleted.UnclaimedPrizeCount;
                databaseGiveawayCompleted.WinnerCount = giveawayCompleted.WinnerCount;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGiveawayCompletedToMessage {giveawayCompleted.ToString()}");
                throw;
            }
        }
        private void MapTelegramGiveawayToMessage(IObjectSpace securedObjectSpacke, Giveaway giveaway, out TelegramGiveaway? databaseGiveaway)
        {
            try
            {
                databaseGiveaway = securedObjectSpacke.CreateObject<TelegramGiveaway>();
                if (giveaway.CountryCodes != null && giveaway.CountryCodes.LongLength > 0)
                {
                    foreach (var entry in giveaway.CountryCodes)
                        databaseGiveaway.CountryCodes.Add(entry);
                }

                databaseGiveaway.HasPublicWinners = giveaway.HasPublicWinners;
                databaseGiveaway.OnlyNewMembers = giveaway.OnlyNewMembers;
                databaseGiveaway.PremiumSubscriptionMonthCount = giveaway.PremiumSubscriptionMonthCount;
                databaseGiveaway.PrizeDescription = giveaway.PrizeDescription ?? string.Empty;
                databaseGiveaway.PrizeStarCount = giveaway.PrizeStarCount;
                databaseGiveaway.WinnerCount = giveaway.WinnerCount;
                databaseGiveaway.WinnersSelectionDate = giveaway.WinnersSelectionDate;
                if (giveaway.Chats != null && giveaway.Chats.LongLength > 0)
                {
                    foreach (var chat in giveaway.Chats)
                    {
                        MapTelegramChatToMessage(securedObjectSpacke, chat, out var databaseChat);
                        if (databaseChat != null)
                        {
                            databaseGiveaway.Chats.Add(databaseChat);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGiveawayToMessage {giveaway.ToString()}");
                throw;
            }
        }

        private void MapTelegramGiveawayWinnersToMessage(IObjectSpace securedObjectSpace, GiveawayWinners giveawayWinners, out TelegramGiveawayWinners? databaseGiveawayWinners)
        {
            try
            {
                databaseGiveawayWinners = securedObjectSpace.CreateObject<TelegramGiveawayWinners>();
                databaseGiveawayWinners.AdditionalChatCount = giveawayWinners.AdditionalChatCount;
                databaseGiveawayWinners.GiveawayMessageId = giveawayWinners.GiveawayMessageId;
                databaseGiveawayWinners.OnlyNewMembers = giveawayWinners.OnlyNewMembers;
                databaseGiveawayWinners.PremiumSubscriptionMonthCount = giveawayWinners.PremiumSubscriptionMonthCount;
                databaseGiveawayWinners.PrizeDescription = giveawayWinners.PrizeDescription ?? string.Empty;
                databaseGiveawayWinners.PrizeStarCount = giveawayWinners.PrizeStarCount;
                databaseGiveawayWinners.WinnerCount = giveawayWinners.WinnerCount;
                databaseGiveawayWinners.WinnersSelectionDate = giveawayWinners.WinnersSelectionDate;
                databaseGiveawayWinners.UnclaimedPrizeCount = giveawayWinners.UnclaimedPrizeCount;
                if (giveawayWinners.Winners != null && giveawayWinners.Winners.LongLength > 0)
                {
                    foreach (var winner in giveawayWinners.Winners)
                    {
                        MapTelegramUserToMessage(securedObjectSpace, winner, out var databaseUser);
                        if (databaseUser != null)
                        {
                            databaseGiveawayWinners.Winners.Add(databaseUser);
                        }
                    }
                }
                giveawayWinners.WasRefunded = giveawayWinners.WasRefunded;
                giveawayWinners.WinnerCount = giveawayWinners.WinnerCount;
                giveawayWinners.WinnersSelectionDate = giveawayWinners.WinnersSelectionDate;
                if (giveawayWinners.Chat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, giveawayWinners.Chat, out var databaseChat);
                    if (databaseChat != null)
                        databaseGiveawayWinners.Chat = databaseChat;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramGiveawayWinnerToMessage {giveawayWinners.ToString()}");
                throw;
            }
        }
        private void MapTelegramInlineKeyboardButtonToMessage(IObjectSpace securedObjectSpace, InlineKeyboardButton inlineKeyboardButton, out TelegramInlineKeyboardButton? databaseInlineKeyboardButton)
        {
            try
            {
                databaseInlineKeyboardButton = securedObjectSpace.CreateObject<TelegramInlineKeyboardButton>();
                databaseInlineKeyboardButton.Text = inlineKeyboardButton.Text ?? string.Empty;
                if (inlineKeyboardButton.Url != null)
                {
                    databaseInlineKeyboardButton.Url = inlineKeyboardButton.Url ?? string.Empty;
                }
                if (inlineKeyboardButton.CallbackData != null)
                {
                    databaseInlineKeyboardButton.CallbackData = inlineKeyboardButton.CallbackData ?? string.Empty;
                }
                if (inlineKeyboardButton.WebApp != null)
                {
                    MapTelegramWebAppToMessage(securedObjectSpace, inlineKeyboardButton.WebApp, out var databaseWebApp);
                    if (databaseWebApp != null)
                        databaseInlineKeyboardButton.WebApp = databaseWebApp;
                }
                if (inlineKeyboardButton.SwitchInlineQueryChosenChat != null)
                {
                    MapTelegramSwitchInlineQueryChosenChatToMessage(securedObjectSpace, inlineKeyboardButton.SwitchInlineQueryChosenChat, out var databaseSwitchInlineQueryChosenChat);
                    if (databaseSwitchInlineQueryChosenChat != null)
                        databaseInlineKeyboardButton.SwitchInlineQueryChosenChat = databaseSwitchInlineQueryChosenChat;
                }
                if (inlineKeyboardButton.CallbackGame != null)
                {
                    MapTelegramCallbackGameToMessage(securedObjectSpace, inlineKeyboardButton.CallbackGame, out var databaseCallbackGame);
                    if (databaseCallbackGame != null)
                        databaseInlineKeyboardButton.CallbackGame = databaseCallbackGame;
                }
                if (inlineKeyboardButton.CopyText != null)
                {
                    MapTelegramCopyTextButtonToMessage(securedObjectSpace, inlineKeyboardButton.CopyText, out var databaseCopyText);
                    if (databaseCopyText != null)
                        databaseInlineKeyboardButton.CopyText = databaseCopyText;
                }
                if (inlineKeyboardButton.LoginUrl != null)
                {
                    MapTelegramLoginUrlToMessage(securedObjectSpace, inlineKeyboardButton.LoginUrl, out var databaseLoginUrl);
                    if (databaseLoginUrl != null)
                        databaseInlineKeyboardButton.LoginUrl = databaseLoginUrl;
                }
                databaseInlineKeyboardButton.Pay = inlineKeyboardButton.Pay;
                if (inlineKeyboardButton.SwitchInlineQuery != null)
                {
                    databaseInlineKeyboardButton.SwitchInlineQuery = inlineKeyboardButton.SwitchInlineQuery ?? string.Empty;
                }
                if (inlineKeyboardButton.SwitchInlineQueryChosenChat != null)
                {
                    MapTelegramSwitchInlineQueryChosenChatToMessage(securedObjectSpace, inlineKeyboardButton.SwitchInlineQueryChosenChat, out var databaseSwitchInlineQueryChosenChat);
                    if (databaseSwitchInlineQueryChosenChat != null)
                        databaseInlineKeyboardButton.SwitchInlineQueryChosenChat = databaseSwitchInlineQueryChosenChat;
                }
                if (inlineKeyboardButton.SwitchInlineQueryCurrentChat != null)
                {
                    databaseInlineKeyboardButton.SwitchInlineQueryCurrentChat = inlineKeyboardButton.SwitchInlineQueryCurrentChat ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramInlineKeyboardButtonToMessage {inlineKeyboardButton.ToString()}");
                throw;
            }
        }
        private void MapTelegramInlineKeyboardMarkupToMessage(IObjectSpace securedObjectSpace, InlineKeyboardMarkup inlineKeyboardMarkup, out TelegramInlineKeyboardMarkup? databaseInlineKeyboardMarkup)
        {
            try
            {
                databaseInlineKeyboardMarkup = securedObjectSpace.CreateObject<TelegramInlineKeyboardMarkup>();
                if (inlineKeyboardMarkup.InlineKeyboard != null && inlineKeyboardMarkup.InlineKeyboard.Count() > 0)
                {
                    var listInlineKeyboardButtons = new ObservableCollection<ObservableCollection<TelegramInlineKeyboardButton>>();
                    foreach (IEnumerable<InlineKeyboardButton> row in inlineKeyboardMarkup.InlineKeyboard)
                    {
                        var innerListInlineKeyboardButtons = new ObservableCollection<TelegramInlineKeyboardButton>();
                        foreach (InlineKeyboardButton button in row)
                        {

                            MapTelegramInlineKeyboardButtonToMessage(securedObjectSpace, button, out var databaseInlineKeyboardButton);
                            if (databaseInlineKeyboardButton != null)
                            {
                                innerListInlineKeyboardButtons.Add(databaseInlineKeyboardButton);
                            }
                        }
                        if (innerListInlineKeyboardButtons.Count > 0)
                        {
                            listInlineKeyboardButtons.Add(innerListInlineKeyboardButtons);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramInlineKeyboardMarkupToMessage {inlineKeyboardMarkup.ToString()}");
                throw;
            }
        }

        private void MapTelegramInviteLink(IObjectSpace securedObjectSpace, ChatInviteLink telegramInviteLink, out TelegramChatInviteLink? databaseInviteLink)
        {
            try
            {
                databaseInviteLink = securedObjectSpace.CreateObject<TelegramChatInviteLink>();
                databaseInviteLink.CreatesJoinRequest = telegramInviteLink.CreatesJoinRequest;
                if (telegramInviteLink.Creator != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramInviteLink.Creator, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        databaseInviteLink.Creator = databaseUser;
                    }

                }
                if (telegramInviteLink.ExpireDate != null)
                {
                    databaseInviteLink.ExpireDate = telegramInviteLink.ExpireDate;

                }
                databaseInviteLink.InviteLink = telegramInviteLink.InviteLink;
                databaseInviteLink.IsPrimary = telegramInviteLink.IsPrimary;
                databaseInviteLink.IsRevoked = telegramInviteLink.IsRevoked;
                if (telegramInviteLink.MemberLimit != null)
                    databaseInviteLink.MemberLimit = telegramInviteLink.MemberLimit;
                if (telegramInviteLink.Name != null)
                    databaseInviteLink.Name = telegramInviteLink.Name;
                if (telegramInviteLink.PendingJoinRequestCount != null)
                    databaseInviteLink.PendingJoinRequestCount = telegramInviteLink.PendingJoinRequestCount;
                if (telegramInviteLink.SubscriptionPeriod != null)
                    databaseInviteLink.SubscriptionPeriod = telegramInviteLink.SubscriptionPeriod;
                if (telegramInviteLink.SubscriptionPrice != null)
                    databaseInviteLink.SubscriptionPrice = telegramInviteLink.SubscriptionPrice;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramInviteLink {telegramInviteLink.ToString()}");
                throw;
            }
        }

        private void MapTelegramInvoiceToMessage(IObjectSpace securedObjectSpace, Invoice invoice, out TelegramInvoice? databaseInvoice)
        {
            try
            {
                databaseInvoice = securedObjectSpace.CreateObject<TelegramInvoice>();
                databaseInvoice.Title = invoice.Title;
                databaseInvoice.Description = invoice.Description;
                databaseInvoice.Currency = invoice.Currency;
                databaseInvoice.TotalAmount = invoice.TotalAmount;
                databaseInvoice.StartParameter = invoice.StartParameter;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramInvoiceToMessage {invoice.ToString()}");
                throw;
            }
        }
        private void MapTelegramLinkPreviewOptionsToMessage(IObjectSpace securedObjectSpace, LinkPreviewOptions linkPreviewOptions, out TelegramLinkPreviewOptions? databaseLinkPreviewOptions)
        {
            try
            {
                databaseLinkPreviewOptions = securedObjectSpace.CreateObject<TelegramLinkPreviewOptions>();
                databaseLinkPreviewOptions.PreferLargeMedia = linkPreviewOptions.PreferLargeMedia;
                databaseLinkPreviewOptions.Url = linkPreviewOptions.Url ?? string.Empty;
                databaseLinkPreviewOptions.IsDisabled = linkPreviewOptions.IsDisabled;
                databaseLinkPreviewOptions.PreferSmallMedia = linkPreviewOptions.PreferSmallMedia;
                databaseLinkPreviewOptions.ShowAboveText = linkPreviewOptions.ShowAboveText;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramLinkPreviewOptionsToMessage {linkPreviewOptions.ToString()}");
                throw;
            }
        }
        private void MapTelegramLocationToMessage(IObjectSpace securedObjectSpace, Location location, out TelegramLocation? databaseLocation)
        {
            try
            {
                CriteriaOperator locationCriteria = CriteriaOperator.FromLambda<TelegramLocation>(
                    loc => loc.Latitude == location.Latitude && loc.Longitude == location.Longitude && loc.ProximityAlertRadius == location.ProximityAlertRadius && loc.HorizontalAccuracy == location.HorizontalAccuracy && loc.Heading == location.Heading && loc.LivePeriod == location.LivePeriod);
                databaseLocation = securedObjectSpace.FindObject<TelegramLocation>(
                    locationCriteria);
                if (databaseLocation == null)
                {
                    databaseLocation = securedObjectSpace.CreateObject<TelegramLocation>(
                        );

                }
                databaseLocation.Latitude = location.Latitude;
                databaseLocation.Longitude = location.Longitude;
                databaseLocation.HorizontalAccuracy = location.HorizontalAccuracy;
                databaseLocation.LivePeriod = location.LivePeriod;
                databaseLocation.Heading = location.Heading;
                databaseLocation.ProximityAlertRadius = location.ProximityAlertRadius;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramLocationToMessage {location.ToString()}");
                throw;
            }
        }
        private void MapTelegramLoginUrlToMessage(IObjectSpace securedObjectSpace, LoginUrl loginUrl, out TelegramLoginUrl? databaseLoginUrl)
        {
            try
            {
                databaseLoginUrl = securedObjectSpace.CreateObject<TelegramLoginUrl>();
                databaseLoginUrl.Url = loginUrl.Url ?? string.Empty;
                databaseLoginUrl.ForwardText = loginUrl.ForwardText ?? string.Empty;
                databaseLoginUrl.BotUsername = loginUrl.BotUsername ?? string.Empty;
                databaseLoginUrl.RequestWriteAccess = loginUrl.RequestWriteAccess;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramLoginUrlToMessage {loginUrl.ToString()}");
                throw;
            }
        }
        private void MapTelegramMaskPositionToMessage(IObjectSpace securedObjectSpace, MaskPosition maskPosition, out TelegramMaskPosition? databaseMaskPosition)
        {
            try
            {
                CriteriaOperator maskPositionCriteria = CriteriaOperator.FromLambda<TelegramMaskPosition>(
dbmaskPosition => dbmaskPosition.Point == maskPosition.Point && dbmaskPosition.Scale == maskPosition.Scale && dbmaskPosition.XShift == maskPosition.XShift && dbmaskPosition.YShift == maskPosition.YShift);
                databaseMaskPosition = securedObjectSpace.FindObject<TelegramMaskPosition>(
                    maskPositionCriteria);
                if (databaseMaskPosition == null)
                {
                    databaseMaskPosition = securedObjectSpace.CreateObject<TelegramMaskPosition>();


                }
                databaseMaskPosition.Point = maskPosition.Point;
                databaseMaskPosition.XShift = maskPosition.XShift;
                databaseMaskPosition.YShift = maskPosition.YShift;
                databaseMaskPosition.Scale = maskPosition.Scale;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramStickerToMessage {maskPosition.ToString()}");
                throw;
            }
        }
        private void MapTelegramMessageEntitiesToMessage(IObjectSpace securedObjectSpace, MessageEntity[] messageEntities, out ObservableCollection<TelegramMessageEntity>? databaseMessageEntities)
        {
            try
            {

                databaseMessageEntities = new();
                foreach (var entity in messageEntities)
                {
                    CriteriaOperator entityCriteria = CriteriaOperator.FromLambda<TelegramMessageEntity>(
                        x => x.Type == entity.Type && x.Offset == entity.Offset && x.Length == entity.Length);
                    var messageEntity = securedObjectSpace.FindObject<TelegramMessageEntity>(
                        entityCriteria);
                    if (messageEntity == null)
                    {
                        messageEntity = securedObjectSpace.CreateObject<TelegramMessageEntity>(
                            );

                    }
                    messageEntity.Type = entity.Type;
                    messageEntity.Offset = entity.Offset;
                    messageEntity.Length = entity.Length;
                    messageEntity.Url = entity.Url ?? string.Empty;
                    messageEntity.Language = entity.Language ?? string.Empty;
                    messageEntity.CustomEmojiId = entity.CustomEmojiId ?? string.Empty;
                    if (entity.User != null)
                    {
                        ArgumentOutOfRangeException.ThrowIfZero(entity.User.Id);
                        ArgumentOutOfRangeException.ThrowIfLessThan(entity.User.Id, 0);
                        CriteriaOperator userCriteria = CriteriaOperator.FromLambda<TelegramUser>(
                            user => user.UserId == entity.User.Id);
                        var user = securedObjectSpace.FindObject<TelegramUser>(
                            userCriteria);
                        if (user == null)
                        {
                            user = securedObjectSpace.CreateObject<TelegramUser>(
                                );
                            user.UserId = entity.User.Id;
                            user.IsBot = entity.User.IsBot;
                            user.FirstName = entity.User.FirstName ?? string.Empty;
                            user.LastName = entity.User.LastName ?? string.Empty;
                            user.Username = entity.User.Username ?? string.Empty;
                            user.LanguageCode = entity.User.LanguageCode ?? string.Empty;
                            user.IsPremium = entity.User.IsPremium;
                            user.AddedToAttachmentMenu = entity.User.AddedToAttachmentMenu;
                            user.CanJoinGroups = entity.User.CanJoinGroups;
                            user.CanReadAllGroupMessages = entity.User.CanReadAllGroupMessages;
                            user.SupportsInlineQueries = entity.User.SupportsInlineQueries;
                        }
                        messageEntity.User = user;
                    }
                    databaseMessageEntities.Add(messageEntity);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramMessageEntitiesToMessage {messageEntities.ToString()}");
                throw;
            }
        }
        private void MapTelegramMessageOrigin(IObjectSpace securedObjectSpace, MessageOrigin messageOrigin, out TelegramMessageOrigin? databaseMessageOrigin)
        {
            try
            {
                switch (messageOrigin.Type)
                {
                    case MessageOriginType.User:
                        if (messageOrigin is MessageOriginUser messageOriginUser)
                        {
                            var databaseMessageOriginUser = securedObjectSpace.CreateObject<TelegramMessageOriginUser>(
                                );

                            MapTelegramUserToMessage(
                                securedObjectSpace,
                                messageOriginUser.SenderUser,
                                out var databaseUser);
                            if (databaseUser != null)
                                databaseMessageOriginUser.SenderUser = databaseUser;
                            databaseMessageOriginUser.Date = messageOriginUser.Date;
                            databaseMessageOrigin = databaseMessageOriginUser;
                        }
                        else
                            databaseMessageOrigin = null;
                        break;
                    case MessageOriginType.Channel:
                        if (messageOrigin is MessageOriginChannel messageOriginChannel)
                        {
                            var databaseMessageOriginChannel = securedObjectSpace.CreateObject<TelegramMessageOriginChannel>(
                                 );
                            MapTelegramChatToMessage(
                                securedObjectSpace,
                                messageOriginChannel.Chat,
                                out var databaseChannel);
                            if (databaseChannel != null)
                                databaseMessageOriginChannel.Chat = databaseChannel;
                            databaseMessageOriginChannel.Date = messageOriginChannel.Date;
                            databaseMessageOriginChannel.AuthorSignature = messageOriginChannel.AuthorSignature ?? string.Empty;
                            databaseMessageOriginChannel.messageId = messageOriginChannel.MessageId;
                            databaseMessageOrigin = databaseMessageOriginChannel;
                        }
                        else
                            databaseMessageOrigin = null;
                        break;
                    case MessageOriginType.Chat:
                        if (messageOrigin is MessageOriginChat messageOriginChat)
                        {
                            var databaseMessageOriginChat = securedObjectSpace.CreateObject<TelegramMessageOriginChat>(
                                );
                            MapTelegramChatToMessage(
                                securedObjectSpace,
                                messageOriginChat.SenderChat,
                                out var databaseChat);
                            if (databaseChat != null)
                                databaseMessageOriginChat.SenderChat = databaseChat;
                            databaseMessageOriginChat.Date = messageOriginChat.Date;
                            databaseMessageOriginChat.AuthorSignature = messageOriginChat.AuthorSignature ?? string.Empty;
                            databaseMessageOrigin = databaseMessageOriginChat;
                        }
                        else
                            databaseMessageOrigin = null;
                        break;
                    case MessageOriginType.HiddenUser:
                        if (messageOrigin is MessageOriginHiddenUser messageOriginHiddenUser)
                        {
                            var databaseMessageOriginHiddenUser = securedObjectSpace.CreateObject<TelegramMessageOriginHiddenUser>(
                                 );
                            databaseMessageOriginHiddenUser.SenderUserName = messageOriginHiddenUser.SenderUserName ?? string.Empty;
                            databaseMessageOriginHiddenUser.Date = messageOriginHiddenUser.Date;
                            databaseMessageOrigin = databaseMessageOriginHiddenUser;
                        }
                        else
                            databaseMessageOrigin = null;
                        break;
                    default:
                        databaseMessageOrigin = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramMessageOrigin {messageOrigin.ToString()}");
                throw;
            }
        }

        private void MaptelegramMessageReactionCountUpdates(IObjectSpace securedObjectSpace, MessageReactionCountUpdated telegramMessageReactionCountUpdates, out TelegramMessageReactionCountUpdated finaltelegramMessageReactionCountUpdates)
        {
            try
            {
                finaltelegramMessageReactionCountUpdates = securedObjectSpace.CreateObject<TelegramMessageReactionCountUpdated>();
                if (telegramMessageReactionCountUpdates.Chat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, telegramMessageReactionCountUpdates.Chat, out TelegramChat? databaseChat);
                    if (databaseChat != null)
                        finaltelegramMessageReactionCountUpdates.Chat = databaseChat;
                }
                finaltelegramMessageReactionCountUpdates.Date = telegramMessageReactionCountUpdates.Date;
                finaltelegramMessageReactionCountUpdates.TelegramMessageReactionCountUpdatedMessageID = telegramMessageReactionCountUpdates.MessageId;
                if (finaltelegramMessageReactionCountUpdates.Reactions == null)
                {
                    finaltelegramMessageReactionCountUpdates.Reactions = new ObservableCollection<TelegramReactionCount>();
                }
                foreach (var reaction in telegramMessageReactionCountUpdates.Reactions)
                {
                    var databaseReaction = securedObjectSpace.CreateObject<TelegramReactionCount>();
                    databaseReaction.TotalCount = reaction.TotalCount;
                    switch (reaction.Type)
                    {
                        case ReactionTypeCustomEmoji reactionTypeCustomEmoji:
                            var databaseReactionTypeCustomEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeCustomEmoji>();
                            databaseReactionTypeCustomEmoji.CustomEmojiId = reactionTypeCustomEmoji.CustomEmojiId;
                            databaseReaction.Type = databaseReactionTypeCustomEmoji;
                            break;
                        case ReactionTypeEmoji reactionTypeEmoji:
                            var databaseReactionTypeEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeEmoji>();
                            databaseReactionTypeEmoji.Emoji = reactionTypeEmoji.Emoji;
                            databaseReaction.Type = databaseReactionTypeEmoji;
                            break;
                        case ReactionTypePaid reactionTypePaid:
                            var databaseReactionTypePaid = securedObjectSpace.CreateObject<TelegramReactionTypePaid>();
                            databaseReaction.Type = databaseReactionTypePaid;
                            break;
                        default:
                            break;
                    }
                    finaltelegramMessageReactionCountUpdates.Reactions.Add(databaseReaction);
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramMessageReactionCountUpdates {telegramMessageReactionCountUpdates.ToString()}");
                throw;
            }
        }

        private void MapTelegramMessageReactionUpdated(IObjectSpace securedObjectSpace, MessageReactionUpdated telegramMessageReactionUpdated, out TelegramMessageReactionUpdated finalMessageReactionUpdated)
        {
            try
            {
                finalMessageReactionUpdated = securedObjectSpace.CreateObject<TelegramMessageReactionUpdated>();
                if (telegramMessageReactionUpdated.ActorChat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, telegramMessageReactionUpdated.ActorChat, out TelegramChat? databaseactorChat);
                    if (databaseactorChat != null)
                        finalMessageReactionUpdated.ActorChat = databaseactorChat;
                }
                MapTelegramChatToMessage(securedObjectSpace, telegramMessageReactionUpdated.Chat, out TelegramChat? databaseChat);
                if (databaseChat != null)
                    finalMessageReactionUpdated.Chat = databaseChat;
                finalMessageReactionUpdated.Date = telegramMessageReactionUpdated.Date;
                finalMessageReactionUpdated.MessageIdFromReactionUpdate = telegramMessageReactionUpdated.MessageId;
                finalMessageReactionUpdated.UpdateType = UpdateType.MessageReaction;

                if (telegramMessageReactionUpdated.User != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramMessageReactionUpdated.User, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finalMessageReactionUpdated.User = databaseUser;
                    }

                }
                foreach (var reactionTmp in telegramMessageReactionUpdated.NewReaction)
                {
                    switch (reactionTmp)
                    {
                        case ReactionTypeCustomEmoji reactionTypeCustomEmoji:
                            var databaseReactionTypeCustomEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeCustomEmoji>();
                            databaseReactionTypeCustomEmoji.CustomEmojiId = reactionTypeCustomEmoji.CustomEmojiId;
                            finalMessageReactionUpdated.NewReaction.Add(databaseReactionTypeCustomEmoji);
                            break;
                        case ReactionTypeEmoji reactionTypeEmoji:
                            var databaseReactionTypeEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeEmoji>();
                            databaseReactionTypeEmoji.Emoji = reactionTypeEmoji.Emoji;
                            finalMessageReactionUpdated.NewReaction.Add(databaseReactionTypeEmoji);
                            break;
                        case ReactionTypePaid reactionTypePaid:
                            var databaseReactionTypePaid = securedObjectSpace.CreateObject<TelegramReactionTypePaid>();
                            finalMessageReactionUpdated.NewReaction.Add(databaseReactionTypePaid);
                            break;
                        default:
                            break;
                    }
                }
                foreach (var reactionTmp in telegramMessageReactionUpdated.OldReaction)
                {
                    switch (reactionTmp)
                    {
                        case ReactionTypeCustomEmoji reactionTypeCustomEmoji:
                            var databaseReactionTypeCustomEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeCustomEmoji>();
                            databaseReactionTypeCustomEmoji.CustomEmojiId = reactionTypeCustomEmoji.CustomEmojiId;
                            finalMessageReactionUpdated.OldReaction.Add(databaseReactionTypeCustomEmoji);
                            break;
                        case ReactionTypeEmoji reactionTypeEmoji:
                            var databaseReactionTypeEmoji = securedObjectSpace.CreateObject<TelegramReactionTypeEmoji>();
                            databaseReactionTypeEmoji.Emoji = reactionTypeEmoji.Emoji;
                            finalMessageReactionUpdated.OldReaction.Add(databaseReactionTypeEmoji);
                            break;
                        case ReactionTypePaid reactionTypePaid:
                            var databaseReactionTypePaid = securedObjectSpace.CreateObject<TelegramReactionTypePaid>();
                            finalMessageReactionUpdated.OldReaction.Add(databaseReactionTypePaid);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MapTelegramMessageReactionUpdated {telegramMessageReactionUpdated.ToString()}");
                throw;
            }
        }


        private void MapTelegramMessageToDatabase(Message telegramMessage, IObjectSpace securedObjectSpace, out TelegramMessage? databaseMessage)
        {
            try
            {
                CriteriaOperator messageFindCriteria = CriteriaOperator.FromLambda<TelegramMessage>(
                                          x => x.Message_ID == telegramMessage.Id && x.Chat != null && telegramMessage.Chat.Id == x.Chat.ChatId);
                databaseMessage = securedObjectSpace.FindObject<TelegramMessage>(
                    messageFindCriteria);
                if (databaseMessage == null)
                {
                    databaseMessage = securedObjectSpace.CreateObject<TelegramMessage>(
                        );

                }
                if (telegramMessage.Animation != null)
                {
                    MapTelegramAnimationToMessage(
                        securedObjectSpace,
                        telegramMessage.Animation,
                        out var messageAnimation);

                    databaseMessage.Animation = messageAnimation;
                }
                if (telegramMessage.Id > 0)
                {
                    databaseMessage.Message_ID = telegramMessage.MessageId;

                }
                if (telegramMessage.Audio != null)
                {
                    MapTelegramAudioToMessage(securedObjectSpace, telegramMessage.Audio, out var messageAudio);

                    databaseMessage.Audio = messageAudio;
                }

                if (telegramMessage.AuthorSignature != null)
                {
                    databaseMessage.AuthorSignature = telegramMessage.AuthorSignature ?? string.Empty;
                }

                if (telegramMessage.BoostAdded != null)
                {
                    databaseMessage.BoostCount = telegramMessage.BoostAdded.BoostCount;
                }


                if (telegramMessage.Caption != null)
                {
                    databaseMessage.Caption = telegramMessage.Caption ?? string.Empty;
                }

                if (telegramMessage.CaptionEntities != null && telegramMessage.CaptionEntities.Length > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        telegramMessage.CaptionEntities,
                        out var databaseMessageEntities);

                    if (databaseMessageEntities != null)
                        foreach (var ent in databaseMessageEntities)
                            databaseMessage.CaptionEntities.Add(ent);
                }

                if (telegramMessage.ChannelChatCreated != null)
                {
                    databaseMessage.ChannelChatCreated = telegramMessage.ChannelChatCreated;
                }


                if (telegramMessage.Chat != null)
                {

                    MapTelegramChatToMessage(securedObjectSpace, telegramMessage.Chat, out var fromChat);

                    if (fromChat != null)
                        databaseMessage.Chat = fromChat;
                }

                if (telegramMessage.ChatShared != null)
                {
                    MapTelegramChatSharedToMessage(
                        securedObjectSpace,
                        telegramMessage.ChatShared,
                        out var databaseChatShared);

                    databaseMessage.ChatShared = databaseChatShared;
                }

                if (telegramMessage.Contact != null)
                {
                    MapTelegramContactToMessage(
                        securedObjectSpace,
                        telegramMessage.Contact,

                        out var databaseContact);

                    databaseMessage.Contact = databaseContact;
                }

                databaseMessage.Date = telegramMessage.Date;

                if (telegramMessage.Dice != null)
                {
                    MapTelegramDiceToMessage(securedObjectSpace, telegramMessage.Dice, out var databaseDice);

                    databaseMessage.Dice = databaseDice;
                }

                if (telegramMessage.Document != null)
                {
                    MapTelegramDocumentToMessage(
                        securedObjectSpace,
                        telegramMessage.Document,
                        out var databaseDocument);

                    databaseMessage.Document = databaseDocument;
                }

                if (telegramMessage.EditDate != null)
                {
                    databaseMessage.EditDate = telegramMessage.EditDate;
                }

                if (telegramMessage.EffectId != null)
                {
                    databaseMessage.EffectId = telegramMessage.EffectId;
                }

                if (telegramMessage.Entities != null && telegramMessage.Entities.Length > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        telegramMessage.Entities,
                        out var databaseEntities);

                    if (databaseEntities != null && databaseMessage.Entities != null)
                        foreach (var entity in databaseEntities)
                            databaseMessage.Entities.Add(entity);
                }

                databaseMessage.Message_ID = telegramMessage.MessageId;
                if (telegramMessage.Venue != null)
                {
                    MapTelegramVenueToMessage(securedObjectSpace, telegramMessage.Venue, out var messageVenue);

                    databaseMessage.Venue = messageVenue;
                }

                if (telegramMessage.ExternalReply != null)
                {
                    MapTelegramExternalReplyInfoToMessage(
                        securedObjectSpace,
                        telegramMessage.ExternalReply,
                        out var messageExternalReply);

                    databaseMessage.ExternalReply = messageExternalReply;
                }

                if (telegramMessage.ForumTopicClosed != null)
                {
                    databaseMessage.ForumTopicClosed = true;
                }

                if (telegramMessage.ForumTopicCreated != null)
                {
                    MapTelegramForumTopicCreated(
                        securedObjectSpace,
                        telegramMessage.ForumTopicCreated,
                        out var forumTopicCreated);

                    databaseMessage.ForumTopicCreated = forumTopicCreated;
                }

                if (telegramMessage.ForumTopicEdited != null)
                {
                    MapTelegramForumTopicEdited(
                        securedObjectSpace,
                        telegramMessage.ForumTopicEdited,
                        out var forumTopicEdited);

                    databaseMessage.ForumTopicEdited = forumTopicEdited;
                }

                if (telegramMessage.ForumTopicReopened != null)
                {
                    databaseMessage.ForumTopicReopened = true;
                }

                if (telegramMessage.ForwardOrigin != null)
                {
                    MapTelegramMessageOrigin(
                        securedObjectSpace,
                        telegramMessage.ForwardOrigin,
                        out var messageForwardOrigin);

                    databaseMessage.ForwardOrigin = messageForwardOrigin;
                }

                if (telegramMessage.From != null)
                {

                    MapTelegramUserToMessage(securedObjectSpace, telegramMessage.From, out var messageFrom);
                    if (messageFrom != null)
                        databaseMessage.From = messageFrom;
                }

                if (telegramMessage.Game != null)
                {
                    MapTelegramGameToMessage(securedObjectSpace, telegramMessage.Game, out var messageGame);

                    databaseMessage.Game = messageGame;
                }

                if (telegramMessage.GeneralForumTopicHidden != null)
                {
                    databaseMessage.GeneralForumTopicHidden = true;
                }

                if (telegramMessage.GeneralForumTopicUnhidden != null)
                {
                    databaseMessage.GeneralForumTopicUnhidden = true;
                }

                if (telegramMessage.GroupChatCreated != null)
                {
                    databaseMessage.GroupChatCreated = true;
                }

                if (telegramMessage.Gift != null)
                {
                    MapTelegramGiftInfoToMessage(securedObjectSpace, telegramMessage.Gift, out var messageGiftInfo);

                    databaseMessage.Gift = messageGiftInfo;
                }

                if (telegramMessage.Giveaway != null)
                {
                    MapTelegramGiveawayToMessage(securedObjectSpace, telegramMessage.Giveaway, out var messageGiveaway);

                    databaseMessage.Giveaway = messageGiveaway;
                }

                if (telegramMessage.GiveawayCompleted != null)
                {
                    MapTelegramGiveawayCompletedToMessage(
                        securedObjectSpace,
                        telegramMessage.GiveawayCompleted,
                        out var messageGiveawayCompleted);

                    databaseMessage.GiveawayCompleted = messageGiveawayCompleted;
                }

                if (telegramMessage.GiveawayWinners != null)
                {
                    MapTelegramGiveawayWinnersToMessage(
                        securedObjectSpace,
                        telegramMessage.GiveawayWinners,
                        out var messageGiveawayWinners);

                    databaseMessage.GiveawayWinners = messageGiveawayWinners;
                }

                if (telegramMessage.GroupChatCreated != null)
                {
                    databaseMessage.GroupChatCreated = true;
                }


                databaseMessage.HasMediaSpoiler = telegramMessage.HasMediaSpoiler;
                databaseMessage.HasProtectedContent = telegramMessage.HasProtectedContent;

                if (telegramMessage.Invoice != null)
                {
                    MapTelegramInvoiceToMessage(securedObjectSpace, telegramMessage.Invoice, out var messageInvoice);

                    databaseMessage.Invoice = messageInvoice;
                }

                databaseMessage.IsAutomaticForward = telegramMessage.IsAutomaticForward;
                databaseMessage.IsFromOffline = telegramMessage.IsFromOffline;
                databaseMessage.IsTopicMessage = telegramMessage.IsTopicMessage;

                if (telegramMessage.LeftChatMember != null)
                {
                    MapTelegramUserToMessage(
                        securedObjectSpace,
                        telegramMessage.LeftChatMember,
                        out var messageLeftChatMember);

                    databaseMessage.LeftChatMember = messageLeftChatMember;
                }

                if (telegramMessage.LinkPreviewOptions != null)
                {
                    MapTelegramLinkPreviewOptionsToMessage(
                        securedObjectSpace,
                        telegramMessage.LinkPreviewOptions,
                        out var messageLinkPreviewOptions);

                    databaseMessage.LinkPreviewOptions = messageLinkPreviewOptions;
                }

                if (telegramMessage.Location != null)
                {
                    MapTelegramLocationToMessage(securedObjectSpace, telegramMessage.Location, out var messageLocation);

                    databaseMessage.Location = messageLocation;
                }


                if (telegramMessage.MediaGroupId != null)
                {
                    databaseMessage.MediaGroupId = telegramMessage.MediaGroupId ?? string.Empty;
                }
                if (telegramMessage.MessageAutoDeleteTimerChanged != null)
                {
                    databaseMessage.MessageAutoDeleteTimerChanged = telegramMessage.MessageAutoDeleteTimerChanged.MessageAutoDeleteTime;
                }
                if (telegramMessage.MessageThreadId != null)
                {
                    databaseMessage.MessageThreadId = telegramMessage.MessageThreadId;
                }
                if (telegramMessage.MigrateFromChatId != null)
                {
                    databaseMessage.MigrateFromChatId = telegramMessage.MigrateFromChatId;
                }
                if (telegramMessage.MigrateToChatId != null)
                {
                    databaseMessage.MigrateToChatId = telegramMessage.MigrateToChatId;
                }

                if (telegramMessage.NewChatMembers != null && telegramMessage.NewChatMembers.Length > 0)
                {
                    foreach (var newChatMember in telegramMessage.NewChatMembers)
                    {
                        MapTelegramUserToMessage(securedObjectSpace, newChatMember, out var databaseNewChatMember);

                        if (databaseNewChatMember != null)
                        {
                            databaseMessage.NewChatMembers.Add(databaseNewChatMember);
                        }
                    }
                }

                if (telegramMessage.NewChatPhoto != null && telegramMessage.NewChatPhoto.Length > 0)
                {
                    MapTelegramPhotoSizeArrayToMessage(
                        securedObjectSpace,
                        telegramMessage.NewChatPhoto,
                        out var databaseNewChatPhoto);

                    if (databaseNewChatPhoto != null && databaseMessage.NewChatPhoto != null)
                        foreach (var pho in databaseNewChatPhoto)
                            databaseMessage.NewChatPhoto.Add(pho);
                }

                if (telegramMessage.NewChatTitle != null)
                {
                    databaseMessage.NewChatTitle = telegramMessage.NewChatTitle ?? string.Empty;
                }

                if (telegramMessage.PaidMedia != null)
                {
                    MapTelegramPaidMediaInfoToMessage(
                        securedObjectSpace,
                        telegramMessage.PaidMedia,
                        out var messagePaidMedia);

                    databaseMessage.PaidMedia = messagePaidMedia;
                }


                if (telegramMessage.PaidMessagePriceChanged != null)
                {
                    databaseMessage.PaidMessagePriceChanged = telegramMessage.PaidMessagePriceChanged.PaidMessageStarCount;
                }
                if (telegramMessage.PaidStarCount != null)
                {
                    databaseMessage.PaidStarCount = telegramMessage.PaidStarCount;
                }

                if (telegramMessage.PassportData != null)
                {
                    MapTelegramPassportDataToMessage(
                        securedObjectSpace,
                        telegramMessage.PassportData,
                        out var messagePassportData);

                    databaseMessage.PassportData = messagePassportData;
                }

                if (telegramMessage.Photo != null && telegramMessage.Photo.Length > 0)
                {
                    MapTelegramPhotoSizeArrayToMessage(
                        securedObjectSpace,
                        telegramMessage.Photo,
                        out var databasePhotoSizes);

                    if (databasePhotoSizes != null && databaseMessage.Photo != null)
                        foreach (var pho in databasePhotoSizes)
                            databaseMessage.Photo.Add(pho);
                }

                if (telegramMessage.PinnedMessage != null)
                {
                    var findExistingPinnedMessage = CriteriaOperator.FromLambda<TelegramMessage>(
                        m => m.Message_ID == telegramMessage.PinnedMessage.MessageId);
                    var existingPinnedMessage = securedObjectSpace.FindObject<TelegramMessage>(
                        findExistingPinnedMessage);

                    if (existingPinnedMessage != null)
                    {
                        databaseMessage.PinnedMessage = existingPinnedMessage;
                        logger.LogInformation(
                            $"Pinned PinnedMessage {telegramMessage.PinnedMessage.ToString()} already exists in database");
                    }
                    else
                    {
                        try
                        {
                            MapTelegramMessageToDatabase(
                                telegramMessage.PinnedMessage, securedObjectSpace,
                                out TelegramMessage? pinnedMessage);
                            databaseMessage.PinnedMessage = pinnedMessage;
                            logger.LogInformation(
                                $"Nested PinnedMessage {telegramMessage.PinnedMessage.ToString()} saved to database");
                            NotifyClientsAboutNewMessage(databaseMessage.PinnedMessage).GetAwaiter().GetResult();
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(
                                ex,
                                $"Error while adding nested Pinned TelegramMessage to DB {ex.ToString()}");
                            throw;
                        }

                    }
                }

                if (telegramMessage.Poll != null)
                {
                    MapTelegramPollToMessage(securedObjectSpace, telegramMessage.Poll, out var messagePoll);

                    databaseMessage.Poll = messagePoll;
                }

                if (telegramMessage.ProximityAlertTriggered != null)
                {
                    MapTelegramProximityAlertTriggeredToMessage(
                        securedObjectSpace,
                        telegramMessage.ProximityAlertTriggered,
                        out var messageProximityAlertTriggered);

                    databaseMessage.ProximityAlertTriggered = messageProximityAlertTriggered;
                }

                if (telegramMessage.Quote != null)
                {
                    MapTelegramTextQuoteToMessage(securedObjectSpace, telegramMessage.Quote, out var messageQuote);

                    databaseMessage.Quote = messageQuote;
                }

                if (telegramMessage.RefundedPayment != null)
                {
                    MapTelegramRefundedPaymentToMessage(
                        securedObjectSpace,
                        telegramMessage.RefundedPayment,
                        out var messageRefundedPayment);

                    databaseMessage.RefundedPayment = messageRefundedPayment;
                }

                if (telegramMessage.ReplyMarkup != null)
                {
                    MapTelegramInlineKeyboardMarkupToMessage(
                        securedObjectSpace,
                        telegramMessage.ReplyMarkup,
                        out var messageReplyMarkup);

                    databaseMessage.ReplyMarkup = messageReplyMarkup;
                }

                if (telegramMessage.ReplyToMessage != null)
                {
                    var findExistingReplyToMessageMessage = CriteriaOperator.FromLambda<TelegramMessage>(
                        m => m.Message_ID == telegramMessage.ReplyToMessage.MessageId);
                    var existingReplyToMessage = securedObjectSpace.FindObject<TelegramMessage>(
                        findExistingReplyToMessageMessage);

                    if (existingReplyToMessage != null)
                    {
                        databaseMessage.ReplyToMessage = existingReplyToMessage;
                        logger.LogInformation(
                            $"Pinned ReplyToMessage {telegramMessage.ReplyToMessage.ToString()} already exists in database");
                    }
                    else
                    {
                        try
                        {
                            MapTelegramMessageToDatabase(
                                telegramMessage.ReplyToMessage,
                                securedObjectSpace,
                                out TelegramMessage? replyToMessage);

                            TelegramNestedEntityAssignmentHelper.FixTelegramGraph(replyToMessage, securedObjectSpace, logger);


                            databaseMessage.ReplyToMessage = replyToMessage;
                            logger.LogInformation(
                                $"Nested ReplyToMessage {telegramMessage.ReplyToMessage.ToString()} saved to database");
                            NotifyClientsAboutNewMessage(databaseMessage.ReplyToMessage).GetAwaiter().GetResult();

                        }
                        catch (Exception ex)
                        {
                            logger.LogError(
                                ex,
                                $"Error while adding nested ReplyToMessage TelegramMessage to DB {ex.ToString()}");
                            throw;
                        }
                    }

                }

                if (telegramMessage.ReplyToStory != null)
                {
                    MapTelegramStoryToMessage(
                        securedObjectSpace,
                        telegramMessage.ReplyToStory,
                        out var messageReplyToStory);

                    databaseMessage.ReplyToStory = messageReplyToStory;
                }

                if (telegramMessage.SenderBoostCount != null)
                {
                    databaseMessage.SenderBoostCount = telegramMessage.SenderBoostCount;
                }

                if (telegramMessage.SenderBusinessBot != null)
                {
                    MapTelegramUserToMessage(
                        securedObjectSpace,
                        telegramMessage.SenderBusinessBot,
                        out var messageSenderBusinessBot);

                    databaseMessage.SenderBusinessBot = messageSenderBusinessBot;
                }

                if (telegramMessage.SenderChat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, telegramMessage.SenderChat, out var messageSenderChat);

                    if (messageSenderChat != null)
                        databaseMessage.SenderChat = messageSenderChat;
                }

                databaseMessage.ShowCaptionAboveMedia = telegramMessage.ShowCaptionAboveMedia;

                if (telegramMessage.Sticker != null)
                {
                    MapTelegramStickerToMessage(securedObjectSpace, telegramMessage.Sticker, out var messageSticker);

                    databaseMessage.Sticker = messageSticker;
                }

                if (telegramMessage.Story != null)
                {
                    MapTelegramStoryToMessage(securedObjectSpace, telegramMessage.Story, out var messageStory);

                    databaseMessage.Story = messageStory;
                }

                if (telegramMessage.SuccessfulPayment != null)
                {
                    MapTelegramSuccessfulPaymentToMessage(
                        securedObjectSpace,
                        telegramMessage.SuccessfulPayment,
                        out var messageSuccessfulPayment);

                    databaseMessage.SuccessfulPayment = messageSuccessfulPayment;
                }

                if (telegramMessage.SupergroupChatCreated != null)
                {
                    databaseMessage.SupergroupChatCreated = true;
                }
                if (telegramMessage.Text != null)
                {
                    databaseMessage.Text = telegramMessage.Text ?? string.Empty;
                }

                if (telegramMessage.UniqueGift != null)
                {
                    MapTelegramUniqueGiftInfoToMessage(
                        securedObjectSpace,
                        telegramMessage.UniqueGift,
                        out var messageUniqueGiftInfo);

                    databaseMessage.UniqueGift = messageUniqueGiftInfo;
                }

                if (telegramMessage.UsersShared != null)
                {
                    MapTelegramUsersSharedToMessage(
                        securedObjectSpace,
                        telegramMessage.UsersShared,
                        out var messageUsersShared);

                    databaseMessage.UsersShared = messageUsersShared;
                }

                if (telegramMessage.Venue != null)
                {
                    MapTelegramVenueToMessage(securedObjectSpace, telegramMessage.Venue, out var messageVenue);

                    databaseMessage.Venue = messageVenue;
                }

                if (telegramMessage.ViaBot != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramMessage.ViaBot, out var messageViaBot);

                    databaseMessage.ViaBot = messageViaBot;
                }

                if (telegramMessage.Video != null)
                {
                    MapTelegramVideoToMessage(securedObjectSpace, telegramMessage.Video, out var messageVideo);

                    databaseMessage.Video = messageVideo;
                }


                if (telegramMessage.VideoChatEnded != null)
                {
                    databaseMessage.VideoChatEnded = telegramMessage.VideoChatEnded.Duration;
                }

                if (telegramMessage.VideoChatParticipantsInvited != null)
                {
                    MapTelegramVideoChatParticipantsInvitedToMessage(
                        securedObjectSpace,
                        telegramMessage.VideoChatParticipantsInvited,
                        out var messageVideoChatParticipantsInvited);

                    databaseMessage.VideoChatParticipantsInvited = messageVideoChatParticipantsInvited;
                }

                if (telegramMessage.VideoChatScheduled != null)
                {
                    MapTelegramVideoChatScheduledToMessage(
                        securedObjectSpace,
                        telegramMessage.VideoChatScheduled,
                        out var messageVideoChatScheduled);

                    databaseMessage.VideoChatScheduled = messageVideoChatScheduled;
                }

                if (telegramMessage.VideoChatStarted != null)
                {
                    databaseMessage.VideoChatStarted = true;
                }

                if (telegramMessage.VideoNote != null)
                {
                    MapTelegramVideoNoteToMessage(
                        securedObjectSpace,
                        telegramMessage.VideoNote,
                        out var messageVideoNote);

                    databaseMessage.VideoNote = messageVideoNote;
                }

                if (telegramMessage.Voice != null)
                {
                    MapTelegramVoiceToMessage(securedObjectSpace, telegramMessage.Voice, out var messageVoice);
                    databaseMessage.Voice = messageVoice;
                }

                if (telegramMessage.WebAppData != null)
                {
                    MapTelegramWebAppDataToMessage(
                        securedObjectSpace,
                        telegramMessage.WebAppData,
                        out var messageWebAppData);

                    databaseMessage.WebAppData = messageWebAppData;
                }

                if (telegramMessage.WriteAccessAllowed != null)
                {
                    MapTelegramWriteAccessAllowedToMessage(
                        securedObjectSpace,
                        telegramMessage.WriteAccessAllowed,
                        out var messageWriteAccessAllowed);

                    databaseMessage.WriteAccessAllowed = messageWriteAccessAllowed;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramMessageToDatabase {telegramMessage.ToString()}");
                throw;
            }
        }

        private void MapTelegramOrderInfo(IObjectSpace securedObjectSpace, OrderInfo orderInfo, out TelegramOrderInfo? databaseOrderInfo)
        {
            try
            {
                databaseOrderInfo = securedObjectSpace.CreateObject<TelegramOrderInfo>();
                if (orderInfo.Email != null)
                    databaseOrderInfo.Email = orderInfo.Email;
                if (orderInfo.Name != null)
                    databaseOrderInfo.Name = orderInfo.Name;
                if (orderInfo.PhoneNumber != null)
                    databaseOrderInfo.PhoneNumber = orderInfo.PhoneNumber;
                if (orderInfo.ShippingAddress != null)
                {
                    MapTelegramShippingAddressToMessage(securedObjectSpace, orderInfo.ShippingAddress, out TelegramShippingAddress? databaseShippingAddress);
                    if (databaseShippingAddress != null)
                        databaseOrderInfo.ShippingAddress = databaseShippingAddress;

                }

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MapTelegramOrderInfo {orderInfo.ToString()}");
                throw;
            }
        }

        private void MapTelegramOrderInfoToMessage(IObjectSpace securedObjectSpace, OrderInfo orderInfo, out TelegramOrderInfo? databaseOrderInfo)
        {
            try
            {
                databaseOrderInfo = securedObjectSpace.CreateObject<TelegramOrderInfo>();
                if (orderInfo.Name != null)
                {
                    databaseOrderInfo.Name = orderInfo.Name ?? string.Empty;
                }
                if (orderInfo.PhoneNumber != null)
                {
                    databaseOrderInfo.PhoneNumber = orderInfo.PhoneNumber ?? string.Empty;
                }
                if (orderInfo.Email != null)
                {
                    databaseOrderInfo.Email = orderInfo.Email ?? string.Empty;
                }
                if (orderInfo.ShippingAddress != null)
                {
                    MapTelegramShippingAddressToMessage(securedObjectSpace, orderInfo.ShippingAddress, out var databaseShippingAddress);
                    if (databaseShippingAddress != null)
                        databaseOrderInfo.ShippingAddress = databaseShippingAddress;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramOrderInfoToMessage {orderInfo.ToString()}");
                throw;
            }
        }
        private void MapTelegramPaidMediaInfoToMessage(IObjectSpace securedObjectSpace, PaidMediaInfo paidMediaInfo, out TelegramPaidMediaInfo? databasePaidMediaInfo)
        {
            try
            {
                databasePaidMediaInfo = securedObjectSpace.CreateObject<TelegramPaidMediaInfo>();
                databasePaidMediaInfo.StarCount = paidMediaInfo.StarCount;
                if (paidMediaInfo.PaidMedia != null && paidMediaInfo.PaidMedia.LongLength > 0)
                {
                    foreach (var paidMedia in paidMediaInfo.PaidMedia)
                    {
                        MapTelegramPaidMediaToMessage(securedObjectSpace, paidMedia, out var databasePaidMedia);
                        if (databasePaidMedia != null)
                        {
                            databasePaidMediaInfo.PaidMedia.Add(databasePaidMedia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPaidMediaInfoToMessage {paidMediaInfo.ToString()}");
                throw;
            }
        }

        private void MaptelegramPaidMediaPurchased(IObjectSpace securedObjectSpace, PaidMediaPurchased telegramPurchasedPaidMedia, out TelegramPaidMediaPurchased? finaltelegramPurchasedPaidMedia)
        {
            try
            {
                finaltelegramPurchasedPaidMedia = securedObjectSpace.CreateObject<TelegramPaidMediaPurchased>();
                if (telegramPurchasedPaidMedia.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramPurchasedPaidMedia.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramPurchasedPaidMedia.From = databaseUser;
                    }

                }
                finaltelegramPurchasedPaidMedia.PaidMediaPayload = telegramPurchasedPaidMedia.PaidMediaPayload;
                finaltelegramPurchasedPaidMedia.UpdateType = UpdateType.PurchasedPaidMedia;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramPaidMediaPurchased {telegramPurchasedPaidMedia.ToString()}");
                throw;
            }
        }
        private void MapTelegramPaidMediaToMessage(IObjectSpace securedObjectSpace, PaidMedia paidMedia, out TelegramPaidMedia? databasePaidMedia)
        {
            try
            {
                databasePaidMedia = securedObjectSpace.CreateObject<TelegramPaidMedia>();

                switch (paidMedia.Type)
                {
                    case PaidMediaType.Preview:
                        if (paidMedia is PaidMediaPreview paidMediaPreview)
                        {
                            var databasePaidMediaPreview = securedObjectSpace.CreateObject<TelegramPaidMediaPreview>(
                                );
                            databasePaidMediaPreview.Duration = paidMediaPreview.Duration;
                            databasePaidMediaPreview.Height = paidMediaPreview.Height;
                            databasePaidMediaPreview.Width = paidMediaPreview.Width;
                            databasePaidMedia = databasePaidMediaPreview;
                        }
                        else
                            databasePaidMedia = null;
                        break;
                    case PaidMediaType.Photo:
                        if (paidMedia is PaidMediaPhoto paidMediaPhoto)
                        {
                            var databasePaidMediaPhoto = securedObjectSpace.CreateObject<TelegramPaidMediaPhoto>(
                                );
                            if (databasePaidMediaPhoto.Photo != null && paidMediaPhoto.Photo.Length > 0)
                            {
                                MapTelegramPhotoSizeArrayToMessage(securedObjectSpace, paidMediaPhoto.Photo, out var databasePhotoSizes);
                                if (databasePhotoSizes != null)
                                    foreach (var entry in databasePhotoSizes)
                                        databasePaidMediaPhoto.Photo.Add(entry);
                            }
                            databasePaidMedia = databasePaidMediaPhoto;
                        }
                        else
                            databasePaidMedia = null;
                        break;
                    case PaidMediaType.Video:
                        if (paidMedia is PaidMediaVideo paidMediaVideo)
                        {
                            var databasePaidMediaVideo = securedObjectSpace.CreateObject<TelegramPaidMediaVideo>(
                            );
                            if (paidMediaVideo.Video != null)
                            {
                                MapTelegramVideoToDatabase(securedObjectSpace, paidMediaVideo.Video, out var databaseVideo);
                                if (databaseVideo != null)
                                    databasePaidMediaVideo.Video = databaseVideo;
                            }
                            databasePaidMedia = databasePaidMediaVideo;
                        }
                        else
                            databasePaidMedia = null;
                        break;
                    default:
                        databasePaidMedia = null;
                        break;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPaidMediaToMessage {paidMedia.ToString()}");
                throw;
            }
        }
        private void MapTelegramPassportDataElementToMessage(IObjectSpace securedObjectSpace, EncryptedPassportElement encryptedPassportElement, out TelegramEncryptedPassportElement? databaseEncryptedPassportElement)
        {
            try
            {
                databaseEncryptedPassportElement = securedObjectSpace.CreateObject<TelegramEncryptedPassportElement>();
                databaseEncryptedPassportElement.Type = encryptedPassportElement.Type;
                databaseEncryptedPassportElement.Data = encryptedPassportElement.Data ?? string.Empty;
                databaseEncryptedPassportElement.PhoneNumber = encryptedPassportElement.PhoneNumber ?? string.Empty;
                databaseEncryptedPassportElement.Email = encryptedPassportElement.Email ?? string.Empty;
                if (encryptedPassportElement.Files != null && encryptedPassportElement.Files.LongLength > 0)
                {
                    foreach (var file in encryptedPassportElement.Files)
                    {
                        MapTelegramPassportFileToMessage(securedObjectSpace, file, out var databaseTGFilePassportFile);
                        if (databaseTGFilePassportFile != null)
                        {
                            databaseEncryptedPassportElement.Files.Add(databaseTGFilePassportFile);
                        }
                    }
                }
                databaseEncryptedPassportElement.Hash = encryptedPassportElement.Hash ?? string.Empty;
                if (encryptedPassportElement.ReverseSide != null)
                {
                    MapTelegramPassportFileToMessage(securedObjectSpace, encryptedPassportElement.ReverseSide, out var databaseTGFileReverseSide);
                    if (databaseTGFileReverseSide != null)
                        databaseEncryptedPassportElement.ReverseSide = databaseTGFileReverseSide;
                }
                if (encryptedPassportElement.Selfie != null)
                {
                    MapTelegramPassportFileToMessage(securedObjectSpace, encryptedPassportElement.Selfie, out var databaseTGFileSelfie);
                    if (databaseTGFileSelfie != null)
                        databaseEncryptedPassportElement.Selfie = databaseTGFileSelfie;
                }
                if (encryptedPassportElement.Translation != null && encryptedPassportElement.Translation.LongLength > 0)
                {
                    foreach (var translationFile in encryptedPassportElement.Translation)
                    {
                        MapTelegramPassportFileToMessage(securedObjectSpace, translationFile, out var databaseTGFileTranslation);
                        if (databaseTGFileTranslation != null)
                        {
                            databaseEncryptedPassportElement.Translation.Add(databaseTGFileTranslation);
                        }
                    }
                }
                if (encryptedPassportElement.FrontSide != null)
                {
                    MapTelegramPassportFileToMessage(securedObjectSpace, encryptedPassportElement.FrontSide, out var databaseTGFileFrontSide);
                    if (databaseTGFileFrontSide != null)
                        databaseEncryptedPassportElement.FrontSide = databaseTGFileFrontSide;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPassportDataElementToMessage {encryptedPassportElement.ToString()}");
                throw;
            }
        }
        private void MapTelegramPassportDataToMessage(IObjectSpace securedObjectSpace, PassportData passportData, out TelegramPassportData? databasePassportData)
        {
            try
            {
                databasePassportData = securedObjectSpace.CreateObject<TelegramPassportData>();
                if (passportData.Data != null && passportData.Data.LongLength > 0)
                {
                    var listPassPortDataElement = new ObservableCollection<TelegramEncryptedPassportElement>();
                    foreach (var passportDataElement in passportData.Data)
                    {
                        MapTelegramPassportDataElementToMessage(securedObjectSpace, passportDataElement, out TelegramEncryptedPassportElement? databasePassportDataElement);
                        if (databasePassportDataElement != null)
                        {
                            listPassPortDataElement.Add(databasePassportDataElement);
                        }
                    }
                }
                if (passportData.Credentials != null)
                {
                    MapTelegramEncryptedCredentialsToMessage(securedObjectSpace, passportData.Credentials, out var databasePassportCredentials);
                    if (databasePassportCredentials != null)
                        databasePassportData.Credentials = databasePassportCredentials;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPassportDataToMessage {passportData.ToString()}");
                throw;
            }
        }
        private void MapTelegramPassportFileToMessage(IObjectSpace securedObjectSpace, PassportFile passportFile, out TelegramPassportFile? databasePassportFile)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(passportFile.FileUniqueId);
                CriteriaOperator passportFileCriteria = CriteriaOperator.FromLambda<TelegramPassportFile>(
                    pf => pf.FileUniqueId == passportFile.FileUniqueId);
                databasePassportFile = securedObjectSpace.FindObject<TelegramPassportFile>(
                    passportFileCriteria);
                if (databasePassportFile == null)
                {
                    databasePassportFile = securedObjectSpace.CreateObject<TelegramPassportFile>(
                        );

                }
                databasePassportFile.FileId = passportFile.FileId;
                databasePassportFile.FileUniqueId = passportFile.FileUniqueId;
                databasePassportFile.FileSize = passportFile.FileSize;
                databasePassportFile.FileDate = passportFile.FileDate;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPassportDataElementToMessage {passportFile.ToString()}");
                throw;
            }
        }

        private void MapTelegramPhotoSizeArrayToMessage(IObjectSpace securedObjectSpace, PhotoSize[] photoSizes, out ObservableCollection<TelegramPhotoSize>? databasePhotoSizes)
        {
            try
            {
                databasePhotoSizes = new();

                foreach (var photo in photoSizes)
                {
                    MapTelegramPhotoSizeToMessage(securedObjectSpace, photo, out var databasePhotoSize);
                    if (databasePhotoSize != null)
                    {
                        databasePhotoSizes.Add(databasePhotoSize);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPhotoSizeArrayToMessage {photoSizes.ToString()}");
                throw;
            }
        }

        private void MapTelegramPhotoSizeToMessage(IObjectSpace securedObjectSpace, PhotoSize photo, out TelegramPhotoSize? databasePhotoSize)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(photo.FileUniqueId);
                CriteriaOperator photoCriteria = CriteriaOperator.FromLambda<TelegramPhotoSize>(
                    p => p.FileUniqueId == photo.FileUniqueId);
                databasePhotoSize = securedObjectSpace.FindObject<TelegramPhotoSize>(
                    photoCriteria);
                if (databasePhotoSize == null)
                {
                    databasePhotoSize = securedObjectSpace.CreateObject<TelegramPhotoSize>(
                        );
                    databasePhotoSize.FileId = photo.FileId;
                    databasePhotoSize.FileUniqueId = photo.FileUniqueId;
                    databasePhotoSize.Width = photo.Width;
                    databasePhotoSize.Height = photo.Height;
                    databasePhotoSize.FileSize = photo.FileSize;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPhotoSizeToMessage {photo.ToString()}");
                throw;
            }
        }
        private void MapTelegramPollAnswerToDatabase(PollAnswer telegramPollAnswer, TelegramPollAnswer databasePollAnswer, IObjectSpace securedObjectSpace, SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider)
        {
            try
            {
                if (telegramPollAnswer != null)
                {
                    MapTelegramPollAnswerToMessage(
                        securedObjectSpace,
                        telegramPollAnswer,
                        out var createdDatabasePollAnswer);
                    if (createdDatabasePollAnswer != null)
                    {
                        databasePollAnswer = createdDatabasePollAnswer;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPollAnswerToDatabase {telegramPollAnswer.ToString()}");
            }
        }

        private void MapTelegramPollAnswerToMessage(IObjectSpace securedObjectSpace, PollAnswer telegramPollAnswer, out TelegramPollAnswer? createdDatabasePollAnswer)
        {
            try
            {
                createdDatabasePollAnswer = securedObjectSpace.CreateObject<TelegramPollAnswer>();
                CriteriaOperator pollCriteria = CriteriaOperator.FromLambda<TelegramPoll>(
                    p => p.PollId == telegramPollAnswer.PollId);
                var databasePoll = securedObjectSpace.FindObject<TelegramPoll>(pollCriteria);
                if (databasePoll != null)
                    createdDatabasePollAnswer.Poll = databasePoll;
                if (telegramPollAnswer.User != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramPollAnswer.User, out var user);
                    if (user != null)
                        createdDatabasePollAnswer.User = user;
                }
                if (telegramPollAnswer.VoterChat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, telegramPollAnswer.VoterChat, out var chat);
                    if (chat != null)
                        createdDatabasePollAnswer.VoterChat = chat;
                }
                if (telegramPollAnswer.OptionIds != null && telegramPollAnswer.OptionIds.LongLength > 0)
                {
                    foreach (var optId in telegramPollAnswer.OptionIds)
                        createdDatabasePollAnswer.OptionIds.Add(optId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPollAnswerToMessage {telegramPollAnswer.ToString()}");
                createdDatabasePollAnswer = null;
            }
        }
        private void MapTelegramPollOptionToMessage(IObjectSpace securedObjectSpace, PollOption pollOption, out TelegramPollOption? databasePollOption)
        {
            try
            {
                databasePollOption = securedObjectSpace.CreateObject<TelegramPollOption>();
                databasePollOption.Text = pollOption.Text ?? string.Empty;
                if (pollOption.TextEntities != null && pollOption.TextEntities.Length > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        pollOption.TextEntities,
                        out var databaseMessageEntities);
                    if (databaseMessageEntities != null)
                        foreach (var entry in databaseMessageEntities)
                            databasePollOption.TextEntities.Add(entry);
                }
                databasePollOption.VoterCount = pollOption.VoterCount;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPollOptionToMessage {pollOption.ToString()}");
                throw;
            }
        }
        private void MapTelegramPollToDatabase(Poll telegramPoll, TelegramPoll databasePoll, IObjectSpace securedObjectSpace, SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider)
        {
            try
            {
                if (telegramPoll != null)
                {
                    MapTelegramPollToMessage(securedObjectSpace, telegramPoll, out var createdDatabasePoll);
                    if (createdDatabasePoll != null)
                    {
                        databasePoll = createdDatabasePoll;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPollToDatabase {telegramPoll.ToString()}");
            }
        }
        private void MapTelegramPollToMessage(IObjectSpace securedObjectSpace, Poll poll, out TelegramPoll? databasePoll)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(poll.Id);
                CriteriaOperator pollCriterio = CriteriaOperator.FromLambda<TelegramPoll>(
                    p => p.PollId == poll.Id);
                databasePoll = securedObjectSpace.FindObject<TelegramPoll>(
                    pollCriterio);
                if (databasePoll == null)
                {
                    databasePoll = securedObjectSpace.CreateObject<TelegramPoll>(
                        );

                }
                databasePoll.PollId = poll.Id;
                databasePoll.Question = poll.Question ?? string.Empty;
                databasePoll.IsClosed = poll.IsClosed;
                databasePoll.IsAnonymous = poll.IsAnonymous;
                databasePoll.AllowsMultipleAnswers = poll.AllowsMultipleAnswers;
                databasePoll.PollType = poll.Type;
                if (poll.Options != null && poll.Options.LongLength > 0)
                {
                    var listOptions = new ObservableCollection<TelegramPollOption>();
                    foreach (var option in poll.Options)
                    {
                        MapTelegramPollOptionToMessage(securedObjectSpace, option, out var databaseOption);
                        if (databaseOption != null)
                        {
                            listOptions.Add(databaseOption);
                        }
                    }
                    foreach (var opt in listOptions)
                    {

                        databasePoll.Options.Add(opt);
                    }
                }
                databasePoll.TotalVoterCount = poll.TotalVoterCount;
                if (poll.Explanation != null)
                {
                    databasePoll.Explanation = poll.Explanation ?? string.Empty;
                }
                if (poll.ExplanationEntities != null && poll.ExplanationEntities.LongLength > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        poll.ExplanationEntities,
                        out var databaseMessageEntities);
                    if (databaseMessageEntities != null)
                        foreach (var ent in databaseMessageEntities)
                            databasePoll.ExplanationEntities.Add(ent);
                }
                databasePoll.OpenPeriod = poll.OpenPeriod;
                databasePoll.CorrectOptionId = poll.CorrectOptionId;
                if (poll.Question != null)
                    databasePoll.Question = poll.Question;
                if (poll.QuestionEntities != null && poll.QuestionEntities.LongLength > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        poll.QuestionEntities,
                        out var databaseMessageEntities);
                    if (databaseMessageEntities != null)
                        foreach (var ent in databaseMessageEntities)
                            databasePoll.QuestionEntities.Add(ent);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPollToMessage {poll.ToString()}");
                throw;
            }
        }

        private void MaptelegramPreCheckoutQuery(IObjectSpace securedObjectSpace, PreCheckoutQuery telegramPreCheckoutQuery, out TelegramPreCheckoutQuery? finaltelegramPreCheckoutQuery)
        {
            try
            {
                finaltelegramPreCheckoutQuery = securedObjectSpace.CreateObject<TelegramPreCheckoutQuery>();
                if (telegramPreCheckoutQuery.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramPreCheckoutQuery.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramPreCheckoutQuery.From = databaseUser;
                    }

                }
                finaltelegramPreCheckoutQuery.Currency = telegramPreCheckoutQuery.Currency;
                finaltelegramPreCheckoutQuery.InvoicePayload = telegramPreCheckoutQuery.InvoicePayload;
                if (telegramPreCheckoutQuery.OrderInfo != null)
                {
                    MapTelegramOrderInfo(securedObjectSpace, telegramPreCheckoutQuery.OrderInfo, out TelegramOrderInfo? databaseOrderInfo);
                    if (databaseOrderInfo != null)
                        finaltelegramPreCheckoutQuery.OrderInfo = databaseOrderInfo;
                }
                if (telegramPreCheckoutQuery.ShippingOptionId != null)
                    finaltelegramPreCheckoutQuery.ShippingOptionId = telegramPreCheckoutQuery.ShippingOptionId;
                finaltelegramPreCheckoutQuery.UpdateType = UpdateType.PreCheckoutQuery;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramPreCheckoutQuery {telegramPreCheckoutQuery.ToString()}");
                throw;
            }
        }
        private void MapTelegramProximityAlertTriggeredToMessage(IObjectSpace securedObjectSpace, ProximityAlertTriggered proximityAlertTriggered, out TelegramProximityAlertTriggered? databaseProximityAlertTriggered)
        {
            try
            {
                databaseProximityAlertTriggered = securedObjectSpace.CreateObject<TelegramProximityAlertTriggered>();
                if (proximityAlertTriggered.Traveler != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, proximityAlertTriggered.Traveler, out var databaseUser);
                    if (databaseUser != null)
                        databaseProximityAlertTriggered.Traveler = databaseUser;
                }
                databaseProximityAlertTriggered.Distance = proximityAlertTriggered.Distance;

                if (proximityAlertTriggered.Traveler != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, proximityAlertTriggered.Traveler, out var databaseUser);
                    if (databaseUser != null)
                        databaseProximityAlertTriggered.Traveler = databaseUser;
                }
                if (proximityAlertTriggered.Watcher != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, proximityAlertTriggered.Watcher, out var databaseUser);
                    if (databaseUser != null)
                        databaseProximityAlertTriggered.Watcher = databaseUser;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramProximityAlertTriggeredToMessage {proximityAlertTriggered.ToString()}");
                throw;
            }
        }
        private void MapTelegramRefundedPaymentToMessage(IObjectSpace securedObjectSpace, RefundedPayment refundedPayment, out TelegramRefundedPayment? databaseRefundedPayment)
        {
            try
            {
                databaseRefundedPayment = securedObjectSpace.CreateObject<TelegramRefundedPayment>();
                if (refundedPayment.Currency != null)
                {
                    databaseRefundedPayment.Currency = refundedPayment.Currency ?? string.Empty;
                }
                databaseRefundedPayment.TotalAmount = refundedPayment.TotalAmount;
                if (refundedPayment.InvoicePayload != null)
                {
                    databaseRefundedPayment.InvoicePayload = refundedPayment.InvoicePayload ?? string.Empty;
                }
                if (refundedPayment.ProviderPaymentChargeId != null)
                {
                    databaseRefundedPayment.ProviderPaymentChargeId = refundedPayment.ProviderPaymentChargeId ?? string.Empty;
                }
                if (refundedPayment.TelegramPaymentChargeId != null)
                {
                    databaseRefundedPayment.TelegramPaymentChargeId = refundedPayment.TelegramPaymentChargeId ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramRefundedPaymentToMessage {refundedPayment.ToString()}");
                throw;
            }
        }
        private void MapTelegramSharedUserToMessage(IObjectSpace securedObjectSpace, SharedUser user, out TelegramSharedUser? databaseSharedUser)
        {
            try
            {
                databaseSharedUser = securedObjectSpace.CreateObject<TelegramSharedUser>();
                if (user.FirstName != null)
                {
                    databaseSharedUser.FirstName = user.FirstName ?? string.Empty;
                }
                if (user.LastName != null)
                {
                    databaseSharedUser.LastName = user.LastName ?? string.Empty;
                }
                if (user.Photo != null)
                {
                    MapTelegramPhotoSizeArrayToMessage(securedObjectSpace, user.Photo, out var databasePhoto);
                    if (databasePhoto != null)
                        foreach (var entry in databasePhoto)
                            databaseSharedUser.Photo.Add(entry);
                }
                databaseSharedUser.UserId = user.UserId;
                if (user.Username != null)
                {
                    databaseSharedUser.Username = user.Username ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramSharedUserToMessage {user.ToString()}");
                throw;
            }
        }

        private void MapTelegramShippingAddressToMessage(IObjectSpace securedObjectSpace, ShippingAddress shippingAddress, out TelegramShippingAddress? databaseShippingAddress)
        {
            try
            {
                databaseShippingAddress = securedObjectSpace.CreateObject<TelegramShippingAddress>();
                if (shippingAddress.CountryCode != null)
                {
                    databaseShippingAddress.CountryCode = shippingAddress.CountryCode ?? string.Empty;
                }
                if (shippingAddress.State != null)
                {
                    databaseShippingAddress.State = shippingAddress.State ?? string.Empty;
                }
                if (shippingAddress.City != null)
                {
                    databaseShippingAddress.City = shippingAddress.City ?? string.Empty;
                }
                if (shippingAddress.StreetLine1 != null)
                {
                    databaseShippingAddress.StreetLine1 = shippingAddress.StreetLine1 ?? string.Empty;
                }
                if (shippingAddress.StreetLine2 != null)
                {
                    databaseShippingAddress.StreetLine2 = shippingAddress.StreetLine2 ?? string.Empty;
                }
                if (shippingAddress.PostCode != null)
                {
                    databaseShippingAddress.PostCode = shippingAddress.PostCode ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramShippingAddressToMessage {shippingAddress.ToString()}");
                throw;
            }
        }

        private void MaptelegramShippingQuery(IObjectSpace securedObjectSpace, ShippingQuery telegramShippingQuery, out TelegramShippingQuery? finaltelegramShippingQuery)
        {
            try
            {
                finaltelegramShippingQuery = securedObjectSpace.CreateObject<TelegramShippingQuery>();
                if (telegramShippingQuery.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramShippingQuery.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramShippingQuery.From = databaseUser;
                    }

                }
                finaltelegramShippingQuery.InvoicePayload = telegramShippingQuery.InvoicePayload;
                MapTelegramShippingAddressToMessage(securedObjectSpace, telegramShippingQuery.ShippingAddress, out TelegramShippingAddress? databaseAddress);
                if (databaseAddress != null)
                    finaltelegramShippingQuery.ShippingAddress = databaseAddress;
                finaltelegramShippingQuery.UpdateType = UpdateType.PreCheckoutQuery;
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramShippingQuery {telegramShippingQuery.ToString()}");
                throw;
            }
        }

        private void MapTelegramStickerToMessage(IObjectSpace securedObjectSpace, Sticker sticker, out TelegramSticker? databaseSticker)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(sticker.FileUniqueId);
                CriteriaOperator stickerCriteria = CriteriaOperator.FromLambda<TelegramSticker>(
                    s => s.FileUniqueId == sticker.FileUniqueId);
                databaseSticker = securedObjectSpace.FindObject<TelegramSticker>(stickerCriteria);
                if (databaseSticker == null)
                {
                    databaseSticker = securedObjectSpace.CreateObject<TelegramSticker>(
                        );

                }
                databaseSticker.FileId = sticker.FileId;
                databaseSticker.FileUniqueId = sticker.FileUniqueId;
                databaseSticker.CustomEmojiId = sticker.CustomEmojiId ?? string.Empty;
                databaseSticker.Emoji = sticker.Emoji ?? string.Empty;
                databaseSticker.Width = sticker.Width;
                databaseSticker.Height = sticker.Height;
                databaseSticker.FileSize = sticker.FileSize;
                databaseSticker.IsAnimated = sticker.IsAnimated;
                databaseSticker.IsVideo = sticker.IsVideo;
                if (sticker.Thumbnail != null)
                {
                    MapTelegramPhotoSizeToMessage(securedObjectSpace, sticker.Thumbnail, out var databaseThumbnail);
                    if (databaseThumbnail != null)
                        databaseSticker.Thumbnail = databaseThumbnail;
                }
                if (sticker.MaskPosition != null)
                {
                    MapTelegramMaskPositionToMessage(securedObjectSpace, sticker.MaskPosition, out var databaseMaskPosition);
                    if (databaseMaskPosition != null)
                        databaseSticker.MaskPosition = databaseMaskPosition;
                }
                if (sticker.SetName != null)
                    databaseSticker.SetName = sticker.SetName;
                databaseSticker.NeedsRepainting = sticker.NeedsRepainting;
                if (sticker.PremiumAnimation != null)
                {
                    MapTelegramTGFileToMessage(securedObjectSpace, sticker.PremiumAnimation, out var databasePremiumAnimation);
                    if (databasePremiumAnimation != null)
                        databaseSticker.PremiumAnimation = databasePremiumAnimation;
                }
                databaseSticker.Type = sticker.Type;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramStickerToMessage {sticker.ToString()}");
                throw;
            }
        }
        private void MapTelegramStoryToMessage(IObjectSpace securedObjectSpace, Story story, out TelegramStory? databaseStory)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(story.Chat.Id);
                ArgumentOutOfRangeException.ThrowIfZero(story.Chat.Id);
                ArgumentNullException.ThrowIfNull(story.Id);
                ArgumentOutOfRangeException.ThrowIfZero((long)story.Id);
                CriteriaOperator storyCriteria = CriteriaOperator.FromLambda<TelegramStory>(
                    s => s.StoryId == story.Id && s.Chat.ChatId == story.Chat.Id);
                databaseStory = securedObjectSpace.FindObject<TelegramStory>(
                    storyCriteria);
                if (databaseStory == null)
                {
                    databaseStory = securedObjectSpace.CreateObject<TelegramStory>();


                }
                databaseStory.StoryId = story.Id;

                if (story.Chat != null)
                {
                    MapTelegramChatToMessage(securedObjectSpace, story.Chat, out var chat);
                    if (chat != null)
                        databaseStory.Chat = chat;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramStoryToMessage {story.ToString()}");
                throw;
            }
        }
        private void MapTelegramSuccessfulPaymentToMessage(IObjectSpace securedObjectSpace, SuccessfulPayment successfulPayment, out TelegramSuccessfulPayment? databaseSuccessfulPayment)
        {
            try
            {
                databaseSuccessfulPayment = securedObjectSpace.CreateObject<TelegramSuccessfulPayment>();
                if (successfulPayment.Currency != null)
                {
                    databaseSuccessfulPayment.Currency = successfulPayment.Currency ?? string.Empty;
                }
                databaseSuccessfulPayment.TotalAmount = successfulPayment.TotalAmount;
                if (successfulPayment.InvoicePayload != null)
                {
                    databaseSuccessfulPayment.InvoicePayload = successfulPayment.InvoicePayload ?? string.Empty;
                }
                if (successfulPayment.TelegramPaymentChargeId != null)
                {
                    databaseSuccessfulPayment.TelegramPaymentChargeId = successfulPayment.TelegramPaymentChargeId ?? string.Empty;
                }
                if (successfulPayment.IsFirstRecurring)
                {
                    databaseSuccessfulPayment.IsFirstRecurring = successfulPayment.IsFirstRecurring;
                }
                if (successfulPayment.IsRecurring)
                {
                    databaseSuccessfulPayment.IsRecurring = successfulPayment.IsRecurring;
                }
                if (successfulPayment.OrderInfo != null)
                {
                    MapTelegramOrderInfoToMessage(securedObjectSpace, successfulPayment.OrderInfo, out var databaseOrderInfo);
                    if (databaseOrderInfo != null)
                        databaseSuccessfulPayment.OrderInfo = databaseOrderInfo;
                }
                if (successfulPayment.ProviderPaymentChargeId != null)
                {
                    databaseSuccessfulPayment.ProviderPaymentChargeId = successfulPayment.ProviderPaymentChargeId ?? string.Empty;
                }
                if (successfulPayment.ShippingOptionId != null)
                {
                    databaseSuccessfulPayment.ShippingOptionId = successfulPayment.ShippingOptionId ?? string.Empty;
                }
                if (successfulPayment.SubscriptionExpirationDate != null)
                {
                    databaseSuccessfulPayment.SubscriptionExpirationDate = successfulPayment.SubscriptionExpirationDate;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramSuccessfulPaymentToMessage {successfulPayment.ToString()}");
                throw;
            }
        }
        private void MapTelegramSwitchInlineQueryChosenChatToMessage(IObjectSpace securedObjectSpace, SwitchInlineQueryChosenChat switchInlineQueryChosenChat, out TelegramSwitchInlineQueryChosenChat? databaseSwitchInlineQueryChosenChat)
        {
            try
            {
                databaseSwitchInlineQueryChosenChat = securedObjectSpace.CreateObject<TelegramSwitchInlineQueryChosenChat>();
                databaseSwitchInlineQueryChosenChat.Query = switchInlineQueryChosenChat.Query ?? string.Empty;
                databaseSwitchInlineQueryChosenChat.AllowUserChats = switchInlineQueryChosenChat.AllowUserChats;
                databaseSwitchInlineQueryChosenChat.AllowBotChats = switchInlineQueryChosenChat.AllowBotChats;
                databaseSwitchInlineQueryChosenChat.AllowGroupChats = switchInlineQueryChosenChat.AllowGroupChats;
                databaseSwitchInlineQueryChosenChat.AllowChannelChats = switchInlineQueryChosenChat.AllowChannelChats;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramSwitchInlineQueryChosenChatToMessage {switchInlineQueryChosenChat.ToString()}");
                throw;
            }
        }

        private void MaptelegramtelegramInlineQueries(IObjectSpace securedObjectSpace, InlineQuery telegramInlineQueries, out TelegramInlineQuery? finaltelegramInlineQueries)
        {
            try
            {
                finaltelegramInlineQueries = securedObjectSpace.CreateObject<TelegramInlineQuery>();
                finaltelegramInlineQueries.InlineQueryId = telegramInlineQueries.Id;
                finaltelegramInlineQueries.ChatType = telegramInlineQueries.ChatType;

                if (telegramInlineQueries.From != null)
                {
                    MapTelegramUserToMessage(securedObjectSpace, telegramInlineQueries.From, out TelegramUser? databaseUser);
                    if (databaseUser != null)
                    {
                        finaltelegramInlineQueries.From = databaseUser;
                    }

                }
                if (telegramInlineQueries.Location != null)
                {
                    MapTelegramLocationToMessage(securedObjectSpace, telegramInlineQueries.Location, out TelegramLocation? telegramLocation);
                    if (telegramLocation != null)
                        finaltelegramInlineQueries.Location = telegramLocation;
                }
                finaltelegramInlineQueries.Offset = telegramInlineQueries.Offset;
                finaltelegramInlineQueries.Query = telegramInlineQueries.Query;
                finaltelegramInlineQueries.UpdateType = UpdateType.InlineQuery;

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error in MaptelegramtelegramInlineQueries {telegramInlineQueries.ToString()}");
                throw;
            }
        }
        private void MapTelegramTextQuoteToMessage(IObjectSpace securedObjectSpace, TextQuote textQuote, out TelegramTextQuote? databaseTextQuote)
        {
            try
            {
                databaseTextQuote = securedObjectSpace.CreateObject<TelegramTextQuote>();
                databaseTextQuote.Text = textQuote.Text ?? string.Empty;
                if (textQuote.Entities != null && textQuote.Entities.LongLength > 0)
                {
                    MapTelegramMessageEntitiesToMessage(
                        securedObjectSpace,
                        textQuote.Entities,
                        out var databaseMessageEntities);
                    if (databaseMessageEntities != null)
                        foreach (var entry in databaseMessageEntities)
                            databaseTextQuote.Entities.Add(entry);
                }
                databaseTextQuote.IsManual = textQuote.IsManual;
                databaseTextQuote.Position = textQuote.Position;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramTextQuoteToMessage {textQuote.ToString()}");
                throw;
            }
        }
        private void MapTelegramTGFileToMessage(IObjectSpace securedObjectSpace, TGFile tgFile, out TelegramTGFile? databaseTGFile)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(tgFile.FileUniqueId);
                CriteriaOperator tgFileCriteria = CriteriaOperator.FromLambda<TelegramTGFile>(
                 s => s.FileUniqueId == tgFile.FileUniqueId);
                databaseTGFile = securedObjectSpace.FindObject<TelegramTGFile>(tgFileCriteria);
                if (databaseTGFile == null)
                {
                    databaseTGFile = securedObjectSpace.CreateObject<TelegramTGFile>(
                        );


                }
                databaseTGFile.FileId = tgFile.FileId;
                databaseTGFile.FileUniqueId = tgFile.FileUniqueId;
                databaseTGFile.FilePath = tgFile.FilePath ?? string.Empty;
                databaseTGFile.FileSize = tgFile.FileSize;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramTGFileToMessage {tgFile.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftBackdropColorsToMessage(IObjectSpace securedObjectSpace, UniqueGiftBackdropColors uniqueGiftBackdropColors, out TelegramUniqueGiftBackdropColors? databaseUniqueGiftBackdropColors)
        {
            try
            {
                CriteriaOperator uniqueGiftBackdropCriteria = CriteriaOperator.FromLambda<TelegramUniqueGiftBackdropColors>(
                    c => c.CenterColor == uniqueGiftBackdropColors.CenterColor &&
                         c.EdgeColor == uniqueGiftBackdropColors.EdgeColor &&
                         c.SymbolColor == uniqueGiftBackdropColors.SymbolColor &&
                         c.TextColor == uniqueGiftBackdropColors.TextColor);
                databaseUniqueGiftBackdropColors = securedObjectSpace.FindObject<TelegramUniqueGiftBackdropColors>(
                    uniqueGiftBackdropCriteria);
                if (databaseUniqueGiftBackdropColors == null)
                {
                    databaseUniqueGiftBackdropColors = securedObjectSpace.CreateObject<TelegramUniqueGiftBackdropColors>();

                }
                databaseUniqueGiftBackdropColors.CenterColor = uniqueGiftBackdropColors.CenterColor;
                databaseUniqueGiftBackdropColors.EdgeColor = uniqueGiftBackdropColors.EdgeColor;
                databaseUniqueGiftBackdropColors.SymbolColor = uniqueGiftBackdropColors.SymbolColor;
                databaseUniqueGiftBackdropColors.TextColor = uniqueGiftBackdropColors.TextColor;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUniqueGiftBackdropToMessage {uniqueGiftBackdropColors.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftBackdropToMessage(IObjectSpace securedObjectSpace, UniqueGiftBackdrop uniqueGiftBackdrop, out TelegramUniqueGiftBackdrop? databaseUniqueGiftBackdrop)
        {
            try
            {
                databaseUniqueGiftBackdrop = securedObjectSpace.CreateObject<TelegramUniqueGiftBackdrop>();
                databaseUniqueGiftBackdrop.RarityPerMille = uniqueGiftBackdrop.RarityPerMille;
                if (databaseUniqueGiftBackdrop.Colors != null)
                {
                    MapTelegramUniqueGiftBackdropColorsToMessage(securedObjectSpace, uniqueGiftBackdrop.Colors, out var databaseUniqueGiftBackdropColors);
                    if (databaseUniqueGiftBackdropColors != null)
                        databaseUniqueGiftBackdrop.Colors = databaseUniqueGiftBackdropColors;
                }
                if (databaseUniqueGiftBackdrop.Name != null)
                {
                    databaseUniqueGiftBackdrop.Name = uniqueGiftBackdrop.Name ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramBackdropToMessage {uniqueGiftBackdrop.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftInfoToMessage(IObjectSpace securedObjectSpace, UniqueGiftInfo uniqueGiftInfo, out TelegramUniqueGiftInfo? databaseUniqueGiftInfo)
        {
            try
            {
                databaseUniqueGiftInfo = securedObjectSpace.CreateObject<TelegramUniqueGiftInfo>();
                if (uniqueGiftInfo.Gift != null)
                {
                    MapTelegramUniqueGiftToMessage(securedObjectSpace, uniqueGiftInfo.Gift, out var databaseUniqueGift);
                    if (databaseUniqueGift != null)
                        databaseUniqueGiftInfo.Gift = databaseUniqueGift;
                }
                if (uniqueGiftInfo.OwnedGiftId != null)
                {
                    databaseUniqueGiftInfo.OwnedGiftId = uniqueGiftInfo.OwnedGiftId;
                }
                if (uniqueGiftInfo.TransferStarCount != null)
                {
                    databaseUniqueGiftInfo.TransferStarCount = uniqueGiftInfo.TransferStarCount;
                }
                if (uniqueGiftInfo.Origin != null)
                {
                    databaseUniqueGiftInfo.Origin = uniqueGiftInfo.Origin ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUniqueGiftInfoToMessage {uniqueGiftInfo.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftModelToMessage(IObjectSpace securedObjectSpace, UniqueGiftModel uniqueGiftModel, out TelegramUniqueGiftModel? databaseUniqueGiftModel)
        {
            try
            {
                databaseUniqueGiftModel = securedObjectSpace.CreateObject<TelegramUniqueGiftModel>();
                databaseUniqueGiftModel.RarityPerMille = uniqueGiftModel.RarityPerMille;
                databaseUniqueGiftModel.Name = uniqueGiftModel.Name ?? string.Empty;
                if (uniqueGiftModel.Sticker != null)
                {
                    MapTelegramStickerToMessage(securedObjectSpace, uniqueGiftModel.Sticker, out var databaseSticker);
                    if (databaseSticker != null)
                        databaseUniqueGiftModel.Sticker = databaseSticker;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUniqueGiftModelToMessage {uniqueGiftModel.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftSymbolToMessage(IObjectSpace securedObjectSpace, UniqueGiftSymbol uniqueGiftSymbol, out TelegramUniqueGiftSymbol? databaseUniqueGiftSymbol)
        {
            try
            {
                databaseUniqueGiftSymbol = securedObjectSpace.CreateObject<TelegramUniqueGiftSymbol>();
                if (uniqueGiftSymbol.Name != null)
                {
                    databaseUniqueGiftSymbol.Name = uniqueGiftSymbol.Name ?? string.Empty;
                }
                if (uniqueGiftSymbol.Sticker != null)
                {
                    MapTelegramStickerToMessage(securedObjectSpace, uniqueGiftSymbol.Sticker, out var databaseSticker);
                    if (databaseSticker != null)
                        databaseUniqueGiftSymbol.Sticker = databaseSticker;
                }
                databaseUniqueGiftSymbol.RarityPerMille = uniqueGiftSymbol.RarityPerMille;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUniqueGiftSymbolToMessage {uniqueGiftSymbol.ToString()}");
                throw;
            }
        }
        private void MapTelegramUniqueGiftToMessage(IObjectSpace securedObjectSpace, UniqueGift uniqueGift, out TelegramUniqueGift? databaseUniqueGift)
        {
            try
            {
                databaseUniqueGift = securedObjectSpace.CreateObject<TelegramUniqueGift>();
                if (uniqueGift.Backdrop != null)
                {
                    MapTelegramUniqueGiftBackdropToMessage(securedObjectSpace, uniqueGift.Backdrop, out var databaseUniqueGiftBackdrop);
                    if (databaseUniqueGiftBackdrop != null)
                        databaseUniqueGift.Backdrop = databaseUniqueGiftBackdrop;
                }
                databaseUniqueGift.Number = uniqueGift.Number;
                if (uniqueGift.BaseName != null)
                {
                    databaseUniqueGift.BaseName = uniqueGift.BaseName ?? string.Empty;
                }
                if (uniqueGift.Name != null)
                {
                    databaseUniqueGift.Name = uniqueGift.Name ?? string.Empty;
                }
                if (uniqueGift.Model != null)
                {
                    MapTelegramUniqueGiftModelToMessage(securedObjectSpace, uniqueGift.Model, out var databaseUniqueGiftModel);
                    if (databaseUniqueGiftModel != null)
                        databaseUniqueGift.Model = databaseUniqueGiftModel;
                }
                if (uniqueGift.Symbol != null)
                {
                    MapTelegramUniqueGiftSymbolToMessage(securedObjectSpace, uniqueGift.Symbol, out var databaseUniqueGiftSymbol);
                    if (databaseUniqueGiftSymbol != null)
                        databaseUniqueGift.Symbol = databaseUniqueGiftSymbol;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramShippingAddressToMessage {uniqueGift.ToString()}");
                throw;
            }
        }
        private void MapTelegramUpdateSetToDatabase(Update telegramUpdate, SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider)
        {
            try
            {
                using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                {
                    if (telegramUpdate != null)
                    {
                        if (telegramUpdate.Message != null && telegramUpdate.Message is Message telegramMessage)
                        {
                            try
                            {
                                MapTelegramMessageToDatabase(telegramMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();

                                logger.LogInformation($"Nested Message {telegramMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested telegramMessage to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.EditedMessage != null && telegramUpdate.EditedMessage is Message telegramEditedMessage)
                        {
                            try
                            {
                                MapTelegramMessageToDatabase(telegramEditedMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested EditedMessage {telegramEditedMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested EditedMessage to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.ChannelPost != null && telegramUpdate.ChannelPost is Message telegramChannelPostMessage)
                        {
                            try
                            {

                                MapTelegramMessageToDatabase(telegramChannelPostMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested ChannelPost {telegramChannelPostMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChannelPost to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.EditedChannelPost != null && telegramUpdate.EditedChannelPost is Message telegramEditedChannelPostMessage)
                        {
                            try
                            {
                                MapTelegramMessageToDatabase(telegramEditedChannelPostMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested EditedChannelPost {telegramEditedChannelPostMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested EditedChannelPost to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.BusinessMessage != null && telegramUpdate.BusinessMessage is Message telegramBusinessMessageMessage)
                        {
                            try
                            {
                                MapTelegramMessageToDatabase(telegramBusinessMessageMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested BusinessMessage {telegramBusinessMessageMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested BusinessMessage to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.EditedBusinessMessage != null && telegramUpdate.EditedBusinessMessage is Message telegramEditedBusinessMessage)
                        {
                            try
                            {
                                MapTelegramMessageToDatabase(telegramEditedBusinessMessage, securedObjectSpace, out TelegramMessage? finalSimpleMessage);
                                ArgumentNullException.ThrowIfNull(finalSimpleMessage);
                                finalSimpleMessage.UpdateId = telegramUpdate.Id;

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested EditedBusinessMessage {telegramEditedBusinessMessage.ToString()} saved to database");
                                NotifyClientsAboutNewMessage(finalSimpleMessage).GetAwaiter().GetResult();
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested EditedBusinessMessage to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.Poll != null && telegramUpdate.Poll is Poll telegramEditedPoll)
                        {
                            try
                            {
                                MapTelegramPollToMessage(securedObjectSpace, telegramEditedPoll, out var finalPoll);
                                if (finalPoll != null)
                                    finalPoll.UpdateId = telegramUpdate.Id;

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"Nested Poll {telegramEditedPoll.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested TelegramPoll to DB {ex.ToString()}");
                            }

                        }
                        if (telegramUpdate.PollAnswer != null && telegramUpdate.PollAnswer is PollAnswer telegramPollAnswer)
                        {
                            try
                            {
                                MapTelegramPollAnswerToMessage(securedObjectSpace, telegramPollAnswer, out var finalPoll);
                                if (finalPoll != null)
                                    finalPoll.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"PollAnswer {telegramPollAnswer.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested PollAnswer to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.BusinessConnection != null && telegramUpdate.BusinessConnection is BusinessConnection telegramBusinessConnection)
                        {
                            try
                            {
                                MapTelegramBusinessConnectionToMessage(securedObjectSpace, telegramBusinessConnection, out var finalBusinessConnection);
                                if (finalBusinessConnection != null)
                                    finalBusinessConnection.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"BusinessConnection {finalBusinessConnection.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested BusinessConnection to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.DeletedBusinessMessages != null && telegramUpdate.DeletedBusinessMessages is BusinessMessagesDeleted telegramBusinessMessagesDeleted)
                        {
                            try
                            {
                                MapTelegramBusinessMessagesDeleted(securedObjectSpace, telegramBusinessMessagesDeleted, out var finalBusinessMessagesDeleted);
                                if (finalBusinessMessagesDeleted != null)
                                    finalBusinessMessagesDeleted.UpdateId = telegramUpdate.Id;
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"BusinessMessagesDeleted {finalBusinessMessagesDeleted.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested BusinessMessagesDeleted to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.MessageReaction != null && telegramUpdate.MessageReaction is MessageReactionUpdated telegramMessageReactionUpdated)
                        {
                            try
                            {
                                MapTelegramMessageReactionUpdated(securedObjectSpace, telegramMessageReactionUpdated, out var finalMessageReactionUpdated);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"MessageReactionUpdated {finalMessageReactionUpdated.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested MessageReactionUpdated to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.MessageReactionCount != null && telegramUpdate.MessageReactionCount is MessageReactionCountUpdated telegramMessageReactionCountUpdates)
                        {
                            try
                            {
                                MaptelegramMessageReactionCountUpdates(securedObjectSpace, telegramMessageReactionCountUpdates, out var finaltelegramMessageReactionCountUpdates);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"MessageReactionCountUpdated {finaltelegramMessageReactionCountUpdates.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested MessageReactionCountUpdated to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.InlineQuery != null && telegramUpdate.InlineQuery is InlineQuery telegramInlineQueries)
                        {
                            try
                            {
                                MaptelegramtelegramInlineQueries(securedObjectSpace, telegramInlineQueries, out var finaltelegramInlineQueries);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"InlineQuery {telegramInlineQueries.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested InlineQuery to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.ChosenInlineResult != null && telegramUpdate.ChosenInlineResult is ChosenInlineResult telegramChosenInlineResult)
                        {
                            try
                            {
                                MaptelegramChosenInlineResult(securedObjectSpace, telegramChosenInlineResult, out var finaltelegramChosenInlineResult);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ChosenInlineResult {telegramChosenInlineResult.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChosenInlineResult  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.CallbackQuery != null && telegramUpdate.CallbackQuery is CallbackQuery telegramCallbackQuery)
                        {
                            try
                            {
                                MaptelegramCallbackQuery(securedObjectSpace, telegramCallbackQuery, out var finaltelegramCallbackQuery);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"CallbackQuery {telegramCallbackQuery.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested CallbackQuery  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.ShippingQuery != null && telegramUpdate.ShippingQuery is ShippingQuery telegramShippingQuery)
                        {
                            try
                            {
                                MaptelegramShippingQuery(securedObjectSpace, telegramShippingQuery, out var finaltelegramShippingQuery);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ShippingQuery {telegramShippingQuery.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ShippingQuery  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.PreCheckoutQuery != null && telegramUpdate.PreCheckoutQuery is PreCheckoutQuery telegramPreCheckoutQuery)
                        {
                            try
                            {
                                MaptelegramPreCheckoutQuery(securedObjectSpace, telegramPreCheckoutQuery, out var finaltelegramPreCheckoutQuery);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"PreCheckoutQuery {telegramPreCheckoutQuery.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested PreCheckoutQuery  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.PurchasedPaidMedia != null && telegramUpdate.PurchasedPaidMedia is PaidMediaPurchased telegramPurchasedPaidMedia)
                        {
                            try
                            {
                                MaptelegramPaidMediaPurchased(securedObjectSpace, telegramPurchasedPaidMedia, out var finaltelegramPurchasedPaidMedia);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"PaidMediaPurchased {telegramPurchasedPaidMedia.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested PaidMediaPurchased  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.MyChatMember != null && telegramUpdate.MyChatMember is ChatMemberUpdated telegramChatMemberUpdated)
                        {
                            try
                            {
                                MaptelegramChatMemberUpdated(securedObjectSpace, telegramChatMemberUpdated, out var finaltelegramChatMemberUpdated);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"MyChatMemberUpdated {telegramChatMemberUpdated.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested MyChatMemberUpdated  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.ChatMember != null && telegramUpdate.ChatMember is ChatMemberUpdated telegramChatMemberUpdated2)
                        {
                            try
                            {
                                MaptelegramChatMemberUpdated(securedObjectSpace, telegramChatMemberUpdated2, out var finaltelegramChatMemberUpdated2);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ChatMemberUpdated {telegramChatMemberUpdated2.ToString()} saved to database");

                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChatMemberUpdated  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.ChatJoinRequest != null && telegramUpdate.ChatJoinRequest is ChatJoinRequest telegramChatJoinRequest)
                        {
                            try
                            {
                                MaptelegramChatJoinRequest(securedObjectSpace, telegramChatJoinRequest, out TelegramChatJoinRequest? finalChatJoinRequest);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ChatJoinRequest {telegramChatJoinRequest.ToString()} saved to database");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChatJoinRequest  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.ChatBoost != null && telegramUpdate.ChatBoost is ChatBoostUpdated telegramChatBoostUpdated)
                        {
                            try
                            {
                                MaptelegramChatBoostUpdated(securedObjectSpace, telegramChatBoostUpdated, out TelegramChatBoostUpdated? databaseChatBoostUpdated);
                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ChatBoostUpdated {telegramChatBoostUpdated.ToString()} saved to database");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChatBoostUpdated  to DB {ex.ToString()}");
                            }
                        }
                        if (telegramUpdate.RemovedChatBoost != null && telegramUpdate.RemovedChatBoost is ChatBoostRemoved telegramChatBoostRemoved)
                        {
                            try
                            {
                                MaptelegramChatBoostRemoved(securedObjectSpace, telegramChatBoostRemoved, out TelegramChatBoostRemoved? databaseTelegramChatBoostRemoved);

                                securedObjectSpace.CommitChanges();
                                logger.LogInformation($"ChatBoostRemoved {telegramChatBoostRemoved.ToString()} saved to database");
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, $"Error while adding nested ChatBoostRemoved  to DB {ex.ToString()}");
                            }
                        }
                    }
                    securedObjectSpace.CommitChanges();
                }
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in MapTelegramUpdateToDatabase {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUpdateToDatabase {telegramUpdate.ToString()}");
            }
        }
        private void MapTelegramUsersSharedToMessage(IObjectSpace securedObjectSpace, UsersShared usersShared, out TelegramUsersShared? databaseUsersShared)
        {
            try
            {
                databaseUsersShared = securedObjectSpace.CreateObject<TelegramUsersShared>();
                if (usersShared.Users != null && usersShared.Users.LongLength > 0)
                {
                    var listDatabaseSharedUsers = new ObservableCollection<TelegramSharedUser>();
                    foreach (SharedUser user in usersShared.Users)
                    {
                        MapTelegramSharedUserToMessage(securedObjectSpace, user, out var databaseSharedUser);
                        if (databaseSharedUser != null)
                        {
                            listDatabaseSharedUsers.Add(databaseSharedUser);
                        }
                    }
                    foreach (var entry in listDatabaseSharedUsers)
                        databaseUsersShared.Users.Add(entry);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUsersSharedToMessage {usersShared.ToString()}");
                throw;
            }
        }

        private void MapTelegramUserToMessage(IObjectSpace securedObjectSpace, User user, out TelegramUser? databaseUser)
        {
            try
            {

                ArgumentNullException.ThrowIfNull(user.Id);
                ArgumentOutOfRangeException.ThrowIfLessThan(user.Id, 0);
                CriteriaOperator userCriteria = CriteriaOperator.FromLambda<TelegramUser>(
                    u => u.UserId == user.Id);
                databaseUser = securedObjectSpace.FindObject<TelegramUser>(
                    userCriteria);
                if (databaseUser == null)
                {
                    databaseUser = securedObjectSpace.CreateObject<TelegramUser>(
                        );

                }
                databaseUser.UserId = user.Id;
                databaseUser.IsBot = user.IsBot;
                databaseUser.FirstName = user.FirstName ?? string.Empty;
                databaseUser.LastName = user.LastName ?? string.Empty;
                databaseUser.Username = user.Username ?? string.Empty;
                databaseUser.LanguageCode = user.LanguageCode ?? string.Empty;
                databaseUser.IsPremium = user.IsPremium;
                databaseUser.AddedToAttachmentMenu = user.AddedToAttachmentMenu;
                databaseUser.CanJoinGroups = user.CanJoinGroups;
                databaseUser.CanReadAllGroupMessages = user.CanReadAllGroupMessages;
                databaseUser.SupportsInlineQueries = user.SupportsInlineQueries;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramUserToMessage {user.ToString()}");
                throw;
            }
        }

        private void MapTelegramVenueToMessage(IObjectSpace securedObjectSpace, Venue venue, out TelegramVenue? databaseVenue)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(venue.GooglePlaceId);
                CriteriaOperator venueCriteria = CriteriaOperator.FromLambda<TelegramVenue>(
                    v => v.FoursquareId == venue.FoursquareId && v.GooglePlaceId == venue.GooglePlaceId);
                databaseVenue = securedObjectSpace.FindObject<TelegramVenue>(
                    venueCriteria);
                if (databaseVenue == null)
                {
                    databaseVenue = securedObjectSpace.CreateObject<TelegramVenue>(
                        );

                }
                databaseVenue.Address = venue.Address ?? string.Empty;
                databaseVenue.FoursquareId = venue.FoursquareId ?? string.Empty;
                databaseVenue.GooglePlaceId = venue.GooglePlaceId ?? string.Empty;
                databaseVenue.Title = venue.Title ?? string.Empty;
                if (venue.Location != null)
                {
                    CriteriaOperator locationCriteria = CriteriaOperator.FromLambda<TelegramLocation>(
                        location => location.Latitude == venue.Location.Latitude &&
                            location.Longitude == venue.Location.Longitude);
                    var messageLocation = securedObjectSpace.FindObject<TelegramLocation>(
                        locationCriteria);
                    if (messageLocation == null)
                    {
                        messageLocation = securedObjectSpace.CreateObject<TelegramLocation>(
                           );
                    }
                    messageLocation.Latitude = venue.Location.Latitude;
                    messageLocation.Longitude = venue.Location.Longitude;
                    messageLocation.HorizontalAccuracy = venue.Location.HorizontalAccuracy;
                    messageLocation.LivePeriod = venue.Location.LivePeriod;
                    messageLocation.Heading = venue.Location.Heading;
                    messageLocation.ProximityAlertRadius = venue.Location.ProximityAlertRadius;
                    databaseVenue.Location = messageLocation;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVenueToMessage {venue.ToString()}");
                throw;
            }
        }
        private void MapTelegramVideoChatParticipantsInvitedToMessage(IObjectSpace securedObjectSpace, VideoChatParticipantsInvited videoChatParticipantsInvited, out TelegramVideoChatParticipantsInvited? databaseVideoChatParticipantsInvited)
        {
            try
            {
                databaseVideoChatParticipantsInvited = securedObjectSpace.CreateObject<TelegramVideoChatParticipantsInvited>();
                if (videoChatParticipantsInvited.Users != null && videoChatParticipantsInvited.Users.LongLength > 0)
                {
                    var listDatabaseUsers = new ObservableCollection<TelegramUser>();
                    foreach (User user in videoChatParticipantsInvited.Users)
                    {
                        MapTelegramUserToMessage(securedObjectSpace, user, out var databaseUser);
                        if (databaseUser != null)
                        {
                            listDatabaseUsers.Add(databaseUser);
                        }
                    }
                    foreach (var entry in listDatabaseUsers)
                        databaseVideoChatParticipantsInvited.Users.Add(entry);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVideoChatParticipantsInvitedToMessage {videoChatParticipantsInvited.ToString()}");
                throw;
            }
        }


        private void MapTelegramVideoChatScheduledToMessage(IObjectSpace securedObjectSpace, VideoChatScheduled videoChatScheduled, out TelegramVideoChatScheduled? databaseVideoChatScheduled)
        {
            try
            {
                databaseVideoChatScheduled = securedObjectSpace.CreateObject<TelegramVideoChatScheduled>();

                databaseVideoChatScheduled.StartDate = videoChatScheduled.StartDate;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVideoChatScheduledToMessage {videoChatScheduled.ToString()}");
                throw;
            }
        }
        private void MapTelegramVideoNoteToMessage(IObjectSpace securedObjectSpace, VideoNote videoNote, out TelegramVideoNote? databaseVideoNote)
        {
            try
            {
                databaseVideoNote = securedObjectSpace.CreateObject<TelegramVideoNote>();
                if (videoNote.FileId != null)
                {
                    databaseVideoNote.FileId = videoNote.FileId ?? string.Empty;
                }
                databaseVideoNote.Duration = videoNote.Duration;
                databaseVideoNote.Length = videoNote.Length;
                if (videoNote.Thumbnail != null)
                {
                    MapTelegramPhotoSizeToMessage(securedObjectSpace, videoNote.Thumbnail, out var databaseThumbnail);
                    if (databaseThumbnail != null)
                        databaseVideoNote.Thumbnail = databaseThumbnail;
                }
                if (videoNote.FileUniqueId != null)
                {
                    databaseVideoNote.FileUniqueId = videoNote.FileUniqueId ?? string.Empty;

                }
                if (videoNote.FileSize != null)
                {
                    databaseVideoNote.FileSize = videoNote.FileSize;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVideoNoteToMessage {videoNote.ToString()}");
                throw;
            }
        }
        private void MapTelegramVideoToDatabase(IObjectSpace securedObjectSpace, Video video, out TelegramVideo? databaseVideo)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(video.FileUniqueId);
                CriteriaOperator videoCriteria = CriteriaOperator.FromLambda<TelegramVideo>(
                    dbVid => dbVid.FileUniqueId == video.FileUniqueId);
                databaseVideo = securedObjectSpace.FindObject<TelegramVideo>(
                    videoCriteria);
                if (databaseVideo == null)
                {
                    databaseVideo = securedObjectSpace.CreateObject<TelegramVideo>(
                        );

                }
                databaseVideo.FileId = video.FileId;
                databaseVideo.FileUniqueId = video.FileUniqueId;
                databaseVideo.Duration = video.Duration;
                databaseVideo.Width = video.Width;
                databaseVideo.Height = video.Height;
                databaseVideo.MimeType = video.MimeType ?? string.Empty;
                databaseVideo.FileSize = video.FileSize;
                if (video.Thumbnail != null)
                {
                    MapTelegramPhotoSizeToMessage(securedObjectSpace, video.Thumbnail, out var databaseThumbnail);
                    if (databaseThumbnail != null)
                        databaseVideo.Thumbnail = databaseThumbnail;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramPaidMediaToMessage {video.ToString()}");
                throw;
            }
        }
        private void MapTelegramVideoToMessage(IObjectSpace securedObjectSpace, Video video, out TelegramVideo? databaseVideo)
        {
            try
            {
                databaseVideo = securedObjectSpace.CreateObject<TelegramVideo>();
                if (video.FileId != null)
                {
                    databaseVideo.FileId = video.FileId ?? string.Empty;
                }
                databaseVideo.Duration = video.Duration;
                databaseVideo.Width = video.Width;
                databaseVideo.Height = video.Height;
                if (video.Thumbnail != null)
                {
                    MapTelegramPhotoSizeToMessage(securedObjectSpace, video.Thumbnail, out var databaseThumbnail);
                    if (databaseThumbnail != null)
                        databaseVideo.Thumbnail = databaseThumbnail;
                }
                if (video.MimeType != null)
                {
                    databaseVideo.MimeType = video.MimeType ?? string.Empty;
                }
                if (video.Cover != null)
                {
                    MapTelegramPhotoSizeArrayToMessage(securedObjectSpace, video.Cover, out var databaseCover);
                    if (databaseCover != null)
                        foreach (var entry in databaseCover)
                            databaseVideo.Cover.Add(entry);
                }
                if (video.StartTimestamp != null)
                {
                    databaseVideo.StartTimestamp = video.StartTimestamp;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVideoToMessage {video.ToString()}");
                throw;
            }
        }
        private void MapTelegramVoiceToMessage(IObjectSpace securedObjectSpace, Voice voice, out TelegramVoice? databaseVoice)
        {
            try
            {
                databaseVoice = securedObjectSpace.CreateObject<TelegramVoice>();
                if (voice.FileId != null)
                {
                    databaseVoice.FileId = voice.FileId ?? string.Empty;
                }
                databaseVoice.Duration = voice.Duration;
                if (voice.MimeType != null)
                {
                    databaseVoice.MimeType = voice.MimeType ?? string.Empty;
                }
                if (voice.FileSize != null)
                {
                    databaseVoice.FileSize = voice.FileSize;
                }
                if (voice.FileUniqueId != null)
                {
                    databaseVoice.FileUniqueId = voice.FileUniqueId ?? string.Empty;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramVoiceToMessage {voice.ToString()}");
                throw;
            }
        }
        private void MapTelegramWebAppDataToMessage(IObjectSpace securedObjectSpace, WebAppData webAppData, out TelegramWebAppData? databaseWebAppData)
        {
            try
            {
                databaseWebAppData = securedObjectSpace.CreateObject<TelegramWebAppData>();
                if (webAppData.Data != null)
                {
                    databaseWebAppData.Data = webAppData.Data ?? string.Empty;
                }
                if (webAppData.ButtonText != null)
                {
                    databaseWebAppData.ButtonText = webAppData.ButtonText ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramWebAppDataToMessage {webAppData.ToString()}");
                throw;
            }
        }
        private void MapTelegramWebAppToMessage(IObjectSpace securedObjectSpace, WebAppInfo webAppInfo, out TelegramWebAppInfo? databaseWebApp)
        {
            try
            {
                databaseWebApp = securedObjectSpace.CreateObject<TelegramWebAppInfo>();
                databaseWebApp.Url = webAppInfo.Url ?? string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramWebAppToMessage {webAppInfo.ToString()}");
                throw;
            }
        }
        private void MapTelegramWriteAccessAllowedToMessage(IObjectSpace securedObjectSpace, WriteAccessAllowed writeAccessAllowed, out TelegramWriteAccessAllowed? databaseWriteAccessAllowed)
        {
            try
            {
                databaseWriteAccessAllowed = securedObjectSpace.CreateObject<TelegramWriteAccessAllowed>();

                databaseWriteAccessAllowed.FromAttachmentMenu = writeAccessAllowed.FromAttachmentMenu;

                databaseWriteAccessAllowed.FromRequest = writeAccessAllowed.FromRequest;

                if (writeAccessAllowed.WebAppName != null)
                {
                    databaseWriteAccessAllowed.WebAppName = writeAccessAllowed.WebAppName ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in MapTelegramWriteAccessAllowedToMessage {writeAccessAllowed.ToString()}");
                throw;
            }
        }

        private async Task NotifyClientsAboutNewMessage(TelegramMessage message)
        {
            try
            {
                await hubContext.Clients.All.SendAsync("ReceiveTelegramMessage", message).ConfigureAwait(false);
                logger.LogInformation("Sent new message to hub clients.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to broadcast to hub.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {

                if (disposing)
                {


                }


                disposed = true;
            }
        }

        public bool AddOrUpdateUser(User user, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        MapTelegramUserToMessage(securedObjectSpace, user, out var databaseUser);
                        securedObjectSpace.CommitChanges();
                        if (databaseUser != null)
                            return true;
                        else
                            return false;
                    }

                }
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in AddOrUpdateUser {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in AddOrUpdateUser {ex.ToString()}");
                return false;
            }
        }

        public bool AddOrUpdateUserChats(ChatMember chatMember, ChatFullInfo telegramChatFullInfo, TelegramChatFullInfo chatFullInfo, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {
                        ArgumentNullException.ThrowIfNull(telegramChatFullInfo);
                        ArgumentNullException.ThrowIfNull(chatMember);
                        ArgumentNullException.ThrowIfNull(chatMember.User);
                        CriteriaOperator userChatCriteria = CriteriaOperator.FromLambda<TelegramUserChat>(
                               userChat => userChat.User != null && userChat.User.UserId == chatMember.User.Id);
                        var userChat = securedObjectSpace.FindObject<TelegramUserChat>(
                            userChatCriteria);
                        if (userChat == null)
                        {
                            logger.LogInformation($"Not found yet in room. Creating... {chatMember.ToString()} ");
                            userChat = securedObjectSpace.CreateObject<TelegramUserChat>(
                                );
                        }
                        else
                        {
                            if (userChat.DateUpdated >= DateOnly.FromDateTime(DateTime.Now))
                            {
                                logger.LogInformation($"User:{chatMember.ToString()} in Chat: {chatFullInfo.ToString()} is up to date already, return");
                                return false;
                            }
                        }
                        MapTelegramUserToMessage(securedObjectSpace, chatMember.User, out TelegramUser? telegramDatabaseUser);
                        ArgumentNullException.ThrowIfNull(telegramDatabaseUser);
                        userChat.User = telegramDatabaseUser;
                        userChat.DateUpdated = DateOnly.FromDateTime(DateTime.Now);
                        MapTelegramChatToMessage(securedObjectSpace, telegramChatFullInfo, out TelegramChat? databaseChat);
                        MapTelegramChatFullInfoToMessage(securedObjectSpace, telegramChatFullInfo, out TelegramChatFullInfo? databaseChatFullInfo);
                        ArgumentNullException.ThrowIfNull(databaseChat);

                        if (chatMember.IsInChat)
                        {
                            if (userChat.ChatThisUserChatBelongsTo == null)
                                userChat.ChatThisUserChatBelongsTo = new ObservableCollection<TelegramChat>();
                            if (!userChat.ChatThisUserChatBelongsTo.Any(x => x.ChatId == databaseChat.ChatId))
                            {
                                userChat.ChatThisUserChatBelongsTo.Add(databaseChat);
                                logger.LogInformation($"Added User to room {chatMember.ToString()} {chatFullInfo.ToString()}");
                            }

                        }
                        else
                        {
                            if (userChat.ChatThisUserChatBelongsTo != null)
                            {
                                if (!userChat.ChatThisUserChatBelongsTo.Contains(databaseChat))
                                {

                                    userChat.ChatThisUserChatBelongsTo.Remove(databaseChat);
                                    logger.LogInformation($"Removed User from {chatMember.ToString()} {chatFullInfo.ToString()}");
                                }
                            }
                        }
                        securedObjectSpace.CommitChanges();
                        return true;
                    }
                }
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in AddOrUpdateUserChats {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in AddOrUpdateUserChats {ex.ToString()} User:{chatMember.ToString()} in Chat: {chatFullInfo.ToString()}");
                return false;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<TelegramChat> GetChats(CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        CriteriaOperator ChatFilter = CriteriaOperator.FromLambda<TelegramChat>(x => x.ChatToIgnore == null || x.ChatToIgnore != true);

                        var returnList = securedObjectSpace.GetObjects<TelegramChat>(ChatFilter).ToList();
                        return returnList;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetChats {ex.ToString()}");
                return new List<TelegramChat>();
            }
        }

        public IList<TelegramChatFullInfo> GetMostRecentChatFullInfos(CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {
                        var returnList = securedObjectSpace.GetObjects<TelegramChatFullInfo>().ToList();

                        foreach (var info in returnList)
                        {
                            if (info.Chat != null)
                            {
                                var id = info.Chat.ChatId;
                                var name = info.Chat.Title;
                            }
                        }

                        var latestPerChat = returnList
                        .Where(x => x.Chat != null)
                        .GroupBy(x => x.Chat.ChatId)
                        .Select(g => g.OrderByDescending(x => x.DateCreated)
                                      .First())
                        .ToList();

                        return latestPerChat;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetChatFullInfos {ex.ToString()}");
                return new List<TelegramChatFullInfo>();
            }
        }

        public IList<TelegramUser> GetTelegramUsers(CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {
                        var returnList = securedObjectSpace.GetObjects<TelegramUser>().ToList();

                        return returnList;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetTelegramUsers {ex.ToString()}");
                return new List<TelegramUser>();
            }
        }

        public IList<TelegramUser> GetUsersAppeardInRoom(TelegramChatFullInfo chatFullInfo, CancellationToken? cancellationToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {
                        ArgumentNullException.ThrowIfNull(chatFullInfo.Chat);
                        CriteriaOperator RoomInMessages = CriteriaOperator.FromLambda<TelegramMessage>(messageFilter => messageFilter.ChatID == chatFullInfo.Chat.ID);

                        var messagesFromUserAndChat = securedObjectSpace.GetObjects<TelegramMessage>(RoomInMessages).ToList();
                        ObservableCollection<TelegramUser> users = new();
                        foreach (var messageFromUserAndChat in messagesFromUserAndChat)
                        {
                            CriteriaOperator userInMessage = CriteriaOperator.FromLambda<TelegramUser>(userFilter => userFilter.ID == messageFromUserAndChat.UserFromID);
                            var returnUser = securedObjectSpace.FindObject<TelegramUser>(userInMessage);
                            if (returnUser != null)
                                users.Add(returnUser);
                        }
                        return users;
                    }
                }
            }

            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in GetTelegramAppeardInRoom {ex.ToString()}");
                return new ObservableCollection<TelegramUser>();
            }
        }

        public bool IgnoreChat(TelegramChat chat, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        CriteriaOperator telegramChatCriteria = CriteriaOperator.FromLambda<TelegramChat>(x => x.ID == chat.ID);
                        var databaseChatToIgnore = securedObjectSpace.FindObject<TelegramChat>(telegramChatCriteria);
                        if (databaseChatToIgnore == null)
                        {
                            return false;
                        }

                        databaseChatToIgnore.ChatToIgnore = true;

                        securedObjectSpace.CommitChanges();
                        return true;
                    }

                }
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in IgnoreChat {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in IgnoreChat {ex.ToString()}");
                return false;
            }
        }

        public Task<string> NewFullChatInfo(ChatFullInfo item, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    MapTelegramChatFullInfoSetToDatabase(item, objectSpaceProvider);

                }

                return Task.FromResult($"NewChatFullInfo {item.ToString()} saved to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in NewChatFullInfo {item.ToString()}");
                return Task.FromResult($"NewChatFullInfo {item.ToString()} failed to save to the database");
            }
        }
        public Task<string> NewMessage(Message item, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        var findExistingMessage = CriteriaOperator.FromLambda<TelegramMessage>(
                      m => m.Message_ID == item.MessageId);
                        var existingPinnedMessage = securedObjectSpace.FindObject<TelegramMessage>(findExistingMessage);
                        if (existingPinnedMessage == null)
                        {
                            MapTelegramMessageToDatabase(item, securedObjectSpace, out TelegramMessage? newPinnedMessage);
                            existingPinnedMessage = newPinnedMessage;
                            securedObjectSpace.CommitChanges();
                            logger.LogInformation($"NewMessage {item.ToString()} saved to database");
                        }
                    }
                }
                return Task.FromResult($"NewMessage {item.ToString()} saved to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in NewMessage {item.ToString()}");
                return Task.FromResult($"NewMessage {item.ToString()} failed to save to the database");
            }
        }

        public Task<string> NewPoll(Poll item, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        var findExistingPoll = CriteriaOperator.FromLambda<TelegramPoll>(
                      m => m.PollId == item.Id);
                        var existingPoll = securedObjectSpace.FindObject<TelegramPoll>(findExistingPoll);
                        if (existingPoll == null)
                        {
                            existingPoll = securedObjectSpace.CreateObject<TelegramPoll>();
                            MapTelegramPollToDatabase(item, existingPoll, securedObjectSpace, objectSpaceProvider);
                            securedObjectSpace.CommitChanges();

                            logger.LogInformation($"NewPoll {item.ToString()} saved to database");
                        }
                    }
                }

                return Task.FromResult($"NewPoll {item.ToString()} saved to database");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in NewPoll {item.ToString()} failed to save to the database");
                return Task.FromResult($"NewPoll {item.ToString()}  failed to save to the database");
            }
        }

        public Task<string> NewPollAnswer(PollAnswer item, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        var pollAnswer = securedObjectSpace.CreateObject<TelegramPollAnswer>(
                            );
                        MapTelegramPollAnswerToDatabase(item, pollAnswer, securedObjectSpace, objectSpaceProvider);
                        securedObjectSpace.CommitChanges();

                        logger.LogInformation($"NewPollAnswer {item.ToString()} saved to database");
                    }
                }

                return Task.FromResult($"NewPollAnswer {item.ToString()} saved to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in NewPollAnswer {item.ToString()}");
                return Task.FromResult($"NewPollAnswer {item.ToString()} failed to save to the database");
            }
        }

        public Task<string> NewUpdate(Update item, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    MapTelegramUpdateSetToDatabase(item, objectSpaceProvider);


                    logger.LogInformation($"NewUpdate {item.ToString()} saved to database");


                }

                return Task.FromResult($"NewUpdate {item.ToString()} saved to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in NewUpdate {item.ToString()}");
                return Task.FromResult($"NewUpdate {item.ToString()} failed to save to the database");
            }
        }


        public bool TelegramChatBotRightsUser(User me, ChatFullInfo chatFullInfo, CancellationToken? externalToken = null)
        {
            try
            {
                using (SecuredEFCoreObjectSpaceProvider<TacoContext> objectSpaceProvider = CreateServiceObjectSpaceProvider())
                {
                    using (IObjectSpace securedObjectSpace = objectSpaceProvider.CreateObjectSpace())
                    {

                        MapTelegramUserToMessage(securedObjectSpace, me, out var databaseUser);
                        MapTelegramChatFullInfoToMessage(securedObjectSpace, chatFullInfo, out var databaseChatFullInfo);


                        CriteriaOperator telegramChatBotRightsUserCriteria = CriteriaOperator.FromLambda<TelegramChatBotRightsUser>(
                         chatBotsRightsUserFilter => chatBotsRightsUserFilter.BotUser.UserId == databaseUser.UserId && chatBotsRightsUserFilter.Chat.ChatId == databaseChatFullInfo.Chat.ChatId);
                        var telegramChatBotRightsUser = securedObjectSpace.FindObject<TelegramChatBotRightsUser>(
                            telegramChatBotRightsUserCriteria);
                        if (telegramChatBotRightsUser == null)
                        {
                            telegramChatBotRightsUser = securedObjectSpace.CreateObject<TelegramChatBotRightsUser>(
                                );
                        }
                        securedObjectSpace.CommitChanges();
                        if (telegramChatBotRightsUser != null && telegramChatBotRightsUser.TacoTeamsChatsThisBotUserRightsIsPartOf != null && telegramChatBotRightsUser.TacoTeamsChatsThisBotUserRightsIsPartOf.Any(x => x.TeamChatID == databaseChatFullInfo?.Chat?.ID && x.BotAssignedID == databaseUser?.ID))
                            return true;
                        else
                            return false;
                    }

                }
            }
            catch (EFCoreSecurityException ex)
            {
                logger.LogError(ex, $"EfSecurity Exception in TelegramChatBotRightsUser {ex.ToString()} {ex.InnerException?.ToString()}");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in TelegramChatBotRightsUser {ex.ToString()}");
                return false;
            }
        }

        public ConnectionStringsCoreOptions ConnectionStrings
        {
            get;
        } = configuration.Get<TacosCore.BusinessObjects.ConfigurationRoot>()!.ConnectionStringsCore!;
        public ServiceConfigurationCoreOptions ServiceConfigurationOptions
        {
            get;
        } = configuration.Get<TacosCore.BusinessObjects.ConfigurationRoot>()!.ServiceConfigurationCore!;
    }
}
