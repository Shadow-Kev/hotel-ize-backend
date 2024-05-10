namespace FSH.WebApi.Application.Common.Interfaces;
public interface IPdfService : ITransientService
{
    public Task<string> GenerateBarInvoice(Guid id);
}
