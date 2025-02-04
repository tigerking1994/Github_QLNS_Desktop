using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11448 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "NS_SKT_ChungTu",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.8_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.8_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.4.8_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "iID_MaDonVi",
                table: "NS_SKT_ChungTu",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
