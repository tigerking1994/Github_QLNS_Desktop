using Microsoft.EntityFrameworkCore.Migrations;
using System;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Migrations
{
    public partial class UpdateVersion_11294 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BH_DM_MucLucNganSach",
                columns: table => new
                {
                    iID = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bDuPhong = table.Column<bool>(nullable: false),
                    bHangCha = table.Column<bool>(nullable: false),
                    bHangChaDuToan = table.Column<bool>(maxLength: 50, nullable: true),
                    bHangChaQuyetToan = table.Column<bool>(nullable: true),
                    bHangMua = table.Column<bool>(maxLength: 50, nullable: false),
                    bHangNhap = table.Column<bool>(maxLength: 250, nullable: false),
                    bHienVat = table.Column<bool>(nullable: false),
                    bNgay = table.Column<bool>(nullable: false),
                    bPhanCap = table.Column<bool>(nullable: false),
                    bSoNguoi = table.Column<bool>(nullable: false),
                    bTonKho = table.Column<bool>(nullable: false),
                    bTuChi = table.Column<bool>(nullable: false),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    iID_MLNS = table.Column<Guid>(nullable: false),
                    iID_MLNS_Cha = table.Column<Guid>(nullable: true),
                    iID_MaBQuanLy = table.Column<string>(maxLength: 10, nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iLoai = table.Column<string>(nullable: true),
                    iLoaiNganSach = table.Column<int>(nullable: true),
                    iLock = table.Column<bool>(nullable: true),
                    iNamLamViec = table.Column<int>(nullable: true),
                    iTrangThai = table.Column<int>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    sCPChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sDuToanChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sK = table.Column<string>(maxLength: 50, nullable: true),
                    sL = table.Column<string>(maxLength: 50, nullable: true),
                    sLNS = table.Column<string>(maxLength: 100, nullable: true),
                    sM = table.Column<string>(maxLength: 50, nullable: true),
                    sMaCB = table.Column<string>(maxLength: 50, nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNG = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiSua = table.Column<string>(maxLength: 50, nullable: true),
                    sNguoiTao = table.Column<string>(maxLength: 50, nullable: true),
                    sNhapTheoTruong = table.Column<string>(maxLength: 200, nullable: true),
                    sQuyetToanChiTietToi = table.Column<string>(maxLength: 50, nullable: true),
                    sTM = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG1 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG2 = table.Column<string>(maxLength: 50, nullable: true),
                    sTNG3 = table.Column<string>(maxLength: 50, nullable: true),
                    sTTM = table.Column<string>(maxLength: 50, nullable: true),
                    Tag = table.Column<string>(maxLength: 250, nullable: true),
                    sXauNoiMa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_DM_MucLucNganSach", x => x.iID);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHC_CheDoBHXH",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    iID_DonVi = table.Column<Guid>(nullable: true),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(nullable: true),
                    dNgayQuyetDinh = table.Column<DateTime>(nullable: true),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTongTienCNVQP = table.Column<double>(nullable: true),
                    fTongTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTongTienHSQBS = table.Column<double>(nullable: true),
                    fTongTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTongTienLDHD = table.Column<double>(nullable: true),
                    fTongTienQNCN = table.Column<double>(nullable: true),
                    fTongTienSQ = table.Column<double>(nullable: true),
                    fTongTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    iID_TongHopID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: false),
                    iNamChungTu = table.Column<int>(nullable: true),
                    iTongSoCNVQP = table.Column<int>(nullable: true),
                    iTongSoDaThucHienNamTruoc = table.Column<int>(nullable: true),
                    iTongSoHSQBS = table.Column<int>(nullable: true),
                    iTongSoKeHoachThucHienNamNay = table.Column<int>(nullable: true),
                    iTongSoLDHD = table.Column<int>(nullable: true),
                    iTongSoQNCN = table.Column<int>(nullable: true),
                    iTongSoSQ = table.Column<int>(nullable: true),
                    iTongSoUocThucHienNamTruoc = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_CheDoBHXH", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHT_BHXH",
                columns: table => new
                {
                    iID_KHT_BHXH = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    bIsKhoa = table.Column<bool>(nullable: false),
                    dNgayChungTu = table.Column<DateTime>(type: "datetime", nullable: false),
                    dNgayQuyetDinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fTongKeHoach = table.Column<double>(nullable: true),
                    iID_MaDonVi = table.Column<string>(nullable: true),
                    IID_TongHop_ID = table.Column<Guid>(nullable: true),
                    iLoaiTongHop = table.Column<int>(nullable: true),
                    INamChungTu = table.Column<int>(nullable: true),
                    sMoTa = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sSoChungTu = table.Column<string>(nullable: true),
                    sSoQuyetDinh = table.Column<string>(nullable: true),
                    sTongHop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHT_BHXH", x => x.iID_KHT_BHXH);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHC_CheDoBHXH_ChiTiet",
                columns: table => new
                {
                    iID_KHC_CheDoBHXHChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BhKhcCheDoBhXhId = table.Column<Guid>(nullable: true),
                    dNgaySua = table.Column<DateTime>(nullable: true),
                    dNgayTao = table.Column<DateTime>(nullable: true),
                    fTienCNVQP = table.Column<double>(nullable: true),
                    fTienDaThucHienNamTruoc = table.Column<double>(nullable: true),
                    fTienHSQBS = table.Column<double>(nullable: true),
                    fTienKeHoachThucHienNamNay = table.Column<double>(nullable: true),
                    fTienLDHD = table.Column<double>(nullable: true),
                    fTienQNCN = table.Column<double>(nullable: true),
                    fTienSQ = table.Column<double>(nullable: true),
                    fTienUocThucHienNamTruoc = table.Column<double>(nullable: true),
                    iID_KHC_CheDoBHXH = table.Column<Guid>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: true),
                    iSoCNVQP = table.Column<int>(nullable: true),
                    iSoDaThucHienNamTruoc = table.Column<int>(nullable: true),
                    iSoHSQBS = table.Column<int>(nullable: true),
                    iSoKeHoachThucHienNamNay = table.Column<int>(nullable: true),
                    iSoLDHD = table.Column<int>(nullable: true),
                    iSoQNCN = table.Column<int>(nullable: true),
                    iSoSQ = table.Column<int>(nullable: true),
                    iSoUocThucHienNamTruoc = table.Column<int>(nullable: true),
                    sGhiChu = table.Column<string>(nullable: true),
                    sLoaiTroCap = table.Column<string>(nullable: true),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHC_CheDoBHXH_ChiTiet", x => x.iID_KHC_CheDoBHXHChiTiet);
                });

            migrationBuilder.CreateTable(
                name: "BH_KHT_BHXH_ChiTiet",
                columns: table => new
                {
                    iID_KHT_BHXHChiTiet = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    BhKhtBHXHId = table.Column<Guid>(nullable: true),
                    dNgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    dNgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    fLuongChinh = table.Column<double>(nullable: true),
                    fNghiOm = table.Column<double>(nullable: true),
                    fPCTNNghe = table.Column<double>(nullable: true),
                    fPCTNVuotKhung = table.Column<double>(nullable: true),
                    fPhuCapChucVu = table.Column<double>(nullable: true),
                    fThuBHTNNguoiLaoDongDong = table.Column<double>(nullable: true),
                    fThuBHTNNguoiSuDungLaoDongDong = table.Column<double>(nullable: true),
                    fThuBHXHNguoiLaoDongDong = table.Column<double>(nullable: true),
                    fThuBHXHNguoiSuDungLaoDongDong = table.Column<double>(nullable: true),
                    fThuBHYTNguoiLaoDongDong = table.Column<double>(nullable: true),
                    fThuBHYTNguoiSuDungLaoDongDong = table.Column<double>(nullable: true),
                    fTongCong = table.Column<double>(nullable: true),
                    fTongQuyTienLuongNam = table.Column<double>(nullable: true),
                    FTongThuBHTN = table.Column<double>(nullable: true),
                    FTongThuBHXH = table.Column<double>(nullable: true),
                    FTongThuBHYT = table.Column<double>(nullable: true),
                    iID_LoaiDoiTuong = table.Column<Guid>(nullable: true),
                    iID_MucLucNganSach = table.Column<Guid>(nullable: false),
                    iQSBQNam = table.Column<int>(nullable: true),
                    iID_KHT_BHXH = table.Column<Guid>(nullable: false),
                    sNguoiSua = table.Column<string>(nullable: true),
                    sNguoiTao = table.Column<string>(nullable: true),
                    sTenLoaiDoiTuong = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BH_KHT_BHXH_ChiTiet", x => x.iID_KHT_BHXHChiTiet)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BH_KHC_CheDoBHXH_ChiTiet_BhKhcCheDoBhXhId",
                table: "BH_KHC_CheDoBHXH_ChiTiet",
                column: "BhKhcCheDoBhXhId");

            migrationBuilder.CreateIndex(
                name: "IX_BH_KHT_BHXH_ChiTiet_BhKhtBHXHId",
                table: "BH_KHT_BHXH_ChiTiet",
                column: "BhKhtBHXHId");
            migrationBuilder.RunSqlScript("AppData/_db/99_update_version_1.12.9.4_insurance.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BH_DM_MucLucNganSach");

            migrationBuilder.DropTable(
                name: "BH_KHC_CheDoBHXH_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_KHT_BHXH_ChiTiet");

            migrationBuilder.DropTable(
                name: "BH_KHC_CheDoBHXH");

            migrationBuilder.DropTable(
                name: "BH_KHT_BHXH");
        }
    }
}
