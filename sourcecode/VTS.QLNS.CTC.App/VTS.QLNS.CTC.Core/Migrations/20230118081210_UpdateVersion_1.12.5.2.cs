using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11252 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.2_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.5.2_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
