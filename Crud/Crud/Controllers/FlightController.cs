using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
    }


public static class FlightEndpoints
{
	public static void MapFlightEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Flight", async (DataContext db) =>
        {
            return await db.Flights.ToListAsync();
        })
        .WithName("GetAllFlights")
        .Produces<List<Flight>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Flight/{id}", async (int id, DataContext db) =>
        {
            return await db.Flights.FindAsync(id)
                is Flight model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetFlightById")
        .Produces<Flight>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Flight/{id}", async (int id, Flight flight, DataContext db) =>
        {
            var foundModel = await db.Flights.AsNoTracking().Where(c => c.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(flight);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateFlight")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Flight/", async (Flight flight, DataContext db) =>
        {
            db.Flights.Add(flight);
            await db.SaveChangesAsync();
            return Results.Created($"/Flights/{flight.id}", flight);
        })
        .WithName("CreateFlight")
        .Produces<Flight>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Flight/{id}", async (int id, DataContext db) =>
        {
            if (await db.Flights.FindAsync(id) is Flight flight)
            {
                db.Flights.Remove(flight);
                await db.SaveChangesAsync();
                return Results.Ok(flight);
            }

            return Results.NotFound();
        })
        .WithName("DeleteFlight")
        .Produces<Flight>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
