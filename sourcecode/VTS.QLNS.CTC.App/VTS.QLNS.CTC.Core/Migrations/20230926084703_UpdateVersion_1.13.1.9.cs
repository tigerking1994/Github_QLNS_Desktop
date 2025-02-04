using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11319 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLanDieuChinh",
                table: "NS_QT_ChungTu",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.AddColumn<int>(
                name: "iLoaiChungTu",
                table: "NS_QT_ChungTu",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.9_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.9_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.9_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iLanDieuChinh",
                table: "NS_QT_ChungTu");

            migrationBuilder.DropColumn(
                name: "iLoaiChungTu",
                table: "NS_QT_ChungTu");

        }
    }
}
