using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11421 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ty_le_huong_nn",
                table: "TL_DM_CanBo",
                type: "numeric(16, 4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNS_HSBL",
                table: "BH_DM_MucLucNganSach",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_salary_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_salary_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_salary_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.1_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ty_le_huong_nn",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "sNS_HSBL",
                table: "BH_DM_MucLucNganSach");
        }
    }
}
