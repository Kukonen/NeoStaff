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

var app = builder.Build();

app.MapControllers();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
