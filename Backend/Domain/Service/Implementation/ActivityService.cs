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
using ThirdParty.Json.LitJson;

namespace Service.Implementation
{
	public class ActivityService : IActivityService
	{
		private readonly ApplicationDbContext _context;

		public ActivityService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BaseResponse<Dictionary<string, object>>> Create(JsonDocument model, string type, string serviceNumber)
		{
			var response = new BaseResponse<Dictionary<string, object>>();

			try
			{
				if (!Validation.CheckActivityCorrectFormat(model, type) || Validation.CheckJsonDocumentForInjection(model.RootElement))
				{
					response.StatusCode = HttpStatusCode.BadRequest;

					return response;
				}

				var activity = Converter.JsonToBson(model);
				activity.Add("type", type);

				await _context.Activities.InsertOneAsync(activity);
				var result = await AddActivityToEmployee(serviceNumber, activity);

				if(result.StatusCode != HttpStatusCode.OK)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					return response;
				}

				response.Data = BsonProcessor.ProcessBsonDocument(activity);
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

		public async Task<BaseResponse<List<Dictionary<string, object>>>> Get(string serviceNumber)
		{
			var response = new BaseResponse<List<Dictionary<string, object>>>();
			response.Data = new List<Dictionary<string, object>>();

			try
			{
				var filterPerson = Builders<BsonDocument>.Filter.Eq("serviceNumber", serviceNumber);

				var person = await _context.Employee.Find(filterPerson).FirstAsync();

				if(person == null)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					return response;
				}

				foreach(var id in person["activities"].AsBsonArray)
				{
					var filter = Builders<BsonDocument>.Filter.Eq("_id", id.AsObjectId);

					var activity = await _context.Activities.Find(filter).FirstAsync();

					if (activity == null)
					{
						response.StatusCode = HttpStatusCode.InternalServerError;
						return response;
					}

					response.Data.Add(BsonProcessor.ProcessBsonDocument(activity));
				}

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

		protected async Task<BaseResponse<BsonDocument>> AddActivityToEmployee(string serviceNumber, BsonDocument activity)
		{
			var response = new BaseResponse<BsonDocument>();

			try
			{
				var filter = Builders<BsonDocument>.Filter.Eq("serviceNumber", serviceNumber);

				var filteredPerson = await _context.Employee.Find(filter).FirstAsync();

				if(filteredPerson == null)
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					return response;
				}

				var scores = filteredPerson["scores"].AsInt32;
				scores += activity["mark"].AsInt32;

				var update = Builders<BsonDocument>.Update.Set("scores", scores)
														  .Push("activities", activity["_id"].AsObjectId);

				var updatedPerson = await _context.Employee.UpdateOneAsync(filter, update);

				if(updatedPerson.ModifiedCount == 0)
				{
					response.StatusCode = HttpStatusCode.InternalServerError;
					return response;
				}

				response.StatusCode = HttpStatusCode.OK;

				return response;
				
			}
			catch(Exception)
			{
				response.StatusCode = HttpStatusCode.InternalServerError;
				return response;
			}
		}

	}
}
