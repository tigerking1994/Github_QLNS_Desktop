using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fLuyKeTUChuaThuHoiKhacNN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeTUChuaThuHoiKhacTN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeTUChuaThuHoiNN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeTUChuaThuHoiTN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeThanhToanNN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fLuyKeThanhToanTN",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fLuyKeTUChuaThuHoiKhacNN",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeTUChuaThuHoiKhacTN",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeTUChuaThuHoiNN",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeTUChuaThuHoiTN",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeThanhToanNN",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "fLuyKeThanhToanTN",
                table: "VDT_TT_DeNghiThanhToan");
        }
    }
}
