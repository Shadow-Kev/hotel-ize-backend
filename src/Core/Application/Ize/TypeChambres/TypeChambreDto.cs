namespace FSH.WebApi.Application.Ize.TypeChambres;

public class TypeChambreDto : IDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Libelle { get; set; } = default!;
}
