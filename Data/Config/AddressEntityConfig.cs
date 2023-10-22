using System.Diagnostics.CodeAnalysis;
using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

[ExcludeFromCodeCoverage]
public class AddressEntityConfig : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable(name: "addresses", table =>
        {
            table.CreateStatusConstraint(name: "CK_addresses_status");
            table.HasCheckConstraint(
                name: "CK_addresses_state",
                sql: "state IN ('AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', " +
                     "'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', " +
                     "'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO')"
            );
        });

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(address => address.Id)
            .HasName("PK_address_id");

        builder.HasOne(address => address.Person)
            .WithMany(person => person.Addresses)
            .HasForeignKey(address => address.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_addresses_person_id");

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(address => address.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(address => address.Street)
            .HasColumnName("street")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(address => address.Number)
            .HasColumnName("number")
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder.Property(address => address.Neighborhood)
            .HasColumnName("neighborhood")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(address => address.City)
            .HasColumnName("city")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(address => address.State)
            .HasColumnName("state")
            .HasColumnType("varchar(2)")
            .IsRequired();

        builder.Property(address => address.ZipCode)
            .HasColumnName("zip_code")
            .HasColumnType("varchar(8)")
            .IsRequired();

        builder.Property(address => address.Complement)
            .HasColumnName("complement")
            .HasColumnType("varchar(50)")
            .IsRequired(false);

        # endregion
    }
}
