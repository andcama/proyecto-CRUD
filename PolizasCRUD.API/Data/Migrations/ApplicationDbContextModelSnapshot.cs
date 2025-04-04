// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolizasCRUD.API.Data;

#nullable disable

namespace PolizasCRUD.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PolizasCRUD.API.Models.Cliente", b =>
                {
                    b.Property<string>("CedulaAsegurado")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SegundoApellido")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TipoPersona")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CedulaAsegurado");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Cobertura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Coberturas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Cubre daños a terceros",
                            Nombre = "Responsabilidad Civil"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Cubre pérdidas por robo",
                            Nombre = "Robo"
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Cubre daños por incendio",
                            Nombre = "Incendio"
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "Cubre daños a la propiedad asegurada",
                            Nombre = "Daños materiales"
                        },
                        new
                        {
                            Id = 5,
                            Descripcion = "Cubre lesiones personales",
                            Nombre = "Accidentes personales"
                        },
                        new
                        {
                            Id = 6,
                            Descripcion = "Cubre gastos médicos",
                            Nombre = "Asistencia médica"
                        },
                        new
                        {
                            Id = 7,
                            Descripcion = "Indemnización por fallecimiento",
                            Nombre = "Fallecimiento"
                        });
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.EstadoPoliza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EstadosPoliza");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Activa"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Cancelada"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Vencida"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "En Trámite"
                        });
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Poliza", b =>
                {
                    b.Property<string>("NumeroPoliza")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Aseguradora")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CedulaAsegurado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("EstadoPolizaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInclusion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MontoAsegurado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Periodo")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Prima")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoPolizaId")
                        .HasColumnType("int");

                    b.HasKey("NumeroPoliza");

                    b.HasIndex("CedulaAsegurado");

                    b.HasIndex("EstadoPolizaId");

                    b.HasIndex("TipoPolizaId");

                    b.ToTable("Polizas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.PolizaCobertura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoberturaId")
                        .HasColumnType("int");

                    b.Property<string>("NumeroPoliza")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CoberturaId");

                    b.HasIndex("NumeroPoliza");

                    b.ToTable("PolizaCoberturas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.TipoPoliza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TiposPoliza");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Vida"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Auto"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Hogar"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Salud"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Viaje"
                        });
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Poliza", b =>
                {
                    b.HasOne("PolizasCRUD.API.Models.Cliente", "Cliente")
                        .WithMany("Polizas")
                        .HasForeignKey("CedulaAsegurado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PolizasCRUD.API.Models.EstadoPoliza", "EstadoPoliza")
                        .WithMany("Polizas")
                        .HasForeignKey("EstadoPolizaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolizasCRUD.API.Models.TipoPoliza", "TipoPoliza")
                        .WithMany("Polizas")
                        .HasForeignKey("TipoPolizaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("EstadoPoliza");

                    b.Navigation("TipoPoliza");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.PolizaCobertura", b =>
                {
                    b.HasOne("PolizasCRUD.API.Models.Cobertura", "Cobertura")
                        .WithMany("PolizaCoberturas")
                        .HasForeignKey("CoberturaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PolizasCRUD.API.Models.Poliza", "Poliza")
                        .WithMany("PolizaCoberturas")
                        .HasForeignKey("NumeroPoliza")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cobertura");

                    b.Navigation("Poliza");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Cliente", b =>
                {
                    b.Navigation("Polizas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Cobertura", b =>
                {
                    b.Navigation("PolizaCoberturas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.EstadoPoliza", b =>
                {
                    b.Navigation("Polizas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.Poliza", b =>
                {
                    b.Navigation("PolizaCoberturas");
                });

            modelBuilder.Entity("PolizasCRUD.API.Models.TipoPoliza", b =>
                {
                    b.Navigation("Polizas");
                });
#pragma warning restore 612, 618
        }
    }
}