using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class CreateChambreRequest : IRequest<Guid>
{
    public string Nom { get; set; } = default!;
    public int Capacite { get; set; }
    public decimal Prix { get; set; }
    public bool Disponible { get; set; }
    public bool Climatisee { get; set; }
    public bool PetitDejeunerInclus { get; set; }
    public Guid TypeChambreId { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateChambreRequestHandler : IRequestHandler<CreateChambreRequest, Guid>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IFileStorageService _file;

    public CreateChambreRequestHandler(IRepository<Chambre> repository, IFileStorageService file)
    {
        _repository = repository;
        _file = file;
    }

    public async Task<Guid> Handle(CreateChambreRequest request, CancellationToken cancellationToken)
    {
        string chambreImagePath = await _file.UploadAsync<Chambre>(request.Image, FileType.Image, cancellationToken);

        var chambre = new Chambre(request.Nom, request.Capacite, request.Prix, chambreImagePath, request.Disponible, request.Climatisee, request.PetitDejeunerInclus, request.TypeChambreId);

        chambre.DomainEvents.Add(EntityCreatedEvent.WithEntity(chambre));

        await _repository.AddAsync(chambre, cancellationToken);
        return chambre.Id;
    }
}
