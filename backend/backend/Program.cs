using backend.Data;
using backend.Helper;
using backend.Interfaces;
using backend.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
     });
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});
builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBrearer =>
{
    var SKey = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var Keybytes = Encoding.ASCII.GetBytes(SKey);
    JwtBrearer.SaveToken = true;
    JwtBrearer.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Keybytes),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,

    };
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<ICabinRepo, CabinRepo>();
builder.Services.AddTransient<IAirCraftRepo, AirCraftRepo>();
builder.Services.AddTransient<IAirCraftRequestRepo, AirCraftRequestRepo>();
builder.Services.AddTransient<IPilotRepo, PilotRepo>();
builder.Services.AddTransient<IFlightRepo, FlightRepo>();
builder.Services.AddTransient<ITechnicianRepo, TechncianRepo>();
builder.Services.AddTransient<IMedicalRequestRepo, MedicalRequestRepo>();
builder.Services.AddTransient<ICopilotRepo, CopilotrRepo>();
builder.Services.AddTransient<IContactUs, ContactusRepo>();
builder.Services.AddTransient<IAdmin, AdminRepo>();
builder.Services.AddTransient<ITreasury, TreasuryRepo>();
builder.Services.AddTransient<IRole, RoleRepo>();
builder.Services.AddTransient<ICabinGroup,CabingroupRepo>();
builder.Services.AddTransient<ITeckGroup, TeckgroupRepo>();
builder.Services.AddScoped<JwtService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();



    app.UseHttpsRedirection();
    app.UseCors(MyAllowSpecificOrigins);
    app.UseAuthentication();
    app.UseCors(options => options.AllowAnyHeader().AllowCredentials().AllowAnyMethod().WithOrigins(new[] { "http://localhost:3000" }));
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
