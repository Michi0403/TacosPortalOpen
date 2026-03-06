//-----------------------------------------------------------------------
// <copyright file="TelegramReplyMarkup.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes;
[JsonPolymorphic(TypeDiscriminatorPropertyName = null)]
[JsonDerivedType(typeof(TelegramForceReplyMarkup))]
[JsonDerivedType(typeof(TelegramReplyKeyboardMarkup))]
[JsonDerivedType(typeof(TelegramInlineKeyboardMarkup))]
[JsonDerivedType(typeof(TelegramReplyKeyboardRemove))]
[Authorize]
[DefaultClassOptions]
public abstract class TelegramReplyMarkup : BaseObject
{
}