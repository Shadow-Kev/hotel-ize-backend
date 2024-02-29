using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class UpdateTypeChambreRequestValidator : CustomValidator<UpdateTypeChambreRequest>
{
    public UpdateTypeChambreRequestValidator(IReadRepository<TypeChambre> typeChambreRepo, IStringLocalizer<UpdateTypeChambreRequestValidator> T)
    {
       

    }
}

