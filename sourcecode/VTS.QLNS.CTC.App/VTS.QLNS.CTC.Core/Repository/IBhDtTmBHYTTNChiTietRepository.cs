using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDtTmBHYTTNChiTietRepository : IRepository<BhDtTmBHYTTNChiTiet>
    {
        bool ExistBHXHChiTiet(Guid bhytId);
        BhDtTmBHYTTNChiTiet FindById(Guid id);
        IEnumerable<BhDtTmBHYTTNChiTiet> FindByParentId(DtTmBHYTTNChiTietCriteria searchCondition);
        IEnumerable<BhDtTmBHYTTNChiTiet> FindDttmBHYTChiTietById(DtTmBHYTTNChiTietCriteria searchModel);
    }
}
