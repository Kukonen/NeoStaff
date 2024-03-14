using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tools
{
	public class BsonProcessor
	{
		static public Dictionary<string, object> ProcessBsonDocument(BsonDocument bsonDocument)
		{
			var processedDocument = new Dictionary<string, object>();

			foreach (var element in bsonDocument)
			{
				if (element.Value.IsObjectId)
				{
					// Преобразовываем ObjectId в строку
					processedDocument[element.Name] = element.Value.AsObjectId.ToString();
				}
				else
				{
					// Добавляем остальные значения как есть
					processedDocument[element.Name] = BsonTypeMapper.MapToDotNetValue(element.Value);
				}
			}

			return processedDocument;
		}
	}
}
