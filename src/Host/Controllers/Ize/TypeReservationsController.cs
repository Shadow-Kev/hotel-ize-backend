using FSH.WebApi.Application.Ize.TypeReservations;

namespace FSH.WebApi.Host.Controllers.Ize;
public class TypeReservationsController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.TypeReservations)]
    [OpenApiOperation("Creer un type creservation", "")]
    public Task<Guid> CreateAsync(CreateTypeReservationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.TypeReservations)]
    [OpenApiOperation("Get Type creservation ", "")]
    public Task<TypeReservationDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTypeReservationRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.TypeReservations)]
    [OpenApiOperation("Get tous les Types creservation ", "")]
    public Task<List<TypeReservationDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllTypeReservationRequest());
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.TypeReservations)]
    [OpenApiOperation("Mettre à jour un type creservation.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTypeReservationRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.TypeReservations)]
    [OpenApiOperation("Recherche de type creservation avec filtre", "")]
    public Task<PaginationResponse<TypeReservationDto>> SearchAsync(TypeReservationBySearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.TypeReservations)]
    [OpenApiOperation("Supprimer un type creservation", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTypeReservationRequest(id));
    }
}