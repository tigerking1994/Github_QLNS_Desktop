using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11417 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iNamLamViec",
                table: "Tl_DM_NgayNghi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaLoaiChi",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sLuongChinh",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNS_LuongChinh",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNS_PCCV",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNS_PCTN",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNS_PCTNVK",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sPCCV",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sPCTN",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sPCTNVK",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.7_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamLamViec",
                table: "Tl_DM_NgayNghi");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DTC_PhanBoDuToanChi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sMaLoaiChi",
                table: "BH_DTC_DuToanChiTrenGiao_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sLuongChinh",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sNS_LuongChinh",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sNS_PCCV",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sNS_PCTN",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sNS_PCTNVK",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sPCCV",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sPCTN",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "sPCTNVK",
                table: "BH_DM_MucLucNganSach");
        }
    }
}
