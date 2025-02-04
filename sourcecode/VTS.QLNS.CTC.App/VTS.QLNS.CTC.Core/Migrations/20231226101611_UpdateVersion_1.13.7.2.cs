using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11372 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: true,
                oldClrType: typeof(int));
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.2_budget.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.2_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.2_social_insurance_3.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.7.2_social_insurance_4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "iNamLamViec",
                table: "BH_KHC_KCB_ChiTiet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
