using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sMaChiPhi",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sMaOrder",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sNoiDung",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VDT_DM_DuToanChi",
                columns: table => new
                {
                    iID_DuToanChi = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    bHangCha = table.Column<bool>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DuToanChi_Parent = table.Column<Guid>(nullable: true),
                    iSoLanSua = table.Column<int>(nullable: true),
                    iThuTu = table.Column<int>(nullable: false),
                    sID_MaNguoiDungSua = table.Column<string>(maxLength: 200, nullable: true),
                    sID_MaNguoiDungTao = table.Column<string>(maxLength: 200, nullable: true),
                    sIPSua = table.Column<string>(maxLength: 20, nullable: true),
                    sMaDuToanChi = table.Column<string>(maxLength: 50, nullable: false),
                    sMoTa = table.Column<string>(maxLength: 500, nullable: true),
                    sTenDuToanChi = table.Column<string>(maxLength: 300, nullable: false),
                    sTenVietTat = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VDT_DM_DuToanChi", x => x.iID_DuToanChi);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VDT_DM_DuToanChi");

            migrationBuilder.DropColumn(
                name: "sMaChiPhi",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sMaOrder",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet");

            migrationBuilder.DropColumn(
                name: "sNoiDung",
                table: "VDT_KHV_PhanBoVon_ChiPhi_ChiTiet");
        }
    }
}
