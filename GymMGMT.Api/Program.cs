using GymMGMT.Api.Middleware;
using GymMGMT.Api.Services;
using GymMGMT.Application;
using GymMGMT.Application.CQRS;
using GymMGMT.Application.Security.Contracts;
using GymMGMT.Infrastructure;
using GymMGMT.Infrastructure.Security;
using GymMGMT.Persistence.EF;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog
var logger = new LoggerConfiguration()
  .WriteTo.Map(evt => evt.Level, (level, wt) => wt.RollingFile("..\\logs\\" + level + "\\" + level + "-{Date}.log"))
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddSingleton(typeof(Serilog.ILogger), logger);

// Quartz
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var membershipValidityJobKey = new JobKey("MembershipValidity");
    q.AddJob<CheckMembershipValidityService>(opts => opts.WithIdentity(membershipValidityJobKey));
    q.AddTrigger(opts => opts
        .ForJob(membershipValidityJobKey)
        .WithIdentity("MembershipValidity-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInHours(1)
            .RepeatForever()));
});
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    builder.Services.AddQuartzServer(q => q.WaitForJobsToComplete = true);
}

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddApplicationCqrsServices();
builder.Services.AddPersistenceEfServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddInfrastructureSecurityServices(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and then your token in the text input below.
              \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                  {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  },
                  Scheme = "oauth2",
                  Name = "Bearer",
                  In = ParameterLocation.Header,

                },
                new List<string>()
              }
            });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GymMGMT API",
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DisplayRequestDuration());
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();



// Make the implicit Program class public so test projects can access it
public partial class Program { }
