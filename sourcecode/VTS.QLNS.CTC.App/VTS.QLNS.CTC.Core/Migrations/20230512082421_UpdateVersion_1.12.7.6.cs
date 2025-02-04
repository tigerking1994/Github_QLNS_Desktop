using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11276 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sDonViNhanDuLieu",
                table: "NS_DT_ChungTu",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.6_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.7.6_budget.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sDonViNhanDuLieu",
                table: "NS_DT_ChungTu");
        }
    }
}
