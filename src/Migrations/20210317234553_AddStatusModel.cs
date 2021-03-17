using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutTracker.Migrations
{
    public partial class AddStatusModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "State" },
                values: new object[] { 1, "Ok" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
