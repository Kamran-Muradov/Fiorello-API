using Microsoft.AspNetCore.Mvc;

namespace FiorelloAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
