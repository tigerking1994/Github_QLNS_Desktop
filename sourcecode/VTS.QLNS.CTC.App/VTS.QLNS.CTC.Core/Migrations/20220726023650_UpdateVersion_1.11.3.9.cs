using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11139 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "bHuongThangTnn",
                table: "TL_DM_CanBo",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValueSql: "((0))");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.3.9.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "bHuongThangTnn",
                table: "TL_DM_CanBo",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
