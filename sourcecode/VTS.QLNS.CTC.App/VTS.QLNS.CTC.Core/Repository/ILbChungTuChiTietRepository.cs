using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ILbChungTuChiTietRepository : IRepository<NsNganhChungTuChiTiet>
    {
        IEnumerable<LbChungTuChiTietQuery> FindChungTuChiTietByCondition(AllocationDetailCriteria searchCondition);
        bool CheckExitsByChungTuId(Guid chungtuId);
        List<NsNganhChungTuChiTiet> FindByChungTuId(Guid chungTuId);
        void DeleteByChungTuId(Guid id);
    }
}

