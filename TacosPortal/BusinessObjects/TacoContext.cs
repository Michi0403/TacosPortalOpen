//-----------------------------------------------------------------------
// <copyright file="TacoContext.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.Security;
using Microsoft.EntityFrameworkCore;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;

namespace TacosPortal.BusinessObjects
{
    [TypesInfoInitializer(typeof(TacoContextInitializer))]
    public class TacoContext : DbContext
    {

        public TacoContext()
        {
        }
        public TacoContext(DbContextOptions<TacoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.UseDeferredDeletion(this);
            modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.Restrict, DeleteBehavior.Cascade);
            _ = modelBuilder.HasChangeTrackingStrategy(
                ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
            _ = modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
            _ = modelBuilder.Entity<ApplicationUserLoginInfo>(
                b => b.HasIndex(
                    nameof(ISecurityUserLoginInfo.LoginProviderName),
                    nameof(ISecurityUserLoginInfo.ProviderUserKey))
                    .IsUnique());
        }

        public DbSet<TacoPermissionPolicyRole> ApiRoles { get; set; }

        public DbSet<ApplicationPushSubscription> ApplicationPushSubscriptions { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserLoginInfo> ApplicationUsersLoginInfos { get; set; }
        public DbSet<DatabaseLog> DatabaseLogs { get; set; }
        public DbSet<TacoTeamChat> TacoTeamChats { get; set; }
        public DbSet<TacoTeam> TacoTeams { get; set; }
        public DbSet<TelegramAcceptedGiftTypes> TelegramAcceptedGiftTypes { get; set; }

        public DbSet<TelegramAffiliateInfo> TelegramAffiliateInfo { get; set; }

        public DbSet<TelegramAnimation> TelegramAnimations { get; set; }

        public DbSet<TelegramApiResponse> TelegramApiResponses { get; set; }

        public DbSet<TelegramAudio> TelegramAudios { get; set; }

        public DbSet<TelegramAuthorizationRequestParameters> TelegramAuthorizationRequestParameters { get; set; }

        public DbSet<TelegramBackgroundFillFreeformGradient> TelegramBackgroundFillFreeformGradients { get; set; }

        public DbSet<TelegramBackgroundFillGradient> TelegramBackgroundFillGradients { get; set; }

        public DbSet<TelegramBackgroundFillSolid> TelegramBackgroundFillSolids { get; set; }


        public DbSet<TelegramBackgroundTypeChatTheme> TelegramBackgroundTypeChatThemes { get; set; }

        public DbSet<TelegramBackgroundTypeFill> TelegramBackgroundTypeFills { get; set; }

        public DbSet<TelegramBackgroundTypePattern> TelegramBackgroundTypePatterns { get; set; }

        public DbSet<TelegramBackgroundTypeWallpaper> TelegramBackgroundTypeWallpapers { get; set; }

        public DbSet<TelegramBirthdate> TelegramBirthdates { get; set; }

        public DbSet<TelegramBotCommand> TelegramBotCommands { get; set; }
        public DbSet<TelegramBotCommandScopeAllChatAdministrators> TelegramBotCommandScopesAllChatAdministrators { get; set; }
        public DbSet<TelegramBotCommandScopeAllGroupChats> TelegramBotCommandScopesAllGroupChatss { get; set; }
        public DbSet<TelegramBotCommandScopeAllPrivateChats> TelegramBotCommandScopesAllPrivateChatss { get; set; }
        public DbSet<TelegramBotCommandScopeChat> TelegramBotCommandScopesChat { get; set; }
        public DbSet<TelegramBotCommandScopeChatAdministrators> TelegramBotCommandScopesChatAdministrators { get; set; }
        public DbSet<TelegramBotCommandScopeChatMember> TelegramBotCommandScopesChatMember { get; set; }

        public DbSet<TelegramBotCommandScopeDefault> TelegramBotCommandScopesDefaults { get; set; }
        public DbSet<TelegramBotDescription> TelegramBotDescriptions { get; set; }

        public DbSet<TelegramBotName> TelegramBotNames { get; set; }

        public DbSet<TelegramBotShortDescription> TelegramBotShortDescription { get; set; }

        public DbSet<TelegramBusinessBotRights> TelegramBusinessBotRights { get; set; }

        public DbSet<TelegramBusinessMessagesDeleted> TelegramBusinessBusinessMessagesDeleteds { get; set; }

        public DbSet<TelegramBusinessConnection> TelegramBusinessConnections { get; set; }

        public DbSet<TelegramBusinessIntro> TelegramBusinessIntros { get; set; }

        public DbSet<TelegramBusinessLocation> TelegramBusinessLocations { get; set; }

        public DbSet<TelegramBusinessOpeningHours> TelegramBusinessOpeningHours { get; set; }

        public DbSet<TelegramBusinessOpeningHoursInterval> TelegramBusinessOpeningHoursIntervals { get; set; }

        public DbSet<TelegramCallbackGame> TelegramCallbackGames { get; set; }

        public DbSet<TelegramCallbackQuery> TelegramCallbackQueries { get; set; }
        public DbSet<TelegramChatAdministratorRights> TelegramChatAdministratorRights { get; set; }
        public DbSet<TelegramChatBackground> TelegramChatBackgrounds { get; set; }
        public DbSet<TelegramChatBoostRemoved> TelegramChatBoostRemoves { get; set; }
        public DbSet<TelegramChatBoost> TelegramChatBoosts { get; set; }

        public DbSet<TelegramChatBoostUpdated> TelegramChatBoostUpdates { get; set; }
        public DbSet<TelegramChatBotRightsUser> TelegramChatBotRightsUsers { get; set; }
        public DbSet<TelegramChatFullInfo> TelegramChatFullInfos { get; set; }
        public DbSet<TelegramChatId> TelegramChatIds { get; set; }
        public DbSet<TelegramChatInviteLink> TelegramChatInviteLinks { get; set; }

        public DbSet<TelegramChatJoinRequest> TelegramChatJoinRequests { get; set; }

        public DbSet<TelegramChatLocation> TelegramChatLocations { get; set; }
        public DbSet<TelegramChatMemberAdministrator> TelegramChatMemberAdministrators { get; set; }
        public DbSet<TelegramChatMemberMember> TelegramChatMemberMembers { get; set; }

        public DbSet<TelegramChatMemberOwner> TelegramChatMemberOwners { get; set; }
        public DbSet<TelegramChatMemberBanned> TelegramChatMembersBanned { get; set; }
        public DbSet<TelegramChatMemberLeft> TelegramChatMembersLeft { get; set; }
        public DbSet<TelegramChatMemberRestricted> TelegramChatMembersRestricted { get; set; }

        public DbSet<TelegramChatMemberUpdated> TelegramChatMemberUpdates { get; set; }

        public DbSet<TelegramChatPermissions> TelegramChatPermissions { get; set; }

        public DbSet<TelegramChatPhoto> TelegramChatPhotos { get; set; }

        public DbSet<TelegramChat> TelegramChats { get; set; }

        public DbSet<TelegramChatShared> TelegramChatShared { get; set; }

        public DbSet<TelegramChosenInlineResult> TelegramChosenInlineResults { get; set; }

        public DbSet<TelegramContact> TelegramContacts { get; set; }
        public DbSet<TelegramCopyTextButton> TelegramCopyTextButtons { get; set; }
        public DbSet<TelegramCredentials> TelegramCredentialss { get; set; }
        public DbSet<TelegramDataCredentials> TelegramDataCredentialss { get; set; }
        public DbSet<TelegramDice> TelegramDices { get; set; }

        public DbSet<TelegramDocument> TelegramDocuments { get; set; }

        public DbSet<TelegramEncryptedCredentials> TelegramEncryptedCredentials { get; set; }

        public DbSet<TelegramEncryptedPassportElement> TelegramEncryptedPassportElements { get; set; }

        public DbSet<TelegramExternalReplyInfo> TelegramExternalReplyInfos { get; set; }

        public DbSet<TelegramFileCredentials> TelegramFileCredentials { get; set; }

        public DbSet<TelegramForceReplyMarkup> TelegramForceReplyMarkups { get; set; }
        public DbSet<TelegramForumTopic> TelegramForumTopics { get; set; }

        public DbSet<TelegramForumTopicCreated> TelegramForumTopicsCreated { get; set; }

        public DbSet<TelegramForumTopicEdited> TelegramForumTopicsEdited { get; set; }
        public DbSet<TelegramGameHighScore> TelegramGameHighScores { get; set; }

        public DbSet<TelegramGame> TelegramGames { get; set; }


        public DbSet<TelegramGiftInfo> TelegramGiftInfos { get; set; }

        public DbSet<TelegramGiftList> TelegramGiftLists { get; set; }

        public DbSet<TelegramGift> TelegramGifts { get; set; }

        public DbSet<TelegramGiveaway> TelegramGiveaways { get; set; }

        public DbSet<TelegramGiveawayCompleted> TelegramGiveawaysCompleted { get; set; }

        public DbSet<TelegramGiveawayCreated> TelegramGiveawaysCreated { get; set; }

        public DbSet<TelegramGiveawayWinners> TelegramGiveawaysWinners { get; set; }

        public DbSet<TelegramInlineKeyboardButton> TelegramInlineKeyboardButtons { get; set; }

        public DbSet<TelegramInlineKeyboardMarkup> TelegramInlineKeyboardMarkups { get; set; }

        public DbSet<TelegramInlineKeyboardRow> TelegramInlineKeyboardRows { get; set; }

        public DbSet<TelegramInlineQueryResultArticle> TelegramInlineQueryResultArticles { get; set; }
        public DbSet<TelegramInlineQueryResultAudio> TelegramInlineQueryResultAudios { get; set; }
        public DbSet<TelegramInlineQueryResultCachedAudio> TelegramInlineQueryResultCachedAudios { get; set; }
        public DbSet<TelegramInlineQueryResultCachedDocument> TelegramInlineQueryResultCachedDocuments { get; set; }
        public DbSet<TelegramInlineQueryResultCachedGif> TelegramInlineQueryResultCachedGifs { get; set; }
        public DbSet<TelegramInlineQueryResultCachedMpeg4Gif> TelegramInlineQueryResultCachedMpeg4Gifs { get; set; }
        public DbSet<TelegramInlineQueryResultCachedPhoto> TelegramInlineQueryResultCachedPhotos { get; set; }
        public DbSet<TelegramInlineQueryResultCachedSticker> TelegramInlineQueryResultCachedStickers { get; set; }
        public DbSet<TelegramInlineQueryResultCachedVideo> TelegramInlineQueryResultCachedVideos { get; set; }
        public DbSet<TelegramInlineQueryResultCachedVoice> TelegramInlineQueryResultCachedVoices { get; set; }
        public DbSet<TelegramInlineQueryResultContact> TelegramInlineQueryResultContacts { get; set; }
        public DbSet<TelegramInlineQueryResultDocument> TelegramInlineQueryResultDocuments { get; set; }
        public DbSet<TelegramInlineQueryResultGame> TelegramInlineQueryResultGames { get; set; }
        public DbSet<TelegramInlineQueryResultGif> TelegramInlineQueryResultGifs { get; set; }
        public DbSet<TelegramInlineQueryResultLocation> TelegramInlineQueryResultLocations { get; set; }
        public DbSet<TelegramInlineQueryResultMpeg4Gif> TelegramInlineQueryResultMpeg4Gifs { get; set; }
        public DbSet<TelegramInlineQueryResultPhoto> TelegramInlineQueryResultPhotos { get; set; }
        public DbSet<TelegramInlineQueryResultVenue> TelegramInlineQueryResultVenues { get; set; }
        public DbSet<TelegramInlineQueryResultVideo> TelegramInlineQueryResultVideos { get; set; }

        public DbSet<TelegramInlineQueryResultVoice> TelegramInlineQueryResultVoices { get; set; }
        public DbSet<TelegramInlineQuery> TelegramInlineQuerys { get; set; }
        public DbSet<TelegramInputContactMessageContent> TelegramInputContactMessageContents { get; set; }


        public DbSet<TelegramInputFileId> TelegramInputFileIds { get; set; }

        public DbSet<TelegramInputFileStream> TelegramInputFileStreams { get; set; }

        public DbSet<TelegramInputFileUrl> TelegramInputFileUrls { get; set; }
        public DbSet<TelegramInputInvoiceMessageContent> TelegramInputInvoiceMessageContents { get; set; }
        public DbSet<TelegramInputLocationMessageContent> TelegramInputLocationMessageContents { get; set; }


        public DbSet<TelegramInputMediaAnimation> TelegramInputMediaAnimations { get; set; }

        public DbSet<TelegramInputMediaAudio> TelegramInputMediaAudios { get; set; }

        public DbSet<TelegramInputMediaDocument> TelegramInputMediaDocuments { get; set; }

        public DbSet<TelegramInputMediaPhoto> TelegramInputMediaPhotos { get; set; }

        public DbSet<TelegramInputMediaVideo> TelegramInputMediaVideos { get; set; }
        public DbSet<TelegramInputPaidMediaPhoto> TelegramInputPaidMediaPhotos { get; set; }
        public DbSet<TelegramInputPaidMediaVideo> TelegramInputPaidMediaVideos { get; set; }
        public DbSet<TelegramInputPollOption> TelegramInputPollOptions { get; set; }
        public DbSet<TelegramInputProfilePhotoAnimated> TelegramInputProfilePhotoAnimateds { get; set; }

        public DbSet<TelegramInputProfilePhotoStatic> TelegramInputProfilePhotoStatics { get; set; }
        public DbSet<TelegramInputSticker> TelegramInputStickers { get; set; }

        public DbSet<TelegramInputStoryContentPhoto> TelegramInputStoryContentPhotos { get; set; }
        public DbSet<TelegramInputStoryContentVideo> TelegramInputStoryContentVideos { get; set; }

        public DbSet<TelegramInputTextMessageContent> TelegramInputTextMessageContents { get; set; }
        public DbSet<TelegramInputVenueMessageContent> TelegramInputVenueMessageContents { get; set; }


        public DbSet<TelegramInvoice> TelegramInvoices { get; set; }

        public DbSet<TelegramKeyboardButtonPollType> TelegramKeyboardButtonPollTypes { get; set; }

        public DbSet<TelegramKeyboardButtonRequestChat> TelegramKeyboardButtonRequestChats { get; set; }

        public DbSet<TelegramKeyboardButtonRequestUsers> TelegramKeyboardButtonRequestUsers { get; set; }

        public DbSet<TelegramKeyboardButton> TelegramKeyboardButtons { get; set; }

        public DbSet<TelegramKeyboardRow> TelegramKeyboardRows { get; set; }

        public DbSet<TelegramLabeledPrice> TelegramLabeledPrices { get; set; }

        public DbSet<TelegramLinkPreviewOptions> TelegramLinkPreviewOptionss { get; set; }

        public DbSet<TelegramLocationAddress> TelegramLocationAddresses { get; set; }

        public DbSet<TelegramLocation> TelegramLocations { get; set; }
        public DbSet<TelegramLoginUrl> TelegramLoginUrls { get; set; }
        public DbSet<TelegramMaskPosition> TelegramMaskPositions { get; set; }

        public DbSet<TelegramMenuButtonCommands> TelegramMenuButtonCommandss { get; set; }
        public DbSet<TelegramMenuButtonDefault> TelegramMenuButtonDefaults { get; set; }
        public DbSet<TelegramMenuButtonWebApp> TelegramMenuButtonWebApps { get; set; }

        public DbSet<TelegramMessageAutoDeleteTimerChanged> TelegramMessageAutoDeleteTimerChanged { get; set; }
        public DbSet<TelegramMessageEntity> TelegramMessageEntitys { get; set; }
        public DbSet<TelegramMessageId> TelegramMessageIds { get; set; }
        public DbSet<TelegramMessageOriginChannel> TelegramMessageOriginChannels { get; set; }

        public DbSet<TelegramMessageOriginChat> TelegramMessageOriginChats { get; set; }

        public DbSet<TelegramMessageOriginHiddenUser> TelegramMessageOriginHiddenUsers { get; set; }

        public DbSet<TelegramMessageOriginUser> TelegramMessageOriginUsers { get; set; }

        public DbSet<TelegramMessageReactionCountUpdated> TelegramMessageReactionCountUpdates { get; set; }

        public DbSet<TelegramMessageReactionUpdated> TelegramMessageReactionUpdates { get; set; }

        public DbSet<TelegramMessage> TelegramMessages { get; set; }

        public DbSet<TelegramOrderInfo> TelegramOrderInfos { get; set; }

        public DbSet<TelegramPaidMediaInfo> TelegramPaidMediaInfos { get; set; }

        public DbSet<TelegramPaidMediaPhoto> TelegramPaidMediaPhotos { get; set; }

        public DbSet<TelegramPaidMediaPreview> TelegramPaidMediaPreviews { get; set; }

        public DbSet<TelegramPaidMediaPurchased> TelegramPaidMediaPurchases { get; set; }

        public DbSet<TelegramPaidMediaVideo> TelegramPaidMediaVideos { get; set; }

        public DbSet<TelegramPaidMessagePriceChanged> TelegramPaidMessagePricesChanged { get; set; }

        public DbSet<TelegramPassportData> TelegramPassportDatas { get; set; }
        public DbSet<TelegramPassportElementErrorDataField> TelegramPassportElementErrorDataFields { get; set; }
        public DbSet<TelegramPassportElementErrorFile> TelegramPassportElementErrorFiles { get; set; }
        public DbSet<TelegramPassportElementErrorFiles> TelegramPassportElementErrorFiless { get; set; }
        public DbSet<TelegramPassportElementErrorFrontSide> TelegramPassportElementErrorFrontSides { get; set; }
        public DbSet<TelegramPassportElementErrorReverseSide> TelegramPassportElementErrorReverseSides { get; set; }
        public DbSet<TelegramPassportElementErrorSelfie> TelegramPassportElementErrorSelfies { get; set; }
        public DbSet<TelegramPassportElementErrorTranslationFile> TelegramPassportElementErrorTranslationFiles { get; set; }
        public DbSet<TelegramPassportElementErrorTranslationFiles> TelegramPassportElementErrorTranslationFiless { get; set; }
        public DbSet<TelegramPassportElementErrorUnspecified> TelegramPassportElementErrorUnspecifieds { get; set; }
        public DbSet<TelegramPassportFile> TelegramPassportFiles { get; set; }
        public DbSet<TelegramPassportScopeElementOneOfSeveral> TelegramPassportScopeElementOneOfSeverals { get; set; }
        public DbSet<TelegramPassportScope> TelegramPassportScopes { get; set; }
        public DbSet<TelegramPersonalDetails> TelegramPersonalDetailss { get; set; }
        public DbSet<TelegramPhotoSizeGroup> TelegramPhotoSizeGroups { get; set; }
        public DbSet<TelegramPhotoSize> TelegramPhotoSizes { get; set; }

        public DbSet<TelegramPollAnswer> TelegramPollAnswers { get; set; }

        public DbSet<TelegramPollOption> TelegramPollOptions { get; set; }

        public DbSet<TelegramPoll> TelegramPolls { get; set; }
        public DbSet<TelegramPreCheckoutQuery> TelegramPreCheckoutQueries { get; set; }

        public DbSet<TelegramPreparedInlineMessage> TelegramPreparedInlineMessages { get; set; }

        public DbSet<TelegramProximityAlertTriggered> TelegramProximityAlertTriggereds { get; set; }

        public DbSet<TelegramReactionCount> TelegramReactionCounts { get; set; }

        public DbSet<TelegramReactionTypeCustomEmoji> TelegramReactionTypeCustomEmojis { get; set; }

        public DbSet<TelegramReactionTypeEmoji> TelegramReactionTypeEmojis { get; set; }

        public DbSet<TelegramReactionTypePaid> TelegramReactionTypePaids { get; set; }

        public DbSet<TelegramRefundedPayment> TelegramRefundedPayments { get; set; }

        public DbSet<TelegramReplyKeyboardMarkup> TelegramReplyKeyboardMarkups { get; set; }

        public DbSet<TelegramReplyKeyboardRemove> TelegramReplyKeyboardRemoves { get; set; }
        public DbSet<TelegramReplyParameters> TelegramReplyParameterss { get; set; }
        public DbSet<TelegramResidentialAddress> TelegramResidentialAddresses { get; set; }

        public DbSet<TelegramRevenueWithdrawalStateFailed> TelegramRevenueWithdrawalStatesFailed { get; set; }
        public DbSet<TelegramRevenueWithdrawalStatePending> TelegramRevenueWithdrawalStatesPending { get; set; }

        public DbSet<TelegramRevenueWithdrawalStateSucceeded> TelegramRevenueWithdrawalStatesSucceeded { get; set; }
        public DbSet<TelegramSecureData> TelegramSecureDatas { get; set; }
        public DbSet<TelegramSecureValue> TelegramSecureValues { get; set; }
        public DbSet<TelegramSentWebAppMessage> TelegramSentWebAppMessages { get; set; }
        public DbSet<TelegramSharedUser> TelegramSharedUsers { get; set; }


        public DbSet<TelegramShippingAddress> TelegramShippingAddresses { get; set; }

        public DbSet<TelegramShippingOption> TelegramShippingOptions { get; set; }

        public DbSet<TelegramShippingQuery> TelegramShippingQueries { get; set; }

        public DbSet<TelegramStarAmount> TelegramStarAmounts { get; set; }

        public DbSet<TelegramStarTransaction> TelegramStarTransactions { get; set; }

        public DbSet<TelegramStarTransactions> TelegramStarTransactionsCollections { get; set; }

        public DbSet<TelegramSticker> TelegramStickers { get; set; }

        public DbSet<TelegramStickerSet> TelegramStickerSets { get; set; }

        public DbSet<TelegramStory> TelegramStories { get; set; }

        public DbSet<TelegramStoryAreaPosition> TelegramStoryAreaPositions { get; set; }

        public DbSet<TelegramStoryArea> TelegramStoryAreas { get; set; }

        public DbSet<TelegramStoryAreaTypeLink> TelegramStoryAreaTypeLinks { get; set; }

        public DbSet<TelegramStoryAreaTypeLocation> TelegramStoryAreaTypeLocations { get; set; }

        public DbSet<TelegramStoryAreaTypeSuggestedReaction> TelegramStoryAreaTypeSuggestedReactions { get; set; }

        public DbSet<TelegramStoryAreaTypeUniqueGift> TelegramStoryAreaTypeUniqueGifts { get; set; }

        public DbSet<TelegramStoryAreaTypeWeather> TelegramStoryAreaTypeWeathers { get; set; }

        public DbSet<TelegramSuccessfulPayment> TelegramSuccessfulPayments { get; set; }

        public DbSet<TelegramSwitchInlineQueryChosenChat> TelegramSwitchInlineQueryChosenChats { get; set; }

        public DbSet<TelegramPassportScopeElementOne> TelegramTelegramPassportScopeElementOne { get; set; }

        public DbSet<TelegramTextQuote> TelegramTextQuotes { get; set; }

        public DbSet<TelegramTGFile> TelegramTGFiles { get; set; }
        public DbSet<TelegramTransactionPartnerAffiliateProgram> TelegramTransactionPartnerAffiliatePrograms { get; set; }

        public DbSet<TelegramTransactionPartnerChat> TelegramTransactionPartnerChats { get; set; }
        public DbSet<TelegramTransactionPartnerFragment> TelegramTransactionPartnerFragments { get; set; }
        public DbSet<TelegramTransactionPartnerOther> TelegramTransactionPartnerOthers { get; set; }
        public DbSet<TelegramTransactionPartnerTelegramAds> TelegramTransactionPartnerTelegramAds { get; set; }
        public DbSet<TelegramTransactionPartnerTelegramApi> TelegramTransactionPartnerTelegramApis { get; set; }
        public DbSet<TelegramTransactionPartnerUser> TelegramTransactionPartnerUsers { get; set; }
        public DbSet<TelegramUniqueGiftBackdropColors> TelegramUniqueGiftBackdropColorss { get; set; }

        public DbSet<TelegramUniqueGiftBackdrop> TelegramUniqueGiftBackdrops { get; set; }

        public DbSet<TelegramUniqueGiftInfo> TelegramUniqueGiftInfos { get; set; }

        public DbSet<TelegramUniqueGiftModel> TelegramUniqueGiftModels { get; set; }

        public DbSet<TelegramUniqueGift> TelegramUniqueGifts { get; set; }

        public DbSet<TelegramUniqueGiftSymbol> TelegramUniqueGiftSymbols { get; set; }
        public DbSet<TelegramUserChatBoosts> TelegramUserChatBoostss { get; set; }
        public DbSet<TelegramUserChat> TelegramUserChats { get; set; }
        public DbSet<TelegramUserProfilePhotos> TelegramUserProfilePhotoss { get; set; }
        public DbSet<TelegramUser> TelegramUsers { get; set; }

        public DbSet<TelegramUsersShared> TelegramUsersShareds { get; set; }
        public DbSet<TelegramVenue> TelegramVenues { get; set; }
        public DbSet<TelegramVideoChatEnded> TelegramVideoChatsEnded { get; set; }

        public DbSet<TelegramVideoChatParticipantsInvited> TelegramVideoChatsParticipantsInvited { get; set; }

        public DbSet<TelegramVideoChatScheduled> TelegramVideoChatsScheduled { get; set; }

        public DbSet<TelegramVideoChatStarted> TelegramVideoChatsStarted { get; set; }

        public DbSet<TelegramVideoNote> TelegramVideoNotes { get; set; }

        public DbSet<TelegramVideo> TelegramVideos { get; set; }

        public DbSet<TelegramVoice> TelegramVoices { get; set; }

        public DbSet<TelegramWebAppData> TelegramWebAppDatas { get; set; }
        public DbSet<TelegramWebAppInfo> TelegramWebAppInfos { get; set; }
        public DbSet<TelegramWebhookInfo> TelegramWebhookInfos { get; set; }
        public DbSet<TelegramWriteAccessAllowed> TelegramWriteAccessAlloweds { get; set; }
    }
}
