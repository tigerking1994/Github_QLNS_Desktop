using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11402 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.2_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
