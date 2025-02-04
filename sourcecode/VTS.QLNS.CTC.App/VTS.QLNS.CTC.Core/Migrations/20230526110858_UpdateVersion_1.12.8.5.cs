using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11285 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.5_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.5_salary.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
