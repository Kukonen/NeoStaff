using DAL.Entity.ActivityInfo;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;
using System.Xml.Linq;

namespace Service.Tools
{
	public class Validation
	{
		public static bool CheckJsonDocumentForInjection(JsonElement element)
		{
			foreach (JsonProperty property in element.EnumerateObject())
			{
				if (property.Value.ValueKind == JsonValueKind.String)
				{
					string value = property.Value.GetString();

					// Проверяем строковое значение на наличие инъекций
					if (HasInjection(value))
					{
						return true;
					}
				}
				else if (property.Value.ValueKind == JsonValueKind.Object)
				{
					// Если значение является объектом, рекурсивно проверяем его пары ключ-значение
					if (CheckJsonDocumentForInjection(property.Value))
					{
						return true;
					}
				}
			}

			return false;
		}

		static bool HasInjection(string value)
		{
			// Пример базовой проверки на наличие инъекций
			// В реальном приложении рекомендуется использовать более сложные и надежные методы

			// Пример: проверяем, содержит ли строка запрещенные символы или конструкции
			string[] forbiddenPatterns = { "'", "\"", "--", ";", "/*", "*/", "<", ">", "script" };

			foreach (string pattern in forbiddenPatterns)
			{
				if (value.Contains(pattern))
				{
					return true;
				}
			}

			return false;
		}

		public static bool CheckActivityCorrectFormat(JsonDocument jsonData, string type)
		{
			JsonElement element = jsonData.RootElement;
			JsonElement updated_element = Converter.AddPropertyToJsonElement(element, "type", type);
			JsonElement internalObject = element.GetProperty("activityInfo");

			Type concreteType = DetermineActivityType(type);

			if(updated_element.EnumerateObject().Count() != typeof(Activity<>).GetProperties().Count() ||
			   internalObject.EnumerateObject().Count() != concreteType.GetProperties().Count())
			{
				return false;
			}

			foreach (var property in typeof(Activity<>).GetProperties())
			{
				var jsonProperty = updated_element.GetProperty(property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1));
				var value = jsonProperty.ToString();

				if (value == null)
				{
					return false;
				}

				if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
				{
					continue;
				}
			}

			var test2 = internalObject.EnumerateObject().Count();

			foreach (var property in concreteType.GetProperties())
			{
				var jsonProperty = internalObject.GetProperty(property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1));
				var value = jsonProperty.ToString();

				if (value == null)
				{
					return false;
				}
			}

			return true;
		}

		private static Type DetermineActivityType(string type)
		{
			switch (type)
			{
				case "start":
				{
					return typeof(StartActivityInfo);
				}

				case "end":
				{
					return typeof(EndActivityInfo);
				}

				case "certification":
				{
					return typeof(CertificationActivityInfo);
				}

				case "learn":
				{
					return typeof(LearnActivityInfo);
				}

				case "competition":
				{
					return typeof(CompetitionActivityInfo);
				}

				case "event":
				{
					return typeof(EventActivityInfo);
				}

				case "endTestPeriod":
				{
					return typeof(EndTestPeriodActivityInfo);
				}

				case "changeSalary":					
				{
					return typeof(ChangeSalaryActivityInfo);
				}

				case "skills":
				{
					return typeof(SkillsActivityInfo);
				}

				case "projectStart":
				{
					return typeof(ProjectStartActivityInfo);
				}

				case "endProject":
				{
					return typeof(EndProjectActivityInfo);
				}

				case "careerDialog":
				{
					return typeof(CareerDialogActivityInfo);
				}

				case "rebuke":
				{
					return typeof(RebukeActivityInfo);
				}

				default:
				{
					throw new Exception();
				}
			}
		}
	}
}
