using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlBangLuongThangNq104Repository : IRepository<TlBangLuongThangNq104>
    {
        IEnumerable<TlDmCanBoNq104> FindCb(decimal? thang, decimal? Nam, string maDonVi);
        int DeleteByParentId(Guid parentId);
        IEnumerable<TlDmThueThuNhapCaNhanNq104> FindThue(bool bIsThueThang);
        IEnumerable<TlBangLuongThangNq104> FindByParentId(Guid parentId);
        DataTable RptDSChiTraCaNhanNganHang(int nam, int thang, List<TlDmDonViNq104> tlDmDonVis);
        DataTable ReportBangLuongThangDong(string maDonVi, int thang, int nam, bool isOrderChucVu, List<string> lstColumnInclude);
        DataTable ReportBangLuongThang(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, decimal tyLeHuong = 0);
        DataTable ReportBangLuongThangTheoDonVi(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi=false, decimal tyLeHuong = 0);
        DataTable ReportBangLuongThangTheoDonViTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong);
        DataTable GetDataLuongThang(Guid id);
        IEnumerable<TlBangLuongThangNq104> FindMaCanBo(string maCanBo);
        DataTable GetDataReportDanhSachChiTraNganHang(string maDonVi, int nam, int thang);
        DataTable ReportBangKeTrichThueTNCN(int thang, int nam, string maCachTl, string maDonVi, bool isExportAll, bool isOrderChucVu);
        DataTable ExportGiaiThichPhaiTru(string maCanBo, string maPhuCap);
        IEnumerable<TlRptDienBienLuongNq104Query> GetDataBangLuong();
        IEnumerable<TlBangLuongThangDongNq104Query> ReportBangLuongThangDong(string maDonVi, string ngach, string maPhuCap, int thang, int nam);
        DataTable ReportBangLuongThangDoc(string maDonVi, string ngach, string maPhuCap, int thang, int nam);
        IEnumerable<TlRptTruyLinhChuyenCheDoNq104Query> ReportTruyLinhChuyenCheDo(string maDonVi, int thangTruoc, int namTruoc, int thangSau, int namSau, string maHieuCanBo, bool isOrderChucVu);
        DataTable ReportDienBienLuong(string maHieuCanBo, string tuNgay, string denNgay);
        DataTable FindLuongNgachCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh, bool isSummary);
        DataTable FindLuongDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh);
        DataTable FindLuongNgachDonViCanBo(int thang, int nam, string maDonVi, string maCachTL, int donViTinh);
        IEnumerable<TlBangLuongThangNq104Query> GetDataInsert(int thang, int nam, string maDonVi, string maCachTl, int soNgay);
        IEnumerable<TlBangLuongThangNq104Query> GetDataInsertBhxh(int thang, int nam, string maDonVi, string maCachTl, int soNgay);
        DataTable ReportBangLuongTruyLinh(string maDonVi, int thang, int nam, bool isTruyLinh, bool isOrderChucVu);
        DataTable ReportDanhSachCapPhatPhuCap(string maDonVi, int thang, int nam);
        DataTable GetDataReportThueTncnNam(string maDonVi, int nam, bool isOrderChucVu);
        DataTable GetDataReportGiaiThichChiTietHsqCs(string maDonVi, int nam, int thang, int donViTinh);
        DataTable ReportGiaiThichLuongChiTiet(string maDonVi, int thang, int nam, string maCachTl, string maPhuCap, string maPhuCapCount, int donViTinh, bool isSummary);
        DataTable ReportTienAn(int thang, int nam, string maDonVi, int daysInMonth);
        DataTable GetDataBangLuongPhuCapTongHopBienPhong(string maDonVi, int thang, int nam);
        DataTable ReportGiaiThichChiTietPhuCapTNVKTHD(string maDonVi, int nam, int thang, string maCachTl, int donViTinh);
        DataTable ReportGiaiThichChiTietPhuCapKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichChiTietPhuCapTruyLinhKhac(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichSoNgayPhuCapTheoNgay(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, int donViTinh, bool isOrderChucVu);
        DataTable ReportChiTraNganHangThuNhapKhac(string maDonVi, int thang, int nam, bool isOrderChucVu);
        DataTable ReportBangLuongTruyLinhDongPhuCap(string maDonVi, string maPhuCap, int nam, int thang, string maCachTl, string condition, int donViTinh, bool isOrderChucVu);
        DataTable ReportGiaiThichRaQuanXuatNgu(string maDonVi, int nam, int thang, int donViTinh);
        DataTable ReportGiaiThichBienPhong(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap);
        DataTable ReportGiaiThichBienPhongTheoHeSo(string maDonVi, int nam, int thang, int donViTinh, string maPhuCap, string maPhuCapTien);
        TlBangLuongThangNq104 GetMonthlySalary(string maCanBo, string maPhuCap, int? thang, int? nam);
        TlBangLuongThangNq104 GetLatestSalary(string maCanBo, int? thang, int? nam);
        DataTable GetDataBangLuongThangTruBHXH(string maDonVi, int thang, int nam, bool isOrderChucVu, bool isGiaTriAm, bool isCheckedMaHuongLuong, bool isInCanBoMoi, decimal tyLeHuong);
        IEnumerable<TlBangLuongThangNq104> FindBangLuongThangByCondition(string maDonVi, int? thang, int? nam, string maCachTL, string maHieuCanBo);
        List<TlBangLuongThangNq104> FindLuongThangCanBo(int? thang, int? nam, string maDonVi, Guid id, string maCach);
    }
}
