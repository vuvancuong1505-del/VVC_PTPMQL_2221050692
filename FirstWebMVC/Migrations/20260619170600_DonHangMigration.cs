using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class DonHangMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaDonHang = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    NgayDat = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TongTien = table.Column<decimal>(type: "TEXT", nullable: false),
                    StudentCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangId);
                    table.ForeignKey(
                        name: "FK_DonHangs_Students_StudentCode",
                        column: x => x.StudentCode,
                        principalTable: "Students",
                        principalColumn: "StudentCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_StudentCode",
                table: "DonHangs",
                column: "StudentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangs");
        }
    }
}
