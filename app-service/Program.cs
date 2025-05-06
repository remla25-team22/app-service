using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        conf =>
        {
            conf
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddControllers();

#if DEBUG
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(opt => {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        opt.IncludeXmlComments(xmlPath);
    });
#endif

var app = builder.Build();

#if DEBUG
    app.UseSwagger(); 
    app.UseSwaggerUI();
#endif


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
