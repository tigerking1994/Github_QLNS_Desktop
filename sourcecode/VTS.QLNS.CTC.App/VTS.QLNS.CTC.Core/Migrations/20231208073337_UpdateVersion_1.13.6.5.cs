using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11365 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.AddColumn<double>(
                name: "fTienLuongThangDongBHXH",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.5_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fTienLuongThangDongBHXH",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "sMaCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.AddColumn<int>(
                name: "iCapBac",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                nullable: false,
                defaultValue: 0);
        }
    }
}
