using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinController : ControllerBase
    {
    }


public static class CabinCrewEndpoints
{
	public static void MapCabinCrewEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/CabinCrew", async (DataContext db) =>
        {
            return await db.CabinCrew.ToListAsync();
        })
        .WithName("GetAllCabinCrews")
        .Produces<List<CabinCrew>>(StatusCodes.Status200OK);

        routes.MapGet("/api/CabinCrew/{id}", async (int Id, DataContext db) =>
        {
            return await db.CabinCrew.FindAsync(Id)
                is CabinCrew model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetCabinCrewById")
        .Produces<CabinCrew>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/CabinCrew/{id}", async (int Id, CabinCrew cabinCrew, DataContext db) =>
        {
            var foundModel = await db.CabinCrew.AsNoTracking().Where(c => c.Id ==Id ).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(cabinCrew);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCabinCrew")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/CabinCrew/", async (CabinCrew cabinCrew, DataContext db) =>
        {
            db.CabinCrew.Add(cabinCrew);
            await db.SaveChangesAsync();
            return Results.Created($"/CabinCrews/{cabinCrew.Id}", cabinCrew);
        })
        .WithName("CreateCabinCrew")
        .Produces<CabinCrew>(StatusCodes.Status201Created);


        routes.MapDelete("/api/CabinCrew/{id}", async (int Id, DataContext db) =>
        {
            if (await db.CabinCrew.FindAsync(Id) is CabinCrew cabinCrew)
            {
                db.CabinCrew.Remove(cabinCrew);
                await db.SaveChangesAsync();
                return Results.Ok(cabinCrew);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCabinCrew")
        .Produces<CabinCrew>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
