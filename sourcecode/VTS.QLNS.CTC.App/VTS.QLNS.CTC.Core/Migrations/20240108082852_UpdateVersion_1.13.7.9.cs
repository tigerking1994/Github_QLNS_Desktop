using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11379 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.9_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.9_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.9_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
