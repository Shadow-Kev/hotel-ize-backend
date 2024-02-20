using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Ize;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;
public class TypeChambreConfig : IEntityTypeConfiguration<TypeChambre>
{
    public void Configure(EntityTypeBuilder<TypeChambre> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(tc => tc.Libelle)
            .HasMaxLength(256);
    }
}

public class ChambreConfig : IEntityTypeConfiguration<Chambre>
{
    public void Configure(EntityTypeBuilder<Chambre> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(c => c.Nom)
            .HasMaxLength(1024);

        builder
            .Property(c => c.ImagePath)
            .HasMaxLength(2048);
    }
}

public class AgentConfig : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(a => a.Nom)
            .HasMaxLength(1024);
    }
}

public class ClientConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(c => c.Nom)
            .HasMaxLength(1024);
    }
}
