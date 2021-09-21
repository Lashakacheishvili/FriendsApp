using Microsoft.AspNetCore.Http;

namespace Common.Helpers
{
    public class RequestContextManager
    {
        public static RequestContextManager Instance { get; set; }

        static RequestContextManager()
        {
            Instance = new RequestContextManager(null);
        }

        private readonly IHttpContextAccessor _contextAccessor;

        public RequestContextManager(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public HttpContext CurrentContext => _contextAccessor?.HttpContext;

        public static void SetRequestContextManager(IHttpContextAccessor contextAccessor)
        {
            Instance = new RequestContextManager(contextAccessor);
        }
    }
}
