using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11264 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.6.4_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.6.4_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
