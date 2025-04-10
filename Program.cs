using MongoDB.Driver;
using Microsoft.Extensions.Options;
using quod_backend_api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB")
);


builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
    {
        var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
        return new MongoClient(settings.ConnectionString);
    }
);

builder.Services.AddScoped<IMongoDatabase>(
    serviceProvider =>
    {
        var client = serviceProvider.GetRequiredService<IMongoClient>();
        var settings = serviceProvider.GetRequiredService<IOptions<MongoDBSettings>>().Value;
        return client.GetDatabase(settings.DatabaseName);
    } 
);


builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

