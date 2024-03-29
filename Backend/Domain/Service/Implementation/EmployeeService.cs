﻿using DAL.DbContext;
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
			response.Data = new List<Dictionary<string, object>>();

			try
			{
				var filteredPersons = await _context.Employee.Find(new BsonDocument()).ToListAsync();

				if(filteredPersons == null || filteredPersons.Count == 0) 
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					return response;
				}

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

		public async Task<BaseResponse<List<string>>> GetPositionsByDate(string dateString, string serviceNumber)
		{
			var response = new BaseResponse<List<string>>();
			response.Data = new List<string>();

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
				var boundaryTime = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);

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

					if (DateTime.ParseExact(activity["date"].AsString, "yyyy-MM-dd", null) <= boundaryTime)
					{
						if (activity["type"] == "start")
						{
							positions.Add(activity["activityInfo"]["position"].AsString);
						}
						else if (activity["type"] == "end" || activity["type"] == "endTestPeriod")
						{
							if (positions.Contains(activity["activityInfo"]["position"].AsString))
							{
								positions.Remove(activity["activityInfo"]["position"].AsString);
							}
						}
					}
				}

				response.Data = positions;
				response.StatusCode = HttpStatusCode.OK;

				return response;
			}
			catch (Exception)
			{
				response.Message = "Error while getting employee's positions by date";
				response.StatusCode = HttpStatusCode.InternalServerError;

				return response;
			}
		}
	}
}
