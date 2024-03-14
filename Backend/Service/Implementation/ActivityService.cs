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
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Implementation
{
	public class ActivityService : IActivityService
	{
		private readonly ApplicationDbContext _context;

		public ActivityService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BaseResponse<BsonDocument>> Create(JsonDocument model)
		{
			var response = new BaseResponse<BsonDocument>();

			try
			{
				var activity = Converter.JsonToBson(model);

				await _context.Activities.InsertOneAsync(activity);

				response.Data = activity;
				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while inserting new activity";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}

		public async Task<BaseResponse<List<BsonDocument>>> Get(string serviceNumber)
		{
			var response = new BaseResponse<List<BsonDocument>>();

			try
			{
				var filter = Builders<BsonDocument>.Filter.Eq("serviceNumber", serviceNumber);

				var filteredActivities = await _context.Activities.Find(filter).ToListAsync();

				if (filteredActivities.Count == 0)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					return response;
				}

				response.Data = filteredActivities;
				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while finding activity by employee's service number";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
