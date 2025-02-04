using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11217 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STng3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "sTNG3");

            migrationBuilder.RenameColumn(
                name: "STng2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "sTNG2");

            migrationBuilder.RenameColumn(
                name: "STng1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "sTNG1");

            migrationBuilder.AddColumn<bool>(
                name: "bIsThueThang",
                table: "TL_DM_ThueThuNhapCaNhan",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                maxLength: 50,
                nullable: true,
                defaultValueSql: "('')",
                oldClrType: typeof(string),
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.1.7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bIsThueThang",
                table: "TL_DM_ThueThuNhapCaNhan");

            migrationBuilder.RenameColumn(
                name: "sTNG3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "STng3");

            migrationBuilder.RenameColumn(
                name: "sTNG2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "STng2");

            migrationBuilder.RenameColumn(
                name: "sTNG1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                newName: "STng1");

            migrationBuilder.AlterColumn<string>(
                name: "STng3",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "STng2",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");

            migrationBuilder.AlterColumn<string>(
                name: "STng1",
                table: "NS_DTDauNam_ChungTuChiTiet",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValueSql: "('')");
        }
    }
}
