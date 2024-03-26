using FSH.WebApi.Application.Ize.Clients;

namespace FSH.WebApi.Host.Controllers.Ize;

public class ClientsController : VersionedApiController
{
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
}
