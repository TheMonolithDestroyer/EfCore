using Microsoft.AspNetCore.Mvc;
using TheNomad.EFCore.Services.Logger;

namespace TheNomad.EFCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseTraceController : ControllerBase
    {
    }
}
