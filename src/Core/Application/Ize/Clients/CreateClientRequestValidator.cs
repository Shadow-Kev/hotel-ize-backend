using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class CreateClientRequestValidator : CustomValidator<CreateClientRequest>
{
    public CreateClientRequestValidator(IReadRepository<Client> ClientRepo, IStringLocalizer<CreateClientRequestValidator> T)
    {
        
    }
}
