using Serilog.Core;
using System.Text.RegularExpressions;

namespace MyFarm.WebApi.Extensions
{
    class MessageEnricher : ILogEventEnricher
    {
        public void Enrich(Serilog.Events.LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.MessageTemplate != null)
            {
                var text = logEvent.MessageTemplate.Render(logEvent.Properties);
                var logEventProperty = propertyFactory.CreateProperty("EscapedMessage", Regex.Replace(text, Environment.NewLine, "\\r\\n"));
                logEvent.AddPropertyIfAbsent(logEventProperty);
            }

            if (logEvent.Properties.ContainsKey("Message"))
            {
                var logEventProperty = propertyFactory.CreateProperty("EscapedMessage", Regex.Replace(logEvent.Properties["Message"].ToString(), Environment.NewLine, "\\r\\n"));
                logEvent.AddPropertyIfAbsent(logEventProperty);
            }
        }
    }
}
