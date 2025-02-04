using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fThuhoiTamUngPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fThuhoiTamUngPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTraDonViThuHuongPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTraDonViThuHuongPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fThuhoiTamUngPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fThuhoiTamUngPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTongPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTraDonViThuHuongPheDuyet_BangSo_USD",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fTraDonViThuHuongPheDuyet_BangSo_VND",
                table: "NH_TT_ThanhToan");
        }
    }
}
