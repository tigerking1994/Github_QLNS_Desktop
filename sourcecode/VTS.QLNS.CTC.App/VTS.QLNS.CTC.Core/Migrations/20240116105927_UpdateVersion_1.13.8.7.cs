using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11387 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_6.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_7.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.7_social_insurance_8.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
