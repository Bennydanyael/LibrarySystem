using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Migrations.LibraryContext
{
    public partial class InitialLibraryDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorsName = table.Column<string>(nullable: true),
                    Biography = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorsID);
                });

            migrationBuilder.CreateTable(
                name: "Maritals",
                columns: table => new
                {
                    MaritalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaritalsName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maritals", x => x.MaritalID);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    ReligionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReligionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.ReligionID);
                });

            migrationBuilder.CreateTable(
                name: "Sexs",
                columns: table => new
                {
                    SexID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SexName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexs", x => x.SexID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BooksID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    AuthorsID = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    PublishName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BooksID);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Authors",
                        principalColumn: "AuthorsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonName = table.Column<string>(nullable: false),
                    SexID = table.Column<int>(nullable: false),
                    MaritalID = table.Column<int>(nullable: false),
                    ReligionID = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Persons_Maritals_MaritalID",
                        column: x => x.MaritalID,
                        principalTable: "Maritals",
                        principalColumn: "MaritalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Religions_ReligionID",
                        column: x => x.ReligionID,
                        principalTable: "Religions",
                        principalColumn: "ReligionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Sexs_SexID",
                        column: x => x.SexID,
                        principalTable: "Sexs",
                        principalColumn: "SexID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibrarySystemRuns",
                columns: table => new
                {
                    LibraryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false),
                    BooksID = table.Column<int>(nullable: false),
                    BorrowedDate = table.Column<DateTime>(nullable: false),
                    DateBack = table.Column<DateTime>(nullable: false),
                    LengthBorrowed = table.Column<int>(nullable: false),
                    Descriptions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrarySystemRuns", x => x.LibraryID);
                    table.ForeignKey(
                        name: "FK_LibrarySystemRuns_Books_BooksID",
                        column: x => x.BooksID,
                        principalTable: "Books",
                        principalColumn: "BooksID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibrarySystemRuns_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorsID",
                table: "Books",
                column: "AuthorsID");

            migrationBuilder.CreateIndex(
                name: "IX_LibrarySystemRuns_BooksID",
                table: "LibrarySystemRuns",
                column: "BooksID");

            migrationBuilder.CreateIndex(
                name: "IX_LibrarySystemRuns_PersonID",
                table: "LibrarySystemRuns",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MaritalID",
                table: "Persons",
                column: "MaritalID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ReligionID",
                table: "Persons",
                column: "ReligionID");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SexID",
                table: "Persons",
                column: "SexID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibrarySystemRuns");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Maritals");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "Sexs");
        }
    }
}
