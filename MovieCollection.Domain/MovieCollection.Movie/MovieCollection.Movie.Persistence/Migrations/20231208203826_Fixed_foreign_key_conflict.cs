using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCollection.Movie.Persistence.Migrations
{
    public partial class Fixed_foreign_key_conflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Collection_CollectionId1",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Movie_CollectionId",
                table: "MovieCollection");

            migrationBuilder.DropIndex(
                name: "IX_MovieCollection_CollectionId1",
                table: "MovieCollection");

            migrationBuilder.DropColumn(
                name: "CollectionId1",
                table: "MovieCollection");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 8, 21, 38, 26, 161, DateTimeKind.Local).AddTicks(5712));

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_MovieId",
                table: "MovieCollection",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Collection_CollectionId",
                table: "MovieCollection",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Collection_CollectionId",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection");

            migrationBuilder.DropIndex(
                name: "IX_MovieCollection_MovieId",
                table: "MovieCollection");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId1",
                table: "MovieCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 12, 8, 14, 57, 16, 631, DateTimeKind.Local).AddTicks(9316));

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollection_CollectionId1",
                table: "MovieCollection",
                column: "CollectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Collection_CollectionId1",
                table: "MovieCollection",
                column: "CollectionId1",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Movie_CollectionId",
                table: "MovieCollection",
                column: "CollectionId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
