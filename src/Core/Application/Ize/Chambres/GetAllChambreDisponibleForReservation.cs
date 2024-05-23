using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class GetAvailableChambreRequest : IRequest<List<ChambreDetailsDto>>
{
    public DateTime DateReservation { get; set; }

    public GetAvailableChambreRequest()
    {
    }
}

public class GetAvailableChambreSpec : Specification<Chambre, ChambreDetailsDto>
{
    public GetAvailableChambreSpec(DateTime dateDisponibilite)
    {
        Query.Include(c => c.TypeChambre)
             .Include(c => c.Clients)
             .Where(c => c.Clients.All(client => client.DateDepart == null || client.DateDepart < dateDisponibilite) || c.Disponible == true);
    }
}

public class GetAvailableChambreRequestHandler : IRequestHandler<GetAvailableChambreRequest, List<ChambreDetailsDto>>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _t;

    public GetAvailableChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<GetAvailableChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<ChambreDetailsDto>> Handle(GetAvailableChambreRequest request, CancellationToken cancellationToken)
    {
        var spec = new GetAvailableChambreSpec(request.DateReservation);
        var chambresDisponibles = await _repository.ListAsync<ChambreDetailsDto>(spec, cancellationToken);

        if (chambresDisponibles == null || !chambresDisponibles.Any())
        {
            throw new NotFoundException(_t["Aucune chambre disponible trouvée à partir de cette date."]);
        }

        return chambresDisponibles;
    }
}
