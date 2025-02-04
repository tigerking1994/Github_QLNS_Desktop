using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11146 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bHuongThangTnn",
                table: "TL_DM_CanBo",
                newName: "bKhongTinhNTN");

            migrationBuilder.AlterColumn<string>(
                name: "KyHieuCha",
                table: "NS_SKT_MucLuc",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24,
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.4.6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bKhongTinhNTN",
                table: "TL_DM_CanBo",
                newName: "bHuongThangTnn");

            migrationBuilder.AlterColumn<string>(
                name: "KyHieuCha",
                table: "NS_SKT_MucLuc",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
