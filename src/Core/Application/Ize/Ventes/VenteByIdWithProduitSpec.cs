using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteByIdWithProduitSpec : Specification<Vente, VenteDetailsDto>, ISingleResultSpecification
{
    public VenteByIdWithProduitSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .Include(c => c.Agent);
}
