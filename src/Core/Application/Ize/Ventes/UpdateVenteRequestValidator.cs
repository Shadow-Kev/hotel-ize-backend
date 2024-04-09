using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class UpdateVenteRequestValidator : CustomValidator<CreateVenteRequest>
{
    public UpdateVenteRequestValidator(IReadRepository<Vente> VenteRepo, IStringLocalizer<CreateVenteRequestValidator> T)
    {



    }
}
