using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

public class ManagerEntityConfig : IEntityTypeConfiguration<ManagerEntity>
{
    public void Configure(EntityTypeBuilder<ManagerEntity> builder)
    {
        builder.ToTable(name: "managers");

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(manager => manager.Id)
            .HasName("PK_manager_id");

        builder.HasOne(manager => manager.Person)
            .WithOne(person => person.Manager)
            .HasForeignKey<ManagerEntity>(manager => manager.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_managers_person_id");

        # endregion

        # region ---- indexes --------------------------------------------------

        builder.HasIndex(manager => manager.Username)
            .HasDatabaseName("IX_manager_username")
            .IsUnique();

        builder.HasIndex(manager => manager.PersonId)
            .HasDatabaseName("IX_manager_person_id")
            .IsUnique();

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(manager => manager.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(manager => manager.Username)
            .HasColumnName("username")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(manager => manager.Password)
            .HasColumnName("password")
            .HasColumnType("varchar(100)")
            .IsRequired();

        # endregion
    }
}
