using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcnBHXHChiTietService
    {
        IEnumerable<BhQtcnBHXHChiTiet> FindByCondition(Expression<Func<BhQtcnBHXHChiTiet, bool>> predicate);
        IEnumerable<BhQtcnBHXHChiTietQuery> GetChiTietQuyetToanChiNamBHXH(Guid idChungTu, int iNamLamViec, bool isTongHop4Quy,int iLoaiTongHop, string sMaDonVi);
        int AddRange(IEnumerable<BhQtcnBHXHChiTiet> items);
        int Update(BhQtcnBHXHChiTiet item);
        int RemoveRange(IEnumerable<BhQtcnBHXHChiTiet> items);
        int UpdateRange(IEnumerable<BhQtcnBHXHChiTiet> items);
        void CreateVoudcherSummary(string idChungTu,string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary);
        void CreateChungTuChiTietTheoQuy(Guid idChungTu, string idMaDonVi, int iNamLamViec, string user, bool isTongHop);
        IEnumerable<BhQtcnBHXHChiTietQuery> ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop);
        IEnumerable<BhBaoCaoQuyetToanChiNamQuery> ExportQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop);
        List<BhQtcnBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, string sMaDonVi, string sLNS,DateTime dNgayChungTu);
        bool ExistVoucherDetail(Guid id, int? namLamViec);
        public void CreateVoudcherDetailSummary(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu, Guid idChungTu);
    }
}
