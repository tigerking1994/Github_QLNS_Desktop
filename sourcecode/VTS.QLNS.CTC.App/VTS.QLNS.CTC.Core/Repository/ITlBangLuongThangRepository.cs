using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBangLuongThangRepository : IRepository<TlBangLuongThang>
    {
        IEnumerable<TlDmCanBo> FindCb(decimal? thang, decimal? Nam, string maDonVi);
        int DeleteByParentId(Guid parentId);
        IEnumerable<TlDmThueThuNhapCaNhan> FindThue(bool bIsThueThang);
        IEnumerable<TlBangLuongThang> FindByParentId(Guid parentId);
        DataTable RptDSChiTraCaNhanNganHang(int nam, int thang, List<TlDmDonVi> tlDmDonVis, bool isReduceBHXH);
        DataTable ReportBangLuongThangDong(string maDonVi, int thang, int nam, bool isOrderChucVu, List<string> lstColumnInclude);
        DataTable ReportBangLuongThang(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, bool isReduceBackPay = false);
        DataTable ReportBangLuongThangTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong);
        DataTable ReportBangLuongThangTheoDonViTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong);
        DataTable GetDataLuongThang(Guid id);
        IEnumerable<TlBangLuongThang> FindMaCanBo(string maCanBo);
        DataTable GetDataReportDanhSachChiTraNganHang(string maDonVi, int nam, int thang, bool isReduceBHXH);
        DataTable ReportBangKeTrichThueTNCN(int thang, int nam, string maCachTl, string maDonVi, bool isExportAll, bool isOrderChucVu);
        DataTable ExportGiaiThichPhaiTru(string maCanBo, string maPhuCap);
        IEnumerable<TlRptDienBienLuongQuery> GetDataBangLuong();
        IEnumerable<TlBangLuongThangDongQuery> ReportBangLuongThangDong(string maDonVi, string ngach, string maPhuCap, int thang, int nam);
        DataTable ReportBangLuongThangDoc(string maDonVi, string ngach, string maPhuCap, int thang, int nam);
        IEnumerable<TlRptTruyLinhChuyenCheDoQuery> ReportTruyLinhChuyenCheDo(string maDonVi, int thangTruoc, int namTruoc, int thangSau, int namSau, string maHieuCanBo, bool isOrderChucVu);
        DataTable ReportDienBienLuong(string maHieuCanBo, string tuNgay, string denNgay);
        DataTable FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh, bool isSummary, bool isReduceBHXH);
        DataTable FindLuongDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh, bool isReduceBHXH);
        DataTable FindLuongNgachDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh, bool isReduceBHXH);
        IEnumerable<TlBangLuongThangQuery> GetDataInsert(int thang, int nam, string maDonVi, string maCachTl, int soNgay);
        IEnumerable<TlBangLuongThangQuery> GetDataInsertBhxh(int thang, int nam, string maDonVi, string maCachTl, int soNgay);
        DataTable ReportBangLuongTruyLinh(string maDonVi, int thang, int nam, bool isTruyLinh, bool isOrderChucVu);
        DataTable ReportDanhSachCapPhatPhuCap(string maDonVi, int thang, int nam);
        DataTable GetDataReportThueTncnNam(string maDonVi, int nam, bool isOrderChucVu);
        DataTable GetDataReportGiaiThichChiTietHsqCs(string maDonVi, int nam, int thang, int donViTinh);
        DataTable ReportGiaiThichLuongChiTiet(string maDonVi, int thang, int nam, string maCachTl, string maPhuCap, string maPhuCapCount, int donViTinh, bool isSummary);
        DataTable ReportTienAn(int thang, int nam, string maDonVi, int daysInMonth);
        DataTable GetDataBangLuongPhuCapTongHopBienPhong(string maDonVi, int thang, int nam);
        DataTable ReportGiaiThichChiTietPhuCapTNVKTHD(string maDonVi, int nam, int thang, string maCachTl, int donViTinh);
        DataTable ReportGiaiThichChiTietPhuCapKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu, bool isReducebackPay = false);
        DataTable ReportGiaiThichChiTietPhuCapTruyLinhKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichSoNgayPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportChiTraNganHangThuNhapKhac(string maDonVi, int thang, int nam, bool isOrderChucVu);
        DataTable ReportBangLuongTruyLinhDongPhuCap(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, string condition, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichRaQuanXuatNgu(string maDonVi, int nam, int thang, int donViTinh);
        DataTable ReportGiaiThichBienPhong(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap);
        DataTable ReportGiaiThichBienPhongTheoHeSo(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap, string maPhuCapTien);
        TlBangLuongThang GetMonthlySalary(string maCanBo, string maPhuCap, int? thang, int? nam);
        TlBangLuongThang GetLatestSalary(string maCanBo, int? thang, int? nam);
        DataTable GetDataBangLuongThangTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, bool isReduceBackPay = false);
        IEnumerable<TlBangLuongThangQuery> GetDataInsertTruyThu(Guid idBangLuong, int thang, int nam, string maDonVi);
        int CleanupData();
    }
}
