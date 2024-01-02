using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Idea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Idea");

            migrationBuilder.CreateTable(
                name: "Assembly",
                schema: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assembly", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concept",
                schema: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptAssemblies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssemblyId = table.Column<int>(type: "int", nullable: false),
                    ConceptId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptAssemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptGroup",
                schema: "Idea",
                columns: table => new
                {
                    ConceptId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptGroup", x => new { x.ConceptId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "ConceptIdea",
                schema: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConceptId = table.Column<int>(type: "int", nullable: false),
                    IdeaId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptIdea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupAssembly",
                schema: "Idea",
                columns: table => new
                {
                    AssemblyId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAssembly", x => new { x.AssemblyId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "Idea",
                schema: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThinkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Label",
                schema: "Idea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelAssembly",
                schema: "Idea",
                columns: table => new
                {
                    LabelId = table.Column<int>(type: "int", nullable: false),
                    AssemblyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelAssembly", x => new { x.LabelId, x.AssemblyId });
                });

            migrationBuilder.CreateTable(
                name: "LabelConcept",
                schema: "Idea",
                columns: table => new
                {
                    LabelId = table.Column<int>(type: "int", nullable: false),
                    ConceptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelConcept", x => new { x.LabelId, x.ConceptId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assembly",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "Concept",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "ConceptAssemblies");

            migrationBuilder.DropTable(
                name: "ConceptGroup",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "ConceptIdea",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "GroupAssembly",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "Idea",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "Label",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "LabelAssembly",
                schema: "Idea");

            migrationBuilder.DropTable(
                name: "LabelConcept",
                schema: "Idea");
        }
    }
}
