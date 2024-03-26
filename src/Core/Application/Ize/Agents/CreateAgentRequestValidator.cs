using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class CreateAgentRequestValidator : CustomValidator<CreateAgentRequest>
{
    public CreateAgentRequestValidator(IReadRepository<Agent> agentRepo, IStringLocalizer<CreateAgentRequestValidator> T)
    {
        RuleFor(a => a.UserCode)
            .NotEmpty()
            .MustAsync(async (userCode, ct) => await agentRepo.FirstOrDefaultAsync(new AgentByUserCodeSpec(userCode), ct) is null)
                .WithMessage((_, userCode) => T["Le user code {0} existe déjà", userCode]);

        //RuleFor(a => new { a.Prenoms, a.Nom })
        //    .MustAsync(async (agent, ct) => await agentRepo.FirstOrDefaultAsync(new AgentByPrenomsNomSpec(agent.Prenoms, agent.Nom), ct) is null)
        //    .WithMessage(agent => $"L'agent avec le prénom '{agent.Prenoms}' et le nom '{agent.Nom}' existe déjà");
    }

}
