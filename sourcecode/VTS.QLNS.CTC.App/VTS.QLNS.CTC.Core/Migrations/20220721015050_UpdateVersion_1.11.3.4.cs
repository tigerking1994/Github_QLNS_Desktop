using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11134 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "bHuongThangTnn",
                table: "TL_DM_CanBo",
                nullable: true,
                defaultValueSql: "((0))");

            migrationBuilder.CreateIndex(
                name: "IX_NH_DM_LoaiCongTrinh_iID_Parent",
                table: "NH_DM_LoaiCongTrinh",
                column: "iID_Parent");

            migrationBuilder.AddForeignKey(
                name: "FK_NH_DM_LoaiCongTrinh_NH_DM_LoaiCongTrinh_iID_Parent",
                table: "NH_DM_LoaiCongTrinh",
                column: "iID_Parent",
                principalTable: "NH_DM_LoaiCongTrinh",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.11.3.4.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NH_DM_LoaiCongTrinh_NH_DM_LoaiCongTrinh_iID_Parent",
                table: "NH_DM_LoaiCongTrinh");

            migrationBuilder.DropIndex(
                name: "IX_NH_DM_LoaiCongTrinh_iID_Parent",
                table: "NH_DM_LoaiCongTrinh");

            migrationBuilder.DropColumn(
                name: "bHuongThangTnn",
                table: "TL_DM_CanBo");
        }
    }
}
