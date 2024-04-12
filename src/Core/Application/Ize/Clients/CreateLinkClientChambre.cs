namespace FSH.WebApi.Application.Ize.Clients;
public class CreateLinkClientChambre : IRequest<Guid>
{
    public string ClientId { get; set; }
    public string ChambreId { get; set; }

}
