using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class UpdateClientRequestValidator : CustomValidator<UpdateClientRequest>
{
    public UpdateClientRequestValidator(IReadRepository<Client> ClientRepo, IStringLocalizer<UpdateClientRequestValidator> T)
    {

    }
}
