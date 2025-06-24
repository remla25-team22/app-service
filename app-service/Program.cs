using System.Reflection;
using ModelServiceConnector;
using Prometheus;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApiClient>(_ => new ApiClient(new HttpClient{BaseAddress = new Uri(builder.Configuration["BackendUrl"])}));
// Add services to the container.
builder.Logging.AddConsole();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();



//app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapMetrics();

app.Run();