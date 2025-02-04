using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11279 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLoaiNguonNganSach",
                table: "NS_SKT_ChungTu",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.9_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.9_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iLoaiNguonNganSach",
                table: "NS_SKT_ChungTu");
        }
    }
}
