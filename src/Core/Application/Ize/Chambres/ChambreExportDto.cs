namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreExportDto : IDto
{
    public string Nom { get; set; } = default!;
    public int Capacite { get; set; }
    public decimal Prix { get; set; }
    public bool Disponible { get; set; }
    public bool Climatisee { get; set; }
    public bool PetitDejeunerInclus { get; set; }
    public string TypeChambreNom { get; set; } = default!;
}
