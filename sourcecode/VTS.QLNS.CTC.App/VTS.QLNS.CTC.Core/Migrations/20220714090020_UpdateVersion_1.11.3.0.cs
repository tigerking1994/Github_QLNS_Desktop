using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11130 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fChuyenNamSau_ChuaCap",
                table: "NS_QT_ChungTuChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fChuyenNamSau_DaCap",
                table: "NS_QT_ChungTuChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.3.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fChuyenNamSau_ChuaCap",
                table: "NS_QT_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fChuyenNamSau_DaCap",
                table: "NS_QT_ChungTuChiTiet");
        }
    }
}
