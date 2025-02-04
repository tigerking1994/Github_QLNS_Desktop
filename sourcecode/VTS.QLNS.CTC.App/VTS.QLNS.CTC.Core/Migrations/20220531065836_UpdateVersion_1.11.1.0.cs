using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "iID_LoaiNguonVonID",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.AddColumn<int>(
                name: "iID_LoaiNguonVonID",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DonViQuanLyID",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iID_MaDonViQuanLy",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.1.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_DonViQuanLyID",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "iID_MaDonViQuanLy",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "iID_LoaiNguonVonID",
                table: "VDT_KHV_PhanBoVon_ChiPhi");

            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiNguonVonID",
                table: "VDT_KHV_PhanBoVon_ChiPhi",
                nullable: true);
        }
    }
}
