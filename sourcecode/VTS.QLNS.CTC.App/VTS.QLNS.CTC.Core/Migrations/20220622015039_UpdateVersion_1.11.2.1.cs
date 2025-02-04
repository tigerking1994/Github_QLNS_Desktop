using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SMoTa",
                table: "VDT_ThongTri",
                newName: "sMoTa");

            migrationBuilder.AlterColumn<string>(
                name: "sMoTa",
                table: "VDT_ThongTri",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.2.1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sMoTa",
                table: "VDT_ThongTri",
                newName: "SMoTa");

            migrationBuilder.AlterColumn<string>(
                name: "SMoTa",
                table: "VDT_ThongTri",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
