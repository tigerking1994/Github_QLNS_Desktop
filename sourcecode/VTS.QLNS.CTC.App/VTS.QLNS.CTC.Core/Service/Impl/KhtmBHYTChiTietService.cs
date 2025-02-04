using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class KhtmBHYTChiTietService : IKhtmBHYTChiTietService
    {
        private readonly IKhtmBHYTChiTietRepository _khtmBHYTChiTietRepository;

        public KhtmBHYTChiTietService(IKhtmBHYTChiTietRepository khtmBHYTChiTietRepository)
        {
            _khtmBHYTChiTietRepository = khtmBHYTChiTietRepository;
        }

        public int AddRange(IEnumerable<BhKhtmBHYTChiTiet> khtmBhytChiTiets)
        {
            return _khtmBHYTChiTietRepository.AddRange(khtmBhytChiTiets);
        }

        public bool ExistBHYTChiTiet(Guid bhytId)
        {
            return _khtmBHYTChiTietRepository.ExistBHYTChiTiet(bhytId);
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindAll(Expression<Func<BhKhtmBHYTChiTiet, bool>> predicate)
        {
            return _khtmBHYTChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindAll()
        {
            return _khtmBHYTChiTietRepository.FindAll();
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTChiTietByCondition(KhtmBHYTChiTietCriteria searchModel)
        {
            return _khtmBHYTChiTietRepository.FindBhKhtmBHYTChiTietByCondition(searchModel);
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindBhKhtmBHYTReportByCondition(KhtmBHYTChiTietCriteria searchModel)
        {
            return _khtmBHYTChiTietRepository.FindBhKhtmBHYTReportByCondition(searchModel);
        }

        public List<BhKhtmBHYTChiTietQuery> FindBhKhtmBHYTTongHopChiTietByCondition(KhtmBHYTChiTietCriteria searchCondition)
        {
            return _khtmBHYTChiTietRepository.FindBhKhtmBHYTTongHopChiTietByCondition(searchCondition);
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindByCondition(Expression<Func<BhKhtmBHYTChiTiet, bool>> predicate)
        {
            return _khtmBHYTChiTietRepository.FindAll(predicate);
        }

        public BhKhtmBHYTChiTiet FindById(Guid id)
        {
            return _khtmBHYTChiTietRepository.FindById(id);
        }

        public IEnumerable<BhKhtmBHYTChiTiet> FindKhtmBHYTChiTietByIdBhyt(KhtmBHYTChiTietCriteria searchModel)
        {
            return _khtmBHYTChiTietRepository.FindKhtmBHYTChiTietByIdBhyt(searchModel);
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytHSSV(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi, int dvt)
        {
            return _khtmBHYTChiTietRepository.FindKhtmDuToanThuBhytHSSV(namLamViec, bloaiChungTu, bDaTongHop, lstDonvi, hSSV, luuHS, hVSQ, sQDuBi, dvt);
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> FindKhtmDuToanThuBhytThanNhan(int namLamViec, int bloaiChungTu, bool bDaTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, string smDuToan, string smHachToan, int dvt)
        {
            return _khtmBHYTChiTietRepository.FindKhtmDuToanThuBhytThanNhan(namLamViec, bloaiChungTu, bDaTongHop, lstDonvi, thanNhanQuanNhan, thanNhanCNVQP, smDuToan, smHachToan, dvt);
        }

        public IEnumerable<BhKhtmBHYTChiTietQuery> GetAggregatePlanData(int iNam, string sMaDonVi)
        {
            return _khtmBHYTChiTietRepository.GetAggregatePlanData(iNam, sMaDonVi);
        }

        public IEnumerable<BhKhtmBHYTChiTietQuery> GetPlanData(int iNam, string sSoChungTu)
        {
            return _khtmBHYTChiTietRepository.GetPlanData(iNam, sSoChungTu);
        }

        public int RemoveRange(IEnumerable<BhKhtmBHYTChiTiet> bhytChungTuChiTiets)
        {
            return _khtmBHYTChiTietRepository.RemoveRange(bhytChungTuChiTiets);
        }

        public int Update(BhKhtmBHYTChiTiet item)
        {
            return _khtmBHYTChiTietRepository.Update(item);
        }
    }
}
