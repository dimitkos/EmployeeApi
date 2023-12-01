using FluentValidation;
using Gateway;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Text;

namespace Api.Middlewares
{
    class ExceptionsMiddleware
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() } };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(
            RequestDelegate next,
            ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }
            catch (ApiClientException ex)
            {
                string requestStr = await FormatRequest(context.Request);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"Something went wrong. ExMessage: {ex.Message}");
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                //var error = new ValidationErrorModel(ex.Message, ex.Errors.Select(x => x.ErrorMessage).ToArray());
                var response = JsonConvert.SerializeObject(ex.Errors, _serializerSettings);
                await context.Response.WriteAsync(response);
            }
            catch (Exception ex)
            {
                await WriteErrorResponse(context, ex);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            if (request.Path.ToString().Contains("authenticate"))
                return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString}";

            request.Body.Position = 0;

            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));

            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task WriteErrorResponse(HttpContext context, Exception ex)
        {
            string requestStr = await FormatRequest(context.Request);
            var errorKey = Guid.NewGuid().ToString();
            _logger.LogError(ex, $"ErrorKey: {errorKey} - Request: {requestStr}");

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"Something went wrong. ErrorKey:{errorKey} - ExMessage: {ex.Message}");
        }
    }
}
