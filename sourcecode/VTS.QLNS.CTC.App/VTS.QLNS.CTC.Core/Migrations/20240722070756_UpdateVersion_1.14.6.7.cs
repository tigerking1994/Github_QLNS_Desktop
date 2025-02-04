using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11467 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sLoaiTruyLinh",
                table: "TL_DM_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fDuToanChuyenNamSau",
                table: "NS_DC_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaSoDVQHNS",
                table: "DonVi",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaSoKBNN",
                table: "DonVi",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sLoaiThu",
                table: "BH_QTT_BHXH_CTCT_GiaiThich",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ILock",
                table: "BH_DM_ThamDinhQuyetToan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.7_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.7_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sLoaiTruyLinh",
                table: "TL_DM_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "fDuToanChuyenNamSau",
                table: "NS_DC_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "sMaSoDVQHNS",
                table: "DonVi");

            migrationBuilder.DropColumn(
                name: "sMaSoKBNN",
                table: "DonVi");

            migrationBuilder.DropColumn(
                name: "sLoaiThu",
                table: "BH_QTT_BHXH_CTCT_GiaiThich");

            migrationBuilder.DropColumn(
                name: "ILock",
                table: "BH_DM_ThamDinhQuyetToan");
        }
    }
}
