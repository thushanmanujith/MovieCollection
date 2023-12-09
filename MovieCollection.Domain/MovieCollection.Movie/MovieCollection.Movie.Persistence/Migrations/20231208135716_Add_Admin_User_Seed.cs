using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCollection.Movie.Persistence.Migrations
{
    public partial class Add_Admin_User_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                table: "User",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedOn", "Email", "FirstName", "HashedPassword", "IsActive", "LastLoginDate", "LastName", "UserRole" },
                values: new object[] { 1, new DateTime(2023, 12, 8, 14, 57, 16, 631, DateTimeKind.Local).AddTicks(9316), "test@mail.com", "Super", "AQAAAAEAACcQAAAAEAap7bv4XkwO9GMc9E19yA5qcnHJYwttBDlZmUODzn/h2Bx6DQOl5VMOg09am5cAWA==", false, null, "Admin", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                table: "User",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
