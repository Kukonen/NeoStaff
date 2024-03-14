using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Text.Json;
using System.Text;
using Service.Tools;
using Service.Interface;
using System.Net;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/staff/activities")]
	public class ActivityController : Controller
	{
		private readonly IActivityService _service;

        public ActivityController(IActivityService service)
        {
			_service = service;
        }

		[HttpGet]
		public async Task<ActionResult> Get([FromQuery] string serviceNumber)
		{
			if (ModelState.IsValid)
			{
				var response = await _service.Get(serviceNumber);

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

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] JsonDocument jsonData)
		{
			if(ModelState.IsValid)
			{
				var response = await _service.Create(jsonData);

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
