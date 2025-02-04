using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11361 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.1_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.1_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
