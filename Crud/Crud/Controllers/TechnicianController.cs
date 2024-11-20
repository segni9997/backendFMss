using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
    }


public static class TechnicianEndpoints
{
	public static void MapTechnicianEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Technician", async (DataContext db) =>
        {
            return await db.Technician.ToListAsync();
        })
        .WithName("GetAllTechnicians")
        .Produces<List<Technician>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Technician/{id}", async (int Id, DataContext db) =>
        {
            return await db.Technician.FindAsync(Id)
                is Technician model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetTechnicianById")
        .Produces<Technician>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Technician/{id}", async (int Id, Technician technician, DataContext db) =>
        {
            var foundModel = await db.Technician.AsNoTracking().Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(technician);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateTechnician")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Technician/", async (Technician technician, DataContext db) =>
        {
            db.Technician.Add(technician);
            await db.SaveChangesAsync();
            return Results.Created($"/Technicians/{technician.Id}", technician);
        })
        .WithName("CreateTechnician")
        .Produces<Technician>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Technician/{id}", async (int Id, DataContext db) =>
        {
            if (await db.Technician.FindAsync(Id) is Technician technician)
            {
                db.Technician.Remove(technician);
                await db.SaveChangesAsync();
                return Results.Ok(technician);
            }

            return Results.NotFound();
        })
        .WithName("DeleteTechnician")
        .Produces<Technician>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
