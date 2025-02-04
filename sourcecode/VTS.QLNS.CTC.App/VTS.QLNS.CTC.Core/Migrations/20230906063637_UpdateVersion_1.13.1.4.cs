using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11314 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_LoaiDanhMucChi",
                table: "BH_DTC_PhanBoDuToanChi",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "iQuy",
                table: "BH_CP_ChungTu",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.4_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.4_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.4_social_insurance_3.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_LoaiDanhMucChi",
                table: "BH_DTC_PhanBoDuToanChi");

            migrationBuilder.DropColumn(
                name: "iQuy",
                table: "BH_CP_ChungTu");
        }
    }
}
