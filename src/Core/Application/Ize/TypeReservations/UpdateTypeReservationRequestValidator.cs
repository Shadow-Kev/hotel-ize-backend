using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class UpdateTypeReservationRequestValidator : CustomValidator<UpdateTypeReservationRequest>
{
    public UpdateTypeReservationRequestValidator(IReadRepository<TypeReservation> typeReservationRepo, IStringLocalizer<UpdateTypeReservationRequestValidator> T)
    {

    }
}
