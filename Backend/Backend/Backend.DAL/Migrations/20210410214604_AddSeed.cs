using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.DAL.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Login", "MiddleName", "PasswordHash" },
                values: new object[] { 1, 40, "admin@mail.com", "Admin", "Man", "Admin", "admin", "Admin", "21232f297a57a5a743894a0e4a801fc3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
