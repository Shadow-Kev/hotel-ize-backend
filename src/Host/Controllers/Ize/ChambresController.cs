using FSH.WebApi.Application.Ize.Chambres;

namespace FSH.WebApi.Host.Controllers.Ize;
public class ChambresController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Chambres)]
    [OpenApiOperation("Recherche des Chambres avec filtre", "")]
    public Task<PaginationResponse<ChambreDto>> SearchAsync(SearchChambresRequest request)
    {
        return Mediator.Send(request);
    }

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
    public Task<ChambreDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetChambreRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Chambres)]
    [OpenApiOperation("Get tous les Chambres ", "")]
    public Task<List<ChambreDetailsDto>> GetAllAsync()
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

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Chambres)]
    [OpenApiOperation("Supprimer un Chambre", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteChambreRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Chambres)]
    [OpenApiOperation("Export les chambres", "")]
    public async Task<FileResult> ExportAsync(ExportChambresRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ChambreExports");
    }
}
