﻿// <auto-generated />
using System;
using Barberiapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Barberiapp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221109050118_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Barberiapp.Entidades.Barberia", b =>
                {
                    b.Property<int>("CodigoBarberia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoBarberia"), 1L, 1);

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("URL_Facebook")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("URL_Instagram")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("URL_Ubicacion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("URL_Youtube")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("CodigoBarberia");

                    b.ToTable("Barberia");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Barbero", b =>
                {
                    b.Property<int>("CodigoBarbero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoBarbero"), 1L, 1);

                    b.Property<int>("CodigoBarberia")
                        .HasColumnType("int");

                    b.Property<string>("CodigoUsuario")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CodigoBarbero");

                    b.HasIndex("CodigoBarberia");

                    b.HasIndex("CodigoUsuario");

                    b.ToTable("Barbero");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cita", b =>
                {
                    b.Property<int>("CodigoCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCita"), 1L, 1);

                    b.Property<int>("CodigoBarbero")
                        .HasColumnType("int");

                    b.Property<int>("CodigoCliente")
                        .HasColumnType("int");

                    b.Property<int>("CodigoMedioPago")
                        .HasColumnType("int");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Hora")
                        .HasColumnType("time");

                    b.Property<int?>("MedioPagoCodigoMedioPago")
                        .HasColumnType("int");

                    b.Property<int>("Valoracion")
                        .HasColumnType("int");

                    b.HasKey("CodigoCita");

                    b.HasIndex("CodigoBarbero");

                    b.HasIndex("CodigoCliente");

                    b.HasIndex("MedioPagoCodigoMedioPago");

                    b.ToTable("Cita");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cliente", b =>
                {
                    b.Property<int>("CodigoCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCliente"), 1L, 1);

                    b.Property<string>("CodigoUsuario")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CodigoCliente");

                    b.HasIndex("CodigoUsuario");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Barberiapp.Entidades.FotoCorte", b =>
                {
                    b.Property<int>("CodigoFotoCorte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoFotoCorte"), 1L, 1);

                    b.Property<int>("CodigoBarbero")
                        .HasColumnType("int");

                    b.Property<string>("URL_Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodigoFotoCorte");

                    b.HasIndex("CodigoBarbero");

                    b.ToTable("FotoCorte");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Horario", b =>
                {
                    b.Property<int>("CodigoHorario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoHorario"), 1L, 1);

                    b.Property<int>("CodigoBarberia")
                        .HasColumnType("int");

                    b.Property<int>("Dia")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("HoraFin")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.HasKey("CodigoHorario");

                    b.HasIndex("CodigoBarberia");

                    b.ToTable("Horario");
                });

            modelBuilder.Entity("Barberiapp.Entidades.MediosPago", b =>
                {
                    b.Property<int>("CodigoMedioPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoMedioPago"), 1L, 1);

                    b.Property<int?>("BarberiaCodigoBarberia")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CodigoMedioPago");

                    b.HasIndex("BarberiaCodigoBarberia");

                    b.ToTable("MediosPago");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Servicio", b =>
                {
                    b.Property<int>("CodigoServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoServicio"), 1L, 1);

                    b.Property<int?>("CitaCodigoCita")
                        .HasColumnType("int");

                    b.Property<int>("CodigoBarberia")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CodigoServicio");

                    b.HasIndex("CitaCodigoCita");

                    b.HasIndex("CodigoBarberia");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("Barberiapp.Entidades.TipoDocumento", b =>
                {
                    b.Property<int>("CodigoTipoDocumento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoTipoDocumento"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CodigoTipoDocumento");

                    b.ToTable("TipoDocumento");
                });

            modelBuilder.Entity("Barberiapp.Entidades.TipoServicio", b =>
                {
                    b.Property<int>("CodigoTipoServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoTipoServicio"), 1L, 1);

                    b.Property<int?>("BarberoCodigoBarbero")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CodigoTipoServicio");

                    b.HasIndex("BarberoCodigoBarbero");

                    b.ToTable("TipoServicio");
                });

            modelBuilder.Entity("Barberiapp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CodigoTipoDocumento")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CodigoTipoDocumento");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ServicioTipoServicio", b =>
                {
                    b.Property<int>("ServiciosCodigoServicio")
                        .HasColumnType("int");

                    b.Property<int>("TipoServiciosCodigoTipoServicio")
                        .HasColumnType("int");

                    b.HasKey("ServiciosCodigoServicio", "TipoServiciosCodigoTipoServicio");

                    b.HasIndex("TipoServiciosCodigoTipoServicio");

                    b.ToTable("ServicioTipoServicio");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Barbero", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barberia", "Barberia")
                        .WithMany("Barberos")
                        .HasForeignKey("CodigoBarberia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barberiapp.Models.ApplicationUser", "Usuario")
                        .WithMany("UsuarioBarbero")
                        .HasForeignKey("CodigoUsuario");

                    b.Navigation("Barberia");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cita", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barbero", "Barbero")
                        .WithMany("Citas")
                        .HasForeignKey("CodigoBarbero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barberiapp.Entidades.Cliente", "Cliente")
                        .WithMany("Citas")
                        .HasForeignKey("CodigoCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barberiapp.Entidades.MediosPago", "MedioPago")
                        .WithMany("Citas")
                        .HasForeignKey("MedioPagoCodigoMedioPago");

                    b.Navigation("Barbero");

                    b.Navigation("Cliente");

                    b.Navigation("MedioPago");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cliente", b =>
                {
                    b.HasOne("Barberiapp.Models.ApplicationUser", "Usuario")
                        .WithMany("UsuarioCliente")
                        .HasForeignKey("CodigoUsuario");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Barberiapp.Entidades.FotoCorte", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barbero", "Barbero")
                        .WithMany("FotosCortes")
                        .HasForeignKey("CodigoBarbero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barbero");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Horario", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barberia", "Barberia")
                        .WithMany("Horarios")
                        .HasForeignKey("CodigoBarberia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barberia");
                });

            modelBuilder.Entity("Barberiapp.Entidades.MediosPago", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barberia", null)
                        .WithMany("MediosPago")
                        .HasForeignKey("BarberiaCodigoBarberia");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Servicio", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Cita", null)
                        .WithMany("Servicios")
                        .HasForeignKey("CitaCodigoCita");

                    b.HasOne("Barberiapp.Entidades.Barberia", "Barberia")
                        .WithMany("Servicios")
                        .HasForeignKey("CodigoBarberia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Barberia");
                });

            modelBuilder.Entity("Barberiapp.Entidades.TipoServicio", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Barbero", null)
                        .WithMany("TipoServicio")
                        .HasForeignKey("BarberoCodigoBarbero");
                });

            modelBuilder.Entity("Barberiapp.Models.ApplicationUser", b =>
                {
                    b.HasOne("Barberiapp.Entidades.TipoDocumento", "TipoDocumento")
                        .WithMany("Usuarios")
                        .HasForeignKey("CodigoTipoDocumento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDocumento");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Barberiapp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Barberiapp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barberiapp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Barberiapp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServicioTipoServicio", b =>
                {
                    b.HasOne("Barberiapp.Entidades.Servicio", null)
                        .WithMany()
                        .HasForeignKey("ServiciosCodigoServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Barberiapp.Entidades.TipoServicio", null)
                        .WithMany()
                        .HasForeignKey("TipoServiciosCodigoTipoServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Barberiapp.Entidades.Barberia", b =>
                {
                    b.Navigation("Barberos");

                    b.Navigation("Horarios");

                    b.Navigation("MediosPago");

                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Barbero", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("FotosCortes");

                    b.Navigation("TipoServicio");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cita", b =>
                {
                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("Barberiapp.Entidades.Cliente", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Barberiapp.Entidades.MediosPago", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("Barberiapp.Entidades.TipoDocumento", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Barberiapp.Models.ApplicationUser", b =>
                {
                    b.Navigation("UsuarioBarbero");

                    b.Navigation("UsuarioCliente");
                });
#pragma warning restore 612, 618
        }
    }
}
