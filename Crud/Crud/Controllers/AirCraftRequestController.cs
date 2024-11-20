using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftRequestController : ControllerBase
    {
    }


public static class RequestAirCraftEndpoints
{
	public static void MapRequestAirCraftEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/RequestAirCraft", async (DataContext db) =>
        {
            return await db.RequestAirCraft.ToListAsync();
        })
        .WithName("GetAllRequestAirCrafts")
        .Produces<List<RequestAirCraft>>(StatusCodes.Status200OK);

        routes.MapGet("/api/RequestAirCraft/{id}", async (int Req_Id, DataContext db) =>
        {
            return await db.RequestAirCraft.FindAsync(Req_Id)
                is RequestAirCraft model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetRequestAirCraftById")
        .Produces<RequestAirCraft>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/RequestAirCraft/{id}", async (int id, RequestAirCraft requestAirCraft, DataContext db) =>
        {
            var foundModel = await db.RequestAirCraft.AsNoTracking().Where(c => c.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(requestAirCraft);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateRequestAirCraft")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/RequestAirCraft/", async (RequestAirCraft requestAirCraft, DataContext db) =>
        {
            db.RequestAirCraft.Add(requestAirCraft);
            await db.SaveChangesAsync();
            return Results.Created($"/RequestAirCrafts/{requestAirCraft.id}", requestAirCraft);
        })
        .WithName("CreateRequestAirCraft")
        .Produces<RequestAirCraft>(StatusCodes.Status201Created);


        routes.MapDelete("/api/RequestAirCraft/{id}", async (int id, DataContext db) =>
        {
            if (await db.RequestAirCraft.FindAsync(id) is RequestAirCraft requestAirCraft)
            {
                db.RequestAirCraft.Remove(requestAirCraft);
                await db.SaveChangesAsync();
                return Results.Ok(requestAirCraft);
            }

            return Results.NotFound();
        })
        .WithName("DeleteRequestAirCraft")
        .Produces<RequestAirCraft>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
