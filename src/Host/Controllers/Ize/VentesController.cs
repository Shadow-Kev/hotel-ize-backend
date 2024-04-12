using FSH.WebApi.Application.Ize.Ventes;

namespace FSH.WebApi.Host.Controllers.Ize;
public class VentesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Ventes)]
    [OpenApiOperation("Recherche des ventes avec filtre", "")]
    public Task<PaginationResponse<VenteDto>> SearchAsync(SearchVentesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Ventes)]
    [OpenApiOperation("Creer une Vente", "")]
    public Task<Guid> CreateAsync(CreateVenteRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Ventes)]
    [OpenApiOperation("Mise à jour d'une vente", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVenteRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Ventes)]
    [OpenApiOperation("Get Vente ", "")]
    public Task<VenteDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetVenteRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Ventes)]
    [OpenApiOperation("Get tous les Ventes ", "")]
    public Task<List<VenteDetailsDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllVentesRequest());
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Ventes)]
    [OpenApiOperation("Supprimer une vente", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVenteRequest(id));
    }
}
