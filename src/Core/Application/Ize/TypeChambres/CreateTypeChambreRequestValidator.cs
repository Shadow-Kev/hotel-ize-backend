using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class CreateTypeChambreRequestValidator : CustomValidator<CreateTypeChambreRequest>
{

    

    public CreateTypeChambreRequestValidator(IReadRepository<TypeChambre> typeChambreRepo, IStringLocalizer<CreateTypeChambreRequestValidator> T)
    {
        RuleFor(tc => tc.Code)
            .NotEmpty()
            .MaximumLength(12)
            .MustAsync(async (code, ct) => await typeChambreRepo.FirstOrDefaultAsync(new TypeChambreByCodeSpec(code), ct) is null)
                .WithMessage((_, code) => T["Le type chambre avec le code {0} existe déjà", code]);

        RuleFor(tc => tc.Libelle)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(async (libelle, ct) => await typeChambreRepo.FirstOrDefaultAsync(new TypeChambreByLibelleSpec(libelle), ct) is null)
                .WithMessage((_, libelle) => T["Le type chambre {0} existe déjà", libelle]);
    }
}
