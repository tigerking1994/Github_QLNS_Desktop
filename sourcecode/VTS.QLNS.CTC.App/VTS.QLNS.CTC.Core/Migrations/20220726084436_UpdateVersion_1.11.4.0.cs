using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11140 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NH_DM_LoaiCongTrinh_NH_DM_LoaiCongTrinh_iID_Parent",
                table: "NH_DM_LoaiCongTrinh");

            migrationBuilder.DropForeignKey(
                name: "FK_NH_DA_HopDong_NH_KHTongThe_NhiemVuChi_iID_KHTongThe_NhiemVuChiID",
                table: "NH_DA_HopDong");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.4.0.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
