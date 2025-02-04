using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11395 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sGiaiThichChenhLech",
                table: "BH_ThamDinhQuyetToan_ChungTu",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "bHangChaDuToanDieuChinh",
                table: "BH_DM_MucLucNganSach",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "iLoaiKinhPhi",
                table: "BH_CP_CapBoSung_KCB_BHYT",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.5_social_insurance_7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sGiaiThichChenhLech",
                table: "BH_ThamDinhQuyetToan_ChungTu");

            migrationBuilder.DropColumn(
                name: "iLoaiKinhPhi",
                table: "BH_CP_CapBoSung_KCB_BHYT");

            migrationBuilder.AlterColumn<bool>(
                name: "bHangChaDuToanDieuChinh",
                table: "BH_DM_MucLucNganSach",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
