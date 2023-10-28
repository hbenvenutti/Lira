using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

public class MediumEntityConfig : IEntityTypeConfiguration<MediumEntity>
{
    public void Configure(EntityTypeBuilder<MediumEntity> builder)
    {
        builder.ToTable(name: "mediums", table =>
        {
            table.CreateStatusConstraint(name: "CK_mediums_status");
        });

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(medium => medium.Id)
            .HasName("PK_medium_id");

        builder.HasOne(medium => medium.Person)
            .WithMany(person => person.Mediums)
            .HasForeignKey(medium => medium.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_mediums_person_id");

        # endregion

        # region ---- indexes --------------------------------------------------

        builder.HasIndex(medium => medium.PersonId)
            .HasDatabaseName("IX_medium_person_id")
            .IsUnique();

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(medium => medium.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(medium => medium.FirstAmaci)
            .HasColumnName("first_amaci")
            .IsRequired(false);

        builder.Property(medium => medium.LastAmaci)
            .HasColumnName("last_amaci")
            .IsRequired(false);

        # endregion
    }
}
