using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class UpdateChambreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public int Capacite { get; set; }
    public decimal Prix { get; set; }
    public bool Disponible { get; set; }
    public bool Climatisee { get; set; }
    public bool PetitDejeunerInclus { get; set; }
    public Guid TypeChambreId { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
    public FileUploadRequest? Image { get; set; }
}

public class UpdateChambreRequestHandler : IRequestHandler<UpdateChambreRequest, Guid>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IFileStorageService _file;
    private readonly IStringLocalizer _localizer;

    public UpdateChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<UpdateChambreRequestHandler> localizer, IFileStorageService file)
    {
        _repository = repository;
        _file = file;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateChambreRequest request, CancellationToken cancellationToken)
    {
        var chambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = chambre ?? throw new NotFoundException(_localizer["Chambre {0} non trouvé", request.Id]);

        if (request.DeleteCurrentImage)
        {
            string? currentChambreImagePath = chambre.ImagePath;
            if (!string.IsNullOrEmpty(currentChambreImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentChambreImagePath));
            }

            chambre = chambre.ClearImagePath();
        }

        string? chambreImagePath = request.Image is not null
            ? await _file.UploadAsync<Chambre>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedChambre = chambre.Update(request.Nom, request.Capacite, request.Prix, chambreImagePath, request.Disponible, request.Climatisee, request.PetitDejeunerInclus, request.TypeChambreId);
        chambre.DomainEvents.Add(EntityUpdatedEvent.WithEntity(chambre));
        await _repository.UpdateAsync(updatedChambre, cancellationToken);

        return request.Id;
    }
}
