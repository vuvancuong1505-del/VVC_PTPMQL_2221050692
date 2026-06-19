using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class DonHangKhachHangRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHangs_Students_StudentCode",
                table: "DonHangs");

            migrationBuilder.DropIndex(
                name: "IX_DonHangs_StudentCode",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "DonHangs");

            migrationBuilder.AddColumn<int>(
                name: "KhachHangId",
                table: "DonHangs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhachHangId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenKhachHang = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhachHangId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_KhachHangId",
                table: "DonHangs",
                column: "KhachHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHangs_KhachHangs_KhachHangId",
                table: "DonHangs",
                column: "KhachHangId",
                principalTable: "KhachHangs",
                principalColumn: "KhachHangId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHangs_KhachHangs_KhachHangId",
                table: "DonHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_DonHangs_KhachHangId",
                table: "DonHangs");

            migrationBuilder.DropColumn(
                name: "KhachHangId",
                table: "DonHangs");

            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "DonHangs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_StudentCode",
                table: "DonHangs",
                column: "StudentCode");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHangs_Students_StudentCode",
                table: "DonHangs",
                column: "StudentCode",
                principalTable: "Students",
                principalColumn: "StudentCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
