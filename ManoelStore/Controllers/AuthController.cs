using ManoelStore.Models;
using ManoelStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManoelStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IAuthService _authService;

		public AuthController(IConfiguration configuration, IAuthService authService)
		{
			_configuration = configuration;
			_authService = authService;
		}

		[HttpPost]
		public ActionResult<string> SignIn([FromBody] PostSignRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = _authService.Validate(request.Email, request.Password);

			if (!result)
				return Unauthorized();

			var userName = _authService.GetUserName(request.Email);

			var claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Sub, userName),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Issuer"],
				claims: claims,
				expires: DateTime.Now.AddHours(4),
				signingCredentials: creds);

			return Ok(new PostSignResponse
			{
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = token.ValidTo
			});
		}
	}
}
