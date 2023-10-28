﻿// <auto-generated />
using System;
using Lira.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lira.Data.Migrations;

[DbContext(typeof(LiraDbContext))]
[Migration("20231028161520_SeedOrixas")]
partial class SeedOrixas
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.12")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("Lira.Data.Entities.AddressEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<string>("City")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("city");

            b.Property<string>("Complement")
                .HasColumnType("varchar(50)")
                .HasColumnName("complement");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<string>("Neighborhood")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("neighborhood");

            b.Property<string>("Number")
                .IsRequired()
                .HasColumnType("varchar(10)")
                .HasColumnName("number");

            b.Property<Guid>("PersonId")
                .HasColumnType("uuid")
                .HasColumnName("person_id");

            b.Property<string>("State")
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasColumnName("state");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<string>("Street")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("street");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.Property<string>("ZipCode")
                .IsRequired()
                .HasColumnType("varchar(8)")
                .HasColumnName("zip_code");

            b.HasKey("Id")
                .HasName("PK_address_id");

            b.HasIndex("PersonId");

            b.ToTable("addresses", null, t =>
            {
                t.HasCheckConstraint("CK_addresses_state", "state IN ('AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO')");

                t.HasCheckConstraint("CK_addresses_status", "status IN ('Active', 'Inactive', 'Deleted')");
            });
        });

        modelBuilder.Entity("Lira.Data.Entities.EmailEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<string>("Address")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("address");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<Guid>("PersonId")
                .HasColumnType("uuid")
                .HasColumnName("person_id");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<string>("Type")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Personal")
                .HasColumnName("type");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.HasKey("Id")
                .HasName("PK_email_id");

            b.HasIndex("Address")
                .IsUnique()
                .HasDatabaseName("IX_email_address");

            b.HasIndex("PersonId");

            b.ToTable("emails", null, t =>
            {
                t.HasCheckConstraint("CK_emails_status", "status IN ('Active', 'Inactive', 'Deleted')");

                t.HasCheckConstraint("CK_emails_type", "type IN ('Personal', 'Corporate')");
            });
        });

        modelBuilder.Entity("Lira.Data.Entities.ManagerEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<string>("Password")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("password");

            b.Property<Guid>("PersonId")
                .HasColumnType("uuid")
                .HasColumnName("person_id");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.Property<string>("Username")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("username");

            b.HasKey("Id")
                .HasName("PK_manager_id");

            b.HasIndex("PersonId")
                .IsUnique()
                .HasDatabaseName("IX_manager_person_id");

            b.HasIndex("Username")
                .IsUnique()
                .HasDatabaseName("IX_manager_username");

            b.ToTable("managers", (string)null);
        });

        modelBuilder.Entity("Lira.Data.Entities.OrixaEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasColumnName("name");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.HasKey("Id")
                .HasName("PK_orixas_id");

            b.HasIndex("Name")
                .IsUnique()
                .HasDatabaseName("IX_orixas_name");

            b.ToTable("orixas", null, t =>
            {
                t.HasCheckConstraint("CK_orixas_status", "status IN ('Active', 'Inactive', 'Deleted')");
            });
        });

        modelBuilder.Entity("Lira.Data.Entities.PersonEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<string>("Cpf")
                .IsRequired()
                .HasColumnType("varchar(11)")
                .HasColumnName("cpf");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("name");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<string>("Surname")
                .IsRequired()
                .HasColumnType("varchar(30)")
                .HasColumnName("surname");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.HasKey("Id")
                .HasName("PK_persons_id");

            b.HasIndex("Cpf")
                .IsUnique()
                .HasDatabaseName("IX_persons_cpf");

            b.ToTable("persons", null, t =>
            {
                t.HasCheckConstraint("CK_persons_status", "status IN ('Active', 'Inactive', 'Deleted')");
            });
        });

        modelBuilder.Entity("Lira.Data.Entities.PhoneEntity", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTime>("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");

            b.Property<string>("Ddd")
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasColumnName("ddd");

            b.Property<DateTime?>("DeletedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("deleted_at");

            b.Property<string>("Number")
                .IsRequired()
                .HasColumnType("varchar(9)")
                .HasColumnName("number");

            b.Property<Guid>("PersonId")
                .HasColumnType("uuid")
                .HasColumnName("person_id");

            b.Property<string>("Status")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("varchar(10)")
                .HasDefaultValue("Active")
                .HasColumnName("status");

            b.Property<DateTime?>("UpdatedAt")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            b.HasKey("Id")
                .HasName("PK_phone_id");

            b.HasIndex("PersonId");

            b.ToTable("phones", null, t =>
            {
                t.HasCheckConstraint("CK_phones_status", "status IN ('Active', 'Inactive', 'Deleted')");
            });
        });

        modelBuilder.Entity("Lira.Data.Entities.AddressEntity", b =>
        {
            b.HasOne("Lira.Data.Entities.PersonEntity", "Person")
                .WithMany("Addresses")
                .HasForeignKey("PersonId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("FK_addresses_person_id");

            b.Navigation("Person");
        });

        modelBuilder.Entity("Lira.Data.Entities.EmailEntity", b =>
        {
            b.HasOne("Lira.Data.Entities.PersonEntity", "Person")
                .WithMany("Emails")
                .HasForeignKey("PersonId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("FK_emails_person_id");

            b.Navigation("Person");
        });

        modelBuilder.Entity("Lira.Data.Entities.ManagerEntity", b =>
        {
            b.HasOne("Lira.Data.Entities.PersonEntity", "Person")
                .WithOne("Manager")
                .HasForeignKey("Lira.Data.Entities.ManagerEntity", "PersonId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("FK_managers_person_id");

            b.Navigation("Person");
        });

        modelBuilder.Entity("Lira.Data.Entities.PhoneEntity", b =>
        {
            b.HasOne("Lira.Data.Entities.PersonEntity", "Person")
                .WithMany("Phones")
                .HasForeignKey("PersonId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("FK_phones_person_id");

            b.Navigation("Person");
        });

        modelBuilder.Entity("Lira.Data.Entities.PersonEntity", b =>
        {
            b.Navigation("Addresses");

            b.Navigation("Emails");

            b.Navigation("Manager")
                .IsRequired();

            b.Navigation("Phones");
        });
#pragma warning restore 612, 618
    }
}
