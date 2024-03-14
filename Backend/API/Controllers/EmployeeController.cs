using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Net;
using System.Text.Json;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/staff/employees")]
	public class EmployeeController : Controller
	{
		private readonly IEmployeeService _service;

		public EmployeeController(IEmployeeService service)
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
