namespace FSH.WebApi.Application.Common.Interfaces;
public interface IPdfService : ITransientService
{
    public Task<Guid> GenerateBarInvoice(Guid id);
}
