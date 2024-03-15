using API.Template;
using DAL.DbContext;
using Service.Implementation;
using Service.Interface;

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
builder.Services.AddTransient<IGraphicsService, GraphicsService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddSingleton<DataSeeder>();

var app = builder.Build();

var databaseInitializer = app.Services.GetRequiredService<DataSeeder>();
await databaseInitializer.SeedDataAsync();

app.MapControllers();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
