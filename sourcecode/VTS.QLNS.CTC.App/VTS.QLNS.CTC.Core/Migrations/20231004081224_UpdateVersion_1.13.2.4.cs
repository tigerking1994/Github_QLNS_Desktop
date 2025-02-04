using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11324 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fDeNghi_ChuyenNamSau",
                table: "NS_QT_ChungTuChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_budget_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_budget_report.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.4_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fDeNghi_ChuyenNamSau",
                table: "NS_QT_ChungTuChiTiet");
        }
    }
}
