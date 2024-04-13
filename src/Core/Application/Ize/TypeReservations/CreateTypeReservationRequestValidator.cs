using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class CreateTypeReservationRequestValidator : CustomValidator<CreateTypeReservationRequest>
{
    public CreateTypeReservationRequestValidator(IReadRepository<TypeReservation> typeReservationRepo, IStringLocalizer<CreateTypeReservationRequestValidator> T)
    {
        RuleFor(tc => tc.Libelle)
           .NotEmpty()
           .MaximumLength(100)
           .MustAsync(async (libelle, ct) => await typeReservationRepo.FirstOrDefaultAsync(new TypeReservationByLibelleSpec(libelle), ct) is null)
               .WithMessage((_, libelle) => T["Le type reservation {0} existe déjà", libelle]);
    }
}
