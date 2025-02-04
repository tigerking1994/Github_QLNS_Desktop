using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11316 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.6_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.6_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
