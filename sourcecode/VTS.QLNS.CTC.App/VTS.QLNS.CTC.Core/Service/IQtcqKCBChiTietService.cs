using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;


namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcqKCBChiTietService
    {
        IEnumerable<BhQtcqKCBChiTiet> FindByCondition(Expression<Func<BhQtcqKCBChiTiet, bool>> predicate);
        int AddRange(IEnumerable<BhQtcqKCBChiTiet> items);
        int Update(BhQtcqKCBChiTiet item);
        int RemoveRange(IEnumerable<BhQtcqKCBChiTiet> items);
        int UpdateRange(IEnumerable<BhQtcqKCBChiTiet> items);
        void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary,string sMaDonVi);
        IEnumerable<BhQtcqKCBChiTietQuery> GetChiTietQuyetToanChiQuyKCB(Guid idChungTu, Guid idLoaiChi, string sLNS, string sMaLoaiChi, string sMaDonVi, DateTime dNgayChungTu, int iQuy, int iNamLamViec, int loai);
        IEnumerable<BhQtcqKCBChiTietQuery> BaoCaoKCBQuanYDonVi(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh);
        void CreateVoudcherForQuaterBefore(BhQtcqKCB entity);
        List<ReportBHQTCQKCBThongTriQuery> GetDataThongTriDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh);
        List<BhQtcqKCBChiTietQuery> GetDataTienDuToanPhanBoChi(int namChungTu, string sDSLNS, string idMaDonVi, DateTime dNgayChungTu, Guid id, int quyChungTu);
        bool ExitChungTuChiTiet(Guid id);
        List<ReportBhQtcQKCBTongHopChi> BaoCaoKCBQuanYDonViTongHopChi(int yearOfWork, int donViTinh, string lstIdDonVi, int iQuy);
    }
}
