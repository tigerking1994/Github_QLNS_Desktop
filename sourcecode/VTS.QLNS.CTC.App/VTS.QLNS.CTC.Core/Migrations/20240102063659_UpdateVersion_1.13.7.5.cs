using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11375 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bDaTongHop",
                table: "BH_DTC_DieuChinhDuToanChi",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DTC_DieuChinhDuToanChi",
                maxLength: 50,
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.5_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.5_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.5_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.5_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bDaTongHop",
                table: "BH_DTC_DieuChinhDuToanChi");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DTC_DieuChinhDuToanChi");
        }
    }
}
