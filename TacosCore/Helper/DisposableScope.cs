//-----------------------------------------------------------------------
// <copyright file="DisposableScope.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TacosCore.Helper
{
    public class DisposableScope : IDisposable
    {
        private readonly string _scopeInfo;
        private bool disposed;

        public DisposableScope(string scopeInfo)
        {
            _scopeInfo = scopeInfo;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                }


                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string ScopeInfo => _scopeInfo;
    }
}
