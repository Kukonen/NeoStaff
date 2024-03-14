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
	public class EmployeeService : IEmployeeService
	{
		private readonly ApplicationDbContext _context;

		public EmployeeService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BaseResponse<List<Dictionary<string, object>>>> Get()
		{
			var response = new BaseResponse<List<Dictionary<string, object>>>();

			try
			{
				var filteredPersons = await _context.Activities.Find(new BsonDocument()).ToListAsync();

				foreach (var person in filteredPersons)
				{
					response.Data.Add(BsonProcessor.ProcessBsonDocument(person));
				}

				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while finding employees";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
