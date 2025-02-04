using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11485 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bCoDinhMuc",
                table: "NS_SKT_MucLuc",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fMucTienPhanBo",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.8.5_salary_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bCoDinhMuc",
                table: "NS_SKT_MucLuc");

            migrationBuilder.DropColumn(
                name: "fMucTienPhanBo",
                table: "NS_DTDauNam_ChungTuChiTiet");
        }
    }
}
