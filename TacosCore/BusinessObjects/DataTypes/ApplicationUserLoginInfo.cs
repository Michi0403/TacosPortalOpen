//-----------------------------------------------------------------------
// <copyright file="ApplicationUserLoginInfo.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.Security;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TacosCore.BusinessObjects.DataTypes;

public class ApplicationUserLoginInfo : ISecurityUserLoginInfo
{

    public ApplicationUserLoginInfo() { }

    object ISecurityUserLoginInfo.User => User;

    [Browsable(false)]
    public virtual Guid ID { get; protected set; }

    public virtual string? LoginProviderName { get; set; }

    public virtual string? ProviderUserKey { get; set; }

    [Required]
    [ForeignKey(nameof(UserForeignKey))]
    [JsonIgnore]
    public virtual ApplicationUser User { get; set; }

    [Browsable(false)]
    public virtual Guid UserForeignKey { get; set; }
}
