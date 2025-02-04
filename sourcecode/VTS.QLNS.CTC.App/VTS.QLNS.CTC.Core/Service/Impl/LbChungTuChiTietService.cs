using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class LbChungTuChiTietService : ILbChungTuChiTietService
    {
        private readonly ILbChungTuChiTietRepository _chungTuChiTietRepository;

        public LbChungTuChiTietService(ILbChungTuChiTietRepository chungTuChiTietRepository)
        {
            _chungTuChiTietRepository = chungTuChiTietRepository;
        }

        public NsNganhChungTuChiTiet Add(NsNganhChungTuChiTiet entity)
        {
            _chungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int AddRange(IEnumerable<NsNganhChungTuChiTiet> entities)
        {
            return _chungTuChiTietRepository.AddRange(entities);
        }

        public bool CheckExitsByChungTuId(Guid chungtuId)
        {
            return _chungTuChiTietRepository.CheckExitsByChungTuId(chungtuId);
        }

        public int Delete(Guid id)
        {
            NsNganhChungTuChiTiet entity = _chungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _chungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public NsNganhChungTuChiTiet Find(params object[] keyValues)
        {
            return _chungTuChiTietRepository.Find(keyValues);
        }

        public IEnumerable<NsNganhChungTuChiTiet> FindAll()
        {
            return _chungTuChiTietRepository.FindAll();
        }

        public IEnumerable<LbChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition)
        {
            var rs = new List<LbChungTuChiTietQuery>();
            IEnumerable<LbChungTuChiTietQuery> _listChungTuChiTiet = _chungTuChiTietRepository.FindChungTuChiTietByCondition(searchCondition);
            //xử lý data
            List<string> listLNS = LNSEnumerableExtensions.SplitLNS(searchCondition.LNS.Split(','));
            var lookup = _listChungTuChiTiet.ToLookup(x => x.Lns);
            foreach (var lns in listLNS)
            {
                rs.AddRange(lookup[lns]);
            }
            return rs.OrderBy(x => x.XauNoiMa);
        }

        public int Update(NsNganhChungTuChiTiet entity)
        {
            return _chungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<NsNganhChungTuChiTiet> FindByCondition(Expression<Func<NsNganhChungTuChiTiet, bool>> predicate)
        {
            return _chungTuChiTietRepository.FindAll(predicate);
        }

        public List<NsNganhChungTuChiTiet> FindByChungTuId(Guid chungTuId)
        {
            return _chungTuChiTietRepository.FindByChungTuId(chungTuId);
        }

        public void DeleteByChungTuId(Guid id)
        {
            _chungTuChiTietRepository.DeleteByChungTuId(id);
        }
    }
}

