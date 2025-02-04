using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11311 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_CP_ChungTu",
                columns: table => new
                {
                    iID_CP_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: true),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienDaCap = table.Column<double>(nullable: true),
                    fTienDuToan = table.Column<double>(nullable: true),
                    fTienKeHoachCap = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_LoaiCap = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTenLoaiCap = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_ChungTu", x => x.iID_CP_ChungTu);
                });

            migrationBuilder.CreateTable(
                name: "BH_CP_ChungTu_ChiTiet",
                columns: table => new
                {
                    iID_CP_ChungTu_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienDaCap = table.Column<double>(nullable: true),
                    fTienDuToan = table.Column<double>(nullable: true),
                    fTienKeHoachCap = table.Column<double>(nullable: true),
                    iID_CP_ChungTu = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_ChungTu_ChiTiet", x => x.iID_CP_ChungTu_ChiTiet);
                });
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.0_social_insurance.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.0_social_insurance_1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_CP_ChungTu");

            migrationBuilder.DropTable(
                name: "BH_CP_ChungTu_ChiTiet");
        }
    }
}
