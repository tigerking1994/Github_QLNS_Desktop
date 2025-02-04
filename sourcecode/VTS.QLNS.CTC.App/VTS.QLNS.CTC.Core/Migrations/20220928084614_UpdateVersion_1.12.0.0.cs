using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11200 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NH_KT_KhoiTaoCapPhat",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    bIsXoa = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    dNgayKhoiTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    iID_DonViID = table.Column<Guid>(nullable: true, defaultValueSql: "''"),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TiGiaID = table.Column<Guid>(nullable: true, defaultValueSql: "''"),
                    iID_TongHopID = table.Column<Guid>(nullable: true, defaultValueSql: "''"),
                    iNamKhoiTao = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNguoiXoa = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KT_KhoiTaoCapPhat", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_KT_KhoiTaoCapPhat_ChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fDeNghiQTNamNay_USD = table.Column<double>(nullable: true),
                    fDeNghiQTNamNay_VND = table.Column<double>(nullable: true),
                    fLuyKeKinhPhiDuocCap_USD = table.Column<double>(nullable: true),
                    fLuyKeKinhPhiDuocCap_VND = table.Column<double>(nullable: true),
                    fQTKinhPhiDuyetCacNamTruoc_USD = table.Column<double>(nullable: true),
                    fQTKinhPhiDuyetCacNamTruoc_VND = table.Column<double>(nullable: true),
                    iID_DuAnID = table.Column<Guid>(nullable: true),
                    iID_HopDongID = table.Column<Guid>(nullable: true),
                    iID_KhoiTaoCapPhatID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_KT_KhoiTaoCapPhat_ChiTiet", x => x.ID);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.0.0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NH_KT_KhoiTaoCapPhat");

            migrationBuilder.DropTable(
                name: "NH_KT_KhoiTaoCapPhat_ChiTiet");
        }
    }
}
