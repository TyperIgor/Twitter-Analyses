using TwitterAnalysis.Application.Middleware;
using TwitterAnalysis.Application.Swagger;
using TwitterAnalysis.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHostDependencies(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddAuthentication();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
