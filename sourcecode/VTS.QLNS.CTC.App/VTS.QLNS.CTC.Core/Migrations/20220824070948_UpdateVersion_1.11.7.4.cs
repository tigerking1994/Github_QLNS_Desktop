using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11174 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FGiaTriThanhToanNNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FGiaTriThanhToanTNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FGiaTriThanhToanNNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "FGiaTriThanhToanTNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan");
        }
    }
}
