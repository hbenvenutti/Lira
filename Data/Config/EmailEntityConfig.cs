using System.Diagnostics.CodeAnalysis;
using Lira.Common.Extensions;
using Lira.Data.Entities;
using Lira.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

[ExcludeFromCodeCoverage]
public class EmailEntityConfig : IEntityTypeConfiguration<EmailEntity>
{
    private static readonly string Personal = EmailType.Personal.ToString();
    private static readonly string Corporate = EmailType.Corporate.ToString();

    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable(name: "emails",table =>
            {
                table.CreateStatusConstraint(name: "CK_emails_status");

                table.HasCheckConstraint(
                    name: "CK_emails_type",
                    sql: $"type IN ('{Personal}', '{Corporate}')"
                );
            }
        );

        builder.ConfigureBaseEntityProperties();

        # region ---- keys -----------------------------------------------------

        builder.HasKey(email => email.Id)
            .HasName("PK_email_id");

        builder.HasOne(email => email.Person)
            .WithMany(person => person.Emails)
            .HasForeignKey(email => email.PersonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_emails_person_id");

        # endregion

        # region ---- indexes --------------------------------------------------

        builder.HasIndex(email => email.Address)
            .HasDatabaseName("IX_email_address")
            .IsUnique();

        # endregion

        # region ---- columns --------------------------------------------------

        builder.Property(email => email.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(email => email.Address)
            .HasColumnName("address")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(email => email.Type)
            .HasColumnName("type")
            .HasConversion(
                type => type.ToString(),
                @string => @string.ParseToEnum<EmailType>()
            )
            .HasDefaultValue(EmailType.Personal)
            .HasColumnType("varchar(10)")
            .IsRequired();

        # endregion
    }
}
