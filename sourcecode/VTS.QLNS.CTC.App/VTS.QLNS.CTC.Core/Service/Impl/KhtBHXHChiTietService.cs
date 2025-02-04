using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class KhtBHXHChiTietService : IKhtBHXHChiTietService
    {
        private readonly IKhtBHXHChiTietRepository _khtBHXHChiTietRepository;

        public KhtBHXHChiTietService(IKhtBHXHChiTietRepository khtBHXHChiTietRepository)
        {
            _khtBHXHChiTietRepository = khtBHXHChiTietRepository;
        }

        public bool ExistBHXHChiTiet(Guid bhxhId)
        {
            return _khtBHXHChiTietRepository.ExistBHXHChiTiet(bhxhId);
        }

        public BhKhtBHXHChiTiet FindById(Guid id)
        {
            return _khtBHXHChiTietRepository.FindById(id);
        }

        public int Update(BhKhtBHXHChiTiet item)
        {
            return _khtBHXHChiTietRepository.Update(item);
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindBhKhtBHXHChiTietByCondition(KhtBHXHChiTietCriteria searchModel)
        {
            return _khtBHXHChiTietRepository.FindBhKhtBHXHChiTietByCondition(searchModel);
        }

        public int AddRange(IEnumerable<BhKhtBHXHChiTiet> khtBhxhChiTiets)
        {
            return _khtBHXHChiTietRepository.AddRange(khtBhxhChiTiets);
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindKhtBHXHChiTietByIdBhxh(KhtBHXHChiTietCriteria searchModel)
        {
            return _khtBHXHChiTietRepository.FindKhtBHXHChiTietByIdBhxh(searchModel);
        }

        public int RemoveRange(IEnumerable<BhKhtBHXHChiTiet> khtBHXHChiTiets)
        {
            return _khtBHXHChiTietRepository.RemoveRange(khtBHXHChiTiets);
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindAll(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate)
        {
            return _khtBHXHChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<BhKhtBHXHChiTiet> FindByCondition(Expression<Func<BhKhtBHXHChiTiet, bool>> predicate)
        {
            return _khtBHXHChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHXH(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt)
        {
            return _khtBHXHChiTietRepository.FindReportKhtDuToanBHXH(namLamViec, bloaiChungTu, bDaTongHop, lstDonvi, khoiDuToan, khoiHachToan, dvt);
        }
        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtDuToanBHYT(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, int dvt)
        {
            return _khtBHXHChiTietRepository.FindReportKhtDuToanBHYT(namLamViec, bloaiChungTu, bDaTongHop, lstDonvi, khoiDuToan, khoiHachToan, sm, dvt);
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> FindReportKhtcTongHop(int namLamViec, string lstDonvi, int dvt)
        {
            return _khtBHXHChiTietRepository.FindReportKhtcTongHop(namLamViec, lstDonvi, dvt);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetPlanSalary(int iNam, string sLuongChinh, string sPhuCapCV, string sPhuCapTNN, string sPhuCapTNVK, string lstChungTuIds)
        {
            return _khtBHXHChiTietRepository.GetPlanSalary(iNam, sLuongChinh, sPhuCapCV, sPhuCapTNN, sPhuCapTNVK, lstChungTuIds);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetQuanSoBinhQuan(int iNam, string sLuongKeHoachId)
        {
            return _khtBHXHChiTietRepository.GetQuanSoBinhQuan(iNam, sLuongKeHoachId);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi)
        {
            return _khtBHXHChiTietRepository.GetAggregatePlanData(iNam, sMaDonVi);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> GetPlanData(int iNam, string sSoChungTu)
        {
            return _khtBHXHChiTietRepository.GetPlanData(iNam, sSoChungTu);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregate(int namLamViec, string maDonvis, int dvt)
        {
            return _khtBHXHChiTietRepository.PrintVoucherDetailAggregate(namLamViec, maDonvis, dvt);
        }

        public IEnumerable<BhKhtBHXHChiTietQuery> PrintVoucherDetailAggregateByUnits(int namLamViec, string maDonvis, bool isAggregate, int loaiChungTu, int dvt)
        {
            return _khtBHXHChiTietRepository.PrintVoucherDetailAggregateByUnits(namLamViec, maDonvis, isAggregate, loaiChungTu, dvt);
        }
    }
}
