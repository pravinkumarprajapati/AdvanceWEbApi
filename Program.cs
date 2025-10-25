using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdvanceWebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdvanceWebApi.Model;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AdvanceWebApiContextConnection") ?? throw new InvalidOperationException("Connection string 'AdvanceWebApiContextConnection' not found.");

builder.Services.AddIdentity<RegisterModel, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AdvanceWebApiContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AdvanceWebApiContext>(options => options.UseSqlite(connectionString));

//builder.Services.AddDefaultIdentity<RegisterModel>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AdvanceWebApiContext>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ReportApiVersions = true;
})
 .AddApiExplorer(options =>
 {
     options.GroupNameFormat = "'v'VVV";
     options.SubstituteApiVersionInUrl = true;
 });
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345ThisIsAVeryLongSecretKeyForJWTTokenGeneration123456789"));
        options.SaveToken = true;
        options.Authority = "https://demo.identityserver.io/";
        options.Audience = "api";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();


app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AdvanceWEbApi v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
