//-----------------------------------------------------------------------
// <copyright file="FileLoggerProvider.cs" company="https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes">
//     Author: Michael Fleischer
//     Copyright (c) https://github.com/Michi0403/TacosPortalOpen as love for blazor WASM and monolithes. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TacosCore.BusinessObjects;

namespace TacosCore.Logging
{
    public class FileLoggerProvider : ILoggerProvider, IDisposable
    {
        private bool disposed;
        private readonly IOptionsMonitor<FileLoggerCoreOptions> options;

        public FileLoggerProvider(IOptionsMonitor<FileLoggerCoreOptions> options)
        {
            this.options = options;
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

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}