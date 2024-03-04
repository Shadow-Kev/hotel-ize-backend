using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class TypeChambreByLibelleSpec : Specification<TypeChambre>, ISingleResultSpecification
{
    public TypeChambreByLibelleSpec(string libelle) =>
        Query.Where(tc => tc.Libelle == libelle);
}
