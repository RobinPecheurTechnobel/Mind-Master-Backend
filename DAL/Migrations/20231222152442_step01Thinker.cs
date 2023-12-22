using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class step01Thinker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.EnsureSchema(
                name: "User");
            migrationBuilder.DropTable("Account", "Account");
            migrationBuilder.DropTable("Thinker", "User");

            migrationBuilder.CreateTable(
                name: "Thinker",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pseudo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thinker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThinkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Thinker_ThinkerId",
                        column: x => x.ThinkerId,
                        principalSchema: "User",
                        principalTable: "Thinker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ThinkerId",
                schema: "Account",
                table: "Account",
                column: "ThinkerId");

            migrationBuilder.InsertData(
                table: "Thinker",
                columns: new[] { "LastName", "FirstName", "Pseudo", "Email" },
                values: new Object[,] {
                    { "DuMont", "Armin", "Admin", null},
                    { null, null, "User", null}
                },
                schema: "User"
             );
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Login", "Role", "HashPassword", "ThinkerId" },
                values: new Object[,] {
                    { "Admin", "Admin", "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w", "1"},
                    { "User", "User", "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w", "2"}
                },
                schema: "Account"
             );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Thinker",
                schema: "User");
        }
    }
}
