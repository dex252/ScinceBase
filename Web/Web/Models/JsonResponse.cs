using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Enums;

namespace Web.Models
{
    public sealed class JsonResponse<T> : ContentResult
        where T : class
    {
        public JsonResponse(MessageStatus status, string message, T content = null, string requestId = null)
        {
            var response = new
            {
                Status = status,
                Message = message,
                Data = content,
                RequestId = requestId
            };

            Content = JsonConvert.SerializeObject(response);
            ContentType = "application/json";
        }

    }
}