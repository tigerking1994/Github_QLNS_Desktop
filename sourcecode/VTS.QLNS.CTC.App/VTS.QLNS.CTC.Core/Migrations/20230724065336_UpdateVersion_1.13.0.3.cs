using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iLoaiChi",
                table: "NS_BK_ChungTuChiTiet",
                maxLength: 10,
                nullable: true,
                defaultValueSql: "(1)");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.3_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.3_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.0.3_forex.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iLoaiChi",
                table: "NS_BK_ChungTuChiTiet");
        }
    }
}
