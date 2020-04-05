using System.Net;
using Microsoft.AspNetCore.Mvc;
using TokenService.Utility;

namespace TokenService.Controllers
{

    [Route("/errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;
            ApiError error = new ApiError(code, parsedCode.ToString());

            return new ObjectResult(error);
        }
    }

}