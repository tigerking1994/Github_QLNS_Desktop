using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11310 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_DTTM_BHYT_ThanNhan_PhanBo",
                columns: table => new
                {
                    iID_DTTM_BHYT_ThanNhan_PhanBo = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    iLoaiDuToan = table.Column<int>(nullable: false),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sDSLNS = table.Column<string>(nullable: true),
                    sDS_DotNhan = table.Column<string>(nullable: true),
                    sDS_IDMaDonVi = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTTM_BHYT_ThanNhan_PhanBo", x => x.iID_DTTM_BHYT_ThanNhan_PhanBo)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet",
                columns: table => new
                {
                    iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    iID_DTTM_BHYT_ThanNhan = table.Column<Guid>(nullable: false),
                    iID_DTTM_BHYT_ThanNhan_PhanBo = table.Column<Guid>(nullable: false),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet", x => x.iID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_DTTM_BHYT_Nhan_PhanBo_Map",
                columns: table => new
                {
                    iID_DTTM_BHYTNhanPhanBoMap = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    iID_DTTM_BHYT_NhanPhanBo = table.Column<Guid>(nullable: false),
                    iID_DTTM_BHYT_PhanBo = table.Column<Guid>(nullable: false),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DTTM_BHYT_Nhan_PhanBo_Map", x => x.iID_DTTM_BHYTNhanPhanBoMap)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.0_social_insurance.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DTTM_BHYT_ThanNhan_PhanBo");

            migrationBuilder.DropTable(
                name: "BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_DTTM_BHYT_Nhan_PhanBo_Map");
        }
    }
}
