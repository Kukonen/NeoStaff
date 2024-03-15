using DAL.Response;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IEmployeeService
	{
		Task<BaseResponse<List<Dictionary<string, object>>>> Get();

		Task<BaseResponse<List<string>>> GetPositionsByDate(string date, string serviceNumber);
	}
}
