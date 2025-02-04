using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11321 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "fTongTienLDHD_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iTongSoLDHD_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH",
                nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.1_salary.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.1_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.1_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.1_social_insurance_2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fTongTienLDHD_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "iTongSoLDHD_DeNghi",
                table: "BH_QTC_Nam_CheDoBHXH");
        }
    }
}
