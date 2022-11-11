using Microsoft.AspNetCore.Mvc;
using SkinetWebApi.Errors;

namespace SkinetWebApi.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
