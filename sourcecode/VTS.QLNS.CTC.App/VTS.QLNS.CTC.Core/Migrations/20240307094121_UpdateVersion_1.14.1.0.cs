using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lang_nang_luong_cvd",
                table: "TL_DM_CanBo",
                newName: "lan_nang_luong_cvd");

            migrationBuilder.RenameColumn(
                name: "lang_nang_luong_cb",
                table: "TL_DM_CanBo",
                newName: "lan_nang_luong_cb");

            migrationBuilder.AddColumn<string>(
                name: "STongHop",
                table: "TL_DS_CapNhap_BangLuong",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHTN_NLD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHTN_NSD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHXH_NLD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHXH_NSD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHYT_NLD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLe_BHYT_NSD",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.0_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.0_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.0_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.0_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STongHop",
                table: "TL_DS_CapNhap_BangLuong");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHTN_NLD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHTN_NSD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHXH_NLD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHXH_NSD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHYT_NLD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.DropColumn(
                name: "fTyLe_BHYT_NSD",
                table: "BH_DM_MucLucNganSach");

            migrationBuilder.RenameColumn(
                name: "lan_nang_luong_cvd",
                table: "TL_DM_CanBo",
                newName: "lang_nang_luong_cvd");

            migrationBuilder.RenameColumn(
                name: "lan_nang_luong_cb",
                table: "TL_DM_CanBo",
                newName: "lang_nang_luong_cb");
        }
    }
}
