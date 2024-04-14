using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class TypeReservationBySearchRequest : PaginationFilter, IRequest<PaginationResponse<TypeReservationDto>>
{
}

public class TypeReservationsBySearchRequestHandler : IRequestHandler<TypeReservationBySearchRequest, PaginationResponse<TypeReservationDto>>
{
    private readonly IReadRepository<TypeReservation> _repository;
    public TypeReservationsBySearchRequestHandler(IReadRepository<TypeReservation> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResponse<TypeReservationDto>> Handle(TypeReservationBySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TypeReservationBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}