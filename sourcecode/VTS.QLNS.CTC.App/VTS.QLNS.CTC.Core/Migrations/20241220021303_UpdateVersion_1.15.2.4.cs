using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11524 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fHeSoLayQuyLuong",
                table: "BH_DM_MucLucNganSach",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.4_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.4_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.4_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.4_social_insurance_ducbq1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fHeSoLayQuyLuong",
                table: "BH_DM_MucLucNganSach");
        }
    }
}
