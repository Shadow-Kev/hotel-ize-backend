using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class TypeReservationByLibelleSpec : Specification<TypeReservation>, ISingleResultSpecification
{
    public TypeReservationByLibelleSpec(string libelle) =>
        Query.Where(tc => tc.Libelle == libelle);
}
