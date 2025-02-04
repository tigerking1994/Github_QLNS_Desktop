using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11127 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stng3",
                table: "TL_PhuCap_MLNS",
                newName: "sTNG3");

            migrationBuilder.RenameColumn(
                name: "Stng2",
                table: "TL_PhuCap_MLNS",
                newName: "sTNG2");

            migrationBuilder.RenameColumn(
                name: "Stng1",
                table: "TL_PhuCap_MLNS",
                newName: "sTNG1");

            migrationBuilder.RenameColumn(
                name: "Stng",
                table: "TL_PhuCap_MLNS",
                newName: "sTNG");

            migrationBuilder.AlterColumn<string>(
                name: "sTNG3",
                table: "TL_PhuCap_MLNS",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG2",
                table: "TL_PhuCap_MLNS",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG1",
                table: "TL_PhuCap_MLNS",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "sTNG",
                table: "TL_PhuCap_MLNS",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.2.7.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sTNG3",
                table: "TL_PhuCap_MLNS",
                newName: "Stng3");

            migrationBuilder.RenameColumn(
                name: "sTNG2",
                table: "TL_PhuCap_MLNS",
                newName: "Stng2");

            migrationBuilder.RenameColumn(
                name: "sTNG1",
                table: "TL_PhuCap_MLNS",
                newName: "Stng1");

            migrationBuilder.RenameColumn(
                name: "sTNG",
                table: "TL_PhuCap_MLNS",
                newName: "Stng");

            migrationBuilder.AlterColumn<string>(
                name: "Stng3",
                table: "TL_PhuCap_MLNS",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stng2",
                table: "TL_PhuCap_MLNS",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stng1",
                table: "TL_PhuCap_MLNS",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stng",
                table: "TL_PhuCap_MLNS",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
