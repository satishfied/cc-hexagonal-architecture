using System.Net;
using System.Net.Http;
using System.Web.Http;
using DDDSkeleton.ApplicationServices;

namespace DDDSkeleton.WebService.Helpers
{
    public static class HttpResponseBuilder
    {
        public static HttpResponseMessage BuildResponse(this HttpRequestMessage requestMessage,
            ServiceResponseBase baseResponse)
        {
            var statusCode = HttpStatusCode.OK;
            if (baseResponse.Exception != null)
            {
                statusCode = baseResponse.Exception.ConvertToHttpStatusCode();
                var message = new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(baseResponse.Exception.Message)
                };

                throw new HttpResponseException(message);
            }
            return requestMessage.CreateResponse(statusCode, baseResponse);
        }
    }
}