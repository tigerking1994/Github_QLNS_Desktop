using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11190 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongSoWithDonViTinh",
                table: "VDT_TT_PheDuyetThanhToan_ChiTiet");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.9.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TongSoWithDonViTinh",
                table: "VDT_TT_PheDuyetThanhToan_ChiTiet",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
