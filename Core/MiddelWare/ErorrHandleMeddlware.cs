using Core.Besec;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Core.MiddelWare
{
    public class ErorrHandleMeddlware
    {
        private readonly RequestDelegate _next;
        public ErorrHandleMeddlware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception erorr)
            {
                var respnse = context.Response;
                respnse.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = erorr?.Message };
                switch (erorr)
                {
                    case UnauthorizedAccessException e:
                        responseModel.Message = "UnauthorizedAccessException";
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        respnse.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ValidationException e:
                        responseModel.Message = "ValidationException";
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        respnse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;

                    case KeyNotFoundException v:
                        responseModel.Message = "KeyNotFoundException";
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        respnse.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case DbUpdateException e:
                        responseModel.Message = e.Message;
                        responseModel.StatusCode = HttpStatusCode.BadRequest;
                        respnse.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Exception e:
                        if (e.GetType().ToString() == "ApiException")
                        {
                            responseModel.Message += e.Message;
                            responseModel.Message += e.InnerException == null ? "" : "/n" + e.InnerException.Message;
                            responseModel.StatusCode = HttpStatusCode.BadRequest;
                            respnse.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        responseModel.Message = e.Message;
                        responseModel.Message += e.InnerException == null ? "" : "/n" + e.InnerException.Message;
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        respnse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    default:
                        responseModel.Message = "Please Contact the Application Admin ";

                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        respnse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await respnse.WriteAsync(result);


            }
        }
    }
}
