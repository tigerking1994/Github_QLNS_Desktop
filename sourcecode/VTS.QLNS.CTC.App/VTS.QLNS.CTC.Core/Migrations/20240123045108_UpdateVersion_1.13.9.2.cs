using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11392 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HS_TroCapOmDau",
                table: "TL_DM_CapBac",
                type: "numeric(15, 4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaGiaiThich",
                table: "BH_QTT_MucLucGiaiThich",
                maxLength: 50,
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.2_social_insurance_0.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.2_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.2_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.2_social_insurance_4.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.9.2_social_insurance_5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HS_TroCapOmDau",
                table: "TL_DM_CapBac");

            migrationBuilder.DropColumn(
                name: "sMaGiaiThich",
                table: "BH_QTT_MucLucGiaiThich");
        }
    }
}
