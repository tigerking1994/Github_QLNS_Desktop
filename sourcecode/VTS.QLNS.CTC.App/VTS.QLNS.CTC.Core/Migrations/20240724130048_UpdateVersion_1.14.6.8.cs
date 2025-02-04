using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11468 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fKhungNganSachDuocDuyet",
                table: "NS_SKT_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fSoNganhPhanCap",
                table: "NS_SKT_ChungTuChiTiet",
                nullable: false,
                defaultValue: 0.0);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.8_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.8_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.8_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fKhungNganSachDuocDuyet",
                table: "NS_SKT_ChungTuChiTiet");

            migrationBuilder.DropColumn(
                name: "fSoNganhPhanCap",
                table: "NS_SKT_ChungTuChiTiet");
        }
    }
}
