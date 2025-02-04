using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11339 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.9_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.3.9_budget_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
