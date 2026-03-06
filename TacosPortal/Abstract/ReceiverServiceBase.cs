//-----------------------------------------------------------------------
// <copyright file="ReceiverServiceBase.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using TacosPortal.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TacosPortal.Abstract;



public abstract class ReceiverServiceBase<TUpdateHandler>(ITelegramBotClient botClient, TUpdateHandler updateHandler, ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
    : IReceiverService where TUpdateHandler : IUpdateHandler
{

    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {

        var receiverOptions = new ReceiverOptions() { DropPendingUpdates = true, AllowedUpdates = [] };

        var me = await botClient.GetMe(stoppingToken).ConfigureAwait(false);
        logger.LogInformation("Start receiving updates for {TelegramBotName}", me.Username ?? "My Awesome Bot");


        await botClient.ReceiveAsync(updateHandler, receiverOptions, stoppingToken).ConfigureAwait(false);
    }
}
