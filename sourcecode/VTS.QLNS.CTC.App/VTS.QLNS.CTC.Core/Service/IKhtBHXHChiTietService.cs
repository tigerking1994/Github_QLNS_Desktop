using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IKhtBHXHChiTietService
    {
        IEnumerable<BhKhtBHXHChiTiet> FindBhKhtBHXHChiTietByCondition(KhtBHXHChiTietCriteria searchModel);
        bool ExistBHXHChiTiet(Guid bhxhId);
        BhKhtBHXHChiTiet FindById(Guid id);
        int Update(BhKhtBHXHChiTiet item);
        int AddRange(IEnumerable<BhKhtBHXHChiTiet> khtBhxhChiTiets);
        IEnumerable<BhKhtBHXHChiTiet> FindKhtBHXHChiTietByIdBhxh(KhtBHXHChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhKhtBHXHChiTiet> bhxhChungTuChiTiets);
        IEnumerable<BhKhtBHXHChiTiet> FindAll(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate);
        IEnumerable<BhKhtBHXHChiTiet> FindByCondition(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate);
        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHXH(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt);
        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHYT(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, int dvt);
        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtcTongHop(int namLamViec, string lstDonvi, int dvt);
        public IEnumerable<BhKhtBHXHChiTietQuery> GetPlanSalary(int iNam, string sLuongChinh, string sPhuCapCV, string sPhuCapTNN, string sPhuCapTNVK, string lstChungTuIds);
        public IEnumerable<BhKhtBHXHChiTietQuery> GetQuanSoBinhQuan(int iNam, string sLuongKeHoachId);
        IEnumerable<BhKhtBHXHChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi);
        IEnumerable<BhKhtBHXHChiTietQuery> GetPlanData(int iNam, string sSoChungTu);
        IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregate(int namLamViec, string maDonvis, int dvt);
        IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregateByUnits(int namLamViec, string maDonvis, bool isAggregate, int loaiChungTu, int dvt);
    }
}
