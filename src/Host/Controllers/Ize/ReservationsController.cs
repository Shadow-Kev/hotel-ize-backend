using FSH.WebApi.Application.Ize.Reservations;

namespace FSH.WebApi.Host.Controllers.Ize;
public class ReservationsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Reservations)]
    [OpenApiOperation("Recherche de reservation avec les filtres disponible", "")]
    public Task<PaginationResponse<ReservationDto>> SearchAsync(SearchReservationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Reservations)]
    [OpenApiOperation("Creer un Reservation", "")]
    public Task<Guid> CreateAsync(CreateReservationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Reservations)]
    [OpenApiOperation("Mise à jour d'une reservation", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateReservationRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Reservations)]
    [OpenApiOperation("Get Reservation ", "")]
    public Task<ReservationDetailDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetReservationRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Reservations)]
    [OpenApiOperation("Get tous les Reservations ", "")]
    public Task<List<ReservationDetailDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllReservationRequest());
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Reservations)]
    [OpenApiOperation("Supprimer un Reservation", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteReservationRequest(id));
    }

}
