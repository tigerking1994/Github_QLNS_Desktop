using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11465 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fTienTruyLinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iSoNgayTruyLinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap",
                maxLength: 100,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.5_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.5_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.5_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.6.5_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fTienTruyLinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");

            migrationBuilder.DropColumn(
                name: "iSoNgayTruyLinh",
                table: "BH_QTC_Quy_CTCT_GiaiThichTroCap");
        }
    }
}
