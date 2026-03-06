//-----------------------------------------------------------------------
// <copyright file="CustomODataQueryDefaultValidatorService.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;

namespace TacosPortal.Services
{
    public class CustomODataQueryDefaultValidatorService(ILogger<CustomODataQueryDefaultValidatorService> logger) : ODataQueryValidator
    {
        public override void Validate(ODataQueryOptions option,
                                      ODataValidationSettings settings)
        {
            try
            {
                logger.LogInformation(">>> CustomODataQueryValidatorService.Validate INVOKED");
                settings ??= new ODataValidationSettings();
                settings.MaxAnyAllExpressionDepth = 5;
                settings.MaxNodeCount = 10000;
                settings.MaxExpansionDepth = 1000;
                base.Validate(option, settings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error in CustomODataQueryValidatorService {ex.ToString()}");
                throw;
            }

        }

    }

    public class CustomFilterQueryValidator(ILogger<CustomFilterQueryValidator> logger) : FilterQueryValidator
    {

        public override void Validate(FilterQueryOption option, ODataValidationSettings settings)
        {
            logger.LogInformation(">>> CustomFilterQueryValidator.ValidateAnyNode Validate");
            settings ??= new ODataValidationSettings();
            settings.MaxAnyAllExpressionDepth = 5;
            settings.MaxNodeCount = 10000;
            settings.MaxExpansionDepth = 1000;
            base.Validate(option, settings);
        }


    }
}
