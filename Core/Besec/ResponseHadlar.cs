using Core.Resource;
using Microsoft.Extensions.Localization;

namespace Core.Besec
{
    public class ResponseHadlar
    {
        private readonly IStringLocalizer<SheardResource> _Localizer;

        public ResponseHadlar(IStringLocalizer<SheardResource> Localizer)
        {
            _Localizer = Localizer;


        }
        public Response<T> Deleted<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = message == null ? _Localizer[KeySharedResource.Deleted] : message,


            };
        }
        public Response<T> Success<T>(T entites, object meta = null)
        {
            return new Response<T>()
            {
                Data = entites,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Meta = meta,
                Message = _Localizer[KeySharedResource.Success]
            };
        }
        public Response<T> Created<T>(T entites, object meta = null)
        {
            return new Response<T>()
            {
                Data = entites,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _Localizer[KeySharedResource.Create],
                Meta = meta,
            };
        }
        public Response<T> Unauthorzed<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized",
            };
        }
        public Response<T> UnprocessableEntity<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,

                Succeeded = false,
                Message = message == null ? "Unprocessable Entity" : message,

            };
        }
        public Response<T> BadRequst<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = message == null ? "BadRequst" : message,
            };
        }
        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,

                Succeeded = false,
                Message = message == null ? _Localizer[KeySharedResource.NoFound] : message,

            };
        }



    }
}
