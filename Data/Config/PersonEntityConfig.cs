using System.Diagnostics.CodeAnalysis;
using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

[ExcludeFromCodeCoverage]
public class PersonEntityConfig : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable(
            name: "people",
            buildAction: table => table
                .CreateStatusConstraint(name: "CK_people_status")
        );

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(person => person.Id)
            .HasName("PK_persons_id");

        # endregion

        # region ---- indexes --------------------------------------------------

        builder.HasIndex(person => person.Cpf)
            .HasDatabaseName("IX_people_cpf")
            .IsUnique();

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(person => person.Cpf)
            .HasColumnName("cpf")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(person => person.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.Property(person => person.Surname)
            .HasColumnName("surname")
            .HasColumnType("varchar(30)")
            .IsRequired();

        # endregion
    }
}
