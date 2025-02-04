using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11403 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.3_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.3_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.14.0.3_social_insurance.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
