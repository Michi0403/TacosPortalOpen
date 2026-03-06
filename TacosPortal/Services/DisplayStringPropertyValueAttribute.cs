//-----------------------------------------------------------------------
// <copyright file="DisplayStringPropertyValueAttribute.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace TacosPortal.Services;

public class DisplayStringPropertyValueAttribute : StringLengthAttribute
{

    public DisplayStringPropertyValueAttribute() : base(0)
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        => !IsValid(value) ? new ValidationResult(value?.ToString(),
                new[] { validationContext.MemberName }!) : ValidationResult.Success;
}