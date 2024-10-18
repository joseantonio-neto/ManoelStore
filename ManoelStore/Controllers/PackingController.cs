using Microsoft.AspNetCore.Mvc;
using ManoelStore.Models;
using ManoelStore.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManoelStore.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class PackingController : ControllerBase
	{
		private readonly IPackingService _packingService;

		public PackingController(IPackingService packingService) 
		{
			_packingService = packingService;
		}

		[HttpPost]
		public ActionResult<PostPackingResponse> PostPacking([FromBody] PostPackingRequest request)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var response = _packingService.Pack(request);

			return Ok(response);
		}
	}
}
