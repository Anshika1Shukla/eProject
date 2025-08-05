using ePizzaHub.Models.ApiModels.Response;
using System.IO;
using System.Text.Json;

namespace ePizzaHub.API.Middleware
{
    public class CommonReponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CommonReponseMiddleware(RequestDelegate next ) {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                try
                {
                    await _next(context);

                    // logic to convert api reponse into desired format

                    if (context.Response.ContentType != null
                        && context.Response.ContentType.Contains("application/json"))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                        var responseObj = new ApiResponseModel<object>(
                              success: context.Response.StatusCode >= 200 && context.Response.StatusCode < 299,
                              data: JsonSerializer.Deserialize<object>(responseBody)!,
                              message: "Request completed successfully");


                        var jsonResponse = JsonSerializer.Serialize(responseObj);
                        context.Response.Body = originalBodyStream;
                        await context.Response.WriteAsync(jsonResponse);
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    var errorResponse
                         = new ApiResponseModel<object>(
                             success: false,
                             data: (object)null,
                             message: ex.Message);

                    var jsonResponse = JsonSerializer.Serialize(errorResponse);
                    context.Response.Body = originalBodyStream;
                    await context.Response.WriteAsync(jsonResponse);
                }

            }
        }
    }
}
