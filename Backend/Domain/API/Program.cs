using Confluent.Kafka;
using DAL.DbContext;
using Service.Implementation;
using Service.Interface;

// ==================== KAFKA =====================
var config = new ProducerConfig { BootstrapServers = "kafka:9092" };

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    Console.WriteLine(producer);
    for (int i = 0; i < 5; i++)
	{
		Console.WriteLine(i);
        await producer.ProduceAsync("analytics-topic", new Message<Null, string> { Value = "New data" });
		Thread.Sleep(10000);
    }
}
// ================================================

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<ApplicationDbContext>(provider =>
{
	var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");
	var dbName = builder.Configuration["Database:Name"];

	return new ApplicationDbContext(connectionString ?? throw new ArgumentNullException(),
									dbName ?? throw new ArgumentNullException());
});
builder.Services.AddTransient<IActivityService, ActivityService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IPositionService, PositionService>();

var app = builder.Build();

app.MapControllers();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
