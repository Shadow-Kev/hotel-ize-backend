namespace FSH.WebApi.Application.Ize.Agents;
public class AgentDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserCode { get; set; }
    public string? Prenoms { get;  set; }
    public string? Nom { get;  set; }
    public bool IsActive { get;  set; }
}
