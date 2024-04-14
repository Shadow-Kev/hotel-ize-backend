using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreBySearchRequestWithTypeChambresSpec : EntitiesByPaginationFilterSpec<Chambre, ChambreDto>
{
    public ChambreBySearchRequestWithTypeChambresSpec(SearchChambresRequest request)
        : base(request) =>
        Query
            .Include(c => c.TypeChambre)
            .Include(c => c.Clients)
            .OrderBy(c => c.Nom, !request.HasOrderBy())
            .Where(c => c.TypeChambreId.Equals(request.TypeChambreId!.Value), request.TypeChambreId.HasValue);
}
