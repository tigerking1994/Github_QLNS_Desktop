using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11320 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentOld",
                table: "TL_DM_CanBo",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "iID_QuyetDinhKhacID",
                table: "NH_TT_ThanhToan",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "fHSBL",
                table: "BH_KHT_BHXH_ChiTiet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_KinhPhiQuanLy",
                columns: table => new
                {
                    ID_QTC_Nam_KinhPhiQuanLy = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bThucChiTheo4Quy = table.Column<bool>(nullable: true),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiLeThucHienTrenDuToan = table.Column<double>(nullable: true),
                    fTongTienThieu = table.Column<double>(nullable: true),
                    fTongTienThua = table.Column<double>(nullable: true),
                    fTongTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTongTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTongTien_ThucChi = table.Column<double>(nullable: true),
                    fTongTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_KinhPhiQuanLy", x => x.ID_QTC_Nam_KinhPhiQuanLy)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Nam_KinhPhiQuanLy_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTiLeThucHienTrenDuToan = table.Column<double>(nullable: true),
                    fTienThieu = table.Column<double>(nullable: true),
                    fTienThua = table.Column<double>(nullable: true),
                    fTien_DuToanGiaoNamNay = table.Column<double>(nullable: true),
                    fTien_DuToanNamTruocChuyenSang = table.Column<double>(nullable: true),
                    fTien_ThucChi = table.Column<double>(nullable: true),
                    fTien_TongDuToanDuocGiao = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iID_QTC_Nam_KinhPhiQuanLy = table.Column<Guid>(nullable: true),
                    sM = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sNoiDung = table.Column<string>(nullable: true),
                    sTM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Nam_KinhPhiQuanLy_ChiTiet", x => x.ID_QTC_Nam_KinhPhiQuanLy_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_CheDoBHXH",
                columns: table => new
                {
                    ID_QTC_Quy_CheDoBHXH = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: false),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTongTienCNVCQP_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_DuToanDuyet = table.Column<double>(nullable: true),
                    fTongTienLDHD_DeNghi = table.Column<double>(nullable: true),
                    fTongTienHSQBS_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_LuyKeCuoiQuyNay = table.Column<double>(nullable: true),
                    fTongTien_PheDuyet = table.Column<double>(nullable: true),
                    fTongTienQNCN_DeNghi = table.Column<double>(nullable: true),
                    fTongTienSQ_DeNghi = table.Column<double>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    iQuyChungTu = table.Column<int>(nullable: false),
                    iTongSoCNVCQP_DeNghi = table.Column<int>(nullable: true),
                    iTongSo_DeNghi = table.Column<int>(nullable: true),
                    iTongSoLDHD_DeNghi = table.Column<int>(nullable: true),
                    iTongSoHSQBS_DeNghi = table.Column<int>(nullable: true),
                    iTongSo_LuyKeCuoiQuyNay = table.Column<int>(nullable: true),
                    iTongSoQNCN_DeNghi = table.Column<int>(nullable: true),
                    iTongSoSQ_DeNghi = table.Column<int>(nullable: true),
                    sDSSoChungTuTongHop = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_CheDoBHXH", x => x.ID_QTC_Quy_CheDoBHXH)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTC_Quy_CheDoBHXH_ChiTiet",
                columns: table => new
                {
                    ID_QTC_Quy_CheDoBHXH_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienCNVCQP_DeNghi = table.Column<double>(nullable: true),
                    fTienDuToanDuyet = table.Column<double>(nullable: true),
                    fTienHSQBS_DeNghi = table.Column<double>(nullable: true),
                    fTienLDHD_DeNghi = table.Column<double>(nullable: true),
                    fTienLuyKeCuoiQuyNay = table.Column<double>(nullable: true),
                    fTienQNCN_DeNghi = table.Column<double>(nullable: true),
                    fTienSQ_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_DeNghi = table.Column<double>(nullable: true),
                    fTongTien_PheDuyet = table.Column<double>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: false),
                    iSoCNVCQP_DeNghi = table.Column<int>(nullable: false),
                    iSoHSQBS_DeNghi = table.Column<int>(nullable: false),
                    iSoLDHD_DeNghi = table.Column<int>(nullable: false),
                    iSoLuyKeCuoiQuyNay = table.Column<int>(nullable: false),
                    iSoQNCN_DeNghi = table.Column<int>(nullable: false),
                    iSoSQ_DeNghi = table.Column<int>(nullable: false),
                    iTongSo_DeNghi = table.Column<int>(nullable: false),
                    iID_QTC_Quy_CheDoBHXH = table.Column<Guid>(nullable: false),
                    sLoaiTroCap = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTC_Quy_CheDoBHXH_ChiTiet", x => x.ID_QTC_Quy_CheDoBHXH_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTTM_BHYT_Chung_Tu",
                columns: table => new
                {
                    iID_QTTM_BHYT_ChungTu = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDaTongHop = table.Column<bool>(nullable: true),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    fSoPhaiThu = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iQuyNam = table.Column<int>(nullable: false),
                    iQuyNamLoai = table.Column<int>(nullable: false),
                    sDS_MLNS = table.Column<string>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sQuyNamMoTa = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTTM_BHYT", x => x.iID_QTTM_BHYT_ChungTu)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "BH_QTTM_BHYT_Chung_Tu_ChiTiet",
                columns: table => new
                {
                    iID_QTTM_BHYT_ChungTu_ChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fConLai = table.Column<double>(nullable: true),
                    fDaQuyetToan = table.Column<double>(nullable: true),
                    fDuToan = table.Column<double>(nullable: true),
                    fSoPhaiThu = table.Column<double>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLNS = table.Column<string>(nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true),
                    iID_QTTM_BHYT_ChungTu = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_QTTM_BHYT_Chung_Tu_ChiTiet", x => x.iID_QTTM_BHYT_ChungTu_ChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QuyetDinhKhac",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsActive = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsGoc = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    bIsXoa = table.Column<bool>(nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayXoa = table.Column<DateTime>(type: "datetime", nullable: true),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DonViQuanLyID = table.Column<Guid>(nullable: true),
                    iID_KHTT_NhiemVuChiID = table.Column<Guid>(nullable: true),
                    iID_MaDonViQuanLy = table.Column<string>(maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iLanDieuChinh = table.Column<int>(nullable: true),
                    iThuocMenu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 100, nullable: true),
                    sNguoiXoa = table.Column<string>(maxLength: 100, nullable: true),
                    sSoQuyetDinh = table.Column<string>(maxLength: 100, nullable: true),
                    sTenQuyetDinh = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QuyetDinhKhac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NH_DA_QuyetDinhKhac_ChiPhi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    fGiaTriUSD = table.Column<double>(nullable: true),
                    fGiaTriVND = table.Column<double>(nullable: true),
                    iID_DM_ChiPhiID = table.Column<Guid>(nullable: true),
                    iID_ParentID = table.Column<Guid>(nullable: true),
                    iID_QuyetDinhKhacID = table.Column<Guid>(nullable: true),
                    sMaOrder = table.Column<string>(nullable: true),
                    sTenChiPhi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NH_DA_QuyetDinhKhac_ChiPhi", x => x.ID);
                });

            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.0_salary_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.0_social_insurance_1.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.0_social_insurance_2.sql");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.13.2.0_social_insurance_3.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_KinhPhiQuanLy");

            migrationBuilder.DropTable(
                name: "BH_QTC_Nam_KinhPhiQuanLy_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_CheDoBHXH");

            migrationBuilder.DropTable(
                name: "BH_QTC_Quy_CheDoBHXH_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_QTTM_BHYT_Chung_Tu");

            migrationBuilder.DropTable(
                name: "BH_QTTM_BHYT_Chung_Tu_ChiTiet");

            migrationBuilder.DropTable(
                name: "NH_DA_QuyetDinhKhac");

            migrationBuilder.DropTable(
                name: "NH_DA_QuyetDinhKhac_ChiPhi");

            migrationBuilder.DropColumn(
                name: "ParentOld",
                table: "TL_DM_CanBo");

            migrationBuilder.DropColumn(
                name: "iID_QuyetDinhKhacID",
                table: "NH_TT_ThanhToan");

            migrationBuilder.DropColumn(
                name: "fHSBL",
                table: "BH_KHT_BHXH_ChiTiet");
        }
    }
}
