using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Application.Ize.Clients;
public class UpdateClientRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public string? Prenom { get; set; }
    public string? NomDeJeuneFille { get; set; }
    public string? LieuDeNaissance { get; set; }
    public string? Nationalite { get; set; }
    public string? Profession { get; set; }
    public string? Domicile { get; set; }
    public string? MotifDuVoyage { get; set; }
    public string? VenantDe { get; set; }
    public string? AllantA { get; set; }
    public DateTime? DateArrive { get; set; }
    public DateTime? DateDepart { get; set; }
    public string? Identite { get; set; }
    public DateTime? DateIdentiteDelivreeLe { get; set; }
    [DataType(DataType.PhoneNumber, ErrorMessage = "Veuillez saisir un numéro de téléphone valide")]
    [RegularExpression(@"^[9,7,2][0-9]{7}$", ErrorMessage = "Veuillez saisir un numéro de téléphone valide")]
    public string? Contact { get; set; }
    public string? Email { get; set; }
    public string? PersonneAPrevenir { get; set; }
    public Guid AgentId { get; set; }
    public Guid? ChambreId { get; set; }
}

public class UpdateClientRequestHandler : IRequestHandler<UpdateClientRequest, Guid>
{
    private readonly IRepository<Client> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateClientRequestHandler(IRepository<Client> repository, IStringLocalizer<UpdateClientRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }
    public async Task<Guid> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = client ?? throw new NotFoundException(_localizer["Client {0} non trouvé", request.Id]);
        var updatedClient = client.Update(request.Nom, request.Prenom, request.NomDeJeuneFille, request.LieuDeNaissance, request.Nationalite,
            request.Profession, request.Domicile, request.MotifDuVoyage, request.VenantDe, request.AllantA, request.DateArrive,
            request.DateDepart, request.Identite, request.DateIdentiteDelivreeLe, request.Contact, request.Email, request.PersonneAPrevenir,
            request.AgentId, request.ChambreId);
        client.DomainEvents.Add(EntityUpdatedEvent.WithEntity(client));
        await _repository.UpdateAsync(updatedClient, cancellationToken);

        return request.Id;
    }
}