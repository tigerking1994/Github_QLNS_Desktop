using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQttBHXHChiTietService
    {
        IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailById(BhQttBHXHChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhQttBHXHChiTiet> chungTuChiTiets);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        IEnumerable<BhQttBHXHChiTiet> FindByCondition(Expression<Func<BhQttBHXHChiTiet, bool>> predicate);
        void AddAggregateVoucherDetail(BhQttBHXHChiTietCriteria creation);
        int AddRange(IEnumerable<BhQttBHXHChiTiet> chungTuChiTiets);
        BhQttBHXHChiTiet FindById(Guid id);
        int Update(BhQttBHXHChiTiet item);
        IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailByCondition(BhQttBHXHChiTietCriteria searchModel);
        IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailsByCondition(BhQttBHXHChiTietCriteria searchModel);
        List<string> FindListDonViExistSettlement(Guid id, int iNamLamViec, string userName, int selectedQuarter, int loaiQuy);
        List<string> FindChiTietDonViThangQuy(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy);
        List<string> FindListChiTietDonViByListMonth(int iNamLamViec, int loaiTongHop, bool bDaTongHop, string selectedQuarterList);
        List<string> FindChiTietDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy);
        List<string> FindChiTietDonViTongHopThangQuy(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy);
        List<string> FindChiTietDonViTongHop(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy);
        List<string> FindAllDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy);
        IEnumerable<BhQttBHXHChiTiet> FindDetailsQT(string idDonVi, int iNamLamViec, int iNamNganSach, int iNguonNganSach, int selectedQuarter, int loaiQuy);
        bool ExistVoucherDetail(Guid voucherID);
        IEnumerable<BhQttBHXHChiTiet> FindAllVouchers();
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViTheoThoiGian(int iNamLamViec, string sIdDonVis, int donViTinh, string lstMonthSelected);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopTongHopNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop, string sm);
        IEnumerable<BhQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int namLamViec, string lstDonvi, int dvt, bool isTongHop);
        IEnumerable<BhQttBHXHChiTietQuery> GetDataTongHopSoSanhNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter);
        IEnumerable<BhReportQttBHXHChiTietQuery> ExportQTTBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int type);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isLuyKe, bool isTongHop = false);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, bool isTongHop = false);
        IEnumerable<BhQttBHXHChiTietQuery> GetDataLuongCanCu(string maDonVi, int? namLamViec, string months, int loaiQuyNam);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isLuyKe, bool isTongHop = false);
        IEnumerable<BhQttBHXHChiTietQuery> GetChiTietChungTuThangQuy(int? iNamLamViec, string sIdDonVis);
        IEnumerable<BhReportQttBHXHChiTietQuery> FindVoucherDetailsThongTri(BhQttBHXHChiTietCriteria searchCondition);
    }
}
