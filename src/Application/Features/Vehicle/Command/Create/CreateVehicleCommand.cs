using Application.Interfaces.Repositories;
using Mapster;
using MediatR;
using Shared.Wrapper;

namespace Application.Features.Vehicle.Command.Create;

public partial class CreateVehicleCommand : IRequest<Result<Guid>>
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string RegistrationNumber { get; set; }
}

internal class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateVehicleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(
        CreateVehicleCommand request, 
        CancellationToken cancellationToken)
    {
        var vehicle = request.Adapt<Domain.Entity.Vehicle>();
        await _unitOfWork.Repository<Domain.Entity.Vehicle>().AddAsync(vehicle, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result<Guid>.Success(vehicle.Id);
    }
}