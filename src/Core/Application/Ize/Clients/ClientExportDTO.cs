namespace FSH.WebApi.Application.Ize.Clients;
public class ClientExportDTO
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
    public string? Contact { get; set; }
    public string? Email { get; set; }
    public string? PersonneAPrevenir { get; set; }
    public string AgentNom { get; set; } = default!;
    public string? ChambreNom { get; set; }
}
