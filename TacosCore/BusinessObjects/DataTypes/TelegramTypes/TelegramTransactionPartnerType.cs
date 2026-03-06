//-----------------------------------------------------------------------
// <copyright file="TelegramTransactionPartnerType.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;
using Telegram.Bot.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[JsonConverter(typeof(EnumConverter<TelegramTransactionPartnerType>))]
public enum TelegramTransactionPartnerType
{

    Fragment = 1,

    User,

    Other,

    TelegramAds,

    TelegramApi,

    AffiliateProgram,

    Chat,
}
