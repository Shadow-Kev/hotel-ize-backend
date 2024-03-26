using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class CreateChambreRequestValidator : CustomValidator<CreateChambreRequest>
{
    public CreateChambreRequestValidator(IReadRepository<Chambre> ChambreRepo, IStringLocalizer<CreateChambreRequestValidator> T)
    {
        RuleFor(c => c.Nom)
            .NotEmpty()
            .MustAsync(async (nom, c) => await ChambreRepo.FirstOrDefaultAsync(new ChambreByNomSpec(nom), c) is null)
                .WithMessage((_, nom) => T["La chambre avec le nom {0} existe déjà", nom]);


    }
}
