using System;

// Data/Migrations/OcpiDbContextModelSnapshot.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OCPI.Data;

namespace OCPI.Data.Migrations
{
    [DbContext(typeof(OcpiDbContext))]
    partial class OcpiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WattReise.OCPI.Models.Connector", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("EvseUid")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Format")
                    .HasColumnType("nvarchar(max)");

                b.Property<decimal>("MaxAmperage")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("MaxPower")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("MaxVoltage")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("Standard")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("EvseUid");

                b.ToTable("Connectors");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Evse", b =>
            {
                b.Property<string>("Uid")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("EvseId")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("LocationId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Status")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Uid");

                b.HasIndex("LocationId");

                b.ToTable("Evses");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Location", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("Address")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("City")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Country")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("LastUpdated")
                    .HasColumnType("datetime2");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PostalCode")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Locations");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Session", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConnectorId")
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime?>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<string>("LocationId")
                    .HasColumnType("nvarchar(450)");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.Property<string>("Status")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("LocationId");

                b.ToTable("Sessions");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Connector", b =>
            {
                b.HasOne("WattReise.OCPI.Models.Evse", null)
                    .WithMany("Connectors")
                    .HasForeignKey("EvseUid")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Evse", b =>
            {
                b.HasOne("WattReise.OCPI.Models.Location", null)
                    .WithMany("Evses")
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Session", b =>
            {
                b.HasOne("WattReise.OCPI.Models.Location", null)
                    .WithMany()
                    .HasForeignKey("LocationId");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Evse", b =>
            {
                b.Navigation("Connectors");
            });

            modelBuilder.Entity("WattReise.OCPI.Models.Location", b =>
            {
                b.Navigation("Evses");
            });
#pragma warning restore 612, 618
        }
    }
}
