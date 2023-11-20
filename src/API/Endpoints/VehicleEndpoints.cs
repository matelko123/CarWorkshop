using API.Endpoints.Internal;
using Application.Features.Vehicle.Command.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class VehicleEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Vehicles";
    private const string BaseRoute = "vehicles";
    
    
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost(BaseRoute, async (
            [FromBody] CreateVehicleCommand command,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var vehicle = await sender.Send(command, cancellationToken);
            return Results.Ok(vehicle);
        })
            .WithName("CreateVehicle")
            .WithTags(Tag)
            .Accepts<CreateVehicleCommand>(ContentType)
            .Produces<Guid>(StatusCodes.Status200OK);
    }
}