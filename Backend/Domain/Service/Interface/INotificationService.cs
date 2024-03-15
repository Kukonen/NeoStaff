using DAL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface INotificationService
	{
		Task<BaseResponse<List<Dictionary<string, object>>>> GetEmployeesCertification();
	}
}
