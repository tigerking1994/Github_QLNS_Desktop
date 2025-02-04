using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11208 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NH_DuToan_ChiPhi",
                table: "NH_DA_DuToan_ChiPhi");

            migrationBuilder.RenameColumn(
                name: "IThuocMenu",
                table: "NH_DA_KHLCNhaThau",
                newName: "iThuocMenu");

            migrationBuilder.RenameColumn(
                name: "ILoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau",
                newName: "iLoaiKHLCNT");

            migrationBuilder.AlterColumn<int>(
                name: "iThuocMenu",
                table: "NH_DA_KHLCNhaThau",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "iThuocMenu",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "iLoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "iLoaiKHLCNT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ILoai",
                table: "NH_DA_KHLCNhaThau",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "iLoai",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenChiPhi",
                table: "NH_DA_DuToan_ChiPhi",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NH_DA_DuToan_ChiPhi",
                table: "NH_DA_DuToan_ChiPhi",
                column: "ID");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.8.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NH_DA_DuToan_ChiPhi",
                table: "NH_DA_DuToan_ChiPhi");

            migrationBuilder.RenameColumn(
                name: "iThuocMenu",
                table: "NH_DA_KHLCNhaThau",
                newName: "IThuocMenu");

            migrationBuilder.RenameColumn(
                name: "iLoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau",
                newName: "ILoaiKHLCNT");

            migrationBuilder.AlterColumn<int>(
                name: "IThuocMenu",
                table: "NH_DA_KHLCNhaThau",
                type: "iThuocMenu",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ILoaiKHLCNT",
                table: "NH_DA_KHLCNhaThau",
                type: "iLoaiKHLCNT",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ILoai",
                table: "NH_DA_KHLCNhaThau",
                type: "iLoai",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTenChiPhi",
                table: "NH_DA_DuToan_ChiPhi",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NH_DuToan_ChiPhi",
                table: "NH_DA_DuToan_ChiPhi",
                column: "ID");
        }
    }
}
