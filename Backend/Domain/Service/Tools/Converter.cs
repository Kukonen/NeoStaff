using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Tools
{
	public class Converter
	{
		static public BsonDocument JsonToBson(JsonDocument jsonData)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
				{
					jsonData.WriteTo(writer);
					writer.Flush();
					string jsonString = Encoding.UTF8.GetString(stream.ToArray());

					// Преобразуем строку JSON в BsonDocument
					return BsonDocument.Parse(jsonString);
				}
			}
		}

		public static T DeserializeJsonDocument<T>(JsonElement element)
		{
			// Создаем объект класса Person
			T obj = Activator.CreateInstance<T>();

			foreach (var property in typeof(T).GetProperties())
			{
				// Получаем значение свойства из JsonElement
				var jsonProperty = element.GetProperty(property.Name);
				var value = jsonProperty.ToString();

				// Если свойство имеет вложенный объект, вызываем DeserializeJsonDocument рекурсивно
				if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
				{
					var nestedObject = DeserializeJsonDocument<object>(jsonProperty);
					property.SetValue(obj, nestedObject);
				}
				else
				{
					// Устанавливаем значение свойства
					property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
				}
			}

			return obj;
		}
	}
}
