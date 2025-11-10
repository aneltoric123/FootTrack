using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootTrack.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    DrzavaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.DrzavaId);
                });

            migrationBuilder.CreateTable(
                name: "Sodniki",
                columns: table => new
                {
                    SodnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sodniki", x => x.SodnikId);
                });

            migrationBuilder.CreateTable(
                name: "Trenerji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formacija = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trenerji", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mesta",
                columns: table => new
                {
                    MestoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrzavaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesta", x => x.MestoId);
                    table.ForeignKey(
                        name: "FK_Mesta_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stadioni",
                columns: table => new
                {
                    StadionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum_Otvoritve = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kapaciteta = table.Column<int>(type: "int", nullable: false),
                    MestoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadioni", x => x.StadionId);
                    table.ForeignKey(
                        name: "FK_Stadioni_Mesta_MestoId",
                        column: x => x.MestoId,
                        principalTable: "Mesta",
                        principalColumn: "MestoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ekipe",
                columns: table => new
                {
                    EkipaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kratica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum_Ustanovitve = table.Column<DateOnly>(type: "date", nullable: false),
                    StadionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipe", x => x.EkipaId);
                    table.ForeignKey(
                        name: "FK_Ekipe_Stadioni_StadionId",
                        column: x => x.StadionId,
                        principalTable: "Stadioni",
                        principalColumn: "StadionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UporabniskoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NajljubsaEkipaId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Ekipe_NajljubsaEkipaId",
                        column: x => x.NajljubsaEkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId");
                });

            migrationBuilder.CreateTable(
                name: "Igralci",
                columns: table => new
                {
                    IgralecId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priimek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum_Rojstva = table.Column<DateOnly>(type: "date", nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EkipaId = table.Column<int>(type: "int", nullable: false),
                    DrzavaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igralci", x => x.IgralecId);
                    table.ForeignKey(
                        name: "FK_Igralci_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igralci_Ekipe_EkipaId",
                        column: x => x.EkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trenerji_Obdobje",
                columns: table => new
                {
                    TrenerId = table.Column<int>(type: "int", nullable: false),
                    EkipaId = table.Column<int>(type: "int", nullable: false),
                    Zacetek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Konec = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trenerji_Obdobje", x => new { x.TrenerId, x.EkipaId, x.Zacetek });
                    table.ForeignKey(
                        name: "FK_Trenerji_Obdobje_Ekipe_EkipaId",
                        column: x => x.EkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trenerji_Obdobje_Trenerji_TrenerId",
                        column: x => x.TrenerId,
                        principalTable: "Trenerji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tekmovanja",
                columns: table => new
                {
                    TekmovanjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrzavaId = table.Column<int>(type: "int", nullable: false),
                    UporabnikId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tekmovanja", x => x.TekmovanjeId);
                    table.ForeignKey(
                        name: "FK_Tekmovanja_AspNetUsers_UporabnikId",
                        column: x => x.UporabnikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tekmovanja_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "DrzavaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sezone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Leto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TekmovanjeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sezone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sezone_Tekmovanja_TekmovanjeId",
                        column: x => x.TekmovanjeId,
                        principalTable: "Tekmovanja",
                        principalColumn: "TekmovanjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ekipa_V_Sezoni",
                columns: table => new
                {
                    EkipaId = table.Column<int>(type: "int", nullable: false),
                    SezonaId = table.Column<int>(type: "int", nullable: false),
                    Tocke = table.Column<int>(type: "int", nullable: false),
                    Zmage = table.Column<int>(type: "int", nullable: false),
                    Remi = table.Column<int>(type: "int", nullable: false),
                    Porazi = table.Column<int>(type: "int", nullable: false),
                    Goli = table.Column<int>(type: "int", nullable: false),
                    Prejeti_Goli = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipa_V_Sezoni", x => new { x.EkipaId, x.SezonaId });
                    table.ForeignKey(
                        name: "FK_Ekipa_V_Sezoni_Ekipe_EkipaId",
                        column: x => x.EkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ekipa_V_Sezoni_Sezone_SezonaId",
                        column: x => x.SezonaId,
                        principalTable: "Sezone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Krogi",
                columns: table => new
                {
                    KrogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stevilka = table.Column<int>(type: "int", nullable: false),
                    SezonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Krogi", x => x.KrogId);
                    table.ForeignKey(
                        name: "FK_Krogi_Sezone_SezonaId",
                        column: x => x.SezonaId,
                        principalTable: "Sezone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tekme",
                columns: table => new
                {
                    TekmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GoliDomaci = table.Column<int>(type: "int", nullable: false),
                    GoliGosti = table.Column<int>(type: "int", nullable: false),
                    DomacaEkipaId = table.Column<int>(type: "int", nullable: false),
                    GostujocaEkipaId = table.Column<int>(type: "int", nullable: false),
                    StadionId = table.Column<int>(type: "int", nullable: false),
                    KrogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tekme", x => x.TekmaId);
                    table.ForeignKey(
                        name: "FK_Tekme_Ekipe_DomacaEkipaId",
                        column: x => x.DomacaEkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tekme_Ekipe_GostujocaEkipaId",
                        column: x => x.GostujocaEkipaId,
                        principalTable: "Ekipe",
                        principalColumn: "EkipaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tekme_Krogi_KrogId",
                        column: x => x.KrogId,
                        principalTable: "Krogi",
                        principalColumn: "KrogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tekme_Stadioni_StadionId",
                        column: x => x.StadionId,
                        principalTable: "Stadioni",
                        principalColumn: "StadionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dogodek_Na_Tekmi",
                columns: table => new
                {
                    St_Dogodka = table.Column<int>(type: "int", nullable: false),
                    TekmaId = table.Column<int>(type: "int", nullable: false),
                    IgralecId = table.Column<int>(type: "int", nullable: false),
                    TipDogodka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minuta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogodek_Na_Tekmi", x => new { x.IgralecId, x.TekmaId, x.St_Dogodka });
                    table.ForeignKey(
                        name: "FK_Dogodek_Na_Tekmi_Igralci_IgralecId",
                        column: x => x.IgralecId,
                        principalTable: "Igralci",
                        principalColumn: "IgralecId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogodek_Na_Tekmi_Tekme_TekmaId",
                        column: x => x.TekmaId,
                        principalTable: "Tekme",
                        principalColumn: "TekmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Igralec_Na_Tekmi",
                columns: table => new
                {
                    IgralecId = table.Column<int>(type: "int", nullable: false),
                    TekmaId = table.Column<int>(type: "int", nullable: false),
                    ZacetnaPostava = table.Column<bool>(type: "bit", nullable: false),
                    Kapetan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igralec_Na_Tekmi", x => new { x.IgralecId, x.TekmaId });
                    table.ForeignKey(
                        name: "FK_Igralec_Na_Tekmi_Igralci_IgralecId",
                        column: x => x.IgralecId,
                        principalTable: "Igralci",
                        principalColumn: "IgralecId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Igralec_Na_Tekmi_Tekme_TekmaId",
                        column: x => x.TekmaId,
                        principalTable: "Tekme",
                        principalColumn: "TekmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sodniki_Tekma",
                columns: table => new
                {
                    TekmaId = table.Column<int>(type: "int", nullable: false),
                    SodnikId = table.Column<int>(type: "int", nullable: false),
                    Vloga = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sodniki_Tekma", x => new { x.TekmaId, x.SodnikId });
                    table.ForeignKey(
                        name: "FK_Sodniki_Tekma_Sodniki_SodnikId",
                        column: x => x.SodnikId,
                        principalTable: "Sodniki",
                        principalColumn: "SodnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sodniki_Tekma_Tekme_TekmaId",
                        column: x => x.TekmaId,
                        principalTable: "Tekme",
                        principalColumn: "TekmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NajljubsaEkipaId",
                table: "AspNetUsers",
                column: "NajljubsaEkipaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Dogodek_Na_Tekmi_TekmaId",
                table: "Dogodek_Na_Tekmi",
                column: "TekmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipa_V_Sezoni_SezonaId",
                table: "Ekipa_V_Sezoni",
                column: "SezonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipe_StadionId",
                table: "Ekipe",
                column: "StadionId");

            migrationBuilder.CreateIndex(
                name: "IX_Igralci_DrzavaId",
                table: "Igralci",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Igralci_EkipaId",
                table: "Igralci",
                column: "EkipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Igralec_Na_Tekmi_TekmaId",
                table: "Igralec_Na_Tekmi",
                column: "TekmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Krogi_SezonaId",
                table: "Krogi",
                column: "SezonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesta_DrzavaId",
                table: "Mesta",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sezone_TekmovanjeId",
                table: "Sezone",
                column: "TekmovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sodniki_Tekma_SodnikId",
                table: "Sodniki_Tekma",
                column: "SodnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadioni_MestoId",
                table: "Stadioni",
                column: "MestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekme_DomacaEkipaId",
                table: "Tekme",
                column: "DomacaEkipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekme_GostujocaEkipaId",
                table: "Tekme",
                column: "GostujocaEkipaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekme_KrogId",
                table: "Tekme",
                column: "KrogId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekme_StadionId",
                table: "Tekme",
                column: "StadionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekmovanja_DrzavaId",
                table: "Tekmovanja",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tekmovanja_UporabnikId",
                table: "Tekmovanja",
                column: "UporabnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Trenerji_Obdobje_EkipaId",
                table: "Trenerji_Obdobje",
                column: "EkipaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Dogodek_Na_Tekmi");

            migrationBuilder.DropTable(
                name: "Ekipa_V_Sezoni");

            migrationBuilder.DropTable(
                name: "Igralec_Na_Tekmi");

            migrationBuilder.DropTable(
                name: "Sodniki_Tekma");

            migrationBuilder.DropTable(
                name: "Trenerji_Obdobje");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Igralci");

            migrationBuilder.DropTable(
                name: "Sodniki");

            migrationBuilder.DropTable(
                name: "Tekme");

            migrationBuilder.DropTable(
                name: "Trenerji");

            migrationBuilder.DropTable(
                name: "Krogi");

            migrationBuilder.DropTable(
                name: "Sezone");

            migrationBuilder.DropTable(
                name: "Tekmovanja");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Ekipe");

            migrationBuilder.DropTable(
                name: "Stadioni");

            migrationBuilder.DropTable(
                name: "Mesta");

            migrationBuilder.DropTable(
                name: "Drzave");
        }
    }
}
