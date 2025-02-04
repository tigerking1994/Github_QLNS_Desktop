using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ILbChungTuChiTietService
    {
        IEnumerable<NsNganhChungTuChiTiet> FindAll();
        NsNganhChungTuChiTiet Add(NsNganhChungTuChiTiet entity);
        IEnumerable<LbChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition);
        NsNganhChungTuChiTiet Find(params object[] keyValues);
        int Update(NsNganhChungTuChiTiet entity);
        int AddRange(IEnumerable<NsNganhChungTuChiTiet> entities);
        int Delete(Guid id);
        bool CheckExitsByChungTuId(Guid chungtuId);
        public IEnumerable<NsNganhChungTuChiTiet> FindByCondition(Expression<Func<NsNganhChungTuChiTiet, bool>> predicate);
        List<NsNganhChungTuChiTiet> FindByChungTuId(Guid chungTuId);
        void DeleteByChungTuId(Guid id);
    }
}
