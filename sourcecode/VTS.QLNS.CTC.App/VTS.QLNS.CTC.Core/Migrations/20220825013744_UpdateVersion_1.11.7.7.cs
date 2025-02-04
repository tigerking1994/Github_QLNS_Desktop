using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11177 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FGiaTriThanhToanNNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.DropColumn(
                name: "FGiaTriThanhToanTNDuocDuyet",
                table: "VDT_TT_DeNghiThanhToan");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.7.7.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
