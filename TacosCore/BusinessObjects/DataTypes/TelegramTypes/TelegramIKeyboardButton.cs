//-----------------------------------------------------------------------
// <copyright file="TelegramIKeyboardButton.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes.TelegramTypes; [JsonPolymorphic]
[JsonDerivedType(typeof(TelegramInlineKeyboardButton))]
[JsonDerivedType(typeof(TelegramKeyboardButton))]
public interface TelegramIKeyboardButton
{

    string Text { get; set; }
}
