using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11348 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTri",
                table: "TL_DM_CheDoBHXH",
                type: "numeric(17, 3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "fGiaTri",
                table: "TL_CanBo_CheDoBHXH",
                type: "numeric(17, 3)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoaiNguonNganSach",
                table: "NS_DTDauNam_ChungTu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KinhPhiQuanLy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KCB",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Quy_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KCB_QuanYDonVi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KinhPhiQuanLy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KPK",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.8_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fGiaTri",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "fGiaTri",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iLoaiNguonNganSach",
                table: "NS_DTDauNam_ChungTu");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KinhPhiQuanLy");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Quy_KCB");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Quy_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KCB_QuanYDonVi");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KinhPhiQuanLy");

            migrationBuilder.DropColumn(
                name: "sDSLNS",
                table: "BH_QTC_Nam_KPK");
        }
    }
}
