﻿// <auto-generated />
using System;
using Lira.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lira.Data.Migrations;

[DbContext(contextType: typeof(LiraDbContext))]
partial class LiraDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation(annotation: "ProductVersion", value: "7.0.12")
            .HasAnnotation(annotation: "Relational:MaxIdentifierLength", value: 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder: modelBuilder);

        modelBuilder.Entity(name: "Lira.Data.Entities.EmailEntity", buildAction: entity =>
        {
            entity.Property<Guid>(propertyName: "Id")
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "id");

            entity.Property<string>(propertyName: "Address")
                .IsRequired()
                .HasColumnType(typeName: "varchar(50)")
                .HasColumnName(name: "address");

            entity.Property<DateTime>(propertyName: "CreatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "created_at");

            entity.Property<DateTime?>(propertyName: "DeletedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "deleted_at");

            entity.Property<Guid>(propertyName: "PersonId")
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "person_id");

            entity.Property<string>(propertyName: "Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "varchar(10)")
                .HasDefaultValue(value: "Active")
                .HasColumnName(name: "status");

            entity.Property<string>(propertyName: "Type")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "varchar(10)")
                .HasDefaultValue(value: "Personal")
                .HasColumnName(name: "type");

            entity.Property<DateTime?>(propertyName: "UpdatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "updated_at");

            entity.HasKey(propertyNames: "Id")
                .HasName(name: "PK_email_id");

            entity.HasIndex(propertyNames: "Address")
                .IsUnique()
                .HasDatabaseName(name: "IX_email_address");

            entity.HasIndex(propertyNames: "PersonId");

            entity.ToTable(name: "emails", schema: null, buildAction: table =>
            {
                table.HasCheckConstraint(
                    name: "CK_emails_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );

                table.HasCheckConstraint(
                    name: "CK_emails_type",
                    sql: "type IN ('Personal', 'Corporate')"
                );
            });
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.OrixaEntity", buildAction: entity =>
        {
            entity.Property<Guid>(propertyName: "Id")
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "id");

            entity.Property<DateTime>(propertyName: "CreatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "created_at");

            entity.Property<DateTime?>(propertyName: "DeletedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "deleted_at");

            entity.Property<string>(propertyName: "Name")
                .IsRequired()
                .HasColumnType(typeName: "varchar(50)")
                .HasColumnName(name: "name");

            entity.Property<string>(propertyName: "Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "varchar(10)")
                .HasDefaultValue(value: "Active")
                .HasColumnName(name: "status");

            entity.Property<DateTime?>(propertyName: "UpdatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "updated_at");

            entity.HasKey(propertyNames: "Id")
                .HasName(name: "PK_orixas_id");

            entity.HasIndex(propertyNames: "Name")
                .IsUnique()
                .HasDatabaseName(name: "IX_orixas_name");

            entity.ToTable(name: "orixas", schema: null, buildAction: table =>
            {
                table.HasCheckConstraint(
                    name: "CK_orixas_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );
            });
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.PersonEntity", buildAction: entity =>
        {
            entity.Property<Guid>(propertyName: "Id")
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "id");

            entity.Property<string>(propertyName: "Cpf")
                .IsRequired()
                .HasColumnType(typeName: "varchar(11)")
                .HasColumnName(name: "cpf");

            entity.Property<DateTime>(propertyName: "CreatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "created_at");

            entity.Property<DateTime?>(propertyName: "DeletedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "deleted_at");

            entity.Property<string>(propertyName: "Name")
                .IsRequired()
                .HasColumnType(typeName: "varchar(30)")
                .HasColumnName(name: "name");

            entity.Property<string>(propertyName: "Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "varchar(10)")
                .HasDefaultValue(value: "Active")
                .HasColumnName(name: "status");

            entity.Property<string>(propertyName: "Surname")
                .IsRequired()
                .HasColumnType(typeName: "varchar(30)")
                .HasColumnName(name: "surname");

            entity.Property<DateTime?>(propertyName: "UpdatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "updated_at");

            entity.HasKey(propertyNames: "Id")
                .HasName(name: "PK_persons_id");

            entity.HasIndex(propertyNames: "Cpf")
                .IsUnique()
                .HasDatabaseName(name: "IX_persons_cpf");

            entity.ToTable(name: "persons", schema: null, buildAction: table =>
            {
                table.HasCheckConstraint(
                    name: "CK_persons_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );
            });
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.PhoneEntity", buildAction: entity =>
        {
            entity.Property<Guid>(propertyName: "Id")
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "id");

            entity.Property<DateTime>(propertyName: "CreatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "created_at");

            entity.Property<string>(propertyName: "Ddd")
                .IsRequired()
                .HasColumnType(typeName: "varchar(2)")
                .HasColumnName(name: "ddd");

            entity.Property<DateTime?>(propertyName: "DeletedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "deleted_at");

            entity.Property<string>(propertyName: "Number")
                .IsRequired()
                .HasColumnType(typeName: "varchar(9)")
                .HasColumnName(name: "number");

            entity.Property<Guid>(propertyName: "PersonId")
                .HasColumnType(typeName: "uuid")
                .HasColumnName(name: "person_id");

            entity.Property<string>(propertyName: "Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType(typeName: "varchar(10)")
                .HasDefaultValue(value: "Active")
                .HasColumnName(name: "status");

            entity.Property<DateTime?>(propertyName: "UpdatedAt")
                .HasColumnType(typeName: "timestamp with time zone")
                .HasColumnName(name: "updated_at");

            entity.HasKey(propertyNames: "Id")
                .HasName(name: "PK_phone_id");

            entity.HasIndex(propertyNames: "PersonId");

            entity.ToTable(name: "phones", schema: null, buildAction: table =>
            {
                table.HasCheckConstraint(
                    name: "CK_phones_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );
            });
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.EmailEntity", buildAction: entity =>
        {
            entity
                .HasOne(
                    relatedTypeName: "Lira.Data.Entities.PersonEntity",
                    navigationName: "Person"
                )
                .WithMany(collection: "Emails")
                .HasForeignKey(foreignKeyPropertyNames: "PersonId")
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName(name: "FK_emails_person_id");

            entity.Navigation(navigationName: "Person");
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.PhoneEntity", buildAction: entity =>
        {
            entity
                .HasOne(
                    relatedTypeName: "Lira.Data.Entities.PersonEntity",
                    navigationName: "Person"
                )
                .WithMany(collection: "Phones")
                .HasForeignKey(foreignKeyPropertyNames: "PersonId")
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName(name: "FK_phones_person_id");

            entity.Navigation(navigationName: "Person");
        });

        modelBuilder.Entity(name: "Lira.Data.Entities.PersonEntity", buildAction: entity =>
        {
            entity.Navigation(navigationName: "Emails");

            entity.Navigation(navigationName: "Phones");
        });
#pragma warning restore 612, 618
    }
}
