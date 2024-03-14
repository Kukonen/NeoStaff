using DAL.Response;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IActivityService
	{
		Task<BaseResponse<BsonDocument>> Create(JsonDocument model);

		Task<BaseResponse<List<BsonDocument>>> Get(string serviceNumber);
	}
}
