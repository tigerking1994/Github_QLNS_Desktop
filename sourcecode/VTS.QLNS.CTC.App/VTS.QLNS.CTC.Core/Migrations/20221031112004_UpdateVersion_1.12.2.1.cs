using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.2.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: false,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");
        }
    }
}
