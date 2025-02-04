using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11518 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fChuyenNamSau_ChuaCap",
                table: "NS_QT_ChungTuChiTiet");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.8_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.8_budget_5.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.1.8_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fChuyenNamSau_ChuaCap",
                table: "NS_QT_ChungTuChiTiet",
                nullable: true);
        }
    }
}
