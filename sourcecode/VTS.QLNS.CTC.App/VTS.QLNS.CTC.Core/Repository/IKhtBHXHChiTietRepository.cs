using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IKhtBHXHChiTietRepository : IRepository<BhKhtBHXHChiTiet>
    {
        IEnumerable<BhKhtBHXHChiTiet> FindBhKhtBHXHChiTietByCondition(KhtBHXHChiTietCriteria searchCondition);
        bool ExistBHXHChiTiet(Guid bhxhId);
        BhKhtBHXHChiTiet FindById(Guid id);
        IEnumerable<BhKhtBHXHChiTiet> FindKhtBHXHChiTietByIdBhxh(KhtBHXHChiTietCriteria searchCondition);
        IEnumerable<BhKhtBHXHChiTiet> FindByCondition(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate);
        IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHXH(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt);
        IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHYT(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, int dvt);
        IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtcTongHop(int namLamViec, string lstDonvi, int dvt);
        IEnumerable<BhKhtBHXHChiTietQuery> GetPlanSalary(int iNam, string sLuongChinh, string sPhuCapCV, string sPhuCapTNN, string sPhuCapTNVK, string lstChungTuIds);
        IEnumerable<BhKhtBHXHChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi);
        IEnumerable<BhKhtBHXHChiTietQuery> GetQuanSoBinhQuan(int iNam, string sLuongKeHoachId);
        IEnumerable<BhKhtBHXHChiTietQuery> GetPlanData(int iNam, string sSoChungTu);
        IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregate(int namLamViec, string maDonvis, int dvt);
        IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregateByUnits(int namLamViec, string maDonvis, bool isAggregate, int loaiChungTu, int dvt);
    }
}
