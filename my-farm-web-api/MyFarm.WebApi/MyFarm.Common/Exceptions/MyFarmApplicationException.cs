using MyFarm.Common.Enums;
using System.Runtime.Serialization;

namespace MyFarm.Common.Exceptions
{
    [Serializable]
    public class MyFarmApplicationException : Exception
    {
        private readonly string _stackTrace;

        public MyFarmApplicationException(AppErrorCode errorCode, string message = null, Exception innerException = null)
             : base($"ErrorCode:{errorCode}|Message: {message}", innerException)
        {
            ErrorCode = errorCode;
            _stackTrace = new System.Diagnostics.StackTrace().ToString();
        }

        public AppErrorCode ErrorCode { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(ErrorCode), ErrorCode);

            base.GetObjectData(info, context);
        }

        public override string StackTrace => _stackTrace;
    }
}
