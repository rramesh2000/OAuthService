using System.Net;

namespace TokenService.Utility
{
    public class PreconditionFailedError : ApiError
    {
        public PreconditionFailedError()
            : base(404, HttpStatusCode.NotFound.ToString())
        {
        }

        public PreconditionFailedError(string message)
            : base(404, HttpStatusCode.PreconditionFailed.ToString(), message)
        {
        }
    }
}
