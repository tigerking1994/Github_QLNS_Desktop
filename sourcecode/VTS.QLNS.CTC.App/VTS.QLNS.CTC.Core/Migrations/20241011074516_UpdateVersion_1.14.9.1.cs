using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11491 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_PhanBoVon_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTonKhoanTaiDonViDC",
                table: "VDT_KHV_PhanBoVon_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_PhanBoVon",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_KeHoachVonUng_ChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.1_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.1_investment.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_PhanBoVon_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fTonKhoanTaiDonViDC",
                table: "VDT_KHV_PhanBoVon_ChiTiet");

            migrationBuilder.DropColumn(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_PhanBoVon");

            migrationBuilder.DropColumn(
                name: "fTonKhoanTaiDonVi",
                table: "VDT_KHV_KeHoachVonUng_ChiTiet");
        }
    }
}
