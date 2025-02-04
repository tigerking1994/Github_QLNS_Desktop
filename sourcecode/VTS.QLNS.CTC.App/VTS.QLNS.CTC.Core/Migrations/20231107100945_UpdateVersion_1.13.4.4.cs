using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11344 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.4_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.4_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.4_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
