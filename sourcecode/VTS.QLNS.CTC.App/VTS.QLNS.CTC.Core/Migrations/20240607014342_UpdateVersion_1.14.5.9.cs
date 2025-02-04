using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11459 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.9_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.5.9_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
