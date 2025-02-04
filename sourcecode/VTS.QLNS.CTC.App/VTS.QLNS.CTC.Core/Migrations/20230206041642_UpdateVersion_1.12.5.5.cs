using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11255 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoaiDVBanHanh1",
                table: "DM_ChuKy",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoaiDVBanHanh2",
                table: "DM_ChuKy",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDVBanHanh1",
                table: "DM_ChuKy",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDVBanHanh2",
                table: "DM_ChuKy",
                maxLength: 100,
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.5_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.5_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoaiDVBanHanh1",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "LoaiDVBanHanh2",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "TenDVBanHanh1",
                table: "DM_ChuKy");

            migrationBuilder.DropColumn(
                name: "TenDVBanHanh2",
                table: "DM_ChuKy");
        }
    }
}
