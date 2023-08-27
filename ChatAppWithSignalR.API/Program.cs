using ChatAppWithSignalR.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:4200")
        );
});

builder.Services.AddSignalR();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatapp");

app.Run();
