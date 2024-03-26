using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreByIdWithTypeChambreSpec : Specification<Chambre, ChambreDetailsDto>, ISingleResultSpecification
{
    public ChambreByIdWithTypeChambreSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .Include(c => c.TypeChambre);
}
