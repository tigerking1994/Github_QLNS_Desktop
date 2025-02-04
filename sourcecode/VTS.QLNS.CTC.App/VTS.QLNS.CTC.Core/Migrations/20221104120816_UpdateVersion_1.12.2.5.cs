using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dNgayDeNghi",
                table: "NH_TH_TongHop",
                type: "datetime",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "iID_KHTT_NhiemVuChiID",
                table: "NH_TH_TongHop",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fGiaTriKH_BQP_VND",
                table: "NH_KHTongThe_NhiemVuChi",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fTongGiaTri_KHBQP_VND",
                table: "NH_KHTongThe",
                nullable: true,
                defaultValue: 0.0);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.2.5.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dNgayDeNghi",
                table: "NH_TH_TongHop");

            migrationBuilder.DropColumn(
                name: "iID_KHTT_NhiemVuChiID",
                table: "NH_TH_TongHop");

            migrationBuilder.DropColumn(
                name: "fGiaTriKH_BQP_VND",
                table: "NH_KHTongThe_NhiemVuChi");

            migrationBuilder.DropColumn(
                name: "fTongGiaTri_KHBQP_VND",
                table: "NH_KHTongThe");
        }
    }
}
