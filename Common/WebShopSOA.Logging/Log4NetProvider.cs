﻿using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;
using WebShopSOA.Logging;

namespace WebShopSOA.Log4Net
{
    public class Log4NetProvider : ILoggerProvider
    {
        private readonly string _ConfigurationFile;

        private readonly ConcurrentDictionary<string, Log4NetLogger> _Loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public Log4NetProvider(string ConfigurationFile) => _ConfigurationFile = ConfigurationFile;

        public ILogger CreateLogger(string CategoryName)
        {
            return _Loggers.GetOrAdd(CategoryName, category =>
            {
                var xml = new XmlDocument();
                var file_name = _ConfigurationFile;
                xml.Load(file_name);
                return new Log4NetLogger(category, xml["log4net"]);
            });
        }

        public void Dispose()
        {
            _Loggers.Clear();
        }
    }
}
