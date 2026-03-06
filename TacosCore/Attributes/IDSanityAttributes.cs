//-----------------------------------------------------------------------
// <copyright file="IDSanityAttributes.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace TacosCore.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LongIntIDSanityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext vc)
        {
            if (value == null)
                return ValidationResult.Success;

            long parsedValue;

            try
            {
                parsedValue = Convert.ToInt64(value);
            }
            catch
            {
                return new ValidationResult($"{vc.DisplayName} is not a valid numeric ID.");
            }

            if (parsedValue <= 0)
            {
                var msg = ErrorMessage ?? $"{vc.DisplayName} must be greater than 0.";
                return new ValidationResult(msg, new[] { vc.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
