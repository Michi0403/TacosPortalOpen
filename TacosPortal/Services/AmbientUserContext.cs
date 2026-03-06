//-----------------------------------------------------------------------
// <copyright file="AmbientUserContext.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosPortal.Services
{
    public interface ICurrentUserAccessor
    {
        string? RequestId { get; }
        Guid? UserId { get; }
        string? UserName { get; }
    }

    public interface IAmbientUserContext
    {
        Guid? UserId { get; set; }
        string? UserName { get; set; }
    }

    public sealed class AmbientUserContext : IAmbientUserContext
    {
        private static readonly AsyncLocal<Guid?> _id = new();
        private static readonly AsyncLocal<string?> _name = new();

        public Guid? UserId { get => _id.Value; set => _id.Value = value; }
        public string? UserName { get => _name.Value; set => _name.Value = value; }
    }
}
