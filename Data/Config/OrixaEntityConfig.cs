using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

public class OrixaEntityConfig : IEntityTypeConfiguration<OrixaEntity>
{
    public void Configure(EntityTypeBuilder<OrixaEntity> builder)
    {
        builder.ToTable(name: "orixas", table =>
        {
            table.CreateStatusConstraint(name: "CK_orixas_status");
        });

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(orixa => orixa.Id)
            .HasName("PK_orixas_id");

        # endregion

        # region ---- indexes --------------------------------------------------

        builder.HasIndex(orixa => orixa.Name)
            .HasDatabaseName("IX_orixas_name")
            .IsUnique();

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(orixa => orixa.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        # endregion
    }
}
