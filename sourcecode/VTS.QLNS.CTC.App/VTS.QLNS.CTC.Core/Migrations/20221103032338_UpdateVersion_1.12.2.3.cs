using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11223 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                nullable: true,
                oldClrType: typeof(Guid));

            //migrationBuilder.AddColumn<int>(
            //    name: "iCoQuanThanhToan",
            //    table: "NH_TH_TongHop",
            //    nullable: true,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "iID_KHTongTheID",
            //    table: "NH_NhuCauChiQuy",
            //    nullable: true);
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.2.3.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "iID_CTDTDauNam",
                table: "NS_DTDauNam_ChungTuChiTiet_CanCu",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            //migrationBuilder.DropColumn(
            //    name: "iCoQuanThanhToan",
            //    table: "NH_TH_TongHop");

            //migrationBuilder.DropColumn(
            //    name: "iID_KHTongTheID",
            //    table: "NH_NhuCauChiQuy");
        }
    }
}
