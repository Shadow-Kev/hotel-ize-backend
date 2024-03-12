using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreBySearchRequestSpec : EntitiesByPaginationFilterSpec<Chambre, ChambreDto>
{
    public ChambreBySearchRequestSpec(ChambreBySearchRequest request)
        : base(request)
        => Query.OrderBy(tc => tc.Nom, !request.HasOrderBy());
}
