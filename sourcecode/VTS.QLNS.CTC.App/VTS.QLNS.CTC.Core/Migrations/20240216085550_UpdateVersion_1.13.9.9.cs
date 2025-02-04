using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11399 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB",
                maxLength: 50,
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.9_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sQuyNamMoTa",
                table: "BH_QTC_CapKinhPhi_KCB");
        }
    }
}
