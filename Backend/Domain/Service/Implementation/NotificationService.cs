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
using static System.Formats.Asn1.AsnWriter;

namespace Service.Implementation
{
	public class NotificationService : INotificationService
	{
		private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task<BaseResponse<List<Dictionary<string, object>>>> GetEmployeesCertification()
		{
			var response = new BaseResponse<List<Dictionary<string, object>>>();
			response.Data = new List<Dictionary<string, object>>();

			try
			{
				var persons = await _context.Employee.Find(new BsonDocument()).ToListAsync();

				if (persons == null)
				{
					response.StatusCode = HttpStatusCode.InternalServerError;
					response.Message = "Ошибка при получении всех работников.";
					return response;
				}

				foreach(var person in persons)
				{
					var dateStrings = new List<string>();

					foreach (var id in person["activities"].AsBsonArray)
					{
						var filter = Builders<BsonDocument>.Filter.Eq("_id", id.AsObjectId);

						var activity = await _context.Activities.Find(filter).FirstAsync();

						if (activity == null)
						{
							response.StatusCode = HttpStatusCode.InternalServerError;
							response.Message = "Внутренняя ошибка при попытке получить активность по идентификатору из массива работника.";
							return response;
						}

						if (activity["type"].AsString == "certification")
						{
							dateStrings.Add(activity["date"].AsString);
						}
					}

					var builder = new StringBuilder();

					if(dateStrings.Count > 0)
					{
						DateTime[] dates = dateStrings.Select(s => DateTime.Parse(s)).ToArray();
						DateTime maxDate = dates.Max();
						builder.Append(maxDate.ToString("yyyy-MM-dd"));
					}
					else
					{
						builder.Append("");
					}

					var certification = new BsonDocument
					{
						{ "serviceNumber", person["serviceNumber"].AsString },
						{ "surname", person["surname"].AsString },
						{ "middlename", person["middlename"].AsString },
						{ "name", person["name"].AsString },
						{ "lastCertification", builder.ToString() }
					};

					response.Data.Add(BsonProcessor.ProcessBsonDocument(certification));
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
