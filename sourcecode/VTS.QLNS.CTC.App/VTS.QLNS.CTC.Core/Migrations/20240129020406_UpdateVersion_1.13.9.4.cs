using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11394 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bHangChaDuToanDieuChinh",
                table: "BH_DM_MucLucNganSach",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sDuToanDieuChinhChiTietToi",
                table: "BH_DM_MucLucNganSach",
                maxLength: 50,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.4_social_insurance_0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.4_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.4_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bHangChaDuToanDieuChinh",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sDuToanDieuChinhChiTietToi",
                table: "BH_DM_MucLucNganSach");
        }
    }
}
