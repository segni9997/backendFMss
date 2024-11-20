using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopilotController : ControllerBase
    {
    }


public static class CoPilotEndpoints
{
	public static void MapCoPilotEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/CoPilot", async (DataContext db) =>
        {
            return await db.CoPilot.ToListAsync();
        })
        .WithName("GetAllCoPilots")
        .Produces<List<CoPilot>>(StatusCodes.Status200OK);

        routes.MapGet("/api/CoPilot/{id}", async (int id, DataContext db) =>
        {
            return await db.CoPilot.FindAsync(id)
                is CoPilot model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCoPilotById")
        .Produces<CoPilot>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/CoPilot/{id}", async (int id, CoPilot coPilot, DataContext db) =>
        {
            var foundModel = await db.CoPilot.AsNoTracking().Where(c => c.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(coPilot);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCoPilot")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/CoPilot/", async (CoPilot coPilot, DataContext db) =>
        {
            db.CoPilot.Add(coPilot);
            await db.SaveChangesAsync();
            return Results.Created($"/CoPilots/{coPilot.id}", coPilot);
        })
        .WithName("CreateCoPilot")
        .Produces<CoPilot>(StatusCodes.Status201Created);


        routes.MapDelete("/api/CoPilot/{id}", async (int id, DataContext db) =>
        {
            if (await db.CoPilot.FindAsync(id) is CoPilot coPilot)
            {
                db.CoPilot.Remove(coPilot);
                await db.SaveChangesAsync();
                return Results.Ok(coPilot);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCoPilot")
        .Produces<CoPilot>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
