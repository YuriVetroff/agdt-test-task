using Microsoft.AspNetCore.Mvc;

namespace AgdtTestTask.Core.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase
        : ControllerBase
    {
        [NonAction]
        public ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }
    }
}
