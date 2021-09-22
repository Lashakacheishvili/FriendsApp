using Microsoft.AspNetCore.Http;
namespace ServiceModels
{
    public class BaseResponseModel
    {
        public int HttpStatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string UserMessage { get; set; }
        public string DeveloperMessage { get; set; }
        public bool Success { get; set; }
    }
}
