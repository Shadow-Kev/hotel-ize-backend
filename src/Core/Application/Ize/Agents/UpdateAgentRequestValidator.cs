using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class UpdateAgentRequestValidator : CustomValidator<UpdateAgentRequest>
{
    public UpdateAgentRequestValidator(IReadRepository<Agent> agentRepo, IStringLocalizer<UpdateAgentRequestValidator> T)
    {

    }
}