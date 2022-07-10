﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnTheTrail.TrailService.Api.Database;

#nullable disable

namespace OnTheTrail.TrailService.Api.Migrations
{
    [DbContext(typeof(TrailDbContext))]
    partial class TrailDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnTheTrail.TrailService.Api.Database.Entities.Node", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("OnTheTrail.TrailService.Api.Database.Entities.Trail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("BeginningId")
                        .HasColumnType("bigint");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<long>("EndId")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxHeight")
                        .HasColumnType("int");

                    b.Property<int>("TimeInMinutes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeginningId");

                    b.HasIndex("EndId");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("OnTheTrail.TrailService.Api.Database.Entities.Trail", b =>
                {
                    b.HasOne("OnTheTrail.TrailService.Api.Database.Entities.Node", "Beginning")
                        .WithMany("TrailsBeginnings")
                        .HasForeignKey("BeginningId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OnTheTrail.TrailService.Api.Database.Entities.Node", "End")
                        .WithMany("TrailsEnds")
                        .HasForeignKey("EndId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Beginning");

                    b.Navigation("End");
                });

            modelBuilder.Entity("OnTheTrail.TrailService.Api.Database.Entities.Node", b =>
                {
                    b.Navigation("TrailsBeginnings");

                    b.Navigation("TrailsEnds");
                });
#pragma warning restore 612, 618
        }
    }
}
