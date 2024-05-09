using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Application.Ize.Clients;

namespace FSH.WebApi.Host.Controllers.Ize;

public class ClientsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Clients)]
    [OpenApiOperation("Search clients avec les filtres disponibles", "")]
    public Task<PaginationResponse<ClientDto>> SearchAsync(SearchClientsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Clients)]
    [OpenApiOperation("Mise à jour d'un client", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateClientRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Clients)]
    [OpenApiOperation("Get tous les clients ", "")]
    public Task<List<ClientDetailsDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllClientRequest());
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Clients)]
    [OpenApiOperation("Get Client ", "")]
    public Task<ClientDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetClientRequest(id));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Clients)]
    [OpenApiOperation("Supprimer un client", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteClientRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Clients)]
    [OpenApiOperation("Creer un client", "")]
    public Task<Guid> CreateAsync(CreateClientRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.Clients)]
    [OpenApiOperation("Export clients", "")]
    public async Task<FileResult> ExportAsync(ExportClientsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ClientExports");
    }
}
