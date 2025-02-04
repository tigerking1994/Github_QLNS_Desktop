using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11517 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.7_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.7_budget_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
