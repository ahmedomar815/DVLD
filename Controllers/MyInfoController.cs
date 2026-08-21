using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MyInfoController : ControllerBase
{

}
