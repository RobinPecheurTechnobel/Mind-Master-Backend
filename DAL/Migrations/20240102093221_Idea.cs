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
            //migrationBuilder.DropTable(
            //    name: "GroupThinker",
            //    schema: "User");
            //migrationBuilder.DropTable(
            //    name: "Thinker",
            //    schema: "User");

            //migrationBuilder.DropTable(
            //    name: "Group",
            //    schema: "User");


            migrationBuilder.EnsureSchema(
                name: "Idea");

            migrationBuilder.EnsureSchema(
                name: "User");

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
                name: "Group",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
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
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Thinker",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pseudo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupThinker",
                schema: "User",
                columns: table => new
                {
                    ThinkerId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    isOwner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupThinker", x => new { x.ThinkerId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupThinker_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "User",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupThinker_Thinker_ThinkerId",
                        column: x => x.ThinkerId,
                        principalSchema: "User",
                        principalTable: "Thinker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupThinker_GroupId",
                schema: "User",
                table: "GroupThinker",
                column: "GroupId");
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
                name: "GroupThinker",
                schema: "User");

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

            migrationBuilder.DropTable(
                name: "Group",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Thinker",
                schema: "User");
        }
    }
}
