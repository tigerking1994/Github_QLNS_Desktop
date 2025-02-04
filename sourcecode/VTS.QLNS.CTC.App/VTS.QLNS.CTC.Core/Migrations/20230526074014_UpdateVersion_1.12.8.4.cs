using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11284 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // build lai 8.1 cho mot so db loi
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.8.1_budget.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
