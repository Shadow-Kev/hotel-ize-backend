using FSH.WebApi.Application.Ize.Agents;
using FSH.WebApi.Application.Ize.TypeChambres;

namespace FSH.WebApi.Host.Controllers.Ize;

public class AgentsController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Agents)]
    [OpenApiOperation("Creer un Agent", "")]
    public Task<Guid> CreateAsync(CreateAgentRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Agents)]
    [OpenApiOperation("Get Agent ", "")]
    public Task<AgentDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAgentRequest(id));
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Agents)]
    [OpenApiOperation("Get tous les Agents ", "")]
    public Task<List<AgentDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllAgentRequest());
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Agents)]
    [OpenApiOperation("Mettre à jour un agent.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAgentRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Agents)]
    [OpenApiOperation("Recherche d' agent avec filtre", "")]
    public Task<PaginationResponse<AgentDto>> SearchAsync(AgentBySearchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Agents)]
    [OpenApiOperation("Supprimer un agent", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAgentRequest(id));
    }
}
