using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11343 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "iID_DuAn_ChiPhiID",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_HopDong_ChiPhiID",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_QuyetDinhKhac_ChiPhiID",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.3_budget_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.3_budget_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.4.3_forex.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_DuAn_ChiPhiID",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "iID_HopDong_ChiPhiID",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "iID_QuyetDinhKhac_ChiPhiID",
                table: "NH_TT_ThanhToan");
        }
    }
}
