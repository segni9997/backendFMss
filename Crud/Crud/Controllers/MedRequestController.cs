using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Crud.Models;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedRequestController : ControllerBase
    {
    }


public static class MedicalRequestEndpoints
{
	public static void MapMedicalRequestEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/MedicalRequest", async (DataContext db) =>
        {
            return await db.MedicalRequests.ToListAsync();
        })
        .WithName("GetAllMedicalRequests")
        .Produces<List<MedicalRequest>>(StatusCodes.Status200OK);

        routes.MapGet("/api/MedicalRequest/{id}", async (int id, DataContext db) =>
        {
            return await db.MedicalRequests.FindAsync(id)
                is MedicalRequest model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetMedicalRequestById")
        .Produces<MedicalRequest>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/MedicalRequest/{id}", async (int id, MedicalRequest medicalRequest, DataContext db) =>
        {
            var foundModel = await db.MedicalRequests.AsNoTracking().Where(c => c.id == id).FirstOrDefaultAsync();

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(medicalRequest);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateMedicalRequest")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/MedicalRequest/", async (MedicalRequest medicalRequest, DataContext db) =>
        {
            db.MedicalRequests.Add(medicalRequest);
            await db.SaveChangesAsync();
            return Results.Created($"/MedicalRequests/{medicalRequest.id}", medicalRequest);
        })
        .WithName("CreateMedicalRequest")
        .Produces<MedicalRequest>(StatusCodes.Status201Created);


        routes.MapDelete("/api/MedicalRequest/{id}", async (int id, DataContext db) =>
        {
            if (await db.MedicalRequests.FindAsync(id) is MedicalRequest medicalRequest)
            {
                db.MedicalRequests.Remove(medicalRequest);
                await db.SaveChangesAsync();
                return Results.Ok(medicalRequest);
            }

            return Results.NotFound();
        })
        .WithName("DeleteMedicalRequest")
        .Produces<MedicalRequest>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}
