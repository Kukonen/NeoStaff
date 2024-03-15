using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Net;

namespace API.Controllers
{
	[ApiController]
	[Route("/api/staff/notification")]
	public class NotificationController : Controller
	{
		private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

		[HttpGet]
		public async Task<IActionResult> GetEmloyeesSpecification()
		{
			if (ModelState.IsValid)
			{
				var response = await _service.GetEmployeesCertification();

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
