using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
    }


public static class PilotEndpoints
{
	public static void MapPilotEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Pilot", async (DataContext db) =>
        {
            return await db.Pilot.ToListAsync();
        })
        .WithName("GetAllPilots")
        .Produces<List<Pilot>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Pilot/{id}", async (int Id, DataContext db) =>
        {
            return await db.Pilot.FindAsync(Id)
                is Pilot model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetPilotById")
        .Produces<Pilot>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Pilot/{id}", async (int Id, Pilot pilot, DataContext db) =>
        {
            var foundModel = await db.Pilot.AsNoTracking().Where(c => c.Id == Id ).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(pilot);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdatePilot")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Pilot/", async (Pilot pilot, DataContext db) =>
        {
            db.Pilot.Add(pilot);
            await db.SaveChangesAsync();
            return Results.Created($"/Pilots/{pilot.Id}", pilot);
        })
        .WithName("CreatePilot")
        .Produces<Pilot>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Pilot/{id}", async (int Id, DataContext db) =>
        {
            if (await db.Pilot.FindAsync(Id) is Pilot pilot)
            {
                db.Pilot.Remove(pilot);
                await db.SaveChangesAsync();
                return Results.Ok(pilot);
            }

            return Results.NotFound();
        })
        .WithName("DeletePilot")
        .Produces<Pilot>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
