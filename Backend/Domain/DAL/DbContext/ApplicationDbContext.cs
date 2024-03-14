using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContext
{
	public class ApplicationDbContext
	{
		private readonly MongoClient mongoClient;
		private readonly IMongoDatabase database;

		public ApplicationDbContext(string connection, string dbName)
		{
			mongoClient = new MongoClient(connection);
			database = mongoClient.GetDatabase(dbName);
		}

		public IMongoCollection<BsonDocument> Employee
		{
			get
			{
				var users = database.GetCollection<BsonDocument>("employee");

				if (users == null)
				{
					database.CreateCollection("employee");
				}

				return database.GetCollection<BsonDocument>("employee");
			}
		}

		public IMongoCollection<BsonDocument> Activities
		{
			get
			{
				var users = database.GetCollection<BsonDocument>("activities");

				if (users == null)
				{
					database.CreateCollection("activities");
				}

				return database.GetCollection<BsonDocument>("activities");
			}
		}


		public IMongoCollection<BsonDocument> Positions
		{
			get
			{
				var users = database.GetCollection<BsonDocument>("positions");

				if (users == null)
				{
					database.CreateCollection("positions");
				}

				return database.GetCollection<BsonDocument>("positions");
			}
		}
	}
}
