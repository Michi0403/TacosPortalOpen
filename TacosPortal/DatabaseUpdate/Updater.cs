//-----------------------------------------------------------------------
// <copyright file="Updater.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.Base;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TacosCore.BusinessObjects;
using TacosCore.BusinessObjects.DataTypes;
using TacosCore.BusinessObjects.DataTypes.PermissionBaseObjects;
using TacosCore.BusinessObjects.DataTypes.TelegramTypes;
using TacosPortal.Services;

namespace TacosPortal.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion, IAmbientUserContext ambient) :
            base(objectSpace, currentDBVersion)
        {
            var ef = (EFCoreObjectSpace)objectSpace;
            var db = ef.DbContext;

            string? dbPrincipal = null;

            if (db.Database.IsSqlServer())
                dbPrincipal = db.Database.SqlQueryRaw<string>("SELECT SUSER_SNAME()").AsEnumerable().FirstOrDefault();

            else if (db.Database.IsSqlite())
                dbPrincipal = "sqlite";

            if (string.IsNullOrWhiteSpace(dbPrincipal))
                dbPrincipal = "DB-INIT";

            ambient.UserId = null;
            ambient.UserName = $"SQL:{dbPrincipal}";
        }

        private TacoPermissionPolicyRole CreateAdminRole()
        {
            TacoPermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<TacoPermissionPolicyRole>(
                r => r.Name == "Administrators");
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<TacoPermissionPolicyRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }
            return adminRole;
        }

        private TacoPermissionPolicyRole CreateDefaultRole()
        {
            TacoPermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<TacoPermissionPolicyRole>(
                role => role.Name == "Default");
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<TacoPermissionPolicyRole>();
                _ = defaultRole.Name = "Default";
                defaultRole.AddTypePermissionsRecursively<DatabaseLog>(
                             SecurityOperations.Read,
                             SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<DatabaseLog>(
                    SecurityOperations.Navigate,
                    SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<DatabaseLog>(
                   SecurityOperations.Create,
                   SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<DatabaseLog>(
                SecurityOperations.Write,
                SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<DatabaseLog>(
                    SecurityOperations.Delete,
                    SecurityPermissionState.Deny);
                _ = defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Read,
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                _ = defaultRole.AddNavigationPermission(
                    @"Application/NavigationItems/Items/Default/Items/MyDetails",
                    SecurityPermissionState.Allow);
                _ = defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Write,
                    "ChangePasswordOnFirstLogon",
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                _ = defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Write,
                    "StoredPassword",
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<TacoPermissionPolicyRole>(
                    SecurityOperations.Read,
                    SecurityPermissionState.Deny);


            }
            return defaultRole;
        }

        private TacoPermissionPolicyRole CreateTelegramAdminRole()
        {
            TacoPermissionPolicyRole telegramAdminRole = ObjectSpace.FirstOrDefault<TacoPermissionPolicyRole>(
                role => role.Name == "TelegramAdmin");
            if (telegramAdminRole == null)
            {
                telegramAdminRole = ObjectSpace.CreateObject<TacoPermissionPolicyRole>();
                telegramAdminRole.Name = "TelegramAdmin";
                telegramAdminRole.CanEditModel = false;
                telegramAdminRole.IsAdministrative = false;
                _ = telegramAdminRole.AddObjectPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Read,
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                _ = telegramAdminRole.AddNavigationPermission(
                    @"Application/NavigationItems/Items/Default/Items/MyDetails",
                    SecurityPermissionState.Allow);
                _ = telegramAdminRole.AddMemberPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Write,
                    "ChangePasswordOnFirstLogon",
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                _ = telegramAdminRole.AddMemberPermissionFromLambda<ApplicationUser>(
                    SecurityOperations.Write,
                    "StoredPassword",
                    cm => cm.ID == (Guid)CurrentUserIdOperator.CurrentUserId(),
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TacoPermissionPolicyRole>(
                    SecurityOperations.Read,
                    SecurityPermissionState.Deny);
                telegramAdminRole.AddTypePermissionsRecursively<TacoTeam>(
   SecurityOperations.FullAccess,
   SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<DatabaseLog>(
                    SecurityOperations.Read,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<DatabaseLog>(
                    SecurityOperations.Navigate,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<DatabaseLog>(
                   SecurityOperations.Create,
                   SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<DatabaseLog>(
                SecurityOperations.Write,
                SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<DatabaseLog>(
                    SecurityOperations.Delete,
                    SecurityPermissionState.Deny);
                telegramAdminRole.AddTypePermissionsRecursively<ApplicationPushSubscription>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TacoTeamChat>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramAcceptedGiftTypes>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramAffiliateInfo>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramAnimation>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramApiResponse>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramAudio>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramAuthorizationRequestParameters>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundFillFreeformGradient>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundFillGradient>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundFillSolid>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundTypeChatTheme>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundTypeFill>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundTypePattern>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBackgroundTypeWallpaper>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBirthdate>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommand>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeDefault>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeAllPrivateChats>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeAllGroupChats>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeAllChatAdministrators>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeChat>(
        SecurityOperations.FullAccess,
        SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeChatAdministrators>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotCommandScopeChatMember>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotDescription>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotName>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBotShortDescription>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessBotRights>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessMessagesDeleted>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessConnection>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessIntro>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessLocation>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessOpeningHours>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramBusinessOpeningHoursInterval>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramCallbackGame>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramCallbackQuery>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChat>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatAdministratorRights>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatBotRightsUser>(
  SecurityOperations.FullAccess,
  SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatBackground>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatBoost>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatBoostRemoved>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatBoostUpdated>(
      SecurityOperations.FullAccess,
      SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatFullInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatId>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);


                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatInviteLink>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatJoinRequest>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatLocation>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberOwner>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberAdministrator>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberMember>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberRestricted>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberLeft>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberBanned>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatMemberUpdated>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatPermissions>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatPhoto>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChatShared>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramChosenInlineResult>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramContact>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramCopyTextButton>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramCredentials>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramDataCredentials>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramDice>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramDocument>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramEncryptedCredentials>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramEncryptedPassportElement>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramExternalReplyInfo>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramFileCredentials>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramForceReplyMarkup>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramForumTopic>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramForumTopicCreated>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramForumTopicEdited>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGame>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGameHighScore>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGift>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiftInfo>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiftList>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiveaway>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiveawayCompleted>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiveawayCreated>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramGiveawayWinners>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineKeyboardButton>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineKeyboardMarkup>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineKeyboardRow>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultArticle>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultPhoto>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultGif>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultMpeg4Gif>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultVideo>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultAudio>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultVoice>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultDocument>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultLocation>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultVenue>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultContact>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultGame>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedPhoto>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedGif>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedMpeg4Gif>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedSticker>(
                    SecurityOperations.FullAccess,
                    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedDocument>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedVideo>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedVoice>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQueryResultCachedAudio>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInlineQuery>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputFileId>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputFileStream>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputFileUrl>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputMediaAnimation>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputMediaAudio>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputMediaDocument>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputMediaPhoto>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputMediaVideo>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputTextMessageContent>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputVenueMessageContent>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputContactMessageContent>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);

                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputInvoiceMessageContent>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputPaidMediaPhoto>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputPaidMediaVideo>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputPollOption>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputProfilePhotoStatic>(
    SecurityOperations.FullAccess,
    SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputProfilePhotoAnimated>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputSticker>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputStoryContentPhoto>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInputStoryContentVideo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramInvoice>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramKeyboardButtonPollType>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramKeyboardButtonRequestChat>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramKeyboardButtonRequestUsers>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramKeyboardButton>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramKeyboardRow>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramLabeledPrice>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramLinkPreviewOptions>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramLocationAddress>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramLocation>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramLoginUrl>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMaskPosition>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMenuButtonCommands>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMenuButtonWebApp>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMenuButtonDefault>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessage>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageAutoDeleteTimerChanged>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageEntity>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageId>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageOriginChannel>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageOriginChat>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageOriginHiddenUser>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageOriginUser>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMediaInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramMessageReactionUpdated>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramOrderInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMediaPhoto>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMediaPreview>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMediaPurchased>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMediaVideo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPaidMessagePriceChanged>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportData>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportFile>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorDataField>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorFrontSide>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorReverseSide>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorSelfie>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorFile>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorFiles>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorTranslationFile>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorTranslationFiles>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportElementErrorUnspecified>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportScope>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPersonalDetails>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPhotoSize>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPollAnswer>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPollOption>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPoll>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPhotoSizeGroup>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPreCheckoutQuery>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPreparedInlineMessage>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramProximityAlertTriggered>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReactionCount>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReactionTypeCustomEmoji>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReactionTypeEmoji>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReactionTypePaid>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramRefundedPayment>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReplyKeyboardMarkup>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReplyKeyboardRemove>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramReplyParameters>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramResidentialAddress>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramRevenueWithdrawalStatePending>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramRevenueWithdrawalStateSucceeded>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramRevenueWithdrawalStateFailed>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportScopeElementOneOfSeveral>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramPassportScopeElementOne>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSecureData>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSecureValue>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSentWebAppMessage>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSharedUser>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramShippingAddress>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramShippingOption>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramShippingQuery>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStarAmount>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStarTransaction>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStarTransactions>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSticker>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStickerSet>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStory>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaPosition>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryArea>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaTypeLink>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaTypeLocation>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaTypeSuggestedReaction>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaTypeUniqueGift>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramStoryAreaTypeWeather>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSuccessfulPayment>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramSwitchInlineQueryChosenChat>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTextQuote>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTGFile>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerUser>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerChat>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerAffiliateProgram>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerFragment>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerTelegramAds>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerTelegramApi>(
SecurityOperations.FullAccess,

SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramTransactionPartnerOther>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGiftBackdropColors>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGiftBackdrop>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGiftInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGiftModel>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGift>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUniqueGiftSymbol>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUser>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUserChat>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUsersShared>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUserProfilePhotos>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramUserChatBoosts>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVenue>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideoChatEnded>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideoChatParticipantsInvited>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideoChatScheduled>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideoChatStarted>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideoNote>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVideo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramVoice>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramWebAppData>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramWebAppInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramWebhookInfo>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);
                telegramAdminRole.AddTypePermissionsRecursively<TelegramWriteAccessAllowed>(
SecurityOperations.FullAccess,
SecurityPermissionState.Allow);

            }
            return telegramAdminRole;
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
#if !RELEASE
            var defaultRole = CreateDefaultRole();
            var adminRole = CreateAdminRole();
            var telegramAdminRole = CreateTelegramAdminRole();

            ObjectSpace.CommitChanges();

            UserManager userManager = ObjectSpace.ServiceProvider.GetRequiredService<UserManager>();

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "User") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "User",
                    "124308548zSHJAFaOFSUHI()!",
                    (user) => user.Roles.Add(defaultRole));
            }

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "Admin") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "Admin",
                    "a789jsbd34abnsdb=22§§(j:M",
                    (user) => user.Roles.Add(adminRole));
            }
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "sense") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "sense",
                    "SenseMilla_1901",
                    (user) => user.Roles.Add(adminRole));
            }
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "TelegramAdminExample") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "TelegramAdminExample",
                    "asd)jknba%kjnf203854025mk&LNKJ",
                    (user) => user.Roles.Add(telegramAdminRole));
            }

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "TelegramBot") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "TelegramBot",
                    "afsdkujzhvbO%!GTgzoulDAS232$%&",
                    (user) => user.Roles.Add(telegramAdminRole));
            }

            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "TelegramBotDebugTest") == null)
            {
                string EmptyPassword = string.Empty;
                _ = userManager.CreateUser<ApplicationUser>(
                    ObjectSpace,
                    "TelegramBotDebugTest",
                    "asd348kljnasdoikn)($OeMJNADvxcS",
                    (user) => user.Roles.Add(telegramAdminRole));
            }
            ObjectSpace.CommitChanges();
#endif
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
    }
}
