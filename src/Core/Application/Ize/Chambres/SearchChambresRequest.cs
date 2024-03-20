using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class SearchChambresRequest : PaginationFilter, IRequest<PaginationResponse<ChambreDto>>
{
    public Guid? TypeChambreId { get; set; }
    public int? Capacite { get; set; }
    public decimal? Prix { get; set; }
}

public class SearchChambresRequestHandler : IRequestHandler<SearchChambresRequest, PaginationResponse<ChambreDto>>
{
    private readonly IReadRepository<Chambre> _repository;
    private readonly IReadRepository<TypeChambre> _typeChambreRepository;

    public SearchChambresRequestHandler(IReadRepository<Chambre> repository, IReadRepository<TypeChambre> typeChambreRepository) => (_repository, _typeChambreRepository) = (repository, typeChambreRepository);

    public async Task<PaginationResponse<ChambreDto>> Handle(SearchChambresRequest request, CancellationToken cancellationToken)
    {
        var spec = new ChambreBySearchRequestWithTypeChambresSpec(request);
        var chambres = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        var chambresDto = new List<ChambreDto>();

        foreach (var c in chambres.Data)
        {
            var typeChambre = await _typeChambreRepository.GetByIdAsync(c.TypeChambreId);
            string typeChambreNom = typeChambre != null ? typeChambre.Libelle : "N/A";

            chambresDto.Add(new ChambreDto
            {
                Id = c.Id,
                Nom = c.Nom,
                Capacite = c.Capacite,
                Prix = c.Prix,
                ImagePath = c.ImagePath,
                Disponible = c.Disponible,
                Climatisee = c.Climatisee,
                PetitDejeunerInclus = c.PetitDejeunerInclus,
                TypeChambreId = c.TypeChambreId,
                TypeChambreNom = typeChambreNom,
            });
        }

        return new PaginationResponse<ChambreDto>(chambresDto, chambresDto.Count, chambres.CurrentPage, chambres.PageSize)
        {
            Data = chambresDto,
            CurrentPage = chambres.CurrentPage,
            PageSize = chambres.PageSize,
            TotalPages = (int)Math.Ceiling(chambresDto.Count / (double)chambres.PageSize),
            TotalCount = chambresDto.Count
        };
    }

}
