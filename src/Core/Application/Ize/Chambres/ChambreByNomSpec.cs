using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreByNomSpec : Specification<Chambre>, ISingleResultSpecification
{
    public ChambreByNomSpec(string nom) =>
        Query.Where(c => c.Nom == nom);

}
