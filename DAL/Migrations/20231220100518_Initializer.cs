using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initializer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Login", "Role", "HashPassword" },
                values: new Object[,] {
                    { "Admin", "Admin", "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w"},
                    { "User", "User", "$argon2id$v=19$m=16,t=2,p=1$VHcwQnJAMW5DM2xscw$EeOAw2dw8t6VpxLj+ciZP9bZcDkp5FpttLEHUmqG/7TtrirM2NsvWLoytunM7AK6Uoc9m/UWQW2nGU8aztqpTu9o4ZJ42dzM0AvkLQFQJGZFU4seLEHLKi0ii1ZxC2URuVNe+w"}
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
        }
    }
}
