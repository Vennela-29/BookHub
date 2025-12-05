using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using LibraryManagement;
using LibraryManagement.Mapping;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryManagement.Repository;
using LibraryManagement.Repositories;
//using PracticeApp.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<LibraryDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",policy => policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});
// Learn more about configuriwagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description="JWT Authorization using Bearer Scheme.Enter Bearer [SPACE] add your token in the text input." +
        "Example : Bearer 7@#$%^OIJH45679*&^@#$%^ ",
        Name="Authorization",
        In=ParameterLocation.Header,
        Scheme="Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Id="Bearer",
                Type=ReferenceType.SecurityScheme
            },
            Scheme="Bearer",
            Name="Bearer",
            In=ParameterLocation.Header
        },
        new List<string>()
        }
    });

});
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));
string Issuer = builder.Configuration.GetValue<string>("Issuer");
string Audience = builder.Configuration.GetValue<string>("Audience");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer=true,
        ValidIssuer=Issuer,
        ValidateAudience=true,
        ValidAudience=Audience
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management");
        c.RoutePrefix = "";
    });
}
app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("api/testpoint2", async context =>
{
    await context.Response.WriteAsync(builder.Configuration.GetValue<string>("JWTSecret"));
});

app.MapControllers();

app.Run();
