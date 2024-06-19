using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorelloAPI.Migrations
{
    public partial class CreatedSocialsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(8999));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(9000));

            migrationBuilder.InsertData(
                table: "Socials",
                columns: new[] { "Id", "CreatedDate", "Icon", "Name", "SoftDeleted", "URL" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(9089), "logo.png", "Twitter", false, "https://x.com/" },
                    { 2, new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(9091), "logo.png", "Instagram", false, "https://www.instagram.com/" },
                    { 3, new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(9092), "logo.png", "Tumblr", false, "https://www.tumblr.com/" },
                    { 4, new DateTime(2024, 6, 19, 17, 17, 3, 873, DateTimeKind.Local).AddTicks(9093), "logo.png", "Pinterest", false, "https://www.pinterest.com/" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 16, 33, 2, 701, DateTimeKind.Local).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 16, 33, 2, 701, DateTimeKind.Local).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 16, 33, 2, 701, DateTimeKind.Local).AddTicks(2845));
        }
    }
}
