
using Crud.Controllers;
using Crud.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("CrudContext") ?? throw new InvalidOperationException("Connection string 'CrudContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/*app.UseCors(buider =>
{
    buider
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapAircraftEndpoints(); 
app.MapFlightEndpoints();   
app.MapUserEndpoints();
app.MapContactusEndpoints();
app.MapMedicalRequestEndpoints();
app.MapRequestAirCraftEndpoints();

app.MapCabinCrewEndpoints();

app.MapTechnicianEndpoints();

app.MapCoPilotEndpoints();

app.MapPilotEndpoints();


app.Run();
