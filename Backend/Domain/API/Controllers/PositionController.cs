using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Net;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/staff/positions")]
	public class PositionController : Controller
	{
		private readonly IPositionService _service;

		public PositionController(IPositionService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			if (ModelState.IsValid)
			{
				var response = await _service.Get();

				if (response.StatusCode != HttpStatusCode.OK)
				{
					return StatusCode((int)response.StatusCode, response.Message);
				}

				return Json(response.Data);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
