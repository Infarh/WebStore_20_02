using System;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public class Log4NetProvider : ILoggerProvider
    {
        public Log4NetProvider(string ConfigurationFile) { }

        public ILogger CreateLogger(string categoryName) { throw new NotImplementedException(); }

        public void Dispose() { throw new NotImplementedException(); }
    }
}
