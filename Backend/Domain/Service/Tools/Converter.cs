﻿using MongoDB.Bson;
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
	}
}