using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IPbdttmBHYTChiTietService
    {
        IEnumerable<BhPbdttmBHYTChiTiet> FindByCondition(Expression<Func<BhPbdttmBHYTChiTiet, bool>> predicate);
        int Add(BhPbdttmBHYTChiTiet item);
        int Update(BhPbdttmBHYTChiTiet item);
        int Delete(BhPbdttmBHYTChiTiet item);
        int AddRange(List<BhPbdttmBHYTChiTiet> items);
        int RemoveRange(List<BhPbdttmBHYTChiTiet> items);
        IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTiet(Guid chungTuId, string sLNS, string sIDDonVi, int iNamLamViec, string userName);
        IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuId, string sLNS, int iNamLamViec, string userName);
        IEnumerable<BhPbdttmBHYTChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec);
        IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytThanNhan(int namLamViec, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP
            , string smDuToan, string smHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound);
        IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytHSSV(int namLamViec, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi
            , string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound);
        IEnumerable<BhPbdttmBHYTChiTiet> FindByXauNoiMaVaSoQuyetDinh(string sSoQuyetDinh, List<string> sLNS, int iNamLamViec, bool isContains = true);
    }
}
