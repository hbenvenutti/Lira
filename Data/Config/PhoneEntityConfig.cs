using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

public class PhoneEntityConfig : IEntityTypeConfiguration<PhoneEntity>
{
    public void Configure(EntityTypeBuilder<PhoneEntity> builder)
    {
        builder.ToTable(name: "phones", table => table
            .CreateStatusConstraint(name: "CK_phones_status")
        );

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(phone => phone.Id)
            .HasName("PK_phone_id");

        builder.HasOne(phone => phone.Person)
            .WithMany(person => person.Phones)
            .HasForeignKey(phone => phone.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_phones_person_id");

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(phone => phone.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(phone => phone.Number)
            .HasColumnName("number")
            .HasColumnType("varchar(9)")
            .IsRequired();

        builder.Property(phone => phone.Ddd)
            .HasColumnName("ddd")
            .HasColumnType("varchar(2)")
            .IsRequired();

        # endregion
    }
}
