using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11362 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iNam",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iThang",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.2_forex.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.2_investment.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.6.2_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNam",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iThang",
                table: "TL_CanBo_CheDoBHXH");
        }
    }
}
