using FSH.WebApi.Application.Identity.Roles;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IReadRepository<Client> _clientRepo;
    private readonly IReadRepository<Chambre> _chambreRepo;
    private readonly IReadRepository<TypeChambre> _typeChambreRepo;
    private readonly IReadRepository<Reservation> _reservationRepo;
    private readonly IStringLocalizer _t;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Brand> brandRepo, IReadRepository<Product> productRepo, IReadRepository<Client> clientRepo, IReadRepository<Chambre> chambreRepo, IReadRepository<TypeChambre> typeChambreRepo, IReadRepository<Reservation> reservationRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _brandRepo = brandRepo;
        _productRepo = productRepo;
        _clientRepo = clientRepo;
        _chambreRepo = chambreRepo;
        _typeChambreRepo = typeChambreRepo;
        _reservationRepo = reservationRepo;
        _t = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            ClientCount = await _clientRepo.CountAsync(cancellationToken),
            ChambreCount = await _chambreRepo.CountAsync(cancellationToken),
            TypeChambreCount = await _typeChambreRepo.CountAsync(cancellationToken),
            ReservationCount = await _reservationRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.UtcNow.Year;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        double[] clientsFigure = new double[13];
        double[] chambresFigure = new double[13];
        double[] typeChambreFigure = new double[13];
        double[] reservationFigure = new double[13];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);
            var clientSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Client>(filterStartDate, filterEndDate);
            var chambreSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Chambre>(filterStartDate, filterEndDate);
            var typeChambreSpec = new AuditableEntitiesByCreatedOnBetweenSpec<TypeChambre>(filterStartDate, filterEndDate);
            var reservationSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Reservation>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
            clientsFigure[i - 1] = await _clientRepo.CountAsync(clientSpec, cancellationToken);
            chambresFigure[i - 1] = await _chambreRepo.CountAsync(chambreSpec, cancellationToken);
            typeChambreFigure[i - 1] = await _typeChambreRepo.CountAsync(typeChambreSpec, cancellationToken);
            reservationFigure[i - 1] = await _reservationRepo.CountAsync(reservationSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Catégories"], Data = brandsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Clients"], Data = clientsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Chambres"], Data = chambresFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Type Chambres"], Data = typeChambreFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Reservations"], Data = reservationFigure });

        return stats;
    }
}
