using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Ize.Ventes;
using MediatR;
using Microsoft.Extensions.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace FSH.WebApi.Infrastructure.Common.Services;
public class PdfServices : IPdfService
{
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _config;
    public PdfServices(IMediator mediator, ICurrentUser currentUser, IConfiguration config)
    {
        _mediator = mediator;
        _currentUser = currentUser;
        _config = config;

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
    }

    public async Task<string> GenerateBarInvoice(Guid id)
    {
        var venteRequest = new GetVenteRequest(id);
        var vente = await _mediator.Send(venteRequest);
        if (vente is null)
        {
            return string.Empty;
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
        var pdfFileName = $"facture_vente_{vente.Id}.pdf";
        var pdfFilePath = Path.Combine("wwwroot", "pdfs", pdfFileName);

        document.GeneratePdfAndShow();
        //document.GeneratePdf(pdfFilePath);
        //document.ShowInPreviewer();

        return $"pdfs/{pdfFileName}";
    }

    public void ComposeHeader(IContainer container, VenteDetailsDto model)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
        var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        var logoPath = _config.GetValue<string>("Printing:logo");
        var logo = Path.Combine(rootPath, "wwwroot", logoPath);

        container.Row(row =>
        {
            row.ConstantItem(100).Height(50).Image($"{logo}").FitHeight();

            row.RelativeItem(2).AlignCenter().Text("HOTEL IZE").FontSize(20);
            row.RelativeItem(1).AlignRight().Text($"Kpalime le {DateTime.UtcNow.ToString("d")}").FontSize(12);
        });
    }

    public void ComposeContent(IContainer container, VenteDetailsDto model)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(con => ComposeTable(con, model));

            var totalPrice = model.VenteProduits.Sum(x => x.Prix * x.Quantite);
            column.Item().AlignRight().Text($"Total: {totalPrice}FCF").FontSize(14);
        });
    }

    public void ComposeTable(IContainer container, VenteDetailsDto model)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Désignation");
                header.Cell().Element(CellStyle).AlignRight().Text("Prix Unitaire");
                header.Cell().Element(CellStyle).AlignRight().Text("Quantité");
                header.Cell().Element(CellStyle).AlignRight().Text("Prix Total");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            foreach (var item in model.VenteProduits)
            {
                table.Cell().Element(CellStyle).Text(model.VenteProduits.ToList().IndexOf(item) + 1);
                table.Cell().Element(CellStyle).Text(item.Product.Name);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Prix}FCFA");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantite.ToString());
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Prix * item.Quantite}FCFA");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }
}
