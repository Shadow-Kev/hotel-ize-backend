using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Ize.Chambres;
using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class ChambreService : IChambreService
{
    private readonly IMediator _mediator;
    public ChambreService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CheckAndUpdateChambreStatus()
    {
        var reservations = await _mediator.Send(new GetAllReservationRequest());
        var currentDate = DateTime.UtcNow.Date; 

        foreach (var reservation in reservations)
        {
            if (reservation.StatutReservation == Statut.RESERVE && reservation.DateArrive == currentDate)
            {
                await _mediator.Send(new UpdateChambreStatutRequest
                {
                    Id = reservation.Chambre.Id,
                    Disponible = false,
                });
            }

            if (reservation.StatutReservation == Statut.RESERVE && reservation.DateArrive < currentDate)
            {
                continue;
            }
        }
    }
}
