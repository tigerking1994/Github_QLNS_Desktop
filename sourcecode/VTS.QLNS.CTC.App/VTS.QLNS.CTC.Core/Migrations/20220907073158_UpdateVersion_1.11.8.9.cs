using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11189 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TongSoWithDonViTinh",
                table: "VDT_TT_PheDuyetThanhToan_ChiTiet",
                nullable: true,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongSoWithDonViTinh",
                table: "VDT_TT_PheDuyetThanhToan_ChiTiet");
        }
    }
}
