using FSH.WebApi.Application.Ize.TypeChambres;

namespace FSH.WebApi.Host.Controllers.Ize;

public class TypeChambresController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.TypeChambres)]
    [OpenApiOperation("Creer un type chambre", "")]
    public Task<Guid> CreateAsync(CreateTypeChambreRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.TypeChambres)]
    [OpenApiOperation("Get Type chambre ", "")]
    public Task<TypeChambreDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTypeChambreRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.TypeChambres)]
    [OpenApiOperation("Get tous les Types chambre ", "")]
    public Task<List<TypeChambreDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllTypeChambreRequest());
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.TypeChambres)]
    [OpenApiOperation("Mettre à jour un type chambre.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTypeChambreRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.TypeChambres)]
    [OpenApiOperation("Recherche de type chambre avec filtre", "")]
    public Task<PaginationResponse<TypeChambreDto>> SearchAsync(TypeChambresBySearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.TypeChambres)]
    [OpenApiOperation("Supprimer un type chambre", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTypeChambreRequest(id));
    }
}
