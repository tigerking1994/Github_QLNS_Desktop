using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;


namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/09_script_update_mlns_2023.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.4.2_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
