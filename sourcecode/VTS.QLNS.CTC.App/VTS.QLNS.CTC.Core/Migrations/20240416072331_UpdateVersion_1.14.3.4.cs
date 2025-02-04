using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11434 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.4_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.4_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.4_salary_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.3.4_social_insurance_1.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
