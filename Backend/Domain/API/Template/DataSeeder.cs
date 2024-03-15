using DAL.DbContext;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Interface;
using System.Text.Json;

namespace API.Template
{
	public class DataSeeder
	{
		private readonly ApplicationDbContext _context;
		private readonly IActivityService _service;

		public DataSeeder(ApplicationDbContext context, IActivityService service)
		{
			_context = context;
			_service = service;
		}

		public async Task SeedDataAsync()
		{
			//Вставка персонала
			var employees = new List<BsonDocument>
			{
				new BsonDocument
				{
					{ "name", "Иван" },
					{ "surname", "Иванов" },
					{ "middlename", "Иванович" },
					{ "scores", 85 },
					{ "serviceNumber", "12345-67890" },
					{ "salary", 50000 },
					{ "positions", new BsonArray() },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Елена" },
					{ "surname", "Петрова" },
					{ "middlename", "Сергеевна" },
					{ "scores", 92 },
					{ "serviceNumber", "54321-09876" },
					{ "salary", 60000 },
					{ "positions", new BsonArray() },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Алексей" },
					{ "surname", "Сидоров" },
					{ "middlename", "Павлович" },
					{ "scores", 78 },
					{ "serviceNumber", "98765-43210" },
					{ "salary", 55000 },
					{ "positions", new BsonArray() },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Мария" },
					{ "surname", "Козлова" },
					{ "middlename", "Андреевна" },
					{ "scores", 89 },
					{ "serviceNumber", "13579-24680" },
					{ "salary", 58000 },
					{ "positions", new BsonArray() },
					{ "activities", new BsonArray() }
				}
			};

			//Вставка должностей
			var positions = new List<BsonDocument>
			{
				new BsonDocument
				{
					{ "title", "Software Engineer" },
					{ "requirementScores", 9000 }
				},
				new BsonDocument
				{
					{ "title", "Data Scientist" },
					{ "requirementScores", 9500 }
				},
				new BsonDocument
				{
					{ "title", "Project Manager" },
					{ "requirementScores", 8500 }
				},
				new BsonDocument
				{
					{ "title", "UX/UI Designer" },
					{ "requirementScores", 8800 }
				}
			};

			//Вставка активностей
			var activities = new List<BsonDocument>
			{
				new BsonDocument
				{
					{ "date", new BsonDateTime(new DateTime(2023, 12, 31)) },
					{ "note", "Important meeting" },
					{ "mark", 40 },
					{ "activityInfo", new BsonDocument
						{
							{ "position", "Manager" }
						}
					},
					{ "type", "Meeting" }
				},
				new BsonDocument
				{
					{ "date", new BsonDateTime(new DateTime(2023, 12, 15)) },
					{ "note", "Presentation preparation" },
					{ "mark", 5 },
					{ "activityInfo", new BsonDocument
						{
							{ "position", "Developer" }
						}
					},
					{ "type", "Task" }
				},
			};

			await _context.Employee.InsertManyAsync(employees);
			await _context.Positions.InsertManyAsync(positions);

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2023-12-31T00:00:00Z"",
				""note"": ""Important meeting"",
				""mark"": 40,
				""activityInfo"": {
					""position"": ""Manager""
				}
			}"), "end", "13579-24680");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2023-12-15T00:00:00Z"",
				""note"": ""Presentation preparation"",
				""mark"": 50,
				""activityInfo"": {
					""position"": ""Developer""
				}
			}"), "end", "98765-43210");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2023-12-31T00:00:00Z"",
				""note"": ""Important meeting"",
				""mark"": 40,
				""activityInfo"": {
					""position"": ""Manager""
				}
			}"), "end", "54321-09876");

			//await _context.Activities.InsertManyAsync(activities);
		}
	}
}
