namespace FSH.WebApi.Application.Ize.TypeReservations;
public class TypeReservationDto : IDto
{
    public Guid Id { get; set; }
    public string Libelle { get; set; } = default!;
}
