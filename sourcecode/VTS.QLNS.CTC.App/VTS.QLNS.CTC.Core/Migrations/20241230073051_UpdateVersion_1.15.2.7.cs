using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11527 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_DTC_DuToanChiTrenGiao_ChiTiet_XNM",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fTuChi = table.Column<double>(nullable: true),
                    iID_DTC_DuToanChiTrenGiao = table.Column<Guid>(nullable: false),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTC_DuToanChiTrenGiao_ChiTiet_XNM", x => x.iID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.7_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.7_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.7_budget_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.15.2.7_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTC_DuToanChiTrenGiao_ChiTiet_XNM");
        }
    }
}
