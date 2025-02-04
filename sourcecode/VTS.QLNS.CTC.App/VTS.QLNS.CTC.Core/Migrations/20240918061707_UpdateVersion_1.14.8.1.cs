using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11481 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.8.1_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.8.1_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.8.1_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.8.1_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
