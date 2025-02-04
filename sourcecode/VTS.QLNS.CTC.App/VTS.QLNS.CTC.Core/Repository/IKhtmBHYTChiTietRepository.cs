using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IKhtmBHYTChiTietRepository : IRepository<BhKhtmBHYTChiTiet>
    {
        IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTietByIdBhyt(KhtmBHYTChiTietCriteria searchCondition);
        BhKhtmBHYTChiTiet FindById(Guid id);
        IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition);
        IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTReportByCondition(KhtmBHYTChiTietCriteria searchModel);
        bool ExistBHYTChiTiet(Guid bhytId);
        IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytThanNhan(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi,
            string thanNhanQuanNhan, string thanNhanCNVQP, string @smDuToan, string smHachToan, int dvt);
        IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytHSSV(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi,
            string thanNhanQuanNhan, string thanNhanCNVQP, string @smDuToan, string smHachToan, int dvt);
        IEnumerable<BhKhtmBHYTChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi);
        IEnumerable<BhKhtmBHYTChiTietQuery> GetPlanData(int iNam, string sSoChungTu);
        List<BhKhtmBHYTChiTietQuery> FindBhKhtmBHYTTongHopChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition);
    }
}
