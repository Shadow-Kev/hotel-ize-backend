using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class TypeChambreByCodeSpec : Specification<TypeChambre>, ISingleResultSpecification
{
    public TypeChambreByCodeSpec(string code) =>
        Query.Where(tc => tc.Code == code);
}
