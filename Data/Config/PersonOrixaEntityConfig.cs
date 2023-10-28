using Lira.Common.Extensions;
using Lira.Data.Entities;
using Lira.Domain.Religion.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

public class PersonOrixaEntityConfig : IEntityTypeConfiguration<PersonOrixaEntity>
{
    public void Configure(EntityTypeBuilder<PersonOrixaEntity> builder)
    {
        builder.ToTable(name: "person_orixas", table =>
        {
            table.CreateStatusConstraint(name: "CK_person_orixas_status");

            table.HasCheckConstraint(
                name: "CK_person_orixas_type",
                sql: $"type IN ('{OrixaType.Front}', '{OrixaType.Ancestor}', '{OrixaType.Close}')"
            );
        });

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(personOrixa => personOrixa.Id)
            .HasName("PK_person_orixas_id");

        builder.HasOne(personOrixa => personOrixa.Person)
            .WithMany(person => person.PersonOrixas)
            .HasForeignKey(personOrixa => personOrixa.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_person_orixas_person_id");

        builder.HasOne(personOrixa => personOrixa.Orixa)
            .WithMany(orixa => orixa.PersonOrixas)
            .HasForeignKey(personOrixa => personOrixa.OrixaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_person_orixas_orixa_id");

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(personOrixa => personOrixa.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(personOrixa => personOrixa.OrixaId)
            .HasColumnName("orixa_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(personOrixa => personOrixa.Type)
            .HasColumnName("type")
            .HasConversion(
                type => type.ToString(),
                @string => @string.ParseToEnum<OrixaType>()
            )
            .HasColumnType("varchar(10)")
            .IsRequired();

        # endregion
    }
}
