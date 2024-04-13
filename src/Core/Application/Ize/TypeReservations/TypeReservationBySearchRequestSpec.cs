using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class TypeReservationBySearchRequestSpec : EntitiesByPaginationFilterSpec<TypeReservation, TypeReservationDto>
{
    public TypeReservationBySearchRequestSpec(TypeReservationBySearchRequest request)
        : base(request)
        => Query.OrderBy(tc => tc.Libelle, !request.HasOrderBy());
}
