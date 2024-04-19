using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Ize.Ventes;
using FSH.WebApi.Domain.Ize;
using MediatR;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class PdfServices : IPdfService
{
    private readonly IMediator _mediator;
    private readonly IReadRepository<Vente> _venteRepo;
    private readonly ICurrentUser _currentUser;

    public PdfServices(IMediator mediator, IReadRepository<Vente> venteRepo, ICurrentUser currentUser)
    { 
        _mediator = mediator;
        _venteRepo = venteRepo;
        _currentUser = currentUser;

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
    }

    public async Task<Guid> GenerateBarInvoice(Guid id)
    {
        //var venteRequest = new GetVenteRequest(id);
        //var vente = await _mediator.Send(venteRequest);
        var vente = await _venteRepo.GetByIdAsync(id);
        if (vente is null)
        {
            return Guid.Empty;
        }

        var document = Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.Margin(50);
                page.Header().Element(con => ComposeHeader(con, vente));
                page.Content().Element(con => ComposeContent(con, vente));
                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        });

        document.ShowInPreviewer();

        return id;
    }

    public void ComposeHeader(IContainer container, Vente model)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.ConstantItem(100).Height(50).Placeholder();

            row.RelativeItem(2).AlignCenter().Text("HOTEL IZE").FontSize(20);
            row.RelativeItem(1).AlignRight().Text($"Kpalime le {DateTime.UtcNow}").FontSize(16);
        });
    }

    public void ComposeContent(IContainer container, Vente model)
    {
        container
            .PaddingVertical(40)
            .Height(500)
            .Background(Colors.Grey.Lighten3)
            .AlignCenter()
            .AlignMiddle()
            .Text("Content").FontSize(16);
    }
}
