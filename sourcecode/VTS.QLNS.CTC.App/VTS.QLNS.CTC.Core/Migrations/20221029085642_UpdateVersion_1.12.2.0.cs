using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11220 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sXauNoiMaGoc",
                table: "NS_DTDauNam_PhanCap",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.2.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sXauNoiMaGoc",
                table: "NS_DTDauNam_PhanCap");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldDefaultValueSql: "('')");
        }
    }
}
