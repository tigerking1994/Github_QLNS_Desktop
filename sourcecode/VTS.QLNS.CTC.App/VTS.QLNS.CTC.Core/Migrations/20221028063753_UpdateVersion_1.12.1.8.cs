using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_PhanCap",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");

            migrationBuilder.AddColumn<string>(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.1.8.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_PhanCap");

            migrationBuilder.DropColumn(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu");

            migrationBuilder.DropColumn(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu");

            migrationBuilder.DropColumn(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu");

            migrationBuilder.DropColumn(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu");
        }
    }
}
