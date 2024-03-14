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
	public class PositionService : IPositionService
	{
		private readonly ApplicationDbContext _context;

		public PositionService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BaseResponse<List<Dictionary<string, object>>>> Get()
		{
			var response = new BaseResponse<List<Dictionary<string, object>>>();

			try
			{
				var filteredPositions = await _context.Positions.Find(new BsonDocument()).ToListAsync();

				foreach(var position in filteredPositions)
				{
					response.Data.Add(BsonProcessor.ProcessBsonDocument(position));
				}

				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while finding employees's positions";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
