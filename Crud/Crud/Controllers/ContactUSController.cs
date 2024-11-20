using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUSController : ControllerBase
    {
    }


public static class ContactusEndpoints
{
	public static void MapContactusEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Contactus", async (DataContext db) =>
        {
            return await db.Contactus.ToListAsync();
        })
        .WithName("GetAllContactuss")
        .Produces<List<Contactus>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Contactus/{id}", async (int id, DataContext db) =>
        {
            return await db.Contactus.FindAsync(id)
                is Contactus model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetContactusById")
        .Produces<Contactus>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Contactus/{id}", async (int id, Contactus contactus, DataContext db) =>
        {
            var foundModel = await db.Contactus.AsNoTracking().Where(c => c.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(contactus);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateContactus")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Contactus/", async (Contactus contactus, DataContext db) =>
        {
            db.Contactus.Add(contactus);
            await db.SaveChangesAsync();
            return Results.Created($"/Contactuss/{contactus.id}", contactus);
        })
        .WithName("CreateContactus")
        .Produces<Contactus>(StatusCodes.Status201Created);


        routes.MapDelete("/api/Contactus/{id}", async (int id, DataContext db) =>
        {
            if (await db.Contactus.FindAsync(id) is Contactus contactus)
            {
                db.Contactus.Remove(contactus);
                await db.SaveChangesAsync();
                return Results.Ok(contactus);
            }

            return Results.NotFound();
        })
        .WithName("DeleteContactus")
        .Produces<Contactus>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
