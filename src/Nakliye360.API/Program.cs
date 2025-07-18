using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nakliye360.API.CustomAttributes.RoleManagement;
using Nakliye360.API.Filters;
using Nakliye360.API.Middlewares;
using Nakliye360.Application;
using Nakliye360.Application.Abstractions.Session;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Infrastructure;
using Nakliye360.Infrastructure.Services.Session;
using Nakliye360.Persistence;
using Nakliye360.Persistence.Contexts;
using Nakliye360.Persistence.Seeder;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();


#region Added 
builder.Services.AddApplicationServices();
builder.Services.AddCustomFluentValidation();
builder.Services.AddScoped(typeof(ValidationFilter<>));

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

#endregion End Added


#region JWT  Authentication, CORS, HTTP Logging, Controllers



builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]!)
        ),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicPermission", policy =>
        policy.Requirements.Add(new PermissionRequirement("Placeholder"))); // Gerçek kod runtime'da verilecek

    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
});
builder.Services.AddScoped<ICurrentUserSession, CurrentUserSession>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();



#endregion

#region SWAGGER 

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token kullanarak kimlik doðrulama yapýn. 'Bearer {token}' formatýnda gönderin."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});
#endregion

#region SERILOG

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("DefaultConnection"), "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
            { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
            { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
            { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }

        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

#endregion serilog End

#region HEALTH CHECK    

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("HealthCheck"));

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.SetApiMaxActiveRequests(1);
}).AddInMemoryStorage();

#endregion Health Check End

#region MAPSTER  

//// config + register
//var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
//typeAdapterConfig.Scan(typeof(CustomerMappingConfig).Assembly);

//builder.Services.AddSingleton(typeAdapterConfig);
//builder.Services.AddScoped<IMapper, ServiceMapper>();
#endregion

#region VALIDATORS
//builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();
//builder.Services.AddScoped<IValidator<CreateCustomerDto>, CreateCustomerDtoValidator>();
//builder.Services.AddFluentValidationAutoValidation(); // Otomatik çalıştırma
//builder.Services.AddFluentValidationClientsideAdapters(); // İstersen

//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.InvalidModelStateResponseFactory = context =>
//    {
//        var errors = context.ModelState
//            .Where(e => e.Value?.Errors.Any() == true)
//            .ToDictionary(
//                kvp => kvp.Key,
//                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
//            );

//        return new BadRequestObjectResult(new
//        {
//            message = "Doğrulama hatası oluştu.",
//            errors
//        });
//    };

//});
#endregion


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // React Native'in erişebilmesi için
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
 
}


app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.GetLevel = (httpContext, elapsed, ex) =>
    {
        return httpContext.Response.StatusCode >= 500 ? LogEventLevel.Error :
               httpContext.Response.StatusCode >= 400 ? LogEventLevel.Warning :
               LogEventLevel.Information;
    };
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
        diagnosticContext.Set("UserId", httpContext.User?.Identity?.Name);
    };
    
});

app.UseHealthChecks("/health");
app.UseHealthChecksUI(config =>
{
    config.UIPath = "/health-ui";
});

app.UseCors("AllowAll");

app.UseCustomMiddlewares();
app.UseSwagger();
app.UseSwaggerUI();

#region seeder 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Nakliye360DbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

    await DbSeeder.SeedAsync(context, userManager, roleManager);
}
#endregion

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
