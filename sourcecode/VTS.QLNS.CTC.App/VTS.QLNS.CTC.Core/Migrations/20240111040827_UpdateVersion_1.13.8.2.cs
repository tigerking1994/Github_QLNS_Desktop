using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11382 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iNamCanCuDong",
                table: "TL_CanBo_CheDoBHXH",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTyLeThu",
                table: "BH_KHC_KCB",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.2_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.2_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.8.2_social_insurance_6.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iNamCanCuDong",
                table: "TL_CanBo_CheDoBHXH");

            migrationBuilder.DropColumn(
                name: "fTyLeThu",
                table: "BH_KHC_KCB");
        }
    }
}
