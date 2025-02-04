using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11411 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "lan_nang_luong_cvd",
                table: "TL_DM_CanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "lan_nang_luong_cb",
                table: "TL_DM_CanBo",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.1_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.1.1_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "lan_nang_luong_cvd",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lan_nang_luong_cb",
                table: "TL_DM_CanBo",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
