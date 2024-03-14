using DAL.Response;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IPositionService
	{
		Task<BaseResponse<List<BsonDocument>>> Get();
	}
}
