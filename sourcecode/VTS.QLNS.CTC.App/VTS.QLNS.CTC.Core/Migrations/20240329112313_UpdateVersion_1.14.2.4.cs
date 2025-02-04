using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11424 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LCS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.2.4_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LCS",
                table: "BH_QTT_BHXH_ChungTu_ChiTiet",
                nullable: true);
        }
    }
}
