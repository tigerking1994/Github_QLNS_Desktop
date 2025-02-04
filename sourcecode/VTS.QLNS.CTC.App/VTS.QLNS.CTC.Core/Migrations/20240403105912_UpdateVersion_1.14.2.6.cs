using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11426 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "tien_nang_luong",
                table: "TL_DM_ChucVu_NQ104",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay_bao_luu_cb",
                table: "TL_DM_CanBo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tien_luong_cb_cu",
                table: "TL_DM_CanBo",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.6_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.6_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.6_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tien_nang_luong",
                table: "TL_DM_ChucVu_NQ104");

            migrationBuilder.DropColumn(
                name: "ngay_bao_luu_cb",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "tien_luong_cb_cu",
                table: "TL_DM_CanBo");
        }
    }
}
