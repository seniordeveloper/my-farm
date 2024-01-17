using Microsoft.AspNetCore.Mvc;
using MyFarm.ApiModels.Wrappers;
using MyFarm.Common.Enums;
using MyFarm.Common.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ILogger = MyFarm.Log.ILogger;

namespace MyFarm.WebApi.Controllers.Base
{
    public class BaseApiController : ControllerBase
    {
        protected const string JsonContentType = "application/json";
        protected readonly ILogger _logger;

        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task<IActionResult> WrapResponseAsync(Func<CancellationToken, Task> responseBuilder)
        {
            var token = HttpContext.RequestAborted;
            ResponseWrapper response;

            try
            {
                await responseBuilder(token);
                response = new ResponseWrapper
                {
                    Success = true
                };
            }
            catch (MyFarmApplicationException ex)
            {
                response = GetResponseFromException(ex);
            }
            catch (Exception ex)
            {
                response = GetResponseFromException(ex);
            }

            var json = ToJson(response);

            return Content(json, JsonContentType);
        }

        protected async Task<IActionResult> WrapResponseAsync<T>(Func<CancellationToken, Task<T>> responseBuilder)
        {
            var token = HttpContext.RequestAborted;
            string response;

            try
            {
                var respValue = new ResponseWrapper
                {
                    Data = await responseBuilder(token),
                    Success = true,
                };

                response = ToJson(respValue);
            }
            catch (MyFarmApplicationException ex)
            {
                response = ToJson(GetResponseFromException(ex));
            }
            catch (Exception ex)
            {
                response = ToJson(GetResponseFromException(ex));
            }

            return Content(response, JsonContentType);
        }

        protected IActionResult WrapResponse<T>(Func<T> responseBuilder)
        {
            ResponseWrapper response;

            try
            {
                response = new ResponseWrapper
                {
                    Data = responseBuilder(),
                    Success = true,
                };
            }
            catch (MyFarmApplicationException ex)
            {
                response = GetResponseFromException(ex);
            }
            catch (Exception ex)
            {
                response = GetResponseFromException(ex);
            }

            var json = ToJson(response);

            return Content(json, JsonContentType);
        }

        protected static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
        }

        protected static T FromJson<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                json = "{}";
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        private ResponseWrapper GetResponseFromException(MyFarmApplicationException exception)
        {
            var response = new ResponseWrapper
            {
                Error = exception.Message,
                Success = false,
                ErrorStackTrace = exception.StackTrace,
                AppErrorCode = exception.ErrorCode,
            };

            _logger.Error(exception);

            return response;
        }

        private ResponseWrapper GetResponseFromException(Exception exception)
        {
            var response = new ResponseWrapper
            {
                Error = exception.Message,
                Success = false,
                ErrorStackTrace = exception.StackTrace,
                AppErrorCode = AppErrorCode.UnhandledError
            };

            _logger.Error(exception);

            return response;
        }
    }
}
