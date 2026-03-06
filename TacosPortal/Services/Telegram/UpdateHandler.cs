//-----------------------------------------------------------------------
// <copyright file="UpdateHandler.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace TacosPortal.Services.Telegram;

public class UpdateHandler(ITelegramBotClient bot, ILogger<UpdateHandler> logger, ITacosApi tacosApi) : IUpdateHandler
{

    public async Task HandleErrorAsync(ITelegramBotClient bot, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("HandleError: {Exception}", exception);

            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).ConfigureAwait(false);
        }
        catch (TaskCanceledException)
        {

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error in HandleErrorAsync {ex.ToString()}");

        }
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {

                _ = tacosApi.NewUpdate(update).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in tacosApi add NewUpdate {ex.ToString()}");
            }
            await Task.CompletedTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error in HandleUpdateAsync {ex.ToString()}");
        }

    }

}
