using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class UpdateChambreRequestValidator : CustomValidator<UpdateChambreRequest>
{
    public UpdateChambreRequestValidator(IReadRepository<Chambre> ChambreRepo, IStringLocalizer<UpdateChambreRequestValidator> T)
    {

    }
}
