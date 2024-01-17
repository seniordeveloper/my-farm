using Serilog.Core;

namespace MyFarm.WebApi.Extensions
{
    class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(Serilog.Events.LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception == null)
                return;

            var logEventProperty = propertyFactory.CreateProperty("EscapedException", logEvent.Exception.ToString().Replace("\r\n", "\\r\\n"));
            logEvent.AddPropertyIfAbsent(logEventProperty);
        }
    }
}
