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

		public async Task<BaseResponse<Dictionary<string, object>>> Create(JsonDocument model, string type)
		{
			var response = new BaseResponse<Dictionary<string, object>>();

			try
			{
				if(!Validation.CheckActivityCorrectFormat(model, type) || Validation.CheckJsonDocumentForInjection(model.RootElement))
				{
					response.StatusCode = HttpStatusCode.BadRequest;

					return response;
				}

				var activity = Converter.JsonToBson(model);

				await _context.Activities.InsertOneAsync(activity);

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

		public async Task<BaseResponse<Dictionary<string, object>>> Get(string serviceNumber)
		{
			var response = new BaseResponse<Dictionary<string, object>>();

			try
			{
				var filter = Builders<BsonDocument>.Filter.Eq("serviceNumber", serviceNumber);

				var activity = await _context.Activities.Find(filter).FirstAsync();

				if (activity == null)
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
				response.Message = "Error while finding activity by employee's service number";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
