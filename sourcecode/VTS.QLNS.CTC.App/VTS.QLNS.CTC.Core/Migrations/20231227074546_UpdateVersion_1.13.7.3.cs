using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11373 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.3_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.3_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.3_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
