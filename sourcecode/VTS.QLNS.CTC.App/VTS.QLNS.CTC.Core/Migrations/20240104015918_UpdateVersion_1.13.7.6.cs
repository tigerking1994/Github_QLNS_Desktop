using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11376 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.6_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.6_social_insurance_0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.6_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.6_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.6_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
