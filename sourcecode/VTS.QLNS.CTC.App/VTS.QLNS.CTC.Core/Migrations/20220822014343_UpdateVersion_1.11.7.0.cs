using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11170 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "loaiCoQuanTaiChinh",
                table: "VDT_TT_DeNghiThanhToan",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.7.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loaiCoQuanTaiChinh",
                table: "VDT_TT_DeNghiThanhToan");
        }
    }
}
