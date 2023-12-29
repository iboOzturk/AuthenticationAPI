using AuthenticationAPI.JWT.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        BuildTokenService buildTokenService;
        public SampleController()
        {
            buildTokenService = new BuildTokenService();
        }
        [HttpGet]
        public  IActionResult GetToken(string username,string pass)
        {
            if (username=="ibo"&&pass=="123")
            {
                var token = buildTokenService.GenerateJwtToken(username);
                if (token != null)
                {
                    return Ok(token);
                }
                else
                {
                    return Unauthorized(new { Message = "Invalid credentials" });
                }
            }
            return BadRequest();

           
        }
        [Authorize]
        [HttpGet]
        public IActionResult SecretKey(string keyValue)
        {
            if (keyValue.Equals("Value"))           
                return Ok(true);
            else
                return Ok(false);
        }
    }
}
