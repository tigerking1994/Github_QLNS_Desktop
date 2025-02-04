using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IKhtmBHYTChiTietService
    {
        IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTietByIdBhyt(KhtmBHYTChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhKhtmBHYTChiTiet> bhytChungTuChiTiets);
        IEnumerable<BhKhtmBHYTChiTiet> FindByCondition(Expression<Func<BhKhtmBHYTChiTiet, bool>> predicate);
        int AddRange(IEnumerable<BhKhtmBHYTChiTiet> khtmBhytChiTiets);
        BhKhtmBHYTChiTiet FindById(Guid id);
        int Update(BhKhtmBHYTChiTiet item);
        IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTChiTietByCondition(KhtmBHYTChiTietCriteria searchModel);
        IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTReportByCondition(KhtmBHYTChiTietCriteria searchModel);
        IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytThanNhan(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, 
            string thanNhanQuanNhan, string thanNhanCNVQP, string @smDuToan, string smHachToan, int dvt);
        bool ExistBHYTChiTiet(Guid bhxhId);
        IEnumerable<BhKhtmBHYTChiTiet> FindAll(Expression<Func<BhKhtmBHYTChiTiet, bool>> predicate);
        IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytHSSV(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi, int dvt);
        IEnumerable<BhKhtmBHYTChiTiet> FindAll();
        IEnumerable<BhKhtmBHYTChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi);
        IEnumerable<BhKhtmBHYTChiTietQuery> GetPlanData(int iNam, string sSoChungTu);
        List<BhKhtmBHYTChiTietQuery> FindBhKhtmBHYTTongHopChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition);
    }
}
