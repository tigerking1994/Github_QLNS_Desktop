using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11204 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "STng1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STng2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STng3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STng1",
                table: "NS_DTDauNam_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "STng2",
                table: "NS_DTDauNam_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "STng3",
                table: "NS_DTDauNam_ChungTuChiTiet");
        }
    }
}
