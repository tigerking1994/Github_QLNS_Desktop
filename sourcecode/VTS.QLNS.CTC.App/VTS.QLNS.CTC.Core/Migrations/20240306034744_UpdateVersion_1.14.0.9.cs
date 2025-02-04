using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11409 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.9_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.9_salary_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.9_salary_new_master.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.9_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
