using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
    }


public static class AircraftEndpoints
{
	public static void MapAircraftEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Aircraft", async (DataContext db) =>
        {
            return await db.Aircrafts.ToListAsync();
        })
        .WithName("GetAllAircrafts")
        .Produces<List<Aircraft>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Aircraft/{id}", async (int id, DataContext db) =>
        {
            return await db.Aircrafts.FindAsync(id)
                is Aircraft model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetAircraftById")
        .Produces<Aircraft>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Aircraft/{id}", async (int id, Aircraft aircraft, DataContext db) =>
        {
            var foundModel = await db.Aircrafts.AsNoTracking().Where(con=>con.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(aircraft);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateAircraft")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Aircraft/", async (Aircraft aircraft, DataContext db) =>
        {
            db.Aircrafts.Add(aircraft);
            await db.SaveChangesAsync();
            return Results.Created($"/Aircrafts/{aircraft.id}", aircraft);
        })
        .WithName("CreateAircraft")
        .Produces<Aircraft>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Aircraft/{id}", async (int id, DataContext db) =>
        {
            if (await db.Aircrafts.FindAsync(id) is Aircraft aircraft)
            {
                db.Aircrafts.Remove(aircraft);
                await db.SaveChangesAsync();
                return Results.Ok(aircraft);
            }

            return Results.NotFound();
        })
        .WithName("DeleteAircraft")
        .Produces<Aircraft>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
