using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Ize.Clients;
using FSH.WebApi.Application.Ize.Ventes;
using FSH.WebApi.Infrastructure.Common.Extensions;
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

    [Obsolete]
    public void ComposeHeaderClient(IContainer container, ClientDetailsDto model)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
        var rootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        var logoPath = _config.GetValue<string>("Printing:logo");
        var logo = Path.Combine(rootPath, "wwwroot", logoPath);

        container.Grid(grid =>
        {
            grid.VerticalSpacing(15);
            grid.HorizontalSpacing(15);
            grid.AlignCenter();

            grid.Item(3).AlignLeft().Height(50).Image($"{logo}").FitHeight();
            grid.Item(6).AlignCenter().Column(c =>
            {
                c.Item().Text("HOTEL IZE").FontSize(20);
                c.Item().Text($"Tél: 00 228 90 43 98 98/ 96 04 98 98").FontSize(12);
                c.Item().Text($"260 BP: 1097 Kpalimé - TOGO").FontSize(12);
            });

            grid.Item(3).AlignRight().Text($"Kpalime, le {DateTime.UtcNow.ToString("d")}").FontSize(12);

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

    public void ComposeContentClient(IContainer container, ClientDetailsDto model)
    {
        var invoiceNumber = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString("0000");

        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);

            column.Item().Row(row =>
            {
                row.RelativeItem(4).AlignCenter().Text("NOTE D'HOTEL").FontSize(20).Bold();
                row.RelativeItem(1).AlignRight().Text($"N°: {invoiceNumber}").FontSize(12);
            });

            column.Item().Element(con => ComposeTableClient(con, model));

            column.Item().Row(row =>
            {

            });

            column.Item().Row(row =>
            {
                row.RelativeItem().AlignLeft().Text("Signature du caissier").Bold();
                row.RelativeItem().AlignRight().Text("Signature du client").Bold();
            });
            //var totalPrice = CalculerPrixTotalClient(model);
            //column.Item().AlignRight().Text($"Total: {totalPrice}FCFA").FontSize(14);
        });
    }

    public decimal CalculerPrixTotalClient(ClientDetailsDto client)
    {
        decimal total = 0;
        total = client.Chambre.Prix * CalculerNombreDeNuites(client);

        if (client.Ventes is not null)
        {
            foreach (var vente in client.Ventes)
            {
                if (vente.VenteProduits is not null)
                {
                    foreach (var venteProduit in vente.VenteProduits)
                    {
                        total += venteProduit.Quantite * venteProduit.Prix;
                    }
                }
            }
        }

        return Math.Round(total);
    }

    public decimal CalculerPrixTotalBarClient(ClientDetailsDto client)
    {
        decimal total = 0;

        if (client.Ventes is not null)
        {
            foreach (var vente in client.Ventes)
            {
                if (vente.VenteProduits is not null)
                {
                    foreach (var venteProduit in vente.VenteProduits)
                    {
                        total += venteProduit.Quantite * venteProduit.Prix;
                    }
                }
            }
        }

        return Math.Round(total);
    }

    public int CalculerNombreDeNuites(ClientDetailsDto model)
    {
        if (model.DateArrive.HasValue && model.DateDepart.HasValue)
        {
            TimeSpan duration = model.DateDepart.Value - model.DateArrive.Value;
            return duration.Days;
        }
        else
        {
            // Gérer le cas où les dates ne sont pas définies
            return 0;
        }
    }

    public decimal CalculerPrixHT(decimal prixTotal, int nombreDeNuites)
    {
        decimal tspt = 1000 * nombreDeNuites;
        decimal prixHT = (prixTotal - tspt) / 1.18m;
        prixHT = Math.Round(prixHT);
        return prixHT;
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

    public void ComposeTableClient(IContainer container, ClientDetailsDto model)
    {
        container.Border(1).Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(100);
                columns.RelativeColumn();
                columns.ConstantColumn(100);
                columns.RelativeColumn();
            });

            int nbreNuite = CalculerNombreDeNuites(model);
            int tspt = 1000 * nbreNuite;
            decimal totalBar = CalculerPrixTotalBarClient(model);
            decimal totalAPayer = CalculerPrixTotalClient(model);

            table.ExtendLastCellsToTableBottom();

            table.Cell().RowSpan(3).LabelCell("Payé par");
            table.Cell().RowSpan(3).ValueCell($"{model.Nom} {model.Prenom}");

            table.Cell().LabelCell("Numéro de chambre");
            table.Cell().ValueCell($"{model.Chambre.Nom}");

            table.Cell().LabelCell("Nombre de nuité");
            table.Cell().ValueCell($"{nbreNuite}");

            table.Cell().LabelCell("Téléphone");
            table.Cell().ValueCell($"{model.Contact}");

            table.Cell().ColumnSpan(2).LabelCell("Date");
            table.Cell().ColumnSpan(1).ValueCell($"Du {model.DateArrive.Value.ToString("d")}");
            table.Cell().ColumnSpan(1).ValueCell($"Au {model.DateDepart.Value.ToString("d")}");

            table.Cell().ColumnSpan(3).LabelCell("TSPT");
            table.Cell().ColumnSpan(1).ValueCell($"{tspt} FCFA");

            table.Cell().ColumnSpan(2).LabelCell("Chambre");
            table.Cell().ColumnSpan(1).ValueCell($"{model.Chambre.Prix.ToString()} FCFA");
            table.Cell().ColumnSpan(1).ValueCell($"{model.Chambre.Prix * nbreNuite} FCFA");

            table.Cell().ColumnSpan(3).LabelCell("BAR");
            table.Cell().ColumnSpan(1).ValueCell($"{totalBar} FCFA");

            table.Cell().ColumnSpan(3).LabelCell("TOTAL A PAYER");
            table.Cell().ColumnSpan(1).ValueCell($"{totalAPayer} FCFA");

        });
    }

    public async Task<string> GenerateClientInvoice(Guid id)
    {
        var clientRequest = new GetClientRequest(id);
        var client = await _mediator.Send(clientRequest);

        if (client is null)
        {
            return string.Empty;
        }

        var document = Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.DefaultTextStyle(ts => ts.FontFamily("Times New Roman"));
                page.Margin(50);
                page.Header().Element(con => ComposeHeaderClient(con, client));
                page.Content().Element(con => ComposeContentClient(con, client));
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Merci pour votre visite et à la prochaine !");
                    x.EmptyLine();
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        });

        var pdfFileName = $"facture_client_{client.Nom}.pdf";
        var pdfFilePath = Path.Combine("wwwroot", "pdfs", pdfFileName);

        //document.ShowInPreviewer();
        document.GeneratePdfAndShow();

        return $"pdfs/{pdfFileName}";
    }
}
