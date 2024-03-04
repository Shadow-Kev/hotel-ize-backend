using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class CreateTypeChambreRequest : IRequest<Guid>
{
    public string Code { get; set; } = default!;
    public string Libelle { get; set; } = default!;
}

public class CreateTypeChambreRequestHandler : IRequestHandler<CreateTypeChambreRequest, Guid>
{
    private readonly IRepository<TypeChambre> _repository;

    public CreateTypeChambreRequestHandler(IRepository<TypeChambre> repository)
    {
        _repository = repository;
    }



    public async Task<Guid> Handle(CreateTypeChambreRequest request, CancellationToken cancellationToken)
    {
        var typeChambre = new TypeChambre(request.Code, request.Libelle);
        typeChambre.DomainEvents.Add(EntityCreatedEvent.WithEntity(typeChambre));
        await _repository.AddAsync(typeChambre, cancellationToken);
        return typeChambre.Id;
    }
}