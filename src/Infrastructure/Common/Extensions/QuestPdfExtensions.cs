using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FSH.WebApi.Infrastructure.Common.Extensions;
public static class QuestPdfExtensions
{
    private static IContainer Cell(this IContainer container, bool dark)
    {
        return container
            .Border(1)
            .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
            .Padding(10);
    }

    // displays only text label
    public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

    // allows you to inject any type of content, e.g. image
    public static void ValueCell(this IContainer container, string text) => container.Cell(false).Text(text);

    // for simplicity, you can also use extension method described in the "Extending DSL" section
    public static IContainer Block(IContainer container)
    {
        return container
            .Border(1)
            .Background(Colors.Grey.Lighten3)
            .ShowOnce()
            .MinWidth(50)
            .MinHeight(50)
            .AlignCenter()
            .AlignMiddle();
    }
}
