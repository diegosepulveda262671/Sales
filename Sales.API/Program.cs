using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Sales.API.Data.DataContext>(x=>x.UseSqlServer("name=DockerConnection"));
builder.Services.AddTransient<Sales.API.Data.SeedDb>();

var app = builder.Build();

SeedData(app);

 void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (IServiceScope? scope = scopeFactory!.CreateScope())
    {
        Sales.API.Data.SeedDb? service = scope.ServiceProvider.GetService<Sales.API.Data.SeedDb>();
        service!.SeedAsync().Wait();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x=>x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin=>true)
.AllowCredentials());

app.Run();

