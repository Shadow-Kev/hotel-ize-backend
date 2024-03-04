using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;
public class TypeChambresBySearchRequestSpec : EntitiesByPaginationFilterSpec<TypeChambre, TypeChambreDto>
{
    public TypeChambresBySearchRequestSpec(TypeChambresBySearchRequest request)
        : base(request)
        => Query.OrderBy(tc => tc.Libelle, !request.HasOrderBy());
}
