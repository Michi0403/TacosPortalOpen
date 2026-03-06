//-----------------------------------------------------------------------
// <copyright file="TelegramNestedEntityAssignmentHelper.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EF;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosPortal.Helper
{
    public static class TelegramNestedEntityAssignmentHelper
    {

        private static void FixRecursiveTelegramMessage(
            TelegramMessage message,
            IObjectSpace objectSpace,
            HashSet<Guid> visited,
            ILogger logger)
        {
            if (message == null || visited.Contains(message.ID))
                return;

            visited.Add(message.ID);

            try
            {
                TelegramChat? chat = message.Chat;
                ReplaceWithTrackedEntity(ref chat, objectSpace);
                message.Chat = chat;

                TelegramUser? from = message.From;
                ReplaceWithTrackedEntity(ref from, objectSpace);
                message.From = from;

                TelegramUser? forwardFrom = message.ForwardFrom;
                ReplaceWithTrackedEntity(ref forwardFrom, objectSpace);
                message.ForwardFrom = forwardFrom;

                TelegramChat? forwardFromChat = message.ForwardFromChat;
                ReplaceWithTrackedEntity(ref forwardFromChat, objectSpace);
                message.ForwardFromChat = forwardFromChat;

                TelegramChat? senderChat = message.SenderChat;
                ReplaceWithTrackedEntity(ref senderChat, objectSpace);
                message.SenderChat = senderChat;

                TelegramUser? senderBusinessBot = message.SenderBusinessBot;
                ReplaceWithTrackedEntity(ref senderBusinessBot, objectSpace);
                message.SenderBusinessBot = senderBusinessBot;

                TelegramMessageOrigin? forwardOrigin = message.ForwardOrigin;
                ReplaceWithTrackedEntity(ref forwardOrigin, objectSpace);
                message.ForwardOrigin = forwardOrigin;

                TelegramExternalReplyInfo? externalReply = message.ExternalReply;
                ReplaceWithTrackedEntity(ref externalReply, objectSpace);
                message.ExternalReply = externalReply;

                TelegramTextQuote? quote = message.Quote;
                ReplaceWithTrackedEntity(ref quote, objectSpace);
                message.Quote = quote;

                TelegramUser? viaBot = message.ViaBot;
                ReplaceWithTrackedEntity(ref viaBot, objectSpace);
                message.ViaBot = viaBot;

                TelegramMessage? replyToMessage = message.ReplyToMessage;
                ReplaceWithTrackedEntity(ref replyToMessage, objectSpace);
                message.ReplyToMessage = replyToMessage;

                TelegramStory? story = message.Story;
                ReplaceWithTrackedEntity(ref story, objectSpace);
                message.Story = story;

                TelegramUser? leftChatMember = message.LeftChatMember;
                ReplaceWithTrackedEntity(ref leftChatMember, objectSpace);
                message.LeftChatMember = leftChatMember;

                TelegramContact? contact = message.Contact;
                ReplaceWithTrackedEntity(ref contact, objectSpace);
                message.Contact = contact;

                TelegramDice? dice = message.Dice;
                ReplaceWithTrackedEntity(ref dice, objectSpace);
                message.Dice = dice;

                TelegramGame? game = message.Game;
                ReplaceWithTrackedEntity(ref game, objectSpace);
                message.Game = game;

                TelegramPoll? poll = message.Poll;
                ReplaceWithTrackedEntity(ref poll, objectSpace);
                message.Poll = poll;

                TelegramVenue? venue = message.Venue;
                ReplaceWithTrackedEntity(ref venue, objectSpace);
                message.Venue = venue;

                TelegramLocation? location = message.Location;
                ReplaceWithTrackedEntity(ref location, objectSpace);
                message.Location = location;

                TelegramInvoice? invoice = message.Invoice;
                ReplaceWithTrackedEntity(ref invoice, objectSpace);
                message.Invoice = invoice;

                TelegramSuccessfulPayment? successfulPayment = message.SuccessfulPayment;
                ReplaceWithTrackedEntity(ref successfulPayment, objectSpace);
                message.SuccessfulPayment = successfulPayment;

                TelegramRefundedPayment? refundedPayment = message.RefundedPayment;
                ReplaceWithTrackedEntity(ref refundedPayment, objectSpace);
                message.RefundedPayment = refundedPayment;

                TelegramUsersShared? usersShared = message.UsersShared;
                ReplaceWithTrackedEntity(ref usersShared, objectSpace);
                message.UsersShared = usersShared;

                TelegramChatShared? chatShared = message.ChatShared;
                ReplaceWithTrackedEntity(ref chatShared, objectSpace);
                message.ChatShared = chatShared;

                TelegramGiftInfo? gift = message.Gift;
                ReplaceWithTrackedEntity(ref gift, objectSpace);
                message.Gift = gift;

                TelegramUniqueGiftInfo? uniqueGift = message.UniqueGift;
                ReplaceWithTrackedEntity(ref uniqueGift, objectSpace);
                message.UniqueGift = uniqueGift;

                TelegramWriteAccessAllowed? writeAccessAllowed = message.WriteAccessAllowed;
                ReplaceWithTrackedEntity(ref writeAccessAllowed, objectSpace);
                message.WriteAccessAllowed = writeAccessAllowed;

                TelegramPassportData? passportData = message.PassportData;
                ReplaceWithTrackedEntity(ref passportData, objectSpace);
                message.PassportData = passportData;

                TelegramProximityAlertTriggered? proximityAlert = message.ProximityAlertTriggered;
                ReplaceWithTrackedEntity(ref proximityAlert, objectSpace);
                message.ProximityAlertTriggered = proximityAlert;

                TelegramChatBackground? chatBackground = message.ChatBackgroundSet;
                ReplaceWithTrackedEntity(ref chatBackground, objectSpace);
                message.ChatBackgroundSet = chatBackground;

                TelegramForumTopicCreated? forumTopicCreated = message.ForumTopicCreated;
                ReplaceWithTrackedEntity(ref forumTopicCreated, objectSpace);
                message.ForumTopicCreated = forumTopicCreated;

                TelegramForumTopicEdited? forumTopicEdited = message.ForumTopicEdited;
                ReplaceWithTrackedEntity(ref forumTopicEdited, objectSpace);
                message.ForumTopicEdited = forumTopicEdited;

                TelegramGiveawayCreated? giveawayCreated = message.GiveawayCreated;
                ReplaceWithTrackedEntity(ref giveawayCreated, objectSpace);
                message.GiveawayCreated = giveawayCreated;

                TelegramGiveaway? giveaway = message.Giveaway;
                ReplaceWithTrackedEntity(ref giveaway, objectSpace);
                message.Giveaway = giveaway;

                TelegramGiveawayWinners? giveawayWinners = message.GiveawayWinners;
                ReplaceWithTrackedEntity(ref giveawayWinners, objectSpace);
                message.GiveawayWinners = giveawayWinners;

                TelegramGiveawayCompleted? giveawayCompleted = message.GiveawayCompleted;
                ReplaceWithTrackedEntity(ref giveawayCompleted, objectSpace);
                message.GiveawayCompleted = giveawayCompleted;

                TelegramVideoChatScheduled? videoChatScheduled = message.VideoChatScheduled;
                ReplaceWithTrackedEntity(ref videoChatScheduled, objectSpace);
                message.VideoChatScheduled = videoChatScheduled;

                TelegramVideoChatParticipantsInvited? participantsInvited = message.VideoChatParticipantsInvited;
                ReplaceWithTrackedEntity(ref participantsInvited, objectSpace);
                message.VideoChatParticipantsInvited = participantsInvited;

                TelegramWebAppData? webAppData = message.WebAppData;
                ReplaceWithTrackedEntity(ref webAppData, objectSpace);
                message.WebAppData = webAppData;

                TelegramInlineKeyboardMarkup? replyMarkup = message.ReplyMarkup;
                ReplaceWithTrackedEntity(ref replyMarkup, objectSpace);
                message.ReplyMarkup = replyMarkup;

                if (message.ReplyToMessage != null && !visited.Contains(message.ReplyToMessage.ID))
                {
                    FixRecursiveTelegramMessage(message.ReplyToMessage, objectSpace, visited, logger);
                }
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, $"[FixRecursive] Entity tracking conflict in message {message.ID}: {ex.Message}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[FixRecursive] General error in message {message.ID}: {ex}");
            }
        }

        private static void ReplaceWithTrackedEntity<T>(ref T? entity, IObjectSpace objectSpace) where T : BaseObject
        {
            if (entity == null)
                return;

            entity = objectSpace.GetObjectByKey<T>(entity.ID);
        }

        public static void FixTelegramGraph(TelegramMessage message, IObjectSpace objectSpace, ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(objectSpace);
            ArgumentNullException.ThrowIfNull(message);

            var visited = new HashSet<Guid>();

            try
            {
                FixRecursiveTelegramMessage(message, objectSpace, visited, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[FixTelegramGraph] Fatal error while processing message {message.ID}: {ex}");
            }
        }
    }
}