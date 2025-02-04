using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11495 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSent",
                table: "NS_DC_ChungTu",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.5_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.5_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.5_investment_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.5_investment_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.9.5_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSent",
                table: "NS_DC_ChungTu");
        }
    }
}
