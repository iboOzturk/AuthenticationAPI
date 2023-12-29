using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.APIKey.Controllers
{
	[Authorize(AuthenticationSchemes = "ApiKeyAuthentication")]
	[Route("api/[controller]")]
	[ApiController]
	public class SampleController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Deneme");
		}
	}
}
