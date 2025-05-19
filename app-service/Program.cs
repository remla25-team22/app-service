using System.Reflection;
using ModelServiceConnector;
using Prometheus;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IApiClient>(_ => new ApiClient(new HttpClient{BaseAddress = new Uri(builder.Configuration["BackendUrl"])}));
// Add services to the container.
builder.Logging.AddConsole();
builder.Services.AddControllers();

#if DEBUG
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});
#endif

WebApplication app = builder.Build();


#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.MapMetrics();

app.Run();