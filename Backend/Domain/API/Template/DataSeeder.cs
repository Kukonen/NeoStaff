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
					{ "name", "Павел" },
					{ "surname", "Биглер" },
					{ "middlename", "Павлович" },
					{ "scores", 0 },
					{ "serviceNumber", "12345-67890" },
					{ "salary", 0 },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Никита" },
					{ "surname", "Карпов" },
					{ "middlename", "Иванович" },
					{ "scores", 0 },
					{ "serviceNumber", "54321-09876" },
					{ "salary", 0 },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Евгений" },
					{ "surname", "Куконен" },
					{ "middlename", "Игоревич" },
					{ "scores", 0 },
					{ "serviceNumber", "98765-43210" },
					{ "salary", 0 },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Мария" },
					{ "surname", "Козлова" },
					{ "middlename", "Андреевна" },
					{ "scores", 0 },
					{ "serviceNumber", "13579-24680" },
					{ "salary", 0 },
					{ "activities", new BsonArray() }
				},
				new BsonDocument
				{
					{ "name", "Петр" },
					{ "surname", "Сидоренко" },
					{ "middlename", "Никитович" },
					{ "scores", 0 },
					{ "serviceNumber", "25179-213370" },
					{ "salary", 0 },
					{ "activities", new BsonArray() }
				}
			};

			//Вставка должностей
			var positions = new List<BsonDocument>
			{
				new BsonDocument
				{
					{ "title", "Software Engineer" },
					{ "requirementScores", 4000 }
				},
				new BsonDocument
				{
					{ "title", "Data Scientist" },
					{ "requirementScores", 4000 }
				},
				new BsonDocument
				{
					{ "title", "Project Manager" },
					{ "requirementScores", 6000 }
				},
				new BsonDocument
				{
					{ "title", "UX/UI Designer" },
					{ "requirementScores", 2500 }
				},
				new BsonDocument
				{
					{ "title", "C# Junior Developer" },
					{ "requirementScores", 1300 }
				},
				new BsonDocument
				{
					{ "title", "Junior Frontend Developer" },
					{ "requirementScores", 1000 }
				},
				new BsonDocument
				{
					{ "title", "Team Leader" },
					{ "requirementScores", 5500 }
				},
				new BsonDocument
				{
					{ "title", "Frontend Senior Developer" },
					{ "requirementScores", 5500 }
				},
				new BsonDocument
				{
					{ "title", "Java Junior Developer" },
					{ "requirementScores", 1400 }
				},
				new BsonDocument
				{
					{ "title", "Java Middle Developer" },
					{ "requirementScores", 2700 }
				},
				new BsonDocument
				{
					{ "title", "Backend Junior Developer" },
					{ "requirementScores", 1600 }
				},
				new BsonDocument
				{
					{ "title", "C# Middle Developer" },
					{ "requirementScores", 2500 }
				},
				new BsonDocument
				{
					{ "title", "Backend Middle Developer" },
					{ "requirementScores", 2700 }
				},
				new BsonDocument
				{
					{ "title", "Teacher of junior staff" },
					{ "requirementScores", 3000 }
				},
				new BsonDocument
				{
					{ "title", "DevOps Junior Developer" },
					{ "requirementScores", 2000 }
				},
				new BsonDocument
				{
					{ "title", "Andriod Middle+ Developer" },
					{ "requirementScores", 3100 }
				},
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
				""date"": ""2024-01-02"",
				""note"": ""С отличием прошел тестовый период"",
				""mark"": 1000,
				""salary"": 60000,
				""activityInfo"": {
					""position"": ""C# Junior Developer"",
					""report"": ""Ценный сотрудник, необходимо больше практики."",
					""result"": ""Принят""
				}
			}"), "endTestPeriod", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-01-16"",
				""note"": ""Назначен в проект компании."",
				""mark"": 200,
				""salary"": 5000,
				""activityInfo"": {
					""position"": ""Backend Junior Developer""
				}
			}"), "start", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-01-20"",
				""note"": ""Несоблюдение устава компании."",
				""mark"": -100,
				""salary"": 0,
				""activityInfo"": {
					""reason"": ""Опоздание на работу.""
				}
			}"), "rebuke", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-02-02"",
				""note"": ""Плановая аттестация сотрудников."",
				""mark"": 200,
				""salary"": 2000,
				""activityInfo"": {
					""certification"": ""Проверка базовых навыков программирования."",
					""result"": ""Успешное прохождение аттестации.""
				}
			}"), "certification", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-04-03"",
				""note"": ""Завершен проект в роли backend разработчика."",
				""mark"": 100,
				""salary"": 1000,
				""activityInfo"": {
					""position"": ""Backend Junior Developer""
				}
			}"), "end", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-05-10"",
				""note"": ""Снятие с должности C# Junior Developer"",
				""mark"": 0,
				""salary"": 0,
				""activityInfo"": {
					""position"": ""C# Junior Developer""
				}
			}"), "end", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-05-12"",
				""note"": ""Повышение до должности C# Middle Developer"",
				""mark"": 500,
				""salary"": 30000,
				""activityInfo"": {
					""position"": ""C# Middle Developer""
				}
			}"), "start", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-07-22"",
				""note"": ""Участие в хакатоне от лица компании"",
				""mark"": 300,
				""salary"": 5000,
				""activityInfo"": {
					""place"": ""СПб ГУАП"",
					""result"": ""2 место"",
					""theme"": ""NeoHack NeoStaff"",
					""role"": ""Backend Developer""
				}
			}"), "competition", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-09-19"",
				""note"": ""Плановая аттестация сотрудников."",
				""mark"": 100,
				""salary"": 2000,
				""activityInfo"": {
					""certification"": ""Проверка навыков построения микросервисных приложений."",
					""result"": ""Успешное прохождение аттестации.""
				}
			}"), "certification", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-10-16"",
				""note"": ""Назначен в проект компании."",
				""mark"": 100,
				""salary"": 0,
				""activityInfo"": {
					""position"": ""Backend Middle Developer""
				}
			}"), "start", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-12-12"",
				""note"": ""Назначен на роль обучения младших сотрудников."",
				""mark"": 200,
				""salary"": 5000,
				""activityInfo"": {
					""position"": ""Teacher of junior staff""
				}
			}"), "start", "12345-67890");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2025-04-03"",
				""note"": ""Завершено участие в проекте в роли Backend Middle Developer."",
				""mark"": 100,
				""salary"": 2000,
				""activityInfo"": {
					""position"": ""Backend Middle Developer""
				}
			}"), "end", "12345-67890");





			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-03-06"",
				""note"": ""С превосходным результатом прошел тестовый период"",
				""mark"": 1200,
				""salary"": 70000,
				""activityInfo"": {
					""position"": ""Java Junior Developer"",
					""report"": ""Ценный сотрудник, очень хорошо знает виртуализацию."",
					""result"": ""Принят""
				}
			}"), "endTestPeriod", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-04-22"",
				""note"": ""Участие в хакатоне от лица компании"",
				""mark"": 100,
				""salary"": 5000,
				""activityInfo"": {
					""place"": ""СПб ПУ"",
					""result"": ""3 место"",
					""theme"": ""Виртуализация и развертывание приложений"",
					""role"": ""Java Developer""
				}
			}"), "competition", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-05-19"",
				""note"": ""Назначен в проект для ускорения разработки."",
				""mark"": 300,
				""salary"": 1000,
				""activityInfo"": {
					""position"": ""DevOps Junior Developer""
				}
			}"), "start", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-07-02"",
				""note"": ""Плановая аттестация сотрудников."",
				""mark"": 200,
				""salary"": 2000,
				""activityInfo"": {
					""certification"": ""Проверка базовых навыков программирования и развертывания приложений."",
					""result"": ""Успешное прохождение аттестации.""
				}
			}"), "certification", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-08-22"",
				""note"": ""Обучение на платформе для сотрудников."",
				""mark"": 200,
				""salary"": 2000,
				""activityInfo"": {
					""skill"": ""Мобильная разработка на Java/Cotlin."",
					""report"": ""Успешное прохождение курсов.""
				}
			}"), "skills", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-09-01"",
				""note"": ""Несоблюдение устава компании."",
				""mark"": -100,
				""salary"": -1000,
				""activityInfo"": {
					""reason"": ""Хамство.""
				}
			}"), "rebuke", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-09-10"",
				""note"": ""Будет рассмотрено повышение в должности."",
				""mark"": 0,
				""salary"": 0,
				""activityInfo"": {
					""result"": ""Поставлен вопрос о повышении."",
					""report"": ""Сотрудник обладает навыками для повышения.""
				}
			}"), "careerDialog", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-10-01"",
				""note"": ""Снятие с должности Java Junior Developer для дальнейшего повышения."",
				""mark"": 0,
				""salary"": 0,
				""activityInfo"": {
					""position"": ""Java Junior Developer""
				}
			}"), "end", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-10-03"",
				""note"": ""Одобрено повышение до Java Middle Developer."",
				""mark"": 400,
				""salary"": 40000,
				""activityInfo"": {
					""position"": ""Java Middle Developer""
				}
			}"), "start", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-12-07"",
				""note"": ""Снятие с проекта в связи с переводом в другой."",
				""mark"": 0,
				""salary"": -3000,
				""activityInfo"": {
					""position"": ""DevOps Junior Developer""
				}
			}"), "end", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-12-07"",
				""note"": ""Снятие с проекта в связи с переводом в другой."",
				""mark"": 200,
				""salary"": 5000,
				""activityInfo"": {
					""place"": ""СПб ГУАП"",
					""theme"": ""Мобильная разработка: основные паттерны"",
					""role"": ""Speaker""
				}
			}"), "event", "54321-09876");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2025-02-13"",
				""note"": ""Назначен в проект разработчки мобильного приложения о компании."",
				""mark"": 200,
				""salary"": 10000,
				""activityInfo"": {
					""position"": ""Andriod Middle+ Developer""
				}
			}"), "start", "54321-09876");





			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-03-03"",
				""note"": ""Взят в штат сотрудников."",
				""mark"": 4500,
				""salary"": 270000,
				""activityInfo"": {
					""position"": ""Frontend Senior Developer""
				}
			}"), "start", "98765-43210");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-05-02"",
				""note"": ""Внеплановая аттестация высших сотрудников."",
				""mark"": 300,
				""salary"": 30000,
				""activityInfo"": {
					""certification"": ""Проверка всех навыков."",
					""result"": ""Успешное прохождение аттестации.""
				}
			}"), "certification", "98765-43210");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-06-29"",
				""note"": ""Назначен лидером проекта по поддержанию сайта компании."",
				""mark"": 500,
				""salary"": 50000,
				""activityInfo"": {
					""position"": ""Team Leader""
				}
			}"), "start", "98765-43210");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2024-12-07"",
				""note"": ""Снятие с проекта в связи с переводом в другой."",
				""mark"": 100,
				""salary"": 5000,
				""activityInfo"": {
					""place"": ""СПб ГУАП"",
					""theme"": ""Веб разработка: самые глубокие темы"",
					""role"": ""Speaker""
				}
			}"), "event", "98765-43210");

			await _service.Create(JsonDocument.Parse(@"{
				""date"": ""2025-01-13"",
				""note"": ""Назначен управляющим проектами компании."",
				""mark"": 700,
				""salary"": 70000,
				""activityInfo"": {
					""position"": ""Project Manager""
				}
			}"), "start", "98765-43210");

			//await _service.Create(JsonDocument.Parse(@"{
			//	""date"": ""2023-12-15"",
			//	""note"": ""Presentation preparation"",
			//	""mark"": 50,
			//	""salary"": 50000,
			//	""activityInfo"": {
			//		""position"": ""Developer""
			//	}
			//}"), "end", "98765-43210");

			//await _service.Create(JsonDocument.Parse(@"{
			//	""date"": ""2023-12-31"",
			//	""note"": ""Important meeting"",
			//	""mark"": 40,
			//	""salary"": 60000,
			//	""activityInfo"": {
			//		""position"": ""Manager""
			//	}
			//}"), "end", "54321-09876");

			//await _context.Activities.InsertManyAsync(activities);
		}
	}
}
