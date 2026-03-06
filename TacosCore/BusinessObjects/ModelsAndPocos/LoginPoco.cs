//-----------------------------------------------------------------------
// <copyright file="LoginPoco.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace TacosCore.BusinessObjects.ModelsAndPocos
{
    public class LoginPoco
    {
        public string passWord { get; set; } = string.Empty;
        [Required(ErrorMessage = "Username is Required")]
        public string userName { get; set; } = string.Empty;
    }
}
