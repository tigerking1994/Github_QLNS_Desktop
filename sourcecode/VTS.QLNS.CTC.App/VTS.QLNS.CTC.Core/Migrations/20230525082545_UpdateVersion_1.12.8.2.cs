using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11282 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // up lai ban 8.1 budget
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.1_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.2_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
