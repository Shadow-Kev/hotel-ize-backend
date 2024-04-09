using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;
using System.ComponentModel.DataAnnotations;

namespace FSH.WebApi.Application.Ize.Clients;
public class CreateClientRequest : IRequest<Guid>
{
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

public class CreateClientRequestHandler : IRequestHandler<CreateClientRequest, Guid>
{
    private readonly IRepository<Client> _repository;

    public CreateClientRequestHandler(IRepository<Client> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var client = new Client(request.Nom, request.Prenom, request.NomDeJeuneFille, request.LieuDeNaissance, request.Nationalite,
            request.Profession, request.Domicile, request.MotifDuVoyage, request.VenantDe, request.AllantA, request.DateArrive,
            request.DateDepart, request.Identite, request.DateIdentiteDelivreeLe, request.Contact, request.Email, request.PersonneAPrevenir,
            request.AgentId, request.ChambreId);
        client.DomainEvents.Add(EntityCreatedEvent.WithEntity(client));
        await _repository.AddAsync(client, cancellationToken);
        return client.Id;
    }
}