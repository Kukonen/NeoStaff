using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Net;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/staff/graphics")]
	public class GraphicsController : Controller
	{
		private readonly IGraphicsService _service;

		public GraphicsController(IGraphicsService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetStatistics([FromQuery] string serviceNumber) 
		{
			if (ModelState.IsValid)
			{
				var response = await _service.GetActivitiesStatistics(serviceNumber);

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
