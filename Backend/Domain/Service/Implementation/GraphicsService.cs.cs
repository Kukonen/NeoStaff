using DAL.DbContext;
using DAL.Response;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Interface;
using Service.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
	public class GraphicsService : IGraphicsService
	{
		private readonly ApplicationDbContext _context;

        public GraphicsService(ApplicationDbContext context)
        {
			_context = context;

		}

		public async Task<BaseResponse<List<Dictionary<string, object>>>> GetActivitiesStatistics(string serviceNumber)
		{
			var response = new BaseResponse<List<Dictionary<string, object>>>();
			response.Data = new List<Dictionary<string, object>>();

			try
			{
				var filterPerson = Builders<BsonDocument>.Filter.Eq("serviceNumber", serviceNumber);

				var person = await _context.Employee.Find(filterPerson).FirstAsync();

				if (person == null)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					response.Message = "Работника с указанным номером не существует.";
					return response;
				}

				var positions = new List<string>(); //Для хранения должностей на каждую активность
				int salary = 0;  //Для хранения зарплаты на каждую активность
				int scores = 0;  //Для хранения количества баллов на каждую активность

				foreach (var id in person["activities"].AsBsonArray)
				{
					var filter = Builders<BsonDocument>.Filter.Eq("_id", id.AsObjectId);

					var activity = await _context.Activities.Find(filter).FirstAsync();

					if (activity == null)
					{
						response.StatusCode = HttpStatusCode.InternalServerError;
						response.Message = "Внутренняя ошибка при попытке получить аактивность по идентификатору из массива работника.";
						return response;
					}

					if (activity["type"] == "start")
					{
						positions.Add(activity["activityInfo"]["position"].AsString);
					}
					else if(activity["type"] == "end" || activity["type"] == "endTestPeriod")
					{
						if(positions.Contains(activity["activityInfo"]["position"].AsString))
						{
							positions.Remove(activity["activityInfo"]["position"].AsString);
						}
					}

					salary += activity["salary"].AsInt32;
					scores += activity["mark"].AsInt32;

					var graphicsInfo = new BsonDocument
					{
						{ "date", activity["date"] },
						{ "salary", salary },
						{ "positions", new BsonArray(positions) },
						{ "scores", scores },
						{ "note",  activity["note"] }
					};

					response.Data.Add(BsonProcessor.ProcessBsonDocument(graphicsInfo));
				}

				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while getting activities statistics.";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
