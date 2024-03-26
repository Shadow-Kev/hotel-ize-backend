using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambresByTypeChambreSpec : Specification<Chambre>
{
    public ChambresByTypeChambreSpec(Guid typeChambreId) =>
        Query.Where(c => c.TypeChambreId == typeChambreId);
}
