using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ILoai",
                table: "NH_DA_QDDauTu",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ILoai",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ILoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IThuocMenu",
                table: "NH_DA_KHLCNhaThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThuocMenu",
                table: "NH_DA_HopDong",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThuocMenu",
                table: "NH_DA_GoiThau",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoai",
                table: "NH_DA_DuToan",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoai",
                table: "NH_DA_DuAn",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iLoai",
                table: "NH_DA_ChuTruongDauTu",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.0.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ILoai",
                table: "NH_DA_QDDauTu");

            migrationBuilder.DropColumn(
                name: "ILoai",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "ILoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "IThuocMenu",
                table: "NH_DA_KHLCNhaThau");

            migrationBuilder.DropColumn(
                name: "iThuocMenu",
                table: "NH_DA_HopDong");

            migrationBuilder.DropColumn(
                name: "iThuocMenu",
                table: "NH_DA_GoiThau");

            migrationBuilder.DropColumn(
                name: "iLoai",
                table: "NH_DA_DuToan");

            migrationBuilder.DropColumn(
                name: "iLoai",
                table: "NH_DA_DuAn");

            migrationBuilder.DropColumn(
                name: "iLoai",
                table: "NH_DA_ChuTruongDauTu");
        }
    }
}
