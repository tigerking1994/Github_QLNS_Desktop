using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iID_DonVi",
                table: "BH_CP_ChungTu");

            migrationBuilder.RenameColumn(
                name: "sTenLoaiCap",
                table: "BH_CP_ChungTu",
                newName: "sTongHop");

            migrationBuilder.RenameColumn(
                name: "iID_MaDonVi",
                table: "BH_CP_ChungTu",
                newName: "sID_MaDonVi");

            migrationBuilder.AlterColumn<bool>(
                name: "bIsKhoa",
                table: "BH_CP_ChungTu",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_TongHop",
                table: "BH_CP_ChungTu",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "iLoaiTongHop",
                table: "BH_CP_ChungTu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BH_CP_CapBoSung_KCB_BHYT",
                columns: table => new
                {
                    iID_CTCapPhatBS = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongDaCapUng = table.Column<double>(nullable: true),
                    fTongDaQuyetToan = table.Column<double>(nullable: true),
                    fTongSoCapBoSung = table.Column<double>(nullable: true),
                    fTongThuaThieu = table.Column<double>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: true),
                    iNamChungTu = table.Column<int>(nullable: true),
                    iQuy = table.Column<int>(nullable: true),
                    iSoChungTuIndex = table.Column<int>(nullable: true),
                    nChiTietToi = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "((0))"),
                    sCoSoYTe = table.Column<string>(nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sDSID_MaDonVi = table.Column<string>(maxLength: 500, nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(maxLength: 250, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sSoChungTu = table.Column<string>(maxLength: 50, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_CapBoSung_KCB_BHYT", x => x.iID_CTCapPhatBS);
                });

            migrationBuilder.CreateTable(
                name: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet",
                columns: table => new
                {
                    iID_CTCapPhatBSChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bHangCha = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fDaCapUng = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fSoCapBoSung = table.Column<double>(nullable: true),
                    fThuaThieu = table.Column<double>(nullable: true),
                    iID_CTCapPhatBS = table.Column<Guid>(nullable: false),
                    iID_CoSoYTe = table.Column<Guid>(nullable: false),
                    iID_MaCoSoYTe = table.Column<string>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sTenDonVi = table.Column<string>(maxLength: 50, nullable: true),
                    sXauNoiMa = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_CapBoSung_KCB_BHYT_ChiTiet", x => x.iID_CTCapPhatBSChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "BH_CP_CapTamUng_KCB_BHYT",
                columns: table => new
                {
                    iID_BH_CP_CapTamUng_KCB_BHYT = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsTongHop = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fQTQuyTruoc = table.Column<double>(nullable: true),
                    fTamUngQuyNay = table.Column<double>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: false),
                    iQuy = table.Column<int>(nullable: false),
                    sDSID_CoSoYTe = table.Column<string>(nullable: true),
                    sDSLNS = table.Column<string>(nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_CapTamUng_KCB_BHYT", x => x.iID_BH_CP_CapTamUng_KCB_BHYT);
                });

            migrationBuilder.CreateTable(
                name: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet",
                columns: table => new
                {
                    iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fLuyKeCapDenCuoiQuy = table.Column<double>(nullable: true),
                    fQTQuyTruoc = table.Column<double>(nullable: true),
                    fTamUngQuyNay = table.Column<double>(nullable: true),
                    iID_BH_CP_CapTamUng_KCB_BHYT = table.Column<Guid>(nullable: true),
                    iID_CoSoYTe = table.Column<Guid>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: true),
                    iID_MaCoSoYTe = table.Column<string>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_CP_CapTamUng_KCB_BHYT_ChiTiet", x => x.iID_BH_CP_CapTamUng_KCB_BHYT_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "DM_CoSoYTe",
                columns: table => new
                {
                    iID_CoSoYTe = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_MaCoSoYTe = table.Column<string>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    sTenCoSoYTe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_CoSoYTe", x => x.iID_CoSoYTe);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_ButToan_Input",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iNamNganSach = table.Column<int>(nullable: true),
                    sCongThucBangLoi = table.Column<string>(nullable: true),
                    sCongThucTheoBangTongHop = table.Column<string>(nullable: true),
                    sMaButToanInput = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "''"),
                    sSuKien = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "''"),
                    sTenButToanInput = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_ButToan_Input", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_CongThuc_Output",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sCongThucBangLoi = table.Column<string>(nullable: true),
                    sCongThucTheoBangTongHop = table.Column<string>(nullable: true),
                    sMaOutput = table.Column<string>(maxLength: 10, nullable: true, defaultValueSql: "''"),
                    sMenuSuDung = table.Column<string>(maxLength: 255, nullable: true, defaultValueSql: "''"),
                    sTenOutput = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_CongThuc_Output", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DM_TaiKhoan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    sMaTaiKhoan = table.Column<string>(nullable: true),
                    sNhomTaiKhoan = table.Column<string>(nullable: true),
                    sTenTaiKhoan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DM_TaiKhoan", x => x.ID);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.2_budget.sql");
            //migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.2_forex.sql");
            //migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.2_social_insurance_1.sql");
            //migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.2_social_insurance_2.sql");
            //migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.1.2_social_insurance_3.sql");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_CP_CapBoSung_KCB_BHYT");

            migrationBuilder.DropTable(
                name: "BH_CP_CapBoSung_KCB_BHYT_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_CP_CapTamUng_KCB_BHYT");

            migrationBuilder.DropTable(
                name: "BH_CP_CapTamUng_KCB_BHYT_ChiTiet");

            migrationBuilder.DropTable(
                name: "DM_CoSoYTe");

            migrationBuilder.DropTable(
                name: "NH_DM_ButToan_Input");

            migrationBuilder.DropTable(
                name: "NH_DM_CongThuc_Output");

            migrationBuilder.DropTable(
                name: "NH_DM_TaiKhoan");

            migrationBuilder.DropColumn(
                name: "iID_TongHop",
                table: "BH_CP_ChungTu");

            migrationBuilder.DropColumn(
                name: "iLoaiTongHop",
                table: "BH_CP_ChungTu");

            migrationBuilder.RenameColumn(
                name: "sTongHop",
                table: "BH_CP_ChungTu",
                newName: "sTenLoaiCap");

            migrationBuilder.RenameColumn(
                name: "sID_MaDonVi",
                table: "BH_CP_ChungTu",
                newName: "iID_MaDonVi");

            migrationBuilder.AlterColumn<bool>(
                name: "bIsKhoa",
                table: "BH_CP_ChungTu",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<Guid>(
                name: "iID_DonVi",
                table: "BH_CP_ChungTu",
                nullable: true);
        }
    }
}
