using FSH.WebApi.Application.Common.Interfaces;

namespace FSH.WebApi.Host.Controllers.Ize;

public class PdfsController : VersionedApiController
{
    private IPdfService _pdfService;

    public PdfsController(IPdfService pdfService)
    {
        _pdfService = pdfService;
    }

    [HttpGet("generateFactureBar/{id:Guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Ventes)]
    [OpenApiOperation("Générer la facture vente du bar", "")]
    public async Task<IActionResult> PrintFactureVente(Guid id)
    {
        var result = await _pdfService.GenerateBarInvoice(id);
        if (result != string.Empty)
            return Ok(result);
        return BadRequest();
    }
}
