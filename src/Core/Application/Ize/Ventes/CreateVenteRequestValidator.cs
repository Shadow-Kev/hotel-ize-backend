using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class CreateVenteRequestValidator : CustomValidator<CreateVenteRequest>
{
    public CreateVenteRequestValidator(IReadRepository<Vente> VenteRepo, IStringLocalizer<CreateVenteRequestValidator> T)
    {

    }
}
