using MyFarm.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace MyFarm.ApiModels.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ResponseWrapper
    {
        public object Data { get; set; }

        public bool Success { get; set; }

        public AppErrorCode? AppErrorCode { get; set; }

        public string Error { get; set; }

        public string ErrorStackTrace { get; set; }
        public bool Cache { get; set; } = false;
    }
}
