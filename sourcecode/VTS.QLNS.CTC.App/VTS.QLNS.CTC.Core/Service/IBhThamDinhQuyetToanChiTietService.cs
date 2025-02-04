using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhThamDinhQuyetToanChiTietService
    {
        IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(Guid iIDChungTu, int yearOfWork, string iIDMaDonVi);
        IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(int yearOfWork, string iIDMaDonVi, int dvt);
        IEnumerable<BhThamDinhQuyetToanChiTTBYTQuery> GetChiKinhPhiTTBYT(int yearOfWork, string iIDMaDonVi, int donViTinh);
        IEnumerable<BhThamDinhQuyetToanChiCheDoBHXHQuery> GetChiKinhPhiCheDoBHXH(int yearOfWork, string iIDMaDonVi, int donViTinh);
        IEnumerable<BhThamDinhQuyetToanChiKCBHSSVNLDQuery> GetChiKinhPhiCSSKHSSVNLD(int yearOfWork, string iIDMaDonVi, int iLoai, int donViTinh);
        IEnumerable<BhThamDinhQuyetToanChiKCBQuanYDonViQuery> GetChiKinhPhiKCBQuanYDonVi(int yearOfWork, string iIDMaDonVi,int donViTinh);
        IEnumerable<BhThamDinhQuyetToanChiTiet> FindAll(Expression<Func<BhThamDinhQuyetToanChiTiet, bool>> predicate);
        int AddRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities);
        int UpdateRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities);
        int RemoveRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities);
        void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, int dvt, bool isBHXH);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhyt(int namLamViec, string lstDonvi, int dvt);
        IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhytQuanNhan(int namLamViec, string lstDonvi, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytThanNhan(int namLamViec, string lstDonvi, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytHssvHvqs(int namLamViec, string lstDonvi, int dvt);
        IEnumerable<BhReportQttBHXHChiTietQuery> ExportThongBaoPheDuyetThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, int type);
        IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, bool isTongHop);
        IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportDuToanKinhPhiChuyenNamSau(int iNamLamViec, string sIdDonVis, int donViTinh);
        IEnumerable<BhThamDinhQuyetToanChiTiet> FindAllOfLastYear(int yearOfWork, string iIDMaDonVi);
        IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportCanCuTrichQuyBhxhSangBhyt(int yearOfWork, string iIDMaDonVi, int donViTinh);
    }
}
