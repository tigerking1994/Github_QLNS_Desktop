using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11522 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sGhiChu",
                table: "BH_QTC_Quy_KCB_ChiTiet",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.2_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.2_budget_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.2_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sGhiChu",
                table: "BH_QTC_Quy_KCB_ChiTiet");
        }
    }
}
