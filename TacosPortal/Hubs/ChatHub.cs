//-----------------------------------------------------------------------
// <copyright file="ChatHub.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosPortal.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task NotifyNewTelegramMessage(TelegramMessage message)
        {
            try
            {
                _logger.LogInformation("Broadcasting new message...");
                await Clients.All.SendAsync("ReceiveTelegramMessage", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in NotifyNewTelegramMessage {ex.ToString()}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                _logger.LogInformation($"Client connected: {Context.ConnectionId}");
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in OnConnectedAsync {ex.ToString()}");
            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in OnDisconnectedAsync {ex.ToString()}");
            }
        }
    }
}
