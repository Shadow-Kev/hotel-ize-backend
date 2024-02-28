using FSH.WebApi.Application.Ize.TypeChambres;

namespace FSH.WebApi.Host.Controllers.Ize;

public class TypeChambresController : VersionedApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.TypeChambres)]
    [OpenApiOperation("Creer un Type chambre","")]
    public Task<Guid> CreateAsync(CreateTypeChambreRequest request)
    {
        return Mediator.Send(request);
    }
}
