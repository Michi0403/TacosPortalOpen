using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TacosPortal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtcTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TacoPermissionPolicyUser",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ChangePasswordOnFirstLogon = table.Column<bool>(type: "bit", nullable: false),
                    StoredPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: true),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoPermissionPolicyUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramAcceptedGiftTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnlimitedGifts = table.Column<bool>(type: "bit", nullable: false),
                    LimitedGifts = table.Column<bool>(type: "bit", nullable: false),
                    UniqueGifts = table.Column<bool>(type: "bit", nullable: false),
                    PremiumSubscription = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAcceptedGiftTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBackgroundFill",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Colors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopColor = table.Column<int>(type: "int", nullable: true),
                    BottomColor = table.Column<int>(type: "int", nullable: true),
                    RotationAngle = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBackgroundFill", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBirthdates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBirthdates", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommands",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Command = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesAllChatAdministrators",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesAllChatAdministrators", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesAllGroupChatss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesAllGroupChatss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesAllPrivateChatss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesAllPrivateChatss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesDefaults",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesDefaults", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotDescriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotDescriptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotNames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotShortDescription",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotShortDescription", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessBotRights",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanReply = table.Column<bool>(type: "bit", nullable: false),
                    CanReadMessages = table.Column<bool>(type: "bit", nullable: false),
                    CanDeleteSentMessages = table.Column<bool>(type: "bit", nullable: false),
                    CanDeleteAllMessages = table.Column<bool>(type: "bit", nullable: false),
                    CanEditName = table.Column<bool>(type: "bit", nullable: false),
                    CanEditBio = table.Column<bool>(type: "bit", nullable: false),
                    CanEditProfilePhoto = table.Column<bool>(type: "bit", nullable: false),
                    CanEditUsername = table.Column<bool>(type: "bit", nullable: false),
                    CanChangeGiftSettings = table.Column<bool>(type: "bit", nullable: false),
                    CanViewGiftsAndStars = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertGiftsToStars = table.Column<bool>(type: "bit", nullable: false),
                    CanTransferAndUpgradeGifts = table.Column<bool>(type: "bit", nullable: false),
                    CanTransferStars = table.Column<bool>(type: "bit", nullable: false),
                    CanManageStories = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessBotRights", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessOpeningHours",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessOpeningHours", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramCallbackGames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramCallbackGames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatAdministratorRights",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CanManageChat = table.Column<bool>(type: "bit", nullable: false),
                    CanDeleteMessages = table.Column<bool>(type: "bit", nullable: false),
                    CanManageVideoChats = table.Column<bool>(type: "bit", nullable: false),
                    CanRestrictMembers = table.Column<bool>(type: "bit", nullable: false),
                    CanPromoteMembers = table.Column<bool>(type: "bit", nullable: false),
                    CanChangeInfo = table.Column<bool>(type: "bit", nullable: false),
                    CanInviteUsers = table.Column<bool>(type: "bit", nullable: false),
                    CanPostStories = table.Column<bool>(type: "bit", nullable: false),
                    CanEditStories = table.Column<bool>(type: "bit", nullable: false),
                    CanDeleteStories = table.Column<bool>(type: "bit", nullable: false),
                    CanPostMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanEditMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanPinMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanManageTopics = table.Column<bool>(type: "bit", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatAdministratorRights", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatIds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatIds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatPermissions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanSendMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanSendAudios = table.Column<bool>(type: "bit", nullable: true),
                    CanSendDocuments = table.Column<bool>(type: "bit", nullable: true),
                    CanSendPhotos = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVideos = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVideoNotes = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVoiceNotes = table.Column<bool>(type: "bit", nullable: true),
                    CanSendPolls = table.Column<bool>(type: "bit", nullable: true),
                    CanSendOtherMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanAddWebPagePreviews = table.Column<bool>(type: "bit", nullable: true),
                    CanChangeInfo = table.Column<bool>(type: "bit", nullable: true),
                    CanInviteUsers = table.Column<bool>(type: "bit", nullable: true),
                    CanPinMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanManageTopics = table.Column<bool>(type: "bit", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatPermissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SmallFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmallFileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BigFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BigFileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatPhotos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsForum = table.Column<bool>(type: "bit", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatShared",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatShared", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramContacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Vcard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramContacts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramCopyTextButtons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramCopyTextButtons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramDataCredentialss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramDataCredentialss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramDices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramDices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramEncryptedCredentials",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramEncryptedCredentials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramFileCredentials",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramFileCredentials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramForceReplyMarkups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForceReply = table.Column<bool>(type: "bit", nullable: false),
                    InputFieldPlaceholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selective = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramForceReplyMarkups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramForumTopics",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageThreadId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconColor = table.Column<int>(type: "int", nullable: false),
                    IconCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramForumTopics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramForumTopicsCreated",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconColor = table.Column<int>(type: "int", nullable: false),
                    IconCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramForumTopicsCreated", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramForumTopicsEdited",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramForumTopicsEdited", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiftLists",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiftLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiveaways",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnersSelectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinnerCount = table.Column<int>(type: "int", nullable: false),
                    OnlyNewMembers = table.Column<bool>(type: "bit", nullable: false),
                    HasPublicWinners = table.Column<bool>(type: "bit", nullable: false),
                    PrizeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrizeStarCount = table.Column<int>(type: "int", nullable: true),
                    PremiumSubscriptionMonthCount = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiveaways", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiveawaysCreated",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrizeStarCount = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiveawaysCreated", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineKeyboardMarkups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineKeyboardMarkups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputFile",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputFile", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputPollOptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextParseMode = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputPollOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInvoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInvoices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramKeyboardButtonPollTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramKeyboardButtonPollTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramKeyboardButtonRequestUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserIsBot = table.Column<bool>(type: "bit", nullable: true),
                    UserIsPremium = table.Column<bool>(type: "bit", nullable: true),
                    MaxQuantity = table.Column<int>(type: "int", nullable: true),
                    RequestName = table.Column<bool>(type: "bit", nullable: false),
                    RequestUsername = table.Column<bool>(type: "bit", nullable: false),
                    RequestPhoto = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramKeyboardButtonRequestUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramLinkPreviewOptionss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferSmallMedia = table.Column<bool>(type: "bit", nullable: false),
                    PreferLargeMedia = table.Column<bool>(type: "bit", nullable: false),
                    ShowAboveText = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramLinkPreviewOptionss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramLocationAddresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramLocationAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    HorizontalAccuracy = table.Column<double>(type: "float", nullable: true),
                    LivePeriod = table.Column<int>(type: "int", nullable: true),
                    Heading = table.Column<int>(type: "int", nullable: true),
                    ProximityAlertRadius = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramLocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramLoginUrls",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForwardText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BotUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestWriteAccess = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramLoginUrls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMaskPositions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    XShift = table.Column<double>(type: "float", nullable: false),
                    YShift = table.Column<double>(type: "float", nullable: false),
                    Scale = table.Column<double>(type: "float", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMaskPositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMenuButtonCommandss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMenuButtonCommandss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMenuButtonDefaults",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMenuButtonDefaults", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageAutoDeleteTimerChanged",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageAutoDeleteTime = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageAutoDeleteTimerChanged", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageIds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageIdId = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageIds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPaidMediaInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StarCount = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPaidMediaInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPaidMessagePricesChanged",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaidMessageStarCount = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPaidMessagePricesChanged", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorDataFields",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorDataFields", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorFiless",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHashes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorFiless", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorFrontSides",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorFrontSides", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorReverseSides",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorReverseSides", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorSelfies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorSelfies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorTranslationFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorTranslationFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorTranslationFiless",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileHashes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorTranslationFiless", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportElementErrorUnspecifieds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportElementErrorUnspecifieds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportScopes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    V = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportScopes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPersonalDetailss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidenceCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameNative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameNative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleNameNative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPersonalDetailss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPolls",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PollId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalVoterCount = table.Column<int>(type: "int", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    PollType = table.Column<int>(type: "int", nullable: false),
                    AllowsMultipleAnswers = table.Column<bool>(type: "bit", nullable: false),
                    CorrectOptionId = table.Column<int>(type: "int", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenPeriod = table.Column<int>(type: "int", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPolls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPreparedInlineMessages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreparedInlineMessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPreparedInlineMessages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramRefundedPayments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    InvoicePayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelegramPaymentChargeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderPaymentChargeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramRefundedPayments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramReplyKeyboardMarkups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPersistent = table.Column<bool>(type: "bit", nullable: false),
                    ResizeKeyboard = table.Column<bool>(type: "bit", nullable: false),
                    OneTimeKeyboard = table.Column<bool>(type: "bit", nullable: false),
                    InputFieldPlaceholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Selective = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReplyKeyboardMarkups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramReplyKeyboardRemoves",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoveKeyboard = table.Column<bool>(type: "bit", nullable: false),
                    Selective = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReplyKeyboardRemoves", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramResidentialAddresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramResidentialAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramResponseParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MigrateToChatId = table.Column<long>(type: "bigint", nullable: true),
                    RetryAfter = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramResponseParameters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramRevenueWithdrawalState",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramRevenueWithdrawalState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSentWebAppMessages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InlineMessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSentWebAppMessages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSharedUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSharedUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramShippingAddresses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramShippingAddresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramShippingOptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramShippingOptionsId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramShippingOptions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStarAmounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NanostarAmount = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStarAmounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStarTransactionsCollections",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStarTransactionsCollections", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStoryAreaPositions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XPercentage = table.Column<double>(type: "float", nullable: false),
                    YPercentage = table.Column<double>(type: "float", nullable: false),
                    WidthPercentage = table.Column<double>(type: "float", nullable: false),
                    HeightPercentage = table.Column<double>(type: "float", nullable: false),
                    RotationAngle = table.Column<double>(type: "float", nullable: false),
                    CornerRadiusPercentage = table.Column<double>(type: "float", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStoryAreaPositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSwitchInlineQueryChosenChats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowUserChats = table.Column<bool>(type: "bit", nullable: false),
                    AllowBotChats = table.Column<bool>(type: "bit", nullable: false),
                    AllowGroupChats = table.Column<bool>(type: "bit", nullable: false),
                    AllowChannelChats = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSwitchInlineQueryChosenChats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramTextQuotes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsManual = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramTextQuotes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramTGFiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramTGFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGiftBackdropColorss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterColor = table.Column<int>(type: "int", nullable: false),
                    EdgeColor = table.Column<int>(type: "int", nullable: false),
                    SymbolColor = table.Column<int>(type: "int", nullable: false),
                    TextColor = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGiftBackdropColorss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUserChatBoostss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUserChatBoostss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUserProfilePhotoss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUserProfilePhotoss", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUsersShareds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUsersShareds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideoChatsEnded",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideoChatsEnded", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideoChatsParticipantsInvited",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideoChatsParticipantsInvited", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideoChatsScheduled",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideoChatsScheduled", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideoChatsStarted",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideoChatsStarted", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVoices", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramWebAppDatas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramWebAppDatas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramWebAppInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramWebAppInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramWebhookInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasCustomCertificate = table.Column<bool>(type: "bit", nullable: false),
                    PendingUpdateCount = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastErrorDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSynchronizationErrorDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxConnections = table.Column<int>(type: "int", nullable: true),
                    AllowedUpdates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramWebhookInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramWriteAccessAlloweds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromRequest = table.Column<bool>(type: "bit", nullable: false),
                    WebAppName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromAttachmentMenu = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramWriteAccessAlloweds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPushSubscriptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    P256dh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Auth = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPushSubscriptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApplicationPushSubscriptions_TacoPermissionPolicyUser_UserID",
                        column: x => x.UserID,
                        principalTable: "TacoPermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsersLoginInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProviderName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProviderUserKey = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserForeignKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsersLoginInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApplicationUsersLoginInfos_TacoPermissionPolicyUser_UserForeignKey",
                        column: x => x.UserForeignKey,
                        principalTable: "TacoPermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessOpeningHoursIntervals",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpeningMinute = table.Column<int>(type: "int", nullable: false),
                    ClosingMinute = table.Column<int>(type: "int", nullable: false),
                    TelegramBusinessOpeningHoursID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessOpeningHoursIntervals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBusinessOpeningHoursIntervals_TelegramBusinessOpeningHours_TelegramBusinessOpeningHoursID",
                        column: x => x.TelegramBusinessOpeningHoursID,
                        principalTable: "TelegramBusinessOpeningHours",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyRoleBase",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdministrative = table.Column<bool>(type: "bit", nullable: false),
                    CanEditModel = table.Column<bool>(type: "bit", nullable: false),
                    PermissionPolicy = table.Column<int>(type: "int", nullable: false),
                    IsAllowPermissionPriority = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    ChatAdministratorRightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyRoleBase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyRoleBase_TelegramChatAdministratorRights_ChatAdministratorRightsID",
                        column: x => x.ChatAdministratorRightsID,
                        principalTable: "TelegramChatAdministratorRights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramKeyboardButtonRequestChats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ChatIsChannel = table.Column<bool>(type: "bit", nullable: false),
                    ChatIsForum = table.Column<bool>(type: "bit", nullable: true),
                    ChatHasUsername = table.Column<bool>(type: "bit", nullable: true),
                    ChatIsCreated = table.Column<bool>(type: "bit", nullable: false),
                    UserAdministratorRightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BotAdministratorRightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BotIsMember = table.Column<bool>(type: "bit", nullable: false),
                    RequestTitle = table.Column<bool>(type: "bit", nullable: false),
                    RequestUsername = table.Column<bool>(type: "bit", nullable: false),
                    RequestPhoto = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramKeyboardButtonRequestChats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtonRequestChats_TelegramChatAdministratorRights_BotAdministratorRightsID",
                        column: x => x.BotAdministratorRightsID,
                        principalTable: "TelegramChatAdministratorRights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtonRequestChats_TelegramChatAdministratorRights_UserAdministratorRightsID",
                        column: x => x.UserAdministratorRightsID,
                        principalTable: "TelegramChatAdministratorRights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesChat",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatIdID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesChat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBotCommandScopesChat_TelegramChatIds_ChatIdID",
                        column: x => x.ChatIdID,
                        principalTable: "TelegramChatIds",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesChatAdministrators",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatIdID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesChatAdministrators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBotCommandScopesChatAdministrators_TelegramChatIds_ChatIdID",
                        column: x => x.ChatIdID,
                        principalTable: "TelegramChatIds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBotCommandScopesChatMember",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatIdID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBotCommandScopesChatMember", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBotCommandScopesChatMember_TelegramChatIds_ChatIdID",
                        column: x => x.ChatIdID,
                        principalTable: "TelegramChatIds",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramReplyParameterss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ChatIdID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AllowSendingWithoutReply = table.Column<bool>(type: "bit", nullable: false),
                    Quote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteParseMode = table.Column<int>(type: "int", nullable: false),
                    QuotePosition = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReplyParameterss", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramReplyParameterss_TelegramChatIds_ChatIdID",
                        column: x => x.ChatIdID,
                        principalTable: "TelegramChatIds",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessBusinessMessagesDeleteds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessBusinessMessagesDeleteds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBusinessBusinessMessagesDeleteds_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatsToIgnore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatsToIgnore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatsToIgnore_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiveawaysWinners",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayMessageId = table.Column<int>(type: "int", nullable: false),
                    WinnersSelectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinnerCount = table.Column<int>(type: "int", nullable: false),
                    AdditionalChatCount = table.Column<int>(type: "int", nullable: true),
                    PrizeStarCount = table.Column<int>(type: "int", nullable: true),
                    PremiumSubscriptionMonthCount = table.Column<int>(type: "int", nullable: true),
                    UnclaimedPrizeCount = table.Column<int>(type: "int", nullable: true),
                    OnlyNewMembers = table.Column<bool>(type: "bit", nullable: false),
                    WasRefunded = table.Column<bool>(type: "bit", nullable: false),
                    PrizeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiveawaysWinners", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramGiveawaysWinners_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageReactionCountUpdates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramMessageReactionCountUpdatedMessageID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageReactionCountUpdates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionCountUpdates_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStories_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportDatas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CredentialsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportDatas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPassportDatas_TelegramEncryptedCredentials_CredentialsID",
                        column: x => x.CredentialsID,
                        principalTable: "TelegramEncryptedCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSecureValues",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FrontSideID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReverseSideID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SelfieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSecureValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramSecureValues_TelegramDataCredentialss_DataID",
                        column: x => x.DataID,
                        principalTable: "TelegramDataCredentialss",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureValues_TelegramFileCredentials_FrontSideID",
                        column: x => x.FrontSideID,
                        principalTable: "TelegramFileCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramSecureValues_TelegramFileCredentials_ReverseSideID",
                        column: x => x.ReverseSideID,
                        principalTable: "TelegramFileCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramSecureValues_TelegramFileCredentials_SelfieID",
                        column: x => x.SelfieID,
                        principalTable: "TelegramFileCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatTelegramGiveaway",
                columns: table => new
                {
                    ChatsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiveawayThisChatsBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatTelegramGiveaway", x => new { x.ChatsID, x.GiveawayThisChatsBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramChatTelegramGiveaway_TelegramChats_ChatsID",
                        column: x => x.ChatsID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramChatTelegramGiveaway_TelegramGiveaways_GiveawayThisChatsBelongsToID",
                        column: x => x.GiveawayThisChatsBelongsToID,
                        principalTable: "TelegramGiveaways",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineKeyboardRows",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyKeyboardMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineKeyboardRows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardRows_TelegramInlineKeyboardMarkups_ReplyKeyboardMarkupID",
                        column: x => x.ReplyKeyboardMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMediaAnimations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    HasSpoiler = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMediaAnimations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaAnimations_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaAnimations_TelegramInputFile_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMediaAudios",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Performer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMediaAudios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaAudios_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaAudios_TelegramInputFile_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMediaDocuments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisableContentTypeDetection = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMediaDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaDocuments_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaDocuments_TelegramInputFile_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMediaPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    HasSpoiler = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMediaPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaPhotos_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMediaVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoverID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTimestamp = table.Column<int>(type: "int", nullable: true),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SupportsStreaming = table.Column<bool>(type: "bit", nullable: false),
                    HasSpoiler = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMediaVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaVideos_TelegramInputFile_CoverID",
                        column: x => x.CoverID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaVideos_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputMediaVideos_TelegramInputFile_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputPaidMediaPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputPaidMediaPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputPaidMediaPhotos_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputPaidMediaVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CoverID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTimestamp = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SupportsStreaming = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputPaidMediaVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputPaidMediaVideos_TelegramInputFile_CoverID",
                        column: x => x.CoverID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputPaidMediaVideos_TelegramInputFile_MediaID",
                        column: x => x.MediaID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInputPaidMediaVideos_TelegramInputFile_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputProfilePhotoAnimateds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainFrameTimestamp = table.Column<double>(type: "float", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputProfilePhotoAnimateds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputProfilePhotoAnimateds_TelegramInputFile_AnimationID",
                        column: x => x.AnimationID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputProfilePhotoStatics",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputProfilePhotoStatics", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputProfilePhotoStatics_TelegramInputFile_PhotoID",
                        column: x => x.PhotoID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputStoryContentPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputStoryContentPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputStoryContentPhotos_TelegramInputFile_PhotoID",
                        column: x => x.PhotoID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputStoryContentVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    CoverFrameTimestamp = table.Column<double>(type: "float", nullable: true),
                    IsAnimation = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputStoryContentVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputStoryContentVideos_TelegramInputFile_VideoID",
                        column: x => x.VideoID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputMessageContent",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vcard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxTipAmount = table.Column<int>(type: "int", nullable: true),
                    ProviderData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoSize = table.Column<int>(type: "int", nullable: true),
                    PhotoWidth = table.Column<int>(type: "int", nullable: true),
                    PhotoHeight = table.Column<int>(type: "int", nullable: true),
                    NeedName = table.Column<bool>(type: "bit", nullable: true),
                    NeedPhoneNumber = table.Column<bool>(type: "bit", nullable: true),
                    NeedEmail = table.Column<bool>(type: "bit", nullable: true),
                    NeedShippingAddress = table.Column<bool>(type: "bit", nullable: true),
                    SendPhoneNumberToProvider = table.Column<bool>(type: "bit", nullable: true),
                    SendEmailToProvider = table.Column<bool>(type: "bit", nullable: true),
                    IsFlexible = table.Column<bool>(type: "bit", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    HorizontalAccuracy = table.Column<double>(type: "float", nullable: true),
                    LivePeriod = table.Column<int>(type: "int", nullable: true),
                    Heading = table.Column<int>(type: "int", nullable: true),
                    ProximityAlertRadius = table.Column<int>(type: "int", nullable: true),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParseMode = table.Column<int>(type: "int", nullable: true),
                    LinkPreviewOptionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputVenueMessageContent_Latitude = table.Column<double>(type: "float", nullable: true),
                    TelegramInputVenueMessageContent_Longitude = table.Column<double>(type: "float", nullable: true),
                    TelegramInputVenueMessageContent_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoursquareId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoursquareType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GooglePlaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GooglePlaceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputMessageContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputMessageContent_TelegramLinkPreviewOptionss_LinkPreviewOptionsID",
                        column: x => x.LinkPreviewOptionsID,
                        principalTable: "TelegramLinkPreviewOptionss",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessLocations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBusinessLocations_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatLocations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatLocations_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramVenues",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoursquareId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoursquareType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GooglePlaceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GooglePlaceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVenues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramVenues_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInputStickers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    EmojiList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaskPositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInputStickers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInputStickers_TelegramInputFile_StickerID",
                        column: x => x.StickerID,
                        principalTable: "TelegramInputFile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramInputStickers_TelegramMaskPositions_MaskPositionID",
                        column: x => x.MaskPositionID,
                        principalTable: "TelegramMaskPositions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramEncryptedPassportElements",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontSideID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReverseSideID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SelfieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TranslationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramEncryptedPassportElements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElements_TelegramPassportFiles_FrontSideID",
                        column: x => x.FrontSideID,
                        principalTable: "TelegramPassportFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElements_TelegramPassportFiles_ReverseSideID",
                        column: x => x.ReverseSideID,
                        principalTable: "TelegramPassportFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElements_TelegramPassportFiles_SelfieID",
                        column: x => x.SelfieID,
                        principalTable: "TelegramPassportFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramAuthorizationRequestParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BotId = table.Column<long>(type: "bigint", nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nonce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportScopeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAuthorizationRequestParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramAuthorizationRequestParameters_TelegramPassportScopes_PassportScopeID",
                        column: x => x.PassportScopeID,
                        principalTable: "TelegramPassportScopes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramPassportScopeElement",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    TelegramPassportScopeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Selfie = table.Column<bool>(type: "bit", nullable: true),
                    Translation = table.Column<bool>(type: "bit", nullable: true),
                    NativeNames = table.Column<bool>(type: "bit", nullable: true),
                    TelegramPassportScopeElementOneOfSeveralID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramPassportScopeElementOneOfSeveral_Selfie = table.Column<bool>(type: "bit", nullable: true),
                    TelegramPassportScopeElementOneOfSeveral_Translation = table.Column<bool>(type: "bit", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPassportScopeElement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPassportScopeElement_TelegramPassportScopeElement_TelegramPassportScopeElementOneOfSeveralID",
                        column: x => x.TelegramPassportScopeElementOneOfSeveralID,
                        principalTable: "TelegramPassportScopeElement",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramPassportScopeElement_TelegramPassportScopes_TelegramPassportScopeID",
                        column: x => x.TelegramPassportScopeID,
                        principalTable: "TelegramPassportScopes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramPollOptions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoterCount = table.Column<int>(type: "int", nullable: false),
                    PollToPollOptionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPollOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPollOptions_TelegramPolls_PollToPollOptionsID",
                        column: x => x.PollToPollOptionsID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramKeyboardRows",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyKeyboardMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramKeyboardRows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardRows_TelegramReplyKeyboardMarkups_ReplyKeyboardMarkupID",
                        column: x => x.ReplyKeyboardMarkupID,
                        principalTable: "TelegramReplyKeyboardMarkups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramApiResponses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ok = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorCode = table.Column<int>(type: "int", nullable: true),
                    ParametersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramApiResponses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramApiResponses_TelegramResponseParameters_ParametersID",
                        column: x => x.ParametersID,
                        principalTable: "TelegramResponseParameters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramOrderInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramOrderInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramOrderInfos_TelegramShippingAddresses_ShippingAddressID",
                        column: x => x.ShippingAddressID,
                        principalTable: "TelegramShippingAddresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGiftBackdrops",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RarityPerMille = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGiftBackdrops", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGiftBackdrops_TelegramUniqueGiftBackdropColorss_ColorsID",
                        column: x => x.ColorsID,
                        principalTable: "TelegramUniqueGiftBackdropColorss",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPhotoSizeGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramUserProfilePhotosID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPhotoSizeGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizeGroups_TelegramUserProfilePhotoss_TelegramUserProfilePhotosID",
                        column: x => x.TelegramUserProfilePhotosID,
                        principalTable: "TelegramUserProfilePhotoss",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramSharedUserTelegramUsersShared",
                columns: table => new
                {
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersSharedThisUsersSharedsBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSharedUserTelegramUsersShared", x => new { x.UsersID, x.UsersSharedThisUsersSharedsBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramSharedUserTelegramUsersShared_TelegramSharedUsers_UsersID",
                        column: x => x.UsersID,
                        principalTable: "TelegramSharedUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramSharedUserTelegramUsersShared_TelegramUsersShareds_UsersSharedThisUsersSharedsBelongsToID",
                        column: x => x.UsersSharedThisUsersSharedsBelongsToID,
                        principalTable: "TelegramUsersShareds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsBot = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPremium = table.Column<bool>(type: "bit", nullable: false),
                    AddedToAttachmentMenu = table.Column<bool>(type: "bit", nullable: false),
                    CanJoinGroups = table.Column<bool>(type: "bit", nullable: false),
                    CanReadAllGroupMessages = table.Column<bool>(type: "bit", nullable: false),
                    SupportsInlineQueries = table.Column<bool>(type: "bit", nullable: false),
                    CanConnectToBusiness = table.Column<bool>(type: "bit", nullable: false),
                    HasMainWebApp = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserThisTelegramUserBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramVideoChatParticipantsInvitedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUsers_TacoPermissionPolicyUser_ApplicationUserThisTelegramUserBelongsToID",
                        column: x => x.ApplicationUserThisTelegramUserBelongsToID,
                        principalTable: "TacoPermissionPolicyUser",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramUsers_TelegramVideoChatsParticipantsInvited_TelegramVideoChatParticipantsInvitedID",
                        column: x => x.TelegramVideoChatParticipantsInvitedID,
                        principalTable: "TelegramVideoChatsParticipantsInvited",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMenuButtonWebApps",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebAppID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMenuButtonWebApps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMenuButtonWebApps_TelegramWebAppInfos_WebAppID",
                        column: x => x.WebAppID,
                        principalTable: "TelegramWebAppInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyActionPermissionObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyActionPermissionObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyActionPermissionObject_PermissionPolicyRoleBase_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyNavigationPermissionObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetTypeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavigateState = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyNavigationPermissionObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyNavigationPermissionObject_PermissionPolicyRoleBase_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyTypePermissionObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetTypeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReadState = table.Column<int>(type: "int", nullable: true),
                    WriteState = table.Column<int>(type: "int", nullable: true),
                    CreateState = table.Column<int>(type: "int", nullable: true),
                    DeleteState = table.Column<int>(type: "int", nullable: true),
                    NavigateState = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyTypePermissionObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyTypePermissionObject_PermissionPolicyRoleBase_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TacoPermissionPolicyRoleTacoPermissionPolicyUser",
                columns: table => new
                {
                    RolesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoPermissionPolicyRoleTacoPermissionPolicyUser", x => new { x.RolesID, x.UsersID });
                    table.ForeignKey(
                        name: "FK_TacoPermissionPolicyRoleTacoPermissionPolicyUser_PermissionPolicyRoleBase_RolesID",
                        column: x => x.RolesID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TacoPermissionPolicyRoleTacoPermissionPolicyUser_TacoPermissionPolicyUser_UsersID",
                        column: x => x.UsersID,
                        principalTable: "TacoPermissionPolicyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramFileCredentialsTelegramSecureValue",
                columns: table => new
                {
                    FilesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecureFilesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramFileCredentialsTelegramSecureValue", x => new { x.FilesID, x.SecureFilesID });
                    table.ForeignKey(
                        name: "FK_TelegramFileCredentialsTelegramSecureValue_TelegramFileCredentials_FilesID",
                        column: x => x.FilesID,
                        principalTable: "TelegramFileCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramFileCredentialsTelegramSecureValue_TelegramSecureValues_SecureFilesID",
                        column: x => x.SecureFilesID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramFileCredentialsTelegramSecureValue1",
                columns: table => new
                {
                    SecureTranslationsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TranslationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramFileCredentialsTelegramSecureValue1", x => new { x.SecureTranslationsID, x.TranslationID });
                    table.ForeignKey(
                        name: "FK_TelegramFileCredentialsTelegramSecureValue1_TelegramFileCredentials_TranslationID",
                        column: x => x.TranslationID,
                        principalTable: "TelegramFileCredentials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramFileCredentialsTelegramSecureValue1_TelegramSecureValues_SecureTranslationsID",
                        column: x => x.SecureTranslationsID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramSecureDatas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PassportID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InternalPassportID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DriverLicenseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentityCardID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UtilityBillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BankStatementID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RentalAgreementID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PassportRegistrationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemporaryRegistrationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSecureDatas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_AddressID",
                        column: x => x.AddressID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_BankStatementID",
                        column: x => x.BankStatementID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_DriverLicenseID",
                        column: x => x.DriverLicenseID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_IdentityCardID",
                        column: x => x.IdentityCardID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_InternalPassportID",
                        column: x => x.InternalPassportID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_PassportID",
                        column: x => x.PassportID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_PassportRegistrationID",
                        column: x => x.PassportRegistrationID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_PersonalDetailsID",
                        column: x => x.PersonalDetailsID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_RentalAgreementID",
                        column: x => x.RentalAgreementID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_TemporaryRegistrationID",
                        column: x => x.TemporaryRegistrationID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramSecureDatas_TelegramSecureValues_UtilityBillID",
                        column: x => x.UtilityBillID,
                        principalTable: "TelegramSecureValues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineKeyboardButtons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallbackData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebAppID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LoginUrlID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SwitchInlineQuery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SwitchInlineQueryCurrentChat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SwitchInlineQueryChosenChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CopyTextID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CallbackGameID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pay = table.Column<bool>(type: "bit", nullable: false),
                    TelegramInlineKeyboardRowID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineKeyboardButtons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramCallbackGames_CallbackGameID",
                        column: x => x.CallbackGameID,
                        principalTable: "TelegramCallbackGames",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramCopyTextButtons_CopyTextID",
                        column: x => x.CopyTextID,
                        principalTable: "TelegramCopyTextButtons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramInlineKeyboardRows_TelegramInlineKeyboardRowID",
                        column: x => x.TelegramInlineKeyboardRowID,
                        principalTable: "TelegramInlineKeyboardRows",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramLoginUrls_LoginUrlID",
                        column: x => x.LoginUrlID,
                        principalTable: "TelegramLoginUrls",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramSwitchInlineQueryChosenChats_SwitchInlineQueryChosenChatID",
                        column: x => x.SwitchInlineQueryChosenChatID,
                        principalTable: "TelegramSwitchInlineQueryChosenChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineKeyboardButtons_TelegramWebAppInfos_WebAppID",
                        column: x => x.WebAppID,
                        principalTable: "TelegramWebAppInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultArticles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultArticles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultArticles_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultArticles_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultAudios",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    Performer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioDuration = table.Column<int>(type: "int", nullable: true),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultAudios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultAudios_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultAudios_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedAudios",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AudioFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedAudios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedAudios_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedAudios_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedDocuments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedDocuments_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedDocuments_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedGifs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GifFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedGifs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedGifs_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedGifs_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedMpeg4Gifs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mpeg4FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedMpeg4Gifs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedMpeg4Gifs_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedMpeg4Gifs_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedPhotos_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedPhotos_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedStickers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StickerFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedStickers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedStickers_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedStickers_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedVideos_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedVideos_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultCachedVoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoiceFileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultCachedVoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedVoices_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultCachedVoices_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultContacts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vcard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultContacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultContacts_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultContacts_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultDocuments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultDocuments_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultDocuments_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultGames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultGames_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultGames_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultGifs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GifUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GifWidth = table.Column<int>(type: "int", nullable: true),
                    GifHeight = table.Column<int>(type: "int", nullable: true),
                    GifDuration = table.Column<int>(type: "int", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailMimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultGifs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultGifs_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultGifs_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorizontalAccuracy = table.Column<double>(type: "float", nullable: true),
                    LivePeriod = table.Column<int>(type: "int", nullable: true),
                    Heading = table.Column<int>(type: "int", nullable: true),
                    ProximityAlertRadius = table.Column<int>(type: "int", nullable: true),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultLocations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultLocations_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultLocations_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultMpeg4Gifs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mpeg4Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mpeg4Width = table.Column<int>(type: "int", nullable: true),
                    Mpeg4Height = table.Column<int>(type: "int", nullable: true),
                    Mpeg4Duration = table.Column<int>(type: "int", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailMimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultMpeg4Gifs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultMpeg4Gifs_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultMpeg4Gifs_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultPhotos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoWidth = table.Column<int>(type: "int", nullable: true),
                    PhotoHeight = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultPhotos_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultPhotos_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultVenues",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoursquareId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoursquareType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GooglePlaceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GooglePlaceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultVenues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVenues_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVenues_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: false),
                    VideoWidth = table.Column<int>(type: "int", nullable: true),
                    VideoHeight = table.Column<int>(type: "int", nullable: true),
                    VideoDuration = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVideos_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVideos_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQueryResultVoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParseMode = table.Column<int>(type: "int", nullable: false),
                    VoiceDuration = table.Column<int>(type: "int", nullable: true),
                    InputMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    InlineQueryResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQueryResultVoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVoices_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramInlineQueryResultVoices_TelegramInputMessageContent_InputMessageContentID",
                        column: x => x.InputMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramLabeledPrices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TelegramInputInvoiceMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramShippingOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramLabeledPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramLabeledPrices_TelegramInputMessageContent_TelegramInputInvoiceMessageContentID",
                        column: x => x.TelegramInputInvoiceMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramLabeledPrices_TelegramShippingOptions_TelegramShippingOptionID",
                        column: x => x.TelegramShippingOptionID,
                        principalTable: "TelegramShippingOptions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramEncryptedPassportElementTelegramPassportData",
                columns: table => new
                {
                    DataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassportDataThisEncryptedPassportElementBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramEncryptedPassportElementTelegramPassportData", x => new { x.DataID, x.PassportDataThisEncryptedPassportElementBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportData_TelegramEncryptedPassportElements_DataID",
                        column: x => x.DataID,
                        principalTable: "TelegramEncryptedPassportElements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportData_TelegramPassportDatas_PassportDataThisEncryptedPassportElementBelongsTo~",
                        column: x => x.PassportDataThisEncryptedPassportElementBelongsToID,
                        principalTable: "TelegramPassportDatas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramEncryptedPassportElementTelegramPassportFile",
                columns: table => new
                {
                    EncryptedPasswordElementFilesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramEncryptedPassportElementTelegramPassportFile", x => new { x.EncryptedPasswordElementFilesID, x.FilesID });
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportFile_TelegramEncryptedPassportElements_EncryptedPasswordElementFilesID",
                        column: x => x.EncryptedPasswordElementFilesID,
                        principalTable: "TelegramEncryptedPassportElements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportFile_TelegramPassportFiles_FilesID",
                        column: x => x.FilesID,
                        principalTable: "TelegramPassportFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramEncryptedPassportElementTelegramPassportFile1",
                columns: table => new
                {
                    EncryptedPasswordElementTranslationsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TranslationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramEncryptedPassportElementTelegramPassportFile1", x => new { x.EncryptedPasswordElementTranslationsID, x.TranslationID });
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportFile1_TelegramEncryptedPassportElements_EncryptedPasswordElementTranslations~",
                        column: x => x.EncryptedPasswordElementTranslationsID,
                        principalTable: "TelegramEncryptedPassportElements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramEncryptedPassportElementTelegramPassportFile1_TelegramPassportFiles_TranslationID",
                        column: x => x.TranslationID,
                        principalTable: "TelegramPassportFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramKeyboardButtons",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestContact = table.Column<bool>(type: "bit", nullable: false),
                    RequestLocation = table.Column<bool>(type: "bit", nullable: false),
                    RequestPollID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WebAppID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramKeyboardRowID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramKeyboardButtons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtons_TelegramKeyboardButtonPollTypes_RequestPollID",
                        column: x => x.RequestPollID,
                        principalTable: "TelegramKeyboardButtonPollTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtons_TelegramKeyboardButtonRequestChats_RequestChatID",
                        column: x => x.RequestChatID,
                        principalTable: "TelegramKeyboardButtonRequestChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtons_TelegramKeyboardButtonRequestUsers_RequestUsersID",
                        column: x => x.RequestUsersID,
                        principalTable: "TelegramKeyboardButtonRequestUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtons_TelegramKeyboardRows_TelegramKeyboardRowID",
                        column: x => x.TelegramKeyboardRowID,
                        principalTable: "TelegramKeyboardRows",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramKeyboardButtons_TelegramWebAppInfos_WebAppID",
                        column: x => x.WebAppID,
                        principalTable: "TelegramWebAppInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramSuccessfulPayments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    InvoicePayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstRecurring = table.Column<bool>(type: "bit", nullable: false),
                    ShippingOptionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderInfoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramPaymentChargeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderPaymentChargeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramSuccessfulPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramSuccessfulPayments_TelegramOrderInfos_OrderInfoID",
                        column: x => x.OrderInfoID,
                        principalTable: "TelegramOrderInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramAffiliateInfo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AffiliateUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AffiliateChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommissionPerMille = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NanostarAmount = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAffiliateInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramAffiliateInfo_TelegramChats_AffiliateChatID",
                        column: x => x.AffiliateChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramAffiliateInfo_TelegramUsers_AffiliateUserID",
                        column: x => x.AffiliateUserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessConnections",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserChatId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessConnections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBusinessConnections_TelegramBusinessBotRights_RightsID",
                        column: x => x.RightsID,
                        principalTable: "TelegramBusinessBotRights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramBusinessConnections_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBoostSource",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayMessageId = table.Column<int>(type: "int", nullable: true),
                    TelegramChatBoostSourceGiveaway_UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrizeStarCount = table.Column<int>(type: "int", nullable: true),
                    IsUnclaimed = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatBoostSourcePremium_UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBoostSource", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostSource_TelegramUsers_TelegramChatBoostSourceGiveaway_UserID",
                        column: x => x.TelegramChatBoostSourceGiveaway_UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostSource_TelegramUsers_TelegramChatBoostSourcePremium_UserID",
                        column: x => x.TelegramChatBoostSourcePremium_UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostSource_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBotRightsUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BotUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatAdministratorRightsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBotRightsUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBotRightsUsers_TelegramChatAdministratorRights_ChatAdministratorRightsID",
                        column: x => x.ChatAdministratorRightsID,
                        principalTable: "TelegramChatAdministratorRights",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBotRightsUsers_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBotRightsUsers_TelegramUsers_BotUserID",
                        column: x => x.BotUserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatInviteLinks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InviteLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatesJoinRequest = table.Column<bool>(type: "bit", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemberLimit = table.Column<int>(type: "int", nullable: true),
                    PendingJoinRequestCount = table.Column<int>(type: "int", nullable: true),
                    SubscriptionPeriod = table.Column<int>(type: "int", nullable: true),
                    SubscriptionPrice = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatInviteLinks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatInviteLinks_TelegramUsers_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatMember",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsInChat = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    CanBeEdited = table.Column<bool>(type: "bit", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: true),
                    CanManageChat = table.Column<bool>(type: "bit", nullable: true),
                    CanDeleteMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanManageVideoChats = table.Column<bool>(type: "bit", nullable: true),
                    CanRestrictMembers = table.Column<bool>(type: "bit", nullable: true),
                    CanPromoteMembers = table.Column<bool>(type: "bit", nullable: true),
                    CanChangeInfo = table.Column<bool>(type: "bit", nullable: true),
                    CanInviteUsers = table.Column<bool>(type: "bit", nullable: true),
                    CanPostStories = table.Column<bool>(type: "bit", nullable: true),
                    CanEditStories = table.Column<bool>(type: "bit", nullable: true),
                    CanDeleteStories = table.Column<bool>(type: "bit", nullable: true),
                    CanPostMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanEditMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanPinMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanManageTopics = table.Column<bool>(type: "bit", nullable: true),
                    CustomTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TelegramChatMemberMember_UntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TelegramChatMemberOwner_IsAnonymous = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberOwner_CustomTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMember = table.Column<bool>(type: "bit", nullable: true),
                    CanSendMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanSendAudios = table.Column<bool>(type: "bit", nullable: true),
                    CanSendDocuments = table.Column<bool>(type: "bit", nullable: true),
                    CanSendPhotos = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVideos = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVideoNotes = table.Column<bool>(type: "bit", nullable: true),
                    CanSendVoiceNotes = table.Column<bool>(type: "bit", nullable: true),
                    CanSendPolls = table.Column<bool>(type: "bit", nullable: true),
                    CanSendOtherMessages = table.Column<bool>(type: "bit", nullable: true),
                    CanAddWebPagePreviews = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberRestricted_CanChangeInfo = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberRestricted_CanInviteUsers = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberRestricted_CanPinMessages = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberRestricted_CanManageTopics = table.Column<bool>(type: "bit", nullable: true),
                    TelegramChatMemberRestricted_UntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatMember", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatMember_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChosenInlineResults",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InlineMessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChosenInlineResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChosenInlineResults_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChosenInlineResults_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGameHighScores",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGameHighScores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramGameHighScores_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiveawayWinnersTelegramUser",
                columns: table => new
                {
                    GiveawayWinnersThisWinnerUsersBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiveawayWinnersTelegramUser", x => new { x.GiveawayWinnersThisWinnerUsersBelongsToID, x.WinnersID });
                    table.ForeignKey(
                        name: "FK_TelegramGiveawayWinnersTelegramUser_TelegramGiveawaysWinners_GiveawayWinnersThisWinnerUsersBelongsToID",
                        column: x => x.GiveawayWinnersThisWinnerUsersBelongsToID,
                        principalTable: "TelegramGiveawaysWinners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramGiveawayWinnersTelegramUser_TelegramUsers_WinnersID",
                        column: x => x.WinnersID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramInlineQuerys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InlineQueryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatType = table.Column<int>(type: "int", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramInlineQuerys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQuerys_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramInlineQuerys_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageOrigin",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    messageId = table.Column<int>(type: "int", nullable: true),
                    AuthorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramMessageOriginChat_AuthorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageOrigin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageOrigin_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageOrigin_TelegramChats_SenderChatID",
                        column: x => x.SenderChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageOrigin_TelegramUsers_SenderUserID",
                        column: x => x.SenderUserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageReactionUpdates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramMessageReactionUpdatedMessageID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageIdFromReactionUpdate = table.Column<int>(type: "int", nullable: false),
                    ActorChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageReactionUpdates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionUpdates_TelegramChats_ActorChatID",
                        column: x => x.ActorChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionUpdates_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionUpdates_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPaidMediaPurchases",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaidMediaPayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPaidMediaPurchases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPaidMediaPurchases_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPollAnswers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramPollAnswerPollID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoterChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OptionIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPollAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPollAnswers_TelegramChats_VoterChatID",
                        column: x => x.VoterChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramPollAnswers_TelegramPolls_TelegramPollAnswerPollID",
                        column: x => x.TelegramPollAnswerPollID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramPollAnswers_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPreCheckoutQueries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreCheckoutQueryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    InvoicePayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingOptionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderInfoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPreCheckoutQueries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPreCheckoutQueries_TelegramOrderInfos_OrderInfoID",
                        column: x => x.OrderInfoID,
                        principalTable: "TelegramOrderInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramPreCheckoutQueries_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramProximityAlertTriggereds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TravelerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WatcherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramProximityAlertTriggereds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramProximityAlertTriggereds_TelegramUsers_TravelerID",
                        column: x => x.TravelerID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramProximityAlertTriggereds_TelegramUsers_WatcherID",
                        column: x => x.WatcherID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramShippingQueries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingQueryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoicePayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramShippingQueries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramShippingQueries_TelegramShippingAddresses_ShippingAddressID",
                        column: x => x.ShippingAddressID,
                        principalTable: "TelegramShippingAddresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramShippingQueries_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUserChats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateUpdated = table.Column<DateOnly>(type: "date", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUserChats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUserChats_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyMemberPermissionsObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadState = table.Column<int>(type: "int", nullable: true),
                    WriteState = table.Column<int>(type: "int", nullable: true),
                    TypePermissionObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyMemberPermissionsObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyMemberPermissionsObject_PermissionPolicyTypePermissionObject_TypePermissionObjectID",
                        column: x => x.TypePermissionObjectID,
                        principalTable: "PermissionPolicyTypePermissionObject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionPolicyObjectPermissionsObject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Criteria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadState = table.Column<int>(type: "int", nullable: true),
                    WriteState = table.Column<int>(type: "int", nullable: true),
                    DeleteState = table.Column<int>(type: "int", nullable: true),
                    NavigateState = table.Column<int>(type: "int", nullable: true),
                    TypePermissionObjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionPolicyObjectPermissionsObject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermissionPolicyObjectPermissionsObject_PermissionPolicyTypePermissionObject_TypePermissionObjectID",
                        column: x => x.TypePermissionObjectID,
                        principalTable: "PermissionPolicyTypePermissionObject",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramCredentialss",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecureDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nonce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramCredentialss", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramCredentialss_TelegramSecureDatas_SecureDataID",
                        column: x => x.SecureDataID,
                        principalTable: "TelegramSecureDatas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageEntitys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Offset = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelegramInlineQueryResultAudioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedAudioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedDocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedGifID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedMpeg4GifID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedVideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultCachedVoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultDocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultGifID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultMpeg4GifID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultVideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInlineQueryResultVoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputMediaAnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputMediaAudioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputMediaDocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputMediaPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputMediaVideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputPollOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramInputTextMessageContentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramPollOptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramReplyParametersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageEntitys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultAudios_TelegramInlineQueryResultAudioID",
                        column: x => x.TelegramInlineQueryResultAudioID,
                        principalTable: "TelegramInlineQueryResultAudios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedAudios_TelegramInlineQueryResultCachedAudioID",
                        column: x => x.TelegramInlineQueryResultCachedAudioID,
                        principalTable: "TelegramInlineQueryResultCachedAudios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedDocuments_TelegramInlineQueryResultCachedDocumentID",
                        column: x => x.TelegramInlineQueryResultCachedDocumentID,
                        principalTable: "TelegramInlineQueryResultCachedDocuments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedGifs_TelegramInlineQueryResultCachedGifID",
                        column: x => x.TelegramInlineQueryResultCachedGifID,
                        principalTable: "TelegramInlineQueryResultCachedGifs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedMpeg4Gifs_TelegramInlineQueryResultCachedMpeg4GifID",
                        column: x => x.TelegramInlineQueryResultCachedMpeg4GifID,
                        principalTable: "TelegramInlineQueryResultCachedMpeg4Gifs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedPhotos_TelegramInlineQueryResultCachedPhotoID",
                        column: x => x.TelegramInlineQueryResultCachedPhotoID,
                        principalTable: "TelegramInlineQueryResultCachedPhotos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedVideos_TelegramInlineQueryResultCachedVideoID",
                        column: x => x.TelegramInlineQueryResultCachedVideoID,
                        principalTable: "TelegramInlineQueryResultCachedVideos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultCachedVoices_TelegramInlineQueryResultCachedVoiceID",
                        column: x => x.TelegramInlineQueryResultCachedVoiceID,
                        principalTable: "TelegramInlineQueryResultCachedVoices",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultDocuments_TelegramInlineQueryResultDocumentID",
                        column: x => x.TelegramInlineQueryResultDocumentID,
                        principalTable: "TelegramInlineQueryResultDocuments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultGifs_TelegramInlineQueryResultGifID",
                        column: x => x.TelegramInlineQueryResultGifID,
                        principalTable: "TelegramInlineQueryResultGifs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultMpeg4Gifs_TelegramInlineQueryResultMpeg4GifID",
                        column: x => x.TelegramInlineQueryResultMpeg4GifID,
                        principalTable: "TelegramInlineQueryResultMpeg4Gifs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultPhotos_TelegramInlineQueryResultPhotoID",
                        column: x => x.TelegramInlineQueryResultPhotoID,
                        principalTable: "TelegramInlineQueryResultPhotos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultVideos_TelegramInlineQueryResultVideoID",
                        column: x => x.TelegramInlineQueryResultVideoID,
                        principalTable: "TelegramInlineQueryResultVideos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInlineQueryResultVoices_TelegramInlineQueryResultVoiceID",
                        column: x => x.TelegramInlineQueryResultVoiceID,
                        principalTable: "TelegramInlineQueryResultVoices",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMediaAnimations_TelegramInputMediaAnimationID",
                        column: x => x.TelegramInputMediaAnimationID,
                        principalTable: "TelegramInputMediaAnimations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMediaAudios_TelegramInputMediaAudioID",
                        column: x => x.TelegramInputMediaAudioID,
                        principalTable: "TelegramInputMediaAudios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMediaDocuments_TelegramInputMediaDocumentID",
                        column: x => x.TelegramInputMediaDocumentID,
                        principalTable: "TelegramInputMediaDocuments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMediaPhotos_TelegramInputMediaPhotoID",
                        column: x => x.TelegramInputMediaPhotoID,
                        principalTable: "TelegramInputMediaPhotos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMediaVideos_TelegramInputMediaVideoID",
                        column: x => x.TelegramInputMediaVideoID,
                        principalTable: "TelegramInputMediaVideos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputMessageContent_TelegramInputTextMessageContentID",
                        column: x => x.TelegramInputTextMessageContentID,
                        principalTable: "TelegramInputMessageContent",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramInputPollOptions_TelegramInputPollOptionID",
                        column: x => x.TelegramInputPollOptionID,
                        principalTable: "TelegramInputPollOptions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramPollOptions_TelegramPollOptionID",
                        column: x => x.TelegramPollOptionID,
                        principalTable: "TelegramPollOptions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramReplyParameterss_TelegramReplyParametersID",
                        column: x => x.TelegramReplyParametersID,
                        principalTable: "TelegramReplyParameterss",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntitys_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBoostRemoves",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BoostId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBoostRemoves", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostRemoves_TelegramChatBoostSource_SourceID",
                        column: x => x.SourceID,
                        principalTable: "TelegramChatBoostSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostRemoves_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBoosts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoostId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramUserChatBoostsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBoosts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoosts_TelegramChatBoostSource_SourceID",
                        column: x => x.SourceID,
                        principalTable: "TelegramChatBoostSource",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoosts_TelegramUserChatBoostss_TelegramUserChatBoostsID",
                        column: x => x.TelegramUserChatBoostsID,
                        principalTable: "TelegramUserChatBoostss",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TacoTeamChats",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BotAssignedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoTeamChats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TacoTeamChats_TelegramChatBotRightsUsers_BotAssignedID",
                        column: x => x.BotAssignedID,
                        principalTable: "TelegramChatBotRightsUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TacoTeamChats_TelegramChats_TeamChatID",
                        column: x => x.TeamChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatJoinRequests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserChatId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InviteLinkID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatJoinRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatJoinRequests_TelegramChatInviteLinks_InviteLinkID",
                        column: x => x.InviteLinkID,
                        principalTable: "TelegramChatInviteLinks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatJoinRequests_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatJoinRequests_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatMemberUpdates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldChatMemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewChatMemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InviteLinkID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ViaJoinRequest = table.Column<bool>(type: "bit", nullable: false),
                    ViaChatFolderInviteLink = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatMemberUpdates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatMemberUpdates_TelegramChatInviteLinks_InviteLinkID",
                        column: x => x.InviteLinkID,
                        principalTable: "TelegramChatInviteLinks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatMemberUpdates_TelegramChatMember_NewChatMemberID",
                        column: x => x.NewChatMemberID,
                        principalTable: "TelegramChatMember",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatMemberUpdates_TelegramChatMember_OldChatMemberID",
                        column: x => x.OldChatMemberID,
                        principalTable: "TelegramChatMember",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatMemberUpdates_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatMemberUpdates_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatTelegramUserChat",
                columns: table => new
                {
                    ChatThisUserChatBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramUserChatsThisChatBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatTelegramUserChat", x => new { x.ChatThisUserChatBelongsToID, x.TelegramUserChatsThisChatBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramChatTelegramUserChat_TelegramChats_ChatThisUserChatBelongsToID",
                        column: x => x.ChatThisUserChatBelongsToID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramChatTelegramUserChat_TelegramUserChats_TelegramUserChatsThisChatBelongsToID",
                        column: x => x.TelegramUserChatsThisChatBelongsToID,
                        principalTable: "TelegramUserChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageEntityTelegramPoll",
                columns: table => new
                {
                    ExplanationEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PollExplanationEntitiesToThisPollID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageEntityTelegramPoll", x => new { x.ExplanationEntitiesID, x.PollExplanationEntitiesToThisPollID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramPoll_TelegramMessageEntitys_ExplanationEntitiesID",
                        column: x => x.ExplanationEntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramPoll_TelegramPolls_PollExplanationEntitiesToThisPollID",
                        column: x => x.PollExplanationEntitiesToThisPollID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageEntityTelegramPoll1",
                columns: table => new
                {
                    PollQuestionEntitiesToThisPollID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageEntityTelegramPoll1", x => new { x.PollQuestionEntitiesToThisPollID, x.QuestionEntitiesID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramPoll1_TelegramMessageEntitys_QuestionEntitiesID",
                        column: x => x.QuestionEntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramPoll1_TelegramPolls_PollQuestionEntitiesToThisPollID",
                        column: x => x.PollQuestionEntitiesToThisPollID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageEntityTelegramTextQuote",
                columns: table => new
                {
                    EntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextQuoteThisMessageEntityBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageEntityTelegramTextQuote", x => new { x.EntitiesID, x.TextQuoteThisMessageEntityBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramTextQuote_TelegramMessageEntitys_EntitiesID",
                        column: x => x.EntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageEntityTelegramTextQuote_TelegramTextQuotes_TextQuoteThisMessageEntityBelongsToID",
                        column: x => x.TextQuoteThisMessageEntityBelongsToID,
                        principalTable: "TelegramTextQuotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBoostUpdates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BoostID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBoostUpdates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostUpdates_TelegramChatBoosts_BoostID",
                        column: x => x.BoostID,
                        principalTable: "TelegramChatBoosts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatBoostUpdates_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TacoTeams",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamAdminChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoTeams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TacoTeams_TacoTeamChats_TeamAdminChatID",
                        column: x => x.TeamAdminChatID,
                        principalTable: "TacoTeamChats",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TacoPermissionPolicyRoleTacoTeam",
                columns: table => new
                {
                    RolesBelongToThisTeamID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamsThisRoleBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoPermissionPolicyRoleTacoTeam", x => new { x.RolesBelongToThisTeamID, x.TeamsThisRoleBelongsToID });
                    table.ForeignKey(
                        name: "FK_TacoPermissionPolicyRoleTacoTeam_PermissionPolicyRoleBase_RolesBelongToThisTeamID",
                        column: x => x.RolesBelongToThisTeamID,
                        principalTable: "PermissionPolicyRoleBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TacoPermissionPolicyRoleTacoTeam_TacoTeams_TeamsThisRoleBelongsToID",
                        column: x => x.TeamsThisRoleBelongsToID,
                        principalTable: "TacoTeams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TacoTeamTacoTeamChat",
                columns: table => new
                {
                    TeamChatsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamThisTeamChatBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacoTeamTacoTeamChat", x => new { x.TeamChatsID, x.TeamThisTeamChatBelongsToID });
                    table.ForeignKey(
                        name: "FK_TacoTeamTacoTeamChat_TacoTeamChats_TeamChatsID",
                        column: x => x.TeamChatsID,
                        principalTable: "TacoTeamChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TacoTeamTacoTeamChat_TacoTeams_TeamThisTeamChatBelongsToID",
                        column: x => x.TeamThisTeamChatBelongsToID,
                        principalTable: "TacoTeams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramAnimations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAnimations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramGames_TelegramAnimations_AnimationID",
                        column: x => x.AnimationID,
                        principalTable: "TelegramAnimations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGameTelegramMessageEntity",
                columns: table => new
                {
                    GameThisMessageEntityBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGameTelegramMessageEntity", x => new { x.GameThisMessageEntityBelongsToID, x.TextEntitiesID });
                    table.ForeignKey(
                        name: "FK_TelegramGameTelegramMessageEntity_TelegramGames_GameThisMessageEntityBelongsToID",
                        column: x => x.GameThisMessageEntityBelongsToID,
                        principalTable: "TelegramGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramGameTelegramMessageEntity_TelegramMessageEntitys_TextEntitiesID",
                        column: x => x.TextEntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramAudios",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Performer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAudios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBackgroundType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DarkThemeDimming = table.Column<int>(type: "int", nullable: true),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramBackgroundTypePattern_FillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Intensity = table.Column<int>(type: "int", nullable: true),
                    IsInverted = table.Column<bool>(type: "bit", nullable: true),
                    IsMoving = table.Column<bool>(type: "bit", nullable: true),
                    TelegramBackgroundTypeWallpaper_DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramBackgroundTypeWallpaper_DarkThemeDimming = table.Column<int>(type: "int", nullable: true),
                    IsBlurred = table.Column<bool>(type: "bit", nullable: true),
                    TelegramBackgroundTypeWallpaper_IsMoving = table.Column<bool>(type: "bit", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBackgroundType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramBackgroundType_TelegramBackgroundFill_FillID",
                        column: x => x.FillID,
                        principalTable: "TelegramBackgroundFill",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramBackgroundType_TelegramBackgroundFill_TelegramBackgroundTypePattern_FillID",
                        column: x => x.TelegramBackgroundTypePattern_FillID,
                        principalTable: "TelegramBackgroundFill",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatBackgrounds",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatBackgrounds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatBackgrounds_TelegramBackgroundType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TelegramBackgroundType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramBusinessIntros",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramBusinessIntros", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramCallbackQueries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallbackQueryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramCallbackQueryMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InlineMessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatInstance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramCallbackQueries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramCallbackQueries_TelegramUsers_FromID",
                        column: x => x.FromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatFullInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentColorId = table.Column<int>(type: "int", nullable: false),
                    MaxReactionCount = table.Column<int>(type: "int", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActiveUsernames = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthdateID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessIntroID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessLocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessOpeningHoursID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonalChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BackgroundCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfileAccentColorId = table.Column<int>(type: "int", nullable: true),
                    ProfileBackgroundCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmojiStatusCustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmojiStatusExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasPrivateForwards = table.Column<bool>(type: "bit", nullable: false),
                    HasRestrictedVoiceAndVideoMessages = table.Column<bool>(type: "bit", nullable: false),
                    JoinToSendMessages = table.Column<bool>(type: "bit", nullable: false),
                    JoinByRequest = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InviteLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinnedMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PermissionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcceptedGiftTypesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CanSendPaidMedia = table.Column<bool>(type: "bit", nullable: false),
                    SlowModeDelay = table.Column<int>(type: "int", nullable: true),
                    UnrestrictBoostCount = table.Column<int>(type: "int", nullable: true),
                    MessageAutoDeleteTime = table.Column<int>(type: "int", nullable: true),
                    HasAggressiveAntiSpamEnabled = table.Column<bool>(type: "bit", nullable: false),
                    HasHiddenMembers = table.Column<bool>(type: "bit", nullable: false),
                    HasProtectedContent = table.Column<bool>(type: "bit", nullable: false),
                    HasVisibleHistory = table.Column<bool>(type: "bit", nullable: false),
                    StickerSetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanSetStickerSet = table.Column<bool>(type: "bit", nullable: false),
                    CustomEmojiStickerSetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedChatId = table.Column<long>(type: "bigint", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatFullInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramAcceptedGiftTypes_AcceptedGiftTypesID",
                        column: x => x.AcceptedGiftTypesID,
                        principalTable: "TelegramAcceptedGiftTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramBirthdates_BirthdateID",
                        column: x => x.BirthdateID,
                        principalTable: "TelegramBirthdates",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramBusinessIntros_BusinessIntroID",
                        column: x => x.BusinessIntroID,
                        principalTable: "TelegramBusinessIntros",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramBusinessLocations_BusinessLocationID",
                        column: x => x.BusinessLocationID,
                        principalTable: "TelegramBusinessLocations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramBusinessOpeningHours_BusinessOpeningHoursID",
                        column: x => x.BusinessOpeningHoursID,
                        principalTable: "TelegramBusinessOpeningHours",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramChatLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramChatLocations",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramChatPermissions_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "TelegramChatPermissions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramChatPhotos_PhotoID",
                        column: x => x.PhotoID,
                        principalTable: "TelegramChatPhotos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramChatFullInfos_TelegramChats_PersonalChatID",
                        column: x => x.PersonalChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramReactionType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TelegramChatFullInfoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReactionType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramReactionType_TelegramChatFullInfos_TelegramChatFullInfoID",
                        column: x => x.TelegramChatFullInfoID,
                        principalTable: "TelegramChatFullInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageReactionNewJoin",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramMessageReactionUpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramReactionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageReactionNewJoin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionNewJoin_TelegramMessageReactionUpdates_TelegramMessageReactionUpdatedId",
                        column: x => x.TelegramMessageReactionUpdatedId,
                        principalTable: "TelegramMessageReactionUpdates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionNewJoin_TelegramReactionType_TelegramReactionTypeId",
                        column: x => x.TelegramReactionTypeId,
                        principalTable: "TelegramReactionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageReactionOldJoin",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramMessageReactionUpdatedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramReactionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageReactionOldJoin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionOldJoin_TelegramMessageReactionUpdates_TelegramMessageReactionUpdatedId",
                        column: x => x.TelegramMessageReactionUpdatedId,
                        principalTable: "TelegramMessageReactionUpdates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionOldJoin_TelegramReactionType_TelegramReactionTypeId",
                        column: x => x.TelegramReactionTypeId,
                        principalTable: "TelegramReactionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramReactionCounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalCount = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramReactionCounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramReactionCounts_TelegramReactionType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TelegramReactionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStoryAreaType",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReactionTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDark = table.Column<bool>(type: "bit", nullable: true),
                    IsFlipped = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundColor = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStoryAreaType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStoryAreaType_TelegramLocationAddresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "TelegramLocationAddresses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramStoryAreaType_TelegramReactionType_ReactionTypeID",
                        column: x => x.ReactionTypeID,
                        principalTable: "TelegramReactionType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageReactionCountUpdatedTelegramReactionCount",
                columns: table => new
                {
                    MessageReactionCountUpdatedThisReactionCountBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageReactionCountUpdatedTelegramReactionCount", x => new { x.MessageReactionCountUpdatedThisReactionCountBelongsToID, x.ReactionsID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionCountUpdatedTelegramReactionCount_TelegramMessageReactionCountUpdates_MessageReactionCountUpdatedThis~",
                        column: x => x.MessageReactionCountUpdatedThisReactionCountBelongsToID,
                        principalTable: "TelegramMessageReactionCountUpdates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageReactionCountUpdatedTelegramReactionCount_TelegramReactionCounts_ReactionsID",
                        column: x => x.ReactionsID,
                        principalTable: "TelegramReactionCounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStoryAreas",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStoryAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStoryAreas_TelegramStoryAreaPositions_PositionID",
                        column: x => x.PositionID,
                        principalTable: "TelegramStoryAreaPositions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramStoryAreas_TelegramStoryAreaType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "TelegramStoryAreaType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramChatSharedTelegramPhotoSize",
                columns: table => new
                {
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharedChatsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatSharedTelegramPhotoSize", x => new { x.PhotoID, x.SharedChatsID });
                    table.ForeignKey(
                        name: "FK_TelegramChatSharedTelegramPhotoSize_TelegramChatShared_SharedChatsID",
                        column: x => x.SharedChatsID,
                        principalTable: "TelegramChatShared",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramDocuments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramDocuments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramExternalReplyInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UniqueMessageId = table.Column<int>(type: "int", nullable: true),
                    LinkPreviewOptionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AudioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaidMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoNoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HasMediaSpoiler = table.Column<bool>(type: "bit", nullable: false),
                    ContactID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GameID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayWinnersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PollID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VenueID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramExternalReplyInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramAnimations_AnimationID",
                        column: x => x.AnimationID,
                        principalTable: "TelegramAnimations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramAudios_AudioID",
                        column: x => x.AudioID,
                        principalTable: "TelegramAudios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramContacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "TelegramContacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramDices_DiceID",
                        column: x => x.DiceID,
                        principalTable: "TelegramDices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramDocuments_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "TelegramDocuments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramGames_GameID",
                        column: x => x.GameID,
                        principalTable: "TelegramGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramGiveawaysWinners_GiveawayWinnersID",
                        column: x => x.GiveawayWinnersID,
                        principalTable: "TelegramGiveawaysWinners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramGiveaways_GiveawayID",
                        column: x => x.GiveawayID,
                        principalTable: "TelegramGiveaways",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramInvoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "TelegramInvoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramLinkPreviewOptionss_LinkPreviewOptionsID",
                        column: x => x.LinkPreviewOptionsID,
                        principalTable: "TelegramLinkPreviewOptionss",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramMessageOrigin_OriginID",
                        column: x => x.OriginID,
                        principalTable: "TelegramMessageOrigin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramPaidMediaInfos_PaidMediaID",
                        column: x => x.PaidMediaID,
                        principalTable: "TelegramPaidMediaInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramPolls_PollID",
                        column: x => x.PollID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramStories_StoryID",
                        column: x => x.StoryID,
                        principalTable: "TelegramStories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramVenues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "TelegramVenues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfos_TelegramVoices_VoiceID",
                        column: x => x.VoiceID,
                        principalTable: "TelegramVoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramExternalReplyInfoTelegramPhotoSize",
                columns: table => new
                {
                    ExternalRepliesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramExternalReplyInfoTelegramPhotoSize", x => new { x.ExternalRepliesID, x.PhotoID });
                    table.ForeignKey(
                        name: "FK_TelegramExternalReplyInfoTelegramPhotoSize_TelegramExternalReplyInfos_ExternalRepliesID",
                        column: x => x.ExternalRepliesID,
                        principalTable: "TelegramExternalReplyInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGameTelegramPhotoSize",
                columns: table => new
                {
                    GamesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGameTelegramPhotoSize", x => new { x.GamesID, x.PhotoID });
                    table.ForeignKey(
                        name: "FK_TelegramGameTelegramPhotoSize_TelegramGames_GamesID",
                        column: x => x.GamesID,
                        principalTable: "TelegramGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiftInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnedGiftId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConvertStarCount = table.Column<int>(type: "int", nullable: true),
                    PrepaidUpgradeStarCount = table.Column<int>(type: "int", nullable: true),
                    CanBeUpgraded = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiftInfos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiftInfoTelegramMessageEntity",
                columns: table => new
                {
                    EntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiftInfoThisMessageEntityBelongsToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiftInfoTelegramMessageEntity", x => new { x.EntitiesID, x.GiftInfoThisMessageEntityBelongsToID });
                    table.ForeignKey(
                        name: "FK_TelegramGiftInfoTelegramMessageEntity_TelegramGiftInfos_GiftInfoThisMessageEntityBelongsToID",
                        column: x => x.GiftInfoThisMessageEntityBelongsToID,
                        principalTable: "TelegramGiftInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramGiftInfoTelegramMessageEntity_TelegramMessageEntitys_EntitiesID",
                        column: x => x.EntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramGifts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiftId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StarCount = table.Column<int>(type: "int", nullable: false),
                    UpgradeStarCount = table.Column<int>(type: "int", nullable: true),
                    TotalCount = table.Column<int>(type: "int", nullable: true),
                    RemainingCount = table.Column<int>(type: "int", nullable: true),
                    TelegramGiftListID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGifts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramGifts_TelegramGiftLists_TelegramGiftListID",
                        column: x => x.TelegramGiftListID,
                        principalTable: "TelegramGiftLists",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramTransactionPartner",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    SponsorUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommissionPerMille = table.Column<int>(type: "int", nullable: true),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WithdrawalStateID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestCount = table.Column<int>(type: "int", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AffiliateID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoicePayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionPeriod = table.Column<int>(type: "int", nullable: true),
                    PaidMediaPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramTransactionPartnerUser_GiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PremiumSubscriptionDuration = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramTransactionPartner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramAffiliateInfo_AffiliateID",
                        column: x => x.AffiliateID,
                        principalTable: "TelegramAffiliateInfo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramGifts_GiftID",
                        column: x => x.GiftID,
                        principalTable: "TelegramGifts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramGifts_TelegramTransactionPartnerUser_GiftID",
                        column: x => x.TelegramTransactionPartnerUser_GiftID,
                        principalTable: "TelegramGifts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramRevenueWithdrawalState_WithdrawalStateID",
                        column: x => x.WithdrawalStateID,
                        principalTable: "TelegramRevenueWithdrawalState",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramUsers_SponsorUserID",
                        column: x => x.SponsorUserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramTransactionPartner_TelegramUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramStarTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StarTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NanostarAmount = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiverID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramStarTransactionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStarTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStarTransactions_TelegramStarTransactionsCollections_TelegramStarTransactionsID",
                        column: x => x.TelegramStarTransactionsID,
                        principalTable: "TelegramStarTransactionsCollections",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramStarTransactions_TelegramTransactionPartner_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "TelegramTransactionPartner",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramStarTransactions_TelegramTransactionPartner_SourceID",
                        column: x => x.SourceID,
                        principalTable: "TelegramTransactionPartner",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramGiveawaysCompleted",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnerCount = table.Column<int>(type: "int", nullable: false),
                    UnclaimedPrizeCount = table.Column<int>(type: "int", nullable: true),
                    GiveawayMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsStarGiveaway = table.Column<bool>(type: "bit", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramGiveawaysCompleted", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message_ID = table.Column<int>(type: "int", nullable: false),
                    MessageThreadId = table.Column<int>(type: "int", nullable: true),
                    ForwardFromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForwardFromMessageId = table.Column<int>(type: "int", nullable: true),
                    ForwardSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardSenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserFromID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderBoostCount = table.Column<int>(type: "int", nullable: true),
                    SenderBusinessBotID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BusinessConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardFromChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForwardOriginID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsTopicMessage = table.Column<bool>(type: "bit", nullable: true),
                    IsAutomaticForward = table.Column<bool>(type: "bit", nullable: true),
                    ReplyToMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExternalReplyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TextQuoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReplyToStoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ViaBotID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasProtectedContent = table.Column<bool>(type: "bit", nullable: true),
                    IsFromOffline = table.Column<bool>(type: "bit", nullable: true),
                    MediaGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidStarCount = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkPreviewOptionsLinkPreviewOptions = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EffectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AudioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaidMediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoNoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowCaptionAboveMedia = table.Column<bool>(type: "bit", nullable: true),
                    HasMediaSpoiler = table.Column<bool>(type: "bit", nullable: true),
                    ContactID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GameID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PollID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramMessagePollID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VenueID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeftChatMemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewChatTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteChatPhoto = table.Column<bool>(type: "bit", nullable: true),
                    GroupChatCreated = table.Column<bool>(type: "bit", nullable: true),
                    SupergroupChatCreated = table.Column<bool>(type: "bit", nullable: true),
                    ChannelChatCreated = table.Column<bool>(type: "bit", nullable: true),
                    MessageAutoDeleteTimerChanged = table.Column<int>(type: "int", nullable: true),
                    MigrateToChatId = table.Column<long>(type: "bigint", nullable: true),
                    MigrateFromChatId = table.Column<long>(type: "bigint", nullable: true),
                    PinnedMessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuccessfulPaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RefundedPaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsersSharedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChatSharedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UniqueGiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConnectedWebsite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WriteAccessAllowedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PassportDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProximityAlertTriggeredID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BoostCount = table.Column<int>(type: "int", nullable: true),
                    ChatBackgroundSetID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForumTopicCreatedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForumTopicEditedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ForumTopicClosed = table.Column<bool>(type: "bit", nullable: true),
                    ForumTopicReopened = table.Column<bool>(type: "bit", nullable: true),
                    GeneralForumTopicHidden = table.Column<bool>(type: "bit", nullable: true),
                    GeneralForumTopicUnhidden = table.Column<bool>(type: "bit", nullable: true),
                    GiveawayCreatedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayWinnersID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GiveawayCompletedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaidMessagePriceChanged = table.Column<int>(type: "int", nullable: true),
                    VideoChatScheduledID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VideoChatStarted = table.Column<bool>(type: "bit", nullable: true),
                    VideoChatEnded = table.Column<int>(type: "int", nullable: true),
                    VideoChatParticipantsInvitedID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WebAppDataID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReplyMarkupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdateId = table.Column<int>(type: "int", nullable: false),
                    UpdateType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramAnimations_AnimationID",
                        column: x => x.AnimationID,
                        principalTable: "TelegramAnimations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramAudios_AudioID",
                        column: x => x.AudioID,
                        principalTable: "TelegramAudios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChatBackgrounds_ChatBackgroundSetID",
                        column: x => x.ChatBackgroundSetID,
                        principalTable: "TelegramChatBackgrounds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChatShared_ChatSharedID",
                        column: x => x.ChatSharedID,
                        principalTable: "TelegramChatShared",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChats_ForwardFromChatID",
                        column: x => x.ForwardFromChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramChats_SenderChatID",
                        column: x => x.SenderChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramContacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "TelegramContacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramDices_DiceID",
                        column: x => x.DiceID,
                        principalTable: "TelegramDices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramDocuments_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "TelegramDocuments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramExternalReplyInfos_ExternalReplyID",
                        column: x => x.ExternalReplyID,
                        principalTable: "TelegramExternalReplyInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramForumTopicsCreated_ForumTopicCreatedID",
                        column: x => x.ForumTopicCreatedID,
                        principalTable: "TelegramForumTopicsCreated",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramForumTopicsEdited_ForumTopicEditedID",
                        column: x => x.ForumTopicEditedID,
                        principalTable: "TelegramForumTopicsEdited",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGames_GameID",
                        column: x => x.GameID,
                        principalTable: "TelegramGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGiftInfos_GiftID",
                        column: x => x.GiftID,
                        principalTable: "TelegramGiftInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGiveawaysCompleted_GiveawayCompletedID",
                        column: x => x.GiveawayCompletedID,
                        principalTable: "TelegramGiveawaysCompleted",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGiveawaysCreated_GiveawayCreatedID",
                        column: x => x.GiveawayCreatedID,
                        principalTable: "TelegramGiveawaysCreated",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGiveawaysWinners_GiveawayWinnersID",
                        column: x => x.GiveawayWinnersID,
                        principalTable: "TelegramGiveawaysWinners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramGiveaways_GiveawayID",
                        column: x => x.GiveawayID,
                        principalTable: "TelegramGiveaways",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramInlineKeyboardMarkups_ReplyMarkupID",
                        column: x => x.ReplyMarkupID,
                        principalTable: "TelegramInlineKeyboardMarkups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramInvoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "TelegramInvoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramLinkPreviewOptionss_LinkPreviewOptionsLinkPreviewOptions",
                        column: x => x.LinkPreviewOptionsLinkPreviewOptions,
                        principalTable: "TelegramLinkPreviewOptionss",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramLocations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "TelegramLocations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramMessageOrigin_ForwardOriginID",
                        column: x => x.ForwardOriginID,
                        principalTable: "TelegramMessageOrigin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramMessages_PinnedMessageID",
                        column: x => x.PinnedMessageID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramMessages_ReplyToMessageID",
                        column: x => x.ReplyToMessageID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramPaidMediaInfos_PaidMediaID",
                        column: x => x.PaidMediaID,
                        principalTable: "TelegramPaidMediaInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramPassportDatas_PassportDataID",
                        column: x => x.PassportDataID,
                        principalTable: "TelegramPassportDatas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramPolls_PollID",
                        column: x => x.PollID,
                        principalTable: "TelegramPolls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramProximityAlertTriggereds_ProximityAlertTriggeredID",
                        column: x => x.ProximityAlertTriggeredID,
                        principalTable: "TelegramProximityAlertTriggereds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramRefundedPayments_RefundedPaymentID",
                        column: x => x.RefundedPaymentID,
                        principalTable: "TelegramRefundedPayments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramStories_ReplyToStoryID",
                        column: x => x.ReplyToStoryID,
                        principalTable: "TelegramStories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramStories_StoryID",
                        column: x => x.StoryID,
                        principalTable: "TelegramStories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramSuccessfulPayments_SuccessfulPaymentID",
                        column: x => x.SuccessfulPaymentID,
                        principalTable: "TelegramSuccessfulPayments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramTextQuotes_QuoteID",
                        column: x => x.QuoteID,
                        principalTable: "TelegramTextQuotes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsersShareds_UsersSharedID",
                        column: x => x.UsersSharedID,
                        principalTable: "TelegramUsersShareds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsers_ForwardFromID",
                        column: x => x.ForwardFromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsers_LeftChatMemberID",
                        column: x => x.LeftChatMemberID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsers_SenderBusinessBotID",
                        column: x => x.SenderBusinessBotID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsers_UserFromID",
                        column: x => x.UserFromID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramUsers_ViaBotID",
                        column: x => x.ViaBotID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramVenues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "TelegramVenues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramVideoChatsParticipantsInvited_VideoChatParticipantsInvitedID",
                        column: x => x.VideoChatParticipantsInvitedID,
                        principalTable: "TelegramVideoChatsParticipantsInvited",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramVideoChatsScheduled_VideoChatScheduledID",
                        column: x => x.VideoChatScheduledID,
                        principalTable: "TelegramVideoChatsScheduled",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramVoices_VoiceID",
                        column: x => x.VoiceID,
                        principalTable: "TelegramVoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramWebAppDatas_WebAppDataID",
                        column: x => x.WebAppDataID,
                        principalTable: "TelegramWebAppDatas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramMessages_TelegramWriteAccessAlloweds_WriteAccessAllowedID",
                        column: x => x.WriteAccessAllowedID,
                        principalTable: "TelegramWriteAccessAlloweds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageTelegramMessageEntity",
                columns: table => new
                {
                    CaptionEntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageThisCaptionEntitiesBelongingToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageTelegramMessageEntity", x => new { x.CaptionEntitiesID, x.MessageThisCaptionEntitiesBelongingToID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramMessageEntity_TelegramMessageEntitys_CaptionEntitiesID",
                        column: x => x.CaptionEntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramMessageEntity_TelegramMessages_MessageThisCaptionEntitiesBelongingToID",
                        column: x => x.MessageThisCaptionEntitiesBelongingToID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageTelegramMessageEntity1",
                columns: table => new
                {
                    EntitiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageThisMessageEntitiesBelongingToID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageTelegramMessageEntity1", x => new { x.EntitiesID, x.MessageThisMessageEntitiesBelongingToID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramMessageEntity1_TelegramMessageEntitys_EntitiesID",
                        column: x => x.EntitiesID,
                        principalTable: "TelegramMessageEntitys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramMessageEntity1_TelegramMessages_MessageThisMessageEntitiesBelongingToID",
                        column: x => x.MessageThisMessageEntitiesBelongingToID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageTelegramUser",
                columns: table => new
                {
                    NewChatMembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserNewChatMembersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageTelegramUser", x => new { x.NewChatMembersID, x.UserNewChatMembersID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramUser_TelegramMessages_UserNewChatMembersID",
                        column: x => x.UserNewChatMembersID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramUser_TelegramUsers_NewChatMembersID",
                        column: x => x.NewChatMembersID,
                        principalTable: "TelegramUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageTelegramPhotoSize",
                columns: table => new
                {
                    MessagesAsNewChatPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewChatPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageTelegramPhotoSize", x => new { x.MessagesAsNewChatPhotoID, x.NewChatPhotoID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramPhotoSize_TelegramMessages_MessagesAsNewChatPhotoID",
                        column: x => x.MessagesAsNewChatPhotoID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramMessageTelegramPhotoSize1",
                columns: table => new
                {
                    MessagesAsPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramMessageTelegramPhotoSize1", x => new { x.MessagesAsPhotoID, x.PhotoID });
                    table.ForeignKey(
                        name: "FK_TelegramMessageTelegramPhotoSize1_TelegramMessages_MessagesAsPhotoID",
                        column: x => x.MessagesAsPhotoID,
                        principalTable: "TelegramMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPaidMedia",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TelegramPaidMediaInfoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramTransactionPartnerUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPaidMedia", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPaidMedia_TelegramPaidMediaInfos_TelegramPaidMediaInfoID",
                        column: x => x.TelegramPaidMediaInfoID,
                        principalTable: "TelegramPaidMediaInfos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramPaidMedia_TelegramTransactionPartner_TelegramTransactionPartnerUserID",
                        column: x => x.TelegramTransactionPartnerUserID,
                        principalTable: "TelegramTransactionPartner",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramPhotoSizes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    TelegramPaidMediaPhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TelegramPhotoSizeGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPhotoSizes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizes_TelegramPaidMedia_TelegramPaidMediaPhotoID",
                        column: x => x.TelegramPaidMediaPhotoID,
                        principalTable: "TelegramPaidMedia",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizes_TelegramPhotoSizeGroups_TelegramPhotoSizeGroupID",
                        column: x => x.TelegramPhotoSizeGroupID,
                        principalTable: "TelegramPhotoSizeGroups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramPhotoSizeTelegramSharedUser",
                columns: table => new
                {
                    PhotoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharedUsersID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPhotoSizeTelegramSharedUser", x => new { x.PhotoID, x.SharedUsersID });
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizeTelegramSharedUser_TelegramPhotoSizes_PhotoID",
                        column: x => x.PhotoID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizeTelegramSharedUser_TelegramSharedUsers_SharedUsersID",
                        column: x => x.SharedUsersID,
                        principalTable: "TelegramSharedUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStickerSets",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StickerType = table.Column<int>(type: "int", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStickerSets", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStickerSets_TelegramPhotoSizes_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideoNotes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideoNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramVideoNotes_TelegramPhotoSizes_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramVideos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTimestamp = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramVideos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramVideos_TelegramPhotoSizes_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramStickers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    IsAnimated = table.Column<bool>(type: "bit", nullable: false),
                    IsVideo = table.Column<bool>(type: "bit", nullable: false),
                    ThumbnailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiumAnimationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaskPositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomEmojiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedsRepainting = table.Column<bool>(type: "bit", nullable: false),
                    TelegramStickerSetID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUniqueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramStickers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramStickers_TelegramMaskPositions_MaskPositionID",
                        column: x => x.MaskPositionID,
                        principalTable: "TelegramMaskPositions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramStickers_TelegramPhotoSizes_ThumbnailID",
                        column: x => x.ThumbnailID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramStickers_TelegramStickerSets_TelegramStickerSetID",
                        column: x => x.TelegramStickerSetID,
                        principalTable: "TelegramStickerSets",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TelegramStickers_TelegramTGFiles_PremiumAnimationID",
                        column: x => x.PremiumAnimationID,
                        principalTable: "TelegramTGFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramPhotoSizeTelegramVideo",
                columns: table => new
                {
                    CoverID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoCoversID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramPhotoSizeTelegramVideo", x => new { x.CoverID, x.VideoCoversID });
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizeTelegramVideo_TelegramPhotoSizes_CoverID",
                        column: x => x.CoverID,
                        principalTable: "TelegramPhotoSizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramPhotoSizeTelegramVideo_TelegramVideos_VideoCoversID",
                        column: x => x.VideoCoversID,
                        principalTable: "TelegramVideos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGiftModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RarityPerMille = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGiftModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGiftModels_TelegramStickers_StickerID",
                        column: x => x.StickerID,
                        principalTable: "TelegramStickers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGiftSymbols",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StickerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RarityPerMille = table.Column<int>(type: "int", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGiftSymbols", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGiftSymbols_TelegramStickers_StickerID",
                        column: x => x.StickerID,
                        principalTable: "TelegramStickers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGifts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ModelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SymbolID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BackdropID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGifts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGifts_TelegramUniqueGiftBackdrops_BackdropID",
                        column: x => x.BackdropID,
                        principalTable: "TelegramUniqueGiftBackdrops",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGifts_TelegramUniqueGiftModels_ModelID",
                        column: x => x.ModelID,
                        principalTable: "TelegramUniqueGiftModels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGifts_TelegramUniqueGiftSymbols_SymbolID",
                        column: x => x.SymbolID,
                        principalTable: "TelegramUniqueGiftSymbols",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelegramUniqueGiftInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnedGiftId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferStarCount = table.Column<int>(type: "int", nullable: true),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUniqueGiftInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramUniqueGiftInfos_TelegramUniqueGifts_GiftID",
                        column: x => x.GiftID,
                        principalTable: "TelegramUniqueGifts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPushSubscriptions_UserID",
                table: "ApplicationPushSubscriptions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersLoginInfos_LoginProviderName_ProviderUserKey",
                table: "ApplicationUsersLoginInfos",
                columns: new[] { "LoginProviderName", "ProviderUserKey" },
                unique: true,
                filter: "[LoginProviderName] IS NOT NULL AND [ProviderUserKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersLoginInfos_UserForeignKey",
                table: "ApplicationUsersLoginInfos",
                column: "UserForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyActionPermissionObject_RoleID",
                table: "PermissionPolicyActionPermissionObject",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyMemberPermissionsObject_TypePermissionObjectID",
                table: "PermissionPolicyMemberPermissionsObject",
                column: "TypePermissionObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyNavigationPermissionObject_RoleID",
                table: "PermissionPolicyNavigationPermissionObject",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyObjectPermissionsObject_TypePermissionObjectID",
                table: "PermissionPolicyObjectPermissionsObject",
                column: "TypePermissionObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyRoleBase_ChatAdministratorRightsID",
                table: "PermissionPolicyRoleBase",
                column: "ChatAdministratorRightsID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionPolicyTypePermissionObject_RoleID",
                table: "PermissionPolicyTypePermissionObject",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TacoPermissionPolicyRoleTacoPermissionPolicyUser_UsersID",
                table: "TacoPermissionPolicyRoleTacoPermissionPolicyUser",
                column: "UsersID");

            migrationBuilder.CreateIndex(
                name: "IX_TacoPermissionPolicyRoleTacoTeam_TeamsThisRoleBelongsToID",
                table: "TacoPermissionPolicyRoleTacoTeam",
                column: "TeamsThisRoleBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TacoTeamChats_BotAssignedID",
                table: "TacoTeamChats",
                column: "BotAssignedID");

            migrationBuilder.CreateIndex(
                name: "IX_TacoTeamChats_TeamChatID",
                table: "TacoTeamChats",
                column: "TeamChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TacoTeams_TeamAdminChatID",
                table: "TacoTeams",
                column: "TeamAdminChatID",
                unique: true,
                filter: "[TeamAdminChatID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TacoTeamTacoTeamChat_TeamThisTeamChatBelongsToID",
                table: "TacoTeamTacoTeamChat",
                column: "TeamThisTeamChatBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAffiliateInfo_AffiliateChatID",
                table: "TelegramAffiliateInfo",
                column: "AffiliateChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAffiliateInfo_AffiliateUserID",
                table: "TelegramAffiliateInfo",
                column: "AffiliateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAnimations_ThumbnailID",
                table: "TelegramAnimations",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramApiResponses_ParametersID",
                table: "TelegramApiResponses",
                column: "ParametersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAudios_ThumbnailID",
                table: "TelegramAudios",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramAuthorizationRequestParameters_PassportScopeID",
                table: "TelegramAuthorizationRequestParameters",
                column: "PassportScopeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBackgroundType_DocumentID",
                table: "TelegramBackgroundType",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBackgroundType_FillID",
                table: "TelegramBackgroundType",
                column: "FillID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBackgroundType_TelegramBackgroundTypePattern_FillID",
                table: "TelegramBackgroundType",
                column: "TelegramBackgroundTypePattern_FillID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBackgroundType_TelegramBackgroundTypeWallpaper_DocumentID",
                table: "TelegramBackgroundType",
                column: "TelegramBackgroundTypeWallpaper_DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotCommandScopesChat_ChatIdID",
                table: "TelegramBotCommandScopesChat",
                column: "ChatIdID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotCommandScopesChatAdministrators_ChatIdID",
                table: "TelegramBotCommandScopesChatAdministrators",
                column: "ChatIdID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBotCommandScopesChatMember_ChatIdID",
                table: "TelegramBotCommandScopesChatMember",
                column: "ChatIdID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessBusinessMessagesDeleteds_ChatID",
                table: "TelegramBusinessBusinessMessagesDeleteds",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessConnections_RightsID",
                table: "TelegramBusinessConnections",
                column: "RightsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessConnections_UserID",
                table: "TelegramBusinessConnections",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessIntros_StickerID",
                table: "TelegramBusinessIntros",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessLocations_LocationID",
                table: "TelegramBusinessLocations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramBusinessOpeningHoursIntervals_TelegramBusinessOpeningHoursID",
                table: "TelegramBusinessOpeningHoursIntervals",
                column: "TelegramBusinessOpeningHoursID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramCallbackQueries_FromID",
                table: "TelegramCallbackQueries",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramCallbackQueries_TelegramCallbackQueryMessageID",
                table: "TelegramCallbackQueries",
                column: "TelegramCallbackQueryMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBackgrounds_TypeID",
                table: "TelegramChatBackgrounds",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostRemoves_ChatID",
                table: "TelegramChatBoostRemoves",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostRemoves_SourceID",
                table: "TelegramChatBoostRemoves",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoosts_SourceID",
                table: "TelegramChatBoosts",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoosts_TelegramUserChatBoostsID",
                table: "TelegramChatBoosts",
                column: "TelegramUserChatBoostsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostSource_TelegramChatBoostSourceGiveaway_UserID",
                table: "TelegramChatBoostSource",
                column: "TelegramChatBoostSourceGiveaway_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostSource_TelegramChatBoostSourcePremium_UserID",
                table: "TelegramChatBoostSource",
                column: "TelegramChatBoostSourcePremium_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostSource_UserID",
                table: "TelegramChatBoostSource",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostUpdates_BoostID",
                table: "TelegramChatBoostUpdates",
                column: "BoostID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBoostUpdates_ChatID",
                table: "TelegramChatBoostUpdates",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBotRightsUsers_BotUserID",
                table: "TelegramChatBotRightsUsers",
                column: "BotUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBotRightsUsers_ChatAdministratorRightsID",
                table: "TelegramChatBotRightsUsers",
                column: "ChatAdministratorRightsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatBotRightsUsers_ChatID",
                table: "TelegramChatBotRightsUsers",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_AcceptedGiftTypesID",
                table: "TelegramChatFullInfos",
                column: "AcceptedGiftTypesID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_BirthdateID",
                table: "TelegramChatFullInfos",
                column: "BirthdateID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_BusinessIntroID",
                table: "TelegramChatFullInfos",
                column: "BusinessIntroID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_BusinessLocationID",
                table: "TelegramChatFullInfos",
                column: "BusinessLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_BusinessOpeningHoursID",
                table: "TelegramChatFullInfos",
                column: "BusinessOpeningHoursID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_ChatID",
                table: "TelegramChatFullInfos",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_LocationID",
                table: "TelegramChatFullInfos",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_PermissionsID",
                table: "TelegramChatFullInfos",
                column: "PermissionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_PersonalChatID",
                table: "TelegramChatFullInfos",
                column: "PersonalChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_PhotoID",
                table: "TelegramChatFullInfos",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatFullInfos_PinnedMessageID",
                table: "TelegramChatFullInfos",
                column: "PinnedMessageID",
                unique: true,
                filter: "[PinnedMessageID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatInviteLinks_CreatorID",
                table: "TelegramChatInviteLinks",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatJoinRequests_ChatID",
                table: "TelegramChatJoinRequests",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatJoinRequests_FromID",
                table: "TelegramChatJoinRequests",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatJoinRequests_InviteLinkID",
                table: "TelegramChatJoinRequests",
                column: "InviteLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatLocations_LocationID",
                table: "TelegramChatLocations",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMember_UserID",
                table: "TelegramChatMember",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMemberUpdates_ChatID",
                table: "TelegramChatMemberUpdates",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMemberUpdates_FromID",
                table: "TelegramChatMemberUpdates",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMemberUpdates_InviteLinkID",
                table: "TelegramChatMemberUpdates",
                column: "InviteLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMemberUpdates_NewChatMemberID",
                table: "TelegramChatMemberUpdates",
                column: "NewChatMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatMemberUpdates_OldChatMemberID",
                table: "TelegramChatMemberUpdates",
                column: "OldChatMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChats_ChatId",
                table: "TelegramChats",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatSharedTelegramPhotoSize_SharedChatsID",
                table: "TelegramChatSharedTelegramPhotoSize",
                column: "SharedChatsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatsToIgnore_ChatID",
                table: "TelegramChatsToIgnore",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatTelegramGiveaway_GiveawayThisChatsBelongsToID",
                table: "TelegramChatTelegramGiveaway",
                column: "GiveawayThisChatsBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatTelegramUserChat_TelegramUserChatsThisChatBelongsToID",
                table: "TelegramChatTelegramUserChat",
                column: "TelegramUserChatsThisChatBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChosenInlineResults_FromID",
                table: "TelegramChosenInlineResults",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChosenInlineResults_LocationID",
                table: "TelegramChosenInlineResults",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramCredentialss_SecureDataID",
                table: "TelegramCredentialss",
                column: "SecureDataID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramDocuments_ThumbnailID",
                table: "TelegramDocuments",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElements_FrontSideID",
                table: "TelegramEncryptedPassportElements",
                column: "FrontSideID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElements_ReverseSideID",
                table: "TelegramEncryptedPassportElements",
                column: "ReverseSideID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElements_SelfieID",
                table: "TelegramEncryptedPassportElements",
                column: "SelfieID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElementTelegramPassportData_PassportDataThisEncryptedPassportElementBelongsToID",
                table: "TelegramEncryptedPassportElementTelegramPassportData",
                column: "PassportDataThisEncryptedPassportElementBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElementTelegramPassportFile_FilesID",
                table: "TelegramEncryptedPassportElementTelegramPassportFile",
                column: "FilesID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramEncryptedPassportElementTelegramPassportFile1_TranslationID",
                table: "TelegramEncryptedPassportElementTelegramPassportFile1",
                column: "TranslationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_AnimationID",
                table: "TelegramExternalReplyInfos",
                column: "AnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_AudioID",
                table: "TelegramExternalReplyInfos",
                column: "AudioID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_ChatID",
                table: "TelegramExternalReplyInfos",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_ContactID",
                table: "TelegramExternalReplyInfos",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_DiceID",
                table: "TelegramExternalReplyInfos",
                column: "DiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_DocumentID",
                table: "TelegramExternalReplyInfos",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_GameID",
                table: "TelegramExternalReplyInfos",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_GiveawayID",
                table: "TelegramExternalReplyInfos",
                column: "GiveawayID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_GiveawayWinnersID",
                table: "TelegramExternalReplyInfos",
                column: "GiveawayWinnersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_InvoiceID",
                table: "TelegramExternalReplyInfos",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_LinkPreviewOptionsID",
                table: "TelegramExternalReplyInfos",
                column: "LinkPreviewOptionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_LocationID",
                table: "TelegramExternalReplyInfos",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_OriginID",
                table: "TelegramExternalReplyInfos",
                column: "OriginID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_PaidMediaID",
                table: "TelegramExternalReplyInfos",
                column: "PaidMediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_PollID",
                table: "TelegramExternalReplyInfos",
                column: "PollID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_StickerID",
                table: "TelegramExternalReplyInfos",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_StoryID",
                table: "TelegramExternalReplyInfos",
                column: "StoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_VenueID",
                table: "TelegramExternalReplyInfos",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_VideoID",
                table: "TelegramExternalReplyInfos",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_VideoNoteID",
                table: "TelegramExternalReplyInfos",
                column: "VideoNoteID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfos_VoiceID",
                table: "TelegramExternalReplyInfos",
                column: "VoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramExternalReplyInfoTelegramPhotoSize_PhotoID",
                table: "TelegramExternalReplyInfoTelegramPhotoSize",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramFileCredentialsTelegramSecureValue_SecureFilesID",
                table: "TelegramFileCredentialsTelegramSecureValue",
                column: "SecureFilesID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramFileCredentialsTelegramSecureValue1_TranslationID",
                table: "TelegramFileCredentialsTelegramSecureValue1",
                column: "TranslationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGameHighScores_UserID",
                table: "TelegramGameHighScores",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGames_AnimationID",
                table: "TelegramGames",
                column: "AnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGameTelegramMessageEntity_TextEntitiesID",
                table: "TelegramGameTelegramMessageEntity",
                column: "TextEntitiesID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGameTelegramPhotoSize_PhotoID",
                table: "TelegramGameTelegramPhotoSize",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGiftInfos_GiftID",
                table: "TelegramGiftInfos",
                column: "GiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGiftInfoTelegramMessageEntity_GiftInfoThisMessageEntityBelongsToID",
                table: "TelegramGiftInfoTelegramMessageEntity",
                column: "GiftInfoThisMessageEntityBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGifts_StickerID",
                table: "TelegramGifts",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGifts_TelegramGiftListID",
                table: "TelegramGifts",
                column: "TelegramGiftListID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGiveawaysCompleted_GiveawayMessageID",
                table: "TelegramGiveawaysCompleted",
                column: "GiveawayMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGiveawaysWinners_ChatID",
                table: "TelegramGiveawaysWinners",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramGiveawayWinnersTelegramUser_WinnersID",
                table: "TelegramGiveawayWinnersTelegramUser",
                column: "WinnersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_CallbackGameID",
                table: "TelegramInlineKeyboardButtons",
                column: "CallbackGameID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_CopyTextID",
                table: "TelegramInlineKeyboardButtons",
                column: "CopyTextID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_LoginUrlID",
                table: "TelegramInlineKeyboardButtons",
                column: "LoginUrlID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_SwitchInlineQueryChosenChatID",
                table: "TelegramInlineKeyboardButtons",
                column: "SwitchInlineQueryChosenChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_TelegramInlineKeyboardRowID",
                table: "TelegramInlineKeyboardButtons",
                column: "TelegramInlineKeyboardRowID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardButtons_WebAppID",
                table: "TelegramInlineKeyboardButtons",
                column: "WebAppID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineKeyboardRows_ReplyKeyboardMarkupID",
                table: "TelegramInlineKeyboardRows",
                column: "ReplyKeyboardMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultArticles_InputMessageContentID",
                table: "TelegramInlineQueryResultArticles",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultArticles_ReplyMarkupID",
                table: "TelegramInlineQueryResultArticles",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultAudios_InputMessageContentID",
                table: "TelegramInlineQueryResultAudios",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultAudios_ReplyMarkupID",
                table: "TelegramInlineQueryResultAudios",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedAudios_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedAudios",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedAudios_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedAudios",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedDocuments_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedDocuments",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedDocuments_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedDocuments",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedGifs_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedGifs",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedGifs_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedGifs",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedMpeg4Gifs_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedMpeg4Gifs",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedMpeg4Gifs_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedMpeg4Gifs",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedPhotos_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedPhotos",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedPhotos_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedPhotos",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedStickers_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedStickers",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedStickers_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedStickers",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedVideos_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedVideos",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedVideos_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedVideos",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedVoices_InputMessageContentID",
                table: "TelegramInlineQueryResultCachedVoices",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultCachedVoices_ReplyMarkupID",
                table: "TelegramInlineQueryResultCachedVoices",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultContacts_InputMessageContentID",
                table: "TelegramInlineQueryResultContacts",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultContacts_ReplyMarkupID",
                table: "TelegramInlineQueryResultContacts",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultDocuments_InputMessageContentID",
                table: "TelegramInlineQueryResultDocuments",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultDocuments_ReplyMarkupID",
                table: "TelegramInlineQueryResultDocuments",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultGames_InputMessageContentID",
                table: "TelegramInlineQueryResultGames",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultGames_ReplyMarkupID",
                table: "TelegramInlineQueryResultGames",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultGifs_InputMessageContentID",
                table: "TelegramInlineQueryResultGifs",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultGifs_ReplyMarkupID",
                table: "TelegramInlineQueryResultGifs",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultLocations_InputMessageContentID",
                table: "TelegramInlineQueryResultLocations",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultLocations_ReplyMarkupID",
                table: "TelegramInlineQueryResultLocations",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultMpeg4Gifs_InputMessageContentID",
                table: "TelegramInlineQueryResultMpeg4Gifs",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultMpeg4Gifs_ReplyMarkupID",
                table: "TelegramInlineQueryResultMpeg4Gifs",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultPhotos_InputMessageContentID",
                table: "TelegramInlineQueryResultPhotos",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultPhotos_ReplyMarkupID",
                table: "TelegramInlineQueryResultPhotos",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVenues_InputMessageContentID",
                table: "TelegramInlineQueryResultVenues",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVenues_ReplyMarkupID",
                table: "TelegramInlineQueryResultVenues",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVideos_InputMessageContentID",
                table: "TelegramInlineQueryResultVideos",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVideos_ReplyMarkupID",
                table: "TelegramInlineQueryResultVideos",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVoices_InputMessageContentID",
                table: "TelegramInlineQueryResultVoices",
                column: "InputMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQueryResultVoices_ReplyMarkupID",
                table: "TelegramInlineQueryResultVoices",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQuerys_FromID",
                table: "TelegramInlineQuerys",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInlineQuerys_LocationID",
                table: "TelegramInlineQuerys",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaAnimations_MediaID",
                table: "TelegramInputMediaAnimations",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaAnimations_ThumbnailID",
                table: "TelegramInputMediaAnimations",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaAudios_MediaID",
                table: "TelegramInputMediaAudios",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaAudios_ThumbnailID",
                table: "TelegramInputMediaAudios",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaDocuments_MediaID",
                table: "TelegramInputMediaDocuments",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaDocuments_ThumbnailID",
                table: "TelegramInputMediaDocuments",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaPhotos_MediaID",
                table: "TelegramInputMediaPhotos",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaVideos_CoverID",
                table: "TelegramInputMediaVideos",
                column: "CoverID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaVideos_MediaID",
                table: "TelegramInputMediaVideos",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMediaVideos_ThumbnailID",
                table: "TelegramInputMediaVideos",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputMessageContent_LinkPreviewOptionsID",
                table: "TelegramInputMessageContent",
                column: "LinkPreviewOptionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputPaidMediaPhotos_MediaID",
                table: "TelegramInputPaidMediaPhotos",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputPaidMediaVideos_CoverID",
                table: "TelegramInputPaidMediaVideos",
                column: "CoverID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputPaidMediaVideos_MediaID",
                table: "TelegramInputPaidMediaVideos",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputPaidMediaVideos_ThumbnailID",
                table: "TelegramInputPaidMediaVideos",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputProfilePhotoAnimateds_AnimationID",
                table: "TelegramInputProfilePhotoAnimateds",
                column: "AnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputProfilePhotoStatics_PhotoID",
                table: "TelegramInputProfilePhotoStatics",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputStickers_MaskPositionID",
                table: "TelegramInputStickers",
                column: "MaskPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputStickers_StickerID",
                table: "TelegramInputStickers",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputStoryContentPhotos_PhotoID",
                table: "TelegramInputStoryContentPhotos",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramInputStoryContentVideos_VideoID",
                table: "TelegramInputStoryContentVideos",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtonRequestChats_BotAdministratorRightsID",
                table: "TelegramKeyboardButtonRequestChats",
                column: "BotAdministratorRightsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtonRequestChats_UserAdministratorRightsID",
                table: "TelegramKeyboardButtonRequestChats",
                column: "UserAdministratorRightsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtons_RequestChatID",
                table: "TelegramKeyboardButtons",
                column: "RequestChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtons_RequestPollID",
                table: "TelegramKeyboardButtons",
                column: "RequestPollID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtons_RequestUsersID",
                table: "TelegramKeyboardButtons",
                column: "RequestUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtons_TelegramKeyboardRowID",
                table: "TelegramKeyboardButtons",
                column: "TelegramKeyboardRowID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardButtons_WebAppID",
                table: "TelegramKeyboardButtons",
                column: "WebAppID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramKeyboardRows_ReplyKeyboardMarkupID",
                table: "TelegramKeyboardRows",
                column: "ReplyKeyboardMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramLabeledPrices_TelegramInputInvoiceMessageContentID",
                table: "TelegramLabeledPrices",
                column: "TelegramInputInvoiceMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramLabeledPrices_TelegramShippingOptionID",
                table: "TelegramLabeledPrices",
                column: "TelegramShippingOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMenuButtonWebApps_WebAppID",
                table: "TelegramMenuButtonWebApps",
                column: "WebAppID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultAudioID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultAudioID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedAudioID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedAudioID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedDocumentID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedGifID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedGifID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedMpeg4GifID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedMpeg4GifID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedPhotoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedVideoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultCachedVoiceID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultCachedVoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultDocumentID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultGifID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultGifID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultMpeg4GifID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultMpeg4GifID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultPhotoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultVideoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInlineQueryResultVoiceID",
                table: "TelegramMessageEntitys",
                column: "TelegramInlineQueryResultVoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputMediaAnimationID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputMediaAnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputMediaAudioID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputMediaAudioID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputMediaDocumentID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputMediaDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputMediaPhotoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputMediaPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputMediaVideoID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputMediaVideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputPollOptionID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputPollOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramInputTextMessageContentID",
                table: "TelegramMessageEntitys",
                column: "TelegramInputTextMessageContentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramPollOptionID",
                table: "TelegramMessageEntitys",
                column: "TelegramPollOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_TelegramReplyParametersID",
                table: "TelegramMessageEntitys",
                column: "TelegramReplyParametersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntitys_UserID",
                table: "TelegramMessageEntitys",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntityTelegramPoll_PollExplanationEntitiesToThisPollID",
                table: "TelegramMessageEntityTelegramPoll",
                column: "PollExplanationEntitiesToThisPollID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntityTelegramPoll1_QuestionEntitiesID",
                table: "TelegramMessageEntityTelegramPoll1",
                column: "QuestionEntitiesID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageEntityTelegramTextQuote_TextQuoteThisMessageEntityBelongsToID",
                table: "TelegramMessageEntityTelegramTextQuote",
                column: "TextQuoteThisMessageEntityBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageOrigin_ChatID",
                table: "TelegramMessageOrigin",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageOrigin_SenderChatID",
                table: "TelegramMessageOrigin",
                column: "SenderChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageOrigin_SenderUserID",
                table: "TelegramMessageOrigin",
                column: "SenderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionCountUpdatedTelegramReactionCount_ReactionsID",
                table: "TelegramMessageReactionCountUpdatedTelegramReactionCount",
                column: "ReactionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionCountUpdates_ChatID",
                table: "TelegramMessageReactionCountUpdates",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionNewJoin_TelegramMessageReactionUpdatedId",
                table: "TelegramMessageReactionNewJoin",
                column: "TelegramMessageReactionUpdatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionNewJoin_TelegramReactionTypeId",
                table: "TelegramMessageReactionNewJoin",
                column: "TelegramReactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionOldJoin_TelegramMessageReactionUpdatedId",
                table: "TelegramMessageReactionOldJoin",
                column: "TelegramMessageReactionUpdatedId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionOldJoin_TelegramReactionTypeId",
                table: "TelegramMessageReactionOldJoin",
                column: "TelegramReactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionUpdates_ActorChatID",
                table: "TelegramMessageReactionUpdates",
                column: "ActorChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionUpdates_ChatID",
                table: "TelegramMessageReactionUpdates",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageReactionUpdates_UserID",
                table: "TelegramMessageReactionUpdates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_AnimationID",
                table: "TelegramMessages",
                column: "AnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_AudioID",
                table: "TelegramMessages",
                column: "AudioID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ChatBackgroundSetID",
                table: "TelegramMessages",
                column: "ChatBackgroundSetID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ChatID",
                table: "TelegramMessages",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ChatSharedID",
                table: "TelegramMessages",
                column: "ChatSharedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ContactID",
                table: "TelegramMessages",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_DiceID",
                table: "TelegramMessages",
                column: "DiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_DocumentID",
                table: "TelegramMessages",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ExternalReplyID",
                table: "TelegramMessages",
                column: "ExternalReplyID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ForumTopicCreatedID",
                table: "TelegramMessages",
                column: "ForumTopicCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ForumTopicEditedID",
                table: "TelegramMessages",
                column: "ForumTopicEditedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ForwardFromChatID",
                table: "TelegramMessages",
                column: "ForwardFromChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ForwardFromID",
                table: "TelegramMessages",
                column: "ForwardFromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ForwardOriginID",
                table: "TelegramMessages",
                column: "ForwardOriginID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GameID",
                table: "TelegramMessages",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GiftID",
                table: "TelegramMessages",
                column: "GiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GiveawayCompletedID",
                table: "TelegramMessages",
                column: "GiveawayCompletedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GiveawayCreatedID",
                table: "TelegramMessages",
                column: "GiveawayCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GiveawayID",
                table: "TelegramMessages",
                column: "GiveawayID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_GiveawayWinnersID",
                table: "TelegramMessages",
                column: "GiveawayWinnersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_InvoiceID",
                table: "TelegramMessages",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_LeftChatMemberID",
                table: "TelegramMessages",
                column: "LeftChatMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_LinkPreviewOptionsLinkPreviewOptions",
                table: "TelegramMessages",
                column: "LinkPreviewOptionsLinkPreviewOptions");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_LocationID",
                table: "TelegramMessages",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_PaidMediaID",
                table: "TelegramMessages",
                column: "PaidMediaID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_PassportDataID",
                table: "TelegramMessages",
                column: "PassportDataID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_PinnedMessageID",
                table: "TelegramMessages",
                column: "PinnedMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_PollID",
                table: "TelegramMessages",
                column: "PollID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ProximityAlertTriggeredID",
                table: "TelegramMessages",
                column: "ProximityAlertTriggeredID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_QuoteID",
                table: "TelegramMessages",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_RefundedPaymentID",
                table: "TelegramMessages",
                column: "RefundedPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ReplyMarkupID",
                table: "TelegramMessages",
                column: "ReplyMarkupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ReplyToMessageID",
                table: "TelegramMessages",
                column: "ReplyToMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ReplyToStoryID",
                table: "TelegramMessages",
                column: "ReplyToStoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_SenderBusinessBotID",
                table: "TelegramMessages",
                column: "SenderBusinessBotID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_SenderChatID",
                table: "TelegramMessages",
                column: "SenderChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_StickerID",
                table: "TelegramMessages",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_StoryID",
                table: "TelegramMessages",
                column: "StoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_SuccessfulPaymentID",
                table: "TelegramMessages",
                column: "SuccessfulPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_UniqueGiftID",
                table: "TelegramMessages",
                column: "UniqueGiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_UserFromID",
                table: "TelegramMessages",
                column: "UserFromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_UsersSharedID",
                table: "TelegramMessages",
                column: "UsersSharedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VenueID",
                table: "TelegramMessages",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_ViaBotID",
                table: "TelegramMessages",
                column: "ViaBotID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VideoChatParticipantsInvitedID",
                table: "TelegramMessages",
                column: "VideoChatParticipantsInvitedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VideoChatScheduledID",
                table: "TelegramMessages",
                column: "VideoChatScheduledID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VideoID",
                table: "TelegramMessages",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VideoNoteID",
                table: "TelegramMessages",
                column: "VideoNoteID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_VoiceID",
                table: "TelegramMessages",
                column: "VoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_WebAppDataID",
                table: "TelegramMessages",
                column: "WebAppDataID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessages_WriteAccessAllowedID",
                table: "TelegramMessages",
                column: "WriteAccessAllowedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageTelegramMessageEntity_MessageThisCaptionEntitiesBelongingToID",
                table: "TelegramMessageTelegramMessageEntity",
                column: "MessageThisCaptionEntitiesBelongingToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageTelegramMessageEntity1_MessageThisMessageEntitiesBelongingToID",
                table: "TelegramMessageTelegramMessageEntity1",
                column: "MessageThisMessageEntitiesBelongingToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageTelegramPhotoSize_NewChatPhotoID",
                table: "TelegramMessageTelegramPhotoSize",
                column: "NewChatPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageTelegramPhotoSize1_PhotoID",
                table: "TelegramMessageTelegramPhotoSize1",
                column: "PhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramMessageTelegramUser_UserNewChatMembersID",
                table: "TelegramMessageTelegramUser",
                column: "UserNewChatMembersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramOrderInfos_ShippingAddressID",
                table: "TelegramOrderInfos",
                column: "ShippingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPaidMedia_TelegramPaidMediaInfoID",
                table: "TelegramPaidMedia",
                column: "TelegramPaidMediaInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPaidMedia_TelegramTransactionPartnerUserID",
                table: "TelegramPaidMedia",
                column: "TelegramTransactionPartnerUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPaidMedia_VideoID",
                table: "TelegramPaidMedia",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPaidMediaPurchases_FromID",
                table: "TelegramPaidMediaPurchases",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPassportDatas_CredentialsID",
                table: "TelegramPassportDatas",
                column: "CredentialsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPassportScopeElement_TelegramPassportScopeElementOneOfSeveralID",
                table: "TelegramPassportScopeElement",
                column: "TelegramPassportScopeElementOneOfSeveralID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPassportScopeElement_TelegramPassportScopeID",
                table: "TelegramPassportScopeElement",
                column: "TelegramPassportScopeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPhotoSizeGroups_TelegramUserProfilePhotosID",
                table: "TelegramPhotoSizeGroups",
                column: "TelegramUserProfilePhotosID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPhotoSizes_TelegramPaidMediaPhotoID",
                table: "TelegramPhotoSizes",
                column: "TelegramPaidMediaPhotoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPhotoSizes_TelegramPhotoSizeGroupID",
                table: "TelegramPhotoSizes",
                column: "TelegramPhotoSizeGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPhotoSizeTelegramSharedUser_SharedUsersID",
                table: "TelegramPhotoSizeTelegramSharedUser",
                column: "SharedUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPhotoSizeTelegramVideo_VideoCoversID",
                table: "TelegramPhotoSizeTelegramVideo",
                column: "VideoCoversID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPollAnswers_TelegramPollAnswerPollID",
                table: "TelegramPollAnswers",
                column: "TelegramPollAnswerPollID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPollAnswers_UserID",
                table: "TelegramPollAnswers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPollAnswers_VoterChatID",
                table: "TelegramPollAnswers",
                column: "VoterChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPollOptions_PollToPollOptionsID",
                table: "TelegramPollOptions",
                column: "PollToPollOptionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPolls_PollId",
                table: "TelegramPolls",
                column: "PollId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPreCheckoutQueries_FromID",
                table: "TelegramPreCheckoutQueries",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramPreCheckoutQueries_OrderInfoID",
                table: "TelegramPreCheckoutQueries",
                column: "OrderInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramProximityAlertTriggereds_TravelerID",
                table: "TelegramProximityAlertTriggereds",
                column: "TravelerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramProximityAlertTriggereds_WatcherID",
                table: "TelegramProximityAlertTriggereds",
                column: "WatcherID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReactionCounts_TypeID",
                table: "TelegramReactionCounts",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReactionType_TelegramChatFullInfoID",
                table: "TelegramReactionType",
                column: "TelegramChatFullInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramReplyParameterss_ChatIdID",
                table: "TelegramReplyParameterss",
                column: "ChatIdID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_AddressID",
                table: "TelegramSecureDatas",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_BankStatementID",
                table: "TelegramSecureDatas",
                column: "BankStatementID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_DriverLicenseID",
                table: "TelegramSecureDatas",
                column: "DriverLicenseID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_IdentityCardID",
                table: "TelegramSecureDatas",
                column: "IdentityCardID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_InternalPassportID",
                table: "TelegramSecureDatas",
                column: "InternalPassportID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_PassportID",
                table: "TelegramSecureDatas",
                column: "PassportID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_PassportRegistrationID",
                table: "TelegramSecureDatas",
                column: "PassportRegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_PersonalDetailsID",
                table: "TelegramSecureDatas",
                column: "PersonalDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_RentalAgreementID",
                table: "TelegramSecureDatas",
                column: "RentalAgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_TemporaryRegistrationID",
                table: "TelegramSecureDatas",
                column: "TemporaryRegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureDatas_UtilityBillID",
                table: "TelegramSecureDatas",
                column: "UtilityBillID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureValues_DataID",
                table: "TelegramSecureValues",
                column: "DataID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureValues_FrontSideID",
                table: "TelegramSecureValues",
                column: "FrontSideID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureValues_ReverseSideID",
                table: "TelegramSecureValues",
                column: "ReverseSideID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSecureValues_SelfieID",
                table: "TelegramSecureValues",
                column: "SelfieID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSharedUserTelegramUsersShared_UsersSharedThisUsersSharedsBelongsToID",
                table: "TelegramSharedUserTelegramUsersShared",
                column: "UsersSharedThisUsersSharedsBelongsToID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramShippingQueries_FromID",
                table: "TelegramShippingQueries",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramShippingQueries_ShippingAddressID",
                table: "TelegramShippingQueries",
                column: "ShippingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStarTransactions_ReceiverID",
                table: "TelegramStarTransactions",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStarTransactions_SourceID",
                table: "TelegramStarTransactions",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStarTransactions_TelegramStarTransactionsID",
                table: "TelegramStarTransactions",
                column: "TelegramStarTransactionsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStickers_MaskPositionID",
                table: "TelegramStickers",
                column: "MaskPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStickers_PremiumAnimationID",
                table: "TelegramStickers",
                column: "PremiumAnimationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStickers_TelegramStickerSetID",
                table: "TelegramStickers",
                column: "TelegramStickerSetID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStickers_ThumbnailID",
                table: "TelegramStickers",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStickerSets_ThumbnailID",
                table: "TelegramStickerSets",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStories_ChatID",
                table: "TelegramStories",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStoryAreas_PositionID",
                table: "TelegramStoryAreas",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStoryAreas_TypeID",
                table: "TelegramStoryAreas",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStoryAreaType_AddressID",
                table: "TelegramStoryAreaType",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramStoryAreaType_ReactionTypeID",
                table: "TelegramStoryAreaType",
                column: "ReactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramSuccessfulPayments_OrderInfoID",
                table: "TelegramSuccessfulPayments",
                column: "OrderInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_AffiliateID",
                table: "TelegramTransactionPartner",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_ChatID",
                table: "TelegramTransactionPartner",
                column: "ChatID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_GiftID",
                table: "TelegramTransactionPartner",
                column: "GiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_SponsorUserID",
                table: "TelegramTransactionPartner",
                column: "SponsorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_TelegramTransactionPartnerUser_GiftID",
                table: "TelegramTransactionPartner",
                column: "TelegramTransactionPartnerUser_GiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_UserID",
                table: "TelegramTransactionPartner",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramTransactionPartner_WithdrawalStateID",
                table: "TelegramTransactionPartner",
                column: "WithdrawalStateID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGiftBackdrops_ColorsID",
                table: "TelegramUniqueGiftBackdrops",
                column: "ColorsID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGiftInfos_GiftID",
                table: "TelegramUniqueGiftInfos",
                column: "GiftID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGiftModels_StickerID",
                table: "TelegramUniqueGiftModels",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGifts_BackdropID",
                table: "TelegramUniqueGifts",
                column: "BackdropID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGifts_ModelID",
                table: "TelegramUniqueGifts",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGifts_SymbolID",
                table: "TelegramUniqueGifts",
                column: "SymbolID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUniqueGiftSymbols_StickerID",
                table: "TelegramUniqueGiftSymbols",
                column: "StickerID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUserChats_UserID",
                table: "TelegramUserChats",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUsers_ApplicationUserThisTelegramUserBelongsToID",
                table: "TelegramUsers",
                column: "ApplicationUserThisTelegramUserBelongsToID",
                unique: true,
                filter: "[ApplicationUserThisTelegramUserBelongsToID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUsers_TelegramVideoChatParticipantsInvitedID",
                table: "TelegramUsers",
                column: "TelegramVideoChatParticipantsInvitedID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramUsers_UserId",
                table: "TelegramUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelegramVenues_LocationID",
                table: "TelegramVenues",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramVideoNotes_ThumbnailID",
                table: "TelegramVideoNotes",
                column: "ThumbnailID");

            migrationBuilder.CreateIndex(
                name: "IX_TelegramVideos_ThumbnailID",
                table: "TelegramVideos",
                column: "ThumbnailID");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramAnimations_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramAnimations",
                column: "ThumbnailID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramAudios_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramAudios",
                column: "ThumbnailID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBackgroundType_TelegramDocuments_DocumentID",
                table: "TelegramBackgroundType",
                column: "DocumentID",
                principalTable: "TelegramDocuments",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBackgroundType_TelegramDocuments_TelegramBackgroundTypeWallpaper_DocumentID",
                table: "TelegramBackgroundType",
                column: "TelegramBackgroundTypeWallpaper_DocumentID",
                principalTable: "TelegramDocuments",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramBusinessIntros_TelegramStickers_StickerID",
                table: "TelegramBusinessIntros",
                column: "StickerID",
                principalTable: "TelegramStickers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramCallbackQueries_TelegramMessages_TelegramCallbackQueryMessageID",
                table: "TelegramCallbackQueries",
                column: "TelegramCallbackQueryMessageID",
                principalTable: "TelegramMessages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramChatFullInfos_TelegramMessages_PinnedMessageID",
                table: "TelegramChatFullInfos",
                column: "PinnedMessageID",
                principalTable: "TelegramMessages",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramChatSharedTelegramPhotoSize_TelegramPhotoSizes_PhotoID",
                table: "TelegramChatSharedTelegramPhotoSize",
                column: "PhotoID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramDocuments_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramDocuments",
                column: "ThumbnailID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramStickers_StickerID",
                table: "TelegramExternalReplyInfos",
                column: "StickerID",
                principalTable: "TelegramStickers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramVideoNotes_VideoNoteID",
                table: "TelegramExternalReplyInfos",
                column: "VideoNoteID",
                principalTable: "TelegramVideoNotes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramVideos_VideoID",
                table: "TelegramExternalReplyInfos",
                column: "VideoID",
                principalTable: "TelegramVideos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramExternalReplyInfoTelegramPhotoSize_TelegramPhotoSizes_PhotoID",
                table: "TelegramExternalReplyInfoTelegramPhotoSize",
                column: "PhotoID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramGameTelegramPhotoSize_TelegramPhotoSizes_PhotoID",
                table: "TelegramGameTelegramPhotoSize",
                column: "PhotoID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramGiftInfos_TelegramGifts_GiftID",
                table: "TelegramGiftInfos",
                column: "GiftID",
                principalTable: "TelegramGifts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramGifts_TelegramStickers_StickerID",
                table: "TelegramGifts",
                column: "StickerID",
                principalTable: "TelegramStickers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramGiveawaysCompleted_TelegramMessages_GiveawayMessageID",
                table: "TelegramGiveawaysCompleted",
                column: "GiveawayMessageID",
                principalTable: "TelegramMessages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessages_TelegramStickers_StickerID",
                table: "TelegramMessages",
                column: "StickerID",
                principalTable: "TelegramStickers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessages_TelegramUniqueGiftInfos_UniqueGiftID",
                table: "TelegramMessages",
                column: "UniqueGiftID",
                principalTable: "TelegramUniqueGiftInfos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessages_TelegramVideoNotes_VideoNoteID",
                table: "TelegramMessages",
                column: "VideoNoteID",
                principalTable: "TelegramVideoNotes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessages_TelegramVideos_VideoID",
                table: "TelegramMessages",
                column: "VideoID",
                principalTable: "TelegramVideos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessageTelegramPhotoSize_TelegramPhotoSizes_NewChatPhotoID",
                table: "TelegramMessageTelegramPhotoSize",
                column: "NewChatPhotoID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramMessageTelegramPhotoSize1_TelegramPhotoSizes_PhotoID",
                table: "TelegramMessageTelegramPhotoSize1",
                column: "PhotoID",
                principalTable: "TelegramPhotoSizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TelegramPaidMedia_TelegramVideos_VideoID",
                table: "TelegramPaidMedia",
                column: "VideoID",
                principalTable: "TelegramVideos",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelegramUsers_TacoPermissionPolicyUser_ApplicationUserThisTelegramUserBelongsToID",
                table: "TelegramUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramAffiliateInfo_TelegramChats_AffiliateChatID",
                table: "TelegramAffiliateInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramChats_ChatID",
                table: "TelegramExternalReplyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramGiveawaysWinners_TelegramChats_ChatID",
                table: "TelegramGiveawaysWinners");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessageOrigin_TelegramChats_ChatID",
                table: "TelegramMessageOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessageOrigin_TelegramChats_SenderChatID",
                table: "TelegramMessageOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramChats_ChatID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramChats_ForwardFromChatID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramChats_SenderChatID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramStories_TelegramChats_ChatID",
                table: "TelegramStories");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramTransactionPartner_TelegramChats_ChatID",
                table: "TelegramTransactionPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramAffiliateInfo_TelegramUsers_AffiliateUserID",
                table: "TelegramAffiliateInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessageOrigin_TelegramUsers_SenderUserID",
                table: "TelegramMessageOrigin");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramUsers_ForwardFromID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramUsers_LeftChatMemberID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramUsers_SenderBusinessBotID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramUsers_UserFromID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramUsers_ViaBotID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramProximityAlertTriggereds_TelegramUsers_TravelerID",
                table: "TelegramProximityAlertTriggereds");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramProximityAlertTriggereds_TelegramUsers_WatcherID",
                table: "TelegramProximityAlertTriggereds");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramTransactionPartner_TelegramUsers_SponsorUserID",
                table: "TelegramTransactionPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramTransactionPartner_TelegramUsers_UserID",
                table: "TelegramTransactionPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramAnimations_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramAnimations");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramAudios_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramAudios");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramDocuments_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramStickers_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramStickers");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramStickerSets_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramStickerSets");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramVideoNotes_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramVideoNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramVideos_TelegramPhotoSizes_ThumbnailID",
                table: "TelegramVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBackgroundType_TelegramBackgroundFill_FillID",
                table: "TelegramBackgroundType");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBackgroundType_TelegramBackgroundFill_TelegramBackgroundTypePattern_FillID",
                table: "TelegramBackgroundType");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBackgroundType_TelegramDocuments_DocumentID",
                table: "TelegramBackgroundType");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramBackgroundType_TelegramDocuments_TelegramBackgroundTypeWallpaper_DocumentID",
                table: "TelegramBackgroundType");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramDocuments_DocumentID",
                table: "TelegramExternalReplyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramDocuments_DocumentID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramStickers_StickerID",
                table: "TelegramExternalReplyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramGifts_TelegramStickers_StickerID",
                table: "TelegramGifts");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramStickers_StickerID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramUniqueGiftModels_TelegramStickers_StickerID",
                table: "TelegramUniqueGiftModels");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramUniqueGiftSymbols_TelegramStickers_StickerID",
                table: "TelegramUniqueGiftSymbols");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramExternalReplyInfos_TelegramLocations_LocationID",
                table: "TelegramExternalReplyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramMessages_TelegramLocations_LocationID",
                table: "TelegramMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramVenues_TelegramLocations_LocationID",
                table: "TelegramVenues");

            migrationBuilder.DropForeignKey(
                name: "FK_TelegramGiveawaysCompleted_TelegramMessages_GiveawayMessageID",
                table: "TelegramGiveawaysCompleted");

            migrationBuilder.DropTable(
                name: "ApplicationPushSubscriptions");

            migrationBuilder.DropTable(
                name: "ApplicationUsersLoginInfos");

            migrationBuilder.DropTable(
                name: "DatabaseLogs");

            migrationBuilder.DropTable(
                name: "PermissionPolicyActionPermissionObject");

            migrationBuilder.DropTable(
                name: "PermissionPolicyMemberPermissionsObject");

            migrationBuilder.DropTable(
                name: "PermissionPolicyNavigationPermissionObject");

            migrationBuilder.DropTable(
                name: "PermissionPolicyObjectPermissionsObject");

            migrationBuilder.DropTable(
                name: "TacoPermissionPolicyRoleTacoPermissionPolicyUser");

            migrationBuilder.DropTable(
                name: "TacoPermissionPolicyRoleTacoTeam");

            migrationBuilder.DropTable(
                name: "TacoTeamTacoTeamChat");

            migrationBuilder.DropTable(
                name: "TelegramApiResponses");

            migrationBuilder.DropTable(
                name: "TelegramAuthorizationRequestParameters");

            migrationBuilder.DropTable(
                name: "TelegramBotCommands");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesAllChatAdministrators");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesAllGroupChatss");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesAllPrivateChatss");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesChat");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesChatAdministrators");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesChatMember");

            migrationBuilder.DropTable(
                name: "TelegramBotCommandScopesDefaults");

            migrationBuilder.DropTable(
                name: "TelegramBotDescriptions");

            migrationBuilder.DropTable(
                name: "TelegramBotNames");

            migrationBuilder.DropTable(
                name: "TelegramBotShortDescription");

            migrationBuilder.DropTable(
                name: "TelegramBusinessBusinessMessagesDeleteds");

            migrationBuilder.DropTable(
                name: "TelegramBusinessConnections");

            migrationBuilder.DropTable(
                name: "TelegramBusinessOpeningHoursIntervals");

            migrationBuilder.DropTable(
                name: "TelegramCallbackQueries");

            migrationBuilder.DropTable(
                name: "TelegramChatBoostRemoves");

            migrationBuilder.DropTable(
                name: "TelegramChatBoostUpdates");

            migrationBuilder.DropTable(
                name: "TelegramChatJoinRequests");

            migrationBuilder.DropTable(
                name: "TelegramChatMemberUpdates");

            migrationBuilder.DropTable(
                name: "TelegramChatSharedTelegramPhotoSize");

            migrationBuilder.DropTable(
                name: "TelegramChatsToIgnore");

            migrationBuilder.DropTable(
                name: "TelegramChatTelegramGiveaway");

            migrationBuilder.DropTable(
                name: "TelegramChatTelegramUserChat");

            migrationBuilder.DropTable(
                name: "TelegramChosenInlineResults");

            migrationBuilder.DropTable(
                name: "TelegramCredentialss");

            migrationBuilder.DropTable(
                name: "TelegramEncryptedPassportElementTelegramPassportData");

            migrationBuilder.DropTable(
                name: "TelegramEncryptedPassportElementTelegramPassportFile");

            migrationBuilder.DropTable(
                name: "TelegramEncryptedPassportElementTelegramPassportFile1");

            migrationBuilder.DropTable(
                name: "TelegramExternalReplyInfoTelegramPhotoSize");

            migrationBuilder.DropTable(
                name: "TelegramFileCredentialsTelegramSecureValue");

            migrationBuilder.DropTable(
                name: "TelegramFileCredentialsTelegramSecureValue1");

            migrationBuilder.DropTable(
                name: "TelegramForceReplyMarkups");

            migrationBuilder.DropTable(
                name: "TelegramForumTopics");

            migrationBuilder.DropTable(
                name: "TelegramGameHighScores");

            migrationBuilder.DropTable(
                name: "TelegramGameTelegramMessageEntity");

            migrationBuilder.DropTable(
                name: "TelegramGameTelegramPhotoSize");

            migrationBuilder.DropTable(
                name: "TelegramGiftInfoTelegramMessageEntity");

            migrationBuilder.DropTable(
                name: "TelegramGiveawayWinnersTelegramUser");

            migrationBuilder.DropTable(
                name: "TelegramInlineKeyboardButtons");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultArticles");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedStickers");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultContacts");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultGames");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultLocations");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultVenues");

            migrationBuilder.DropTable(
                name: "TelegramInlineQuerys");

            migrationBuilder.DropTable(
                name: "TelegramInputPaidMediaPhotos");

            migrationBuilder.DropTable(
                name: "TelegramInputPaidMediaVideos");

            migrationBuilder.DropTable(
                name: "TelegramInputProfilePhotoAnimateds");

            migrationBuilder.DropTable(
                name: "TelegramInputProfilePhotoStatics");

            migrationBuilder.DropTable(
                name: "TelegramInputStickers");

            migrationBuilder.DropTable(
                name: "TelegramInputStoryContentPhotos");

            migrationBuilder.DropTable(
                name: "TelegramInputStoryContentVideos");

            migrationBuilder.DropTable(
                name: "TelegramKeyboardButtons");

            migrationBuilder.DropTable(
                name: "TelegramLabeledPrices");

            migrationBuilder.DropTable(
                name: "TelegramMenuButtonCommandss");

            migrationBuilder.DropTable(
                name: "TelegramMenuButtonDefaults");

            migrationBuilder.DropTable(
                name: "TelegramMenuButtonWebApps");

            migrationBuilder.DropTable(
                name: "TelegramMessageAutoDeleteTimerChanged");

            migrationBuilder.DropTable(
                name: "TelegramMessageEntityTelegramPoll");

            migrationBuilder.DropTable(
                name: "TelegramMessageEntityTelegramPoll1");

            migrationBuilder.DropTable(
                name: "TelegramMessageEntityTelegramTextQuote");

            migrationBuilder.DropTable(
                name: "TelegramMessageIds");

            migrationBuilder.DropTable(
                name: "TelegramMessageReactionCountUpdatedTelegramReactionCount");

            migrationBuilder.DropTable(
                name: "TelegramMessageReactionNewJoin");

            migrationBuilder.DropTable(
                name: "TelegramMessageReactionOldJoin");

            migrationBuilder.DropTable(
                name: "TelegramMessageTelegramMessageEntity");

            migrationBuilder.DropTable(
                name: "TelegramMessageTelegramMessageEntity1");

            migrationBuilder.DropTable(
                name: "TelegramMessageTelegramPhotoSize");

            migrationBuilder.DropTable(
                name: "TelegramMessageTelegramPhotoSize1");

            migrationBuilder.DropTable(
                name: "TelegramMessageTelegramUser");

            migrationBuilder.DropTable(
                name: "TelegramPaidMediaPurchases");

            migrationBuilder.DropTable(
                name: "TelegramPaidMessagePricesChanged");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorDataFields");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorFiles");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorFiless");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorFrontSides");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorReverseSides");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorSelfies");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorTranslationFiles");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorTranslationFiless");

            migrationBuilder.DropTable(
                name: "TelegramPassportElementErrorUnspecifieds");

            migrationBuilder.DropTable(
                name: "TelegramPassportScopeElement");

            migrationBuilder.DropTable(
                name: "TelegramPersonalDetailss");

            migrationBuilder.DropTable(
                name: "TelegramPhotoSizeTelegramSharedUser");

            migrationBuilder.DropTable(
                name: "TelegramPhotoSizeTelegramVideo");

            migrationBuilder.DropTable(
                name: "TelegramPollAnswers");

            migrationBuilder.DropTable(
                name: "TelegramPreCheckoutQueries");

            migrationBuilder.DropTable(
                name: "TelegramPreparedInlineMessages");

            migrationBuilder.DropTable(
                name: "TelegramReplyKeyboardRemoves");

            migrationBuilder.DropTable(
                name: "TelegramResidentialAddresses");

            migrationBuilder.DropTable(
                name: "TelegramSentWebAppMessages");

            migrationBuilder.DropTable(
                name: "TelegramSharedUserTelegramUsersShared");

            migrationBuilder.DropTable(
                name: "TelegramShippingQueries");

            migrationBuilder.DropTable(
                name: "TelegramStarAmounts");

            migrationBuilder.DropTable(
                name: "TelegramStarTransactions");

            migrationBuilder.DropTable(
                name: "TelegramStoryAreas");

            migrationBuilder.DropTable(
                name: "TelegramVideoChatsEnded");

            migrationBuilder.DropTable(
                name: "TelegramVideoChatsStarted");

            migrationBuilder.DropTable(
                name: "TelegramWebhookInfos");

            migrationBuilder.DropTable(
                name: "PermissionPolicyTypePermissionObject");

            migrationBuilder.DropTable(
                name: "TacoTeams");

            migrationBuilder.DropTable(
                name: "TelegramResponseParameters");

            migrationBuilder.DropTable(
                name: "TelegramBusinessBotRights");

            migrationBuilder.DropTable(
                name: "TelegramChatBoosts");

            migrationBuilder.DropTable(
                name: "TelegramChatInviteLinks");

            migrationBuilder.DropTable(
                name: "TelegramChatMember");

            migrationBuilder.DropTable(
                name: "TelegramUserChats");

            migrationBuilder.DropTable(
                name: "TelegramSecureDatas");

            migrationBuilder.DropTable(
                name: "TelegramEncryptedPassportElements");

            migrationBuilder.DropTable(
                name: "TelegramCallbackGames");

            migrationBuilder.DropTable(
                name: "TelegramCopyTextButtons");

            migrationBuilder.DropTable(
                name: "TelegramInlineKeyboardRows");

            migrationBuilder.DropTable(
                name: "TelegramLoginUrls");

            migrationBuilder.DropTable(
                name: "TelegramSwitchInlineQueryChosenChats");

            migrationBuilder.DropTable(
                name: "TelegramKeyboardButtonPollTypes");

            migrationBuilder.DropTable(
                name: "TelegramKeyboardButtonRequestChats");

            migrationBuilder.DropTable(
                name: "TelegramKeyboardButtonRequestUsers");

            migrationBuilder.DropTable(
                name: "TelegramKeyboardRows");

            migrationBuilder.DropTable(
                name: "TelegramShippingOptions");

            migrationBuilder.DropTable(
                name: "TelegramWebAppInfos");

            migrationBuilder.DropTable(
                name: "TelegramMessageReactionCountUpdates");

            migrationBuilder.DropTable(
                name: "TelegramReactionCounts");

            migrationBuilder.DropTable(
                name: "TelegramMessageReactionUpdates");

            migrationBuilder.DropTable(
                name: "TelegramMessageEntitys");

            migrationBuilder.DropTable(
                name: "TelegramPassportScopes");

            migrationBuilder.DropTable(
                name: "TelegramSharedUsers");

            migrationBuilder.DropTable(
                name: "TelegramStarTransactionsCollections");

            migrationBuilder.DropTable(
                name: "TelegramStoryAreaPositions");

            migrationBuilder.DropTable(
                name: "TelegramStoryAreaType");

            migrationBuilder.DropTable(
                name: "PermissionPolicyRoleBase");

            migrationBuilder.DropTable(
                name: "TacoTeamChats");

            migrationBuilder.DropTable(
                name: "TelegramChatBoostSource");

            migrationBuilder.DropTable(
                name: "TelegramUserChatBoostss");

            migrationBuilder.DropTable(
                name: "TelegramSecureValues");

            migrationBuilder.DropTable(
                name: "TelegramPassportFiles");

            migrationBuilder.DropTable(
                name: "TelegramReplyKeyboardMarkups");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultAudios");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedAudios");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedDocuments");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedGifs");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedMpeg4Gifs");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedPhotos");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedVideos");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultCachedVoices");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultDocuments");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultGifs");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultMpeg4Gifs");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultPhotos");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultVideos");

            migrationBuilder.DropTable(
                name: "TelegramInlineQueryResultVoices");

            migrationBuilder.DropTable(
                name: "TelegramInputMediaAnimations");

            migrationBuilder.DropTable(
                name: "TelegramInputMediaAudios");

            migrationBuilder.DropTable(
                name: "TelegramInputMediaDocuments");

            migrationBuilder.DropTable(
                name: "TelegramInputMediaPhotos");

            migrationBuilder.DropTable(
                name: "TelegramInputMediaVideos");

            migrationBuilder.DropTable(
                name: "TelegramInputPollOptions");

            migrationBuilder.DropTable(
                name: "TelegramPollOptions");

            migrationBuilder.DropTable(
                name: "TelegramReplyParameterss");

            migrationBuilder.DropTable(
                name: "TelegramLocationAddresses");

            migrationBuilder.DropTable(
                name: "TelegramReactionType");

            migrationBuilder.DropTable(
                name: "TelegramChatBotRightsUsers");

            migrationBuilder.DropTable(
                name: "TelegramDataCredentialss");

            migrationBuilder.DropTable(
                name: "TelegramFileCredentials");

            migrationBuilder.DropTable(
                name: "TelegramInputMessageContent");

            migrationBuilder.DropTable(
                name: "TelegramInputFile");

            migrationBuilder.DropTable(
                name: "TelegramChatIds");

            migrationBuilder.DropTable(
                name: "TelegramChatFullInfos");

            migrationBuilder.DropTable(
                name: "TelegramChatAdministratorRights");

            migrationBuilder.DropTable(
                name: "TelegramAcceptedGiftTypes");

            migrationBuilder.DropTable(
                name: "TelegramBirthdates");

            migrationBuilder.DropTable(
                name: "TelegramBusinessIntros");

            migrationBuilder.DropTable(
                name: "TelegramBusinessLocations");

            migrationBuilder.DropTable(
                name: "TelegramBusinessOpeningHours");

            migrationBuilder.DropTable(
                name: "TelegramChatLocations");

            migrationBuilder.DropTable(
                name: "TelegramChatPermissions");

            migrationBuilder.DropTable(
                name: "TelegramChatPhotos");

            migrationBuilder.DropTable(
                name: "TacoPermissionPolicyUser");

            migrationBuilder.DropTable(
                name: "TelegramChats");

            migrationBuilder.DropTable(
                name: "TelegramUsers");

            migrationBuilder.DropTable(
                name: "TelegramPhotoSizes");

            migrationBuilder.DropTable(
                name: "TelegramPaidMedia");

            migrationBuilder.DropTable(
                name: "TelegramPhotoSizeGroups");

            migrationBuilder.DropTable(
                name: "TelegramTransactionPartner");

            migrationBuilder.DropTable(
                name: "TelegramUserProfilePhotoss");

            migrationBuilder.DropTable(
                name: "TelegramAffiliateInfo");

            migrationBuilder.DropTable(
                name: "TelegramRevenueWithdrawalState");

            migrationBuilder.DropTable(
                name: "TelegramBackgroundFill");

            migrationBuilder.DropTable(
                name: "TelegramDocuments");

            migrationBuilder.DropTable(
                name: "TelegramStickers");

            migrationBuilder.DropTable(
                name: "TelegramMaskPositions");

            migrationBuilder.DropTable(
                name: "TelegramStickerSets");

            migrationBuilder.DropTable(
                name: "TelegramTGFiles");

            migrationBuilder.DropTable(
                name: "TelegramLocations");

            migrationBuilder.DropTable(
                name: "TelegramMessages");

            migrationBuilder.DropTable(
                name: "TelegramChatBackgrounds");

            migrationBuilder.DropTable(
                name: "TelegramChatShared");

            migrationBuilder.DropTable(
                name: "TelegramExternalReplyInfos");

            migrationBuilder.DropTable(
                name: "TelegramForumTopicsCreated");

            migrationBuilder.DropTable(
                name: "TelegramForumTopicsEdited");

            migrationBuilder.DropTable(
                name: "TelegramGiftInfos");

            migrationBuilder.DropTable(
                name: "TelegramGiveawaysCompleted");

            migrationBuilder.DropTable(
                name: "TelegramGiveawaysCreated");

            migrationBuilder.DropTable(
                name: "TelegramInlineKeyboardMarkups");

            migrationBuilder.DropTable(
                name: "TelegramPassportDatas");

            migrationBuilder.DropTable(
                name: "TelegramProximityAlertTriggereds");

            migrationBuilder.DropTable(
                name: "TelegramRefundedPayments");

            migrationBuilder.DropTable(
                name: "TelegramSuccessfulPayments");

            migrationBuilder.DropTable(
                name: "TelegramTextQuotes");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGiftInfos");

            migrationBuilder.DropTable(
                name: "TelegramUsersShareds");

            migrationBuilder.DropTable(
                name: "TelegramVideoChatsParticipantsInvited");

            migrationBuilder.DropTable(
                name: "TelegramVideoChatsScheduled");

            migrationBuilder.DropTable(
                name: "TelegramWebAppDatas");

            migrationBuilder.DropTable(
                name: "TelegramWriteAccessAlloweds");

            migrationBuilder.DropTable(
                name: "TelegramBackgroundType");

            migrationBuilder.DropTable(
                name: "TelegramAudios");

            migrationBuilder.DropTable(
                name: "TelegramContacts");

            migrationBuilder.DropTable(
                name: "TelegramDices");

            migrationBuilder.DropTable(
                name: "TelegramGames");

            migrationBuilder.DropTable(
                name: "TelegramGiveawaysWinners");

            migrationBuilder.DropTable(
                name: "TelegramGiveaways");

            migrationBuilder.DropTable(
                name: "TelegramInvoices");

            migrationBuilder.DropTable(
                name: "TelegramLinkPreviewOptionss");

            migrationBuilder.DropTable(
                name: "TelegramMessageOrigin");

            migrationBuilder.DropTable(
                name: "TelegramPaidMediaInfos");

            migrationBuilder.DropTable(
                name: "TelegramPolls");

            migrationBuilder.DropTable(
                name: "TelegramStories");

            migrationBuilder.DropTable(
                name: "TelegramVenues");

            migrationBuilder.DropTable(
                name: "TelegramVideoNotes");

            migrationBuilder.DropTable(
                name: "TelegramVideos");

            migrationBuilder.DropTable(
                name: "TelegramVoices");

            migrationBuilder.DropTable(
                name: "TelegramGifts");

            migrationBuilder.DropTable(
                name: "TelegramEncryptedCredentials");

            migrationBuilder.DropTable(
                name: "TelegramOrderInfos");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGifts");

            migrationBuilder.DropTable(
                name: "TelegramAnimations");

            migrationBuilder.DropTable(
                name: "TelegramGiftLists");

            migrationBuilder.DropTable(
                name: "TelegramShippingAddresses");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGiftBackdrops");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGiftModels");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGiftSymbols");

            migrationBuilder.DropTable(
                name: "TelegramUniqueGiftBackdropColorss");
        }
    }
}
