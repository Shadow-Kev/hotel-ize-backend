using FSH.WebApi.Application.Ize.Chambres;

namespace FSH.WebApi.Host.Controllers.Ize;
public class ChambresController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Chambres)]
    [OpenApiOperation("Creer un Chambre", "")]
    public Task<Guid> CreateAsync(CreateChambreRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Chambres)]
    [OpenApiOperation("Get Chambre ", "")]
    public Task<ChambreDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetChambreRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Chambres)]
    [OpenApiOperation("Get tous les Chambres ", "")]
    public Task<List<ChambreDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllChambreRequest());
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Chambres)]
    [OpenApiOperation("Mettre à jour un Chambre.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateChambreRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Chambres)]
    [OpenApiOperation("Recherche d' Chambre avec filtre", "")]
    public Task<PaginationResponse<ChambreDto>> SearchAsync(ChambreBySearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Chambres)]
    [OpenApiOperation("Supprimer un Chambre", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteChambreRequest(id));
    }
}
