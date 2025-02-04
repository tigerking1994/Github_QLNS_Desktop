using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class CpChungTuChiTietService : ICpChungTuChiTietService
    {
        private readonly ICpChungTuChiTietRepository _cpChungTuChiTietRepository;

        public CpChungTuChiTietService(ICpChungTuChiTietRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public NsCpChungTuChiTiet Add(NsCpChungTuChiTiet entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int AddRange(IEnumerable<NsCpChungTuChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            return _cpChungTuChiTietRepository.CheckExitsByChungTuId(chungtuId);
        }

        public int Delete(Guid id)
        {
            NsCpChungTuChiTiet entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public NsCpChungTuChiTiet Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public IEnumerable<NsCpChungTuChiTiet> FindAll()
        {
            return _cpChungTuChiTietRepository.FindAll();
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition, bool bQueryAll)
        {
            return _cpChungTuChiTietRepository.FindChungTuChiTietByCondition(searchCondition, bQueryAll);
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionForExport(AllocationDetailCriteria searchCondition)
        {
            var rs = new List<CpChungTuChiTietQuery>();
            var _listChungTuChiTiet = _cpChungTuChiTietRepository.FindChungTuChiTietByConditionForExport(searchCondition);
            List<string> listLNS = LNSEnumerableExtensions.SplitLNS(searchCondition.LNS.Split(','));
            var lookup = _listChungTuChiTiet.ToLookup(x => x.Lns);
            foreach (var lns in listLNS)
            {
                rs.AddRange(lookup[lns]);
            }
            return rs.OrderBy(x => x.XauNoiMa);
        }

        public int Update(NsCpChungTuChiTiet entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<NsCpChungTuChiTiet> FindByCondition(Expression<Func<NsCpChungTuChiTiet, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public IEnumerable<CpChungTuChiTietDuToanQuery> FindChungTuChiTietDuToanByCondition(AllocationDetailCriteria searchCondition)
        {
            return _cpChungTuChiTietRepository.FindChungTuChiTietDuToanByCondition(searchCondition);
        }

        public IEnumerable<CpChungTuChiTietDaCapQuery> FindChungTuChiTietDaCapByCondition(AllocationDetailCriteria searchCondition)
        {
            return _cpChungTuChiTietRepository.FindChungTuChiTietDaCapByCondition(searchCondition);
        }

        public void DeleteByVoucherId(Guid voucherId)
        {
            _cpChungTuChiTietRepository.DeleteByVoucherId(voucherId);
        }

        public void CreateVoudcherSummary(string idChungTu, string idDonVi, string nguoiTao, int namLamViec, int namNganSach, int nguonNganSach, string idChungTuSummary)
        {
            _cpChungTuChiTietRepository.CreateVoudcherSummary(idChungTu, idDonVi, nguoiTao, namLamViec, namNganSach, nguonNganSach, idChungTuSummary);
        }

        public int RemoveRange(IEnumerable<NsCpChungTuChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.RemoveRange(entities);
        }

        public IEnumerable<CpChungTuChiTietQuery> FindChungTuChiTietByConditionSummary(AllocationDetailCriteria searchCondition)
        {
            return _cpChungTuChiTietRepository.FindChungTuChiTietByConditionSummary(searchCondition);
        }
    }
}
