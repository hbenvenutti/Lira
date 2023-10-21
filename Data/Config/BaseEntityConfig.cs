using System.Diagnostics.CodeAnalysis;
using Lira.Common.Extensions;
using Lira.Data.Entities;
using Lira.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lira.Data.Config;

[ExcludeFromCodeCoverage]
public static class BaseEntityConfig
{
    # region ---- properties ---------------------------------------------------

    private static readonly string Active = EntityStatus.Active.ToString();
    private static readonly string Inactive = EntityStatus.Inactive.ToString();
    private static readonly string Deleted = EntityStatus.Deleted.ToString();

    private static readonly string StatusConstraintSql =
        $"status IN ('{Active}', '{Inactive}', '{Deleted}')";

    # endregion

    # region ---- constraints --------------------------------------------------

    public static void CreateStatusConstraint<T>(
        this TableBuilder<T> tableBuilder,
        string name
    ) where T : BaseEntity => tableBuilder.HasCheckConstraint(
        name: name,
        sql: StatusConstraintSql
    );

    # endregion

    # region ---- columns ------------------------------------------------------

    public static void ConfigureBaseEntityProperties<T>(
        this EntityTypeBuilder<T> builder
    ) where T : BaseEntity
    {
        builder
            .Property(entity => entity.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .Property(entity => entity.Status)
            .HasColumnType("varchar")
            .HasColumnName("status")
            .HasConversion(
                status => status.ToString(),
                @string => @string.ParseToEnum<EntityStatus>()
            )
            .HasDefaultValue(EntityStatus.Active)
            .IsRequired();

        builder
            .Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(entity => entity.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder
            .Property(entity => entity.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);
    }

    # endregion
}
