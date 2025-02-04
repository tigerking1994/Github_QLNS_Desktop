using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDttBHXHChiTietRepository : IRepository<BhDttBHXHChiTiet>
    {
        IEnumerable<BhDttBHXHChiTiet> FindDttBHXHChiTietByIdBhxh(DttBHXHChiTietCriteria searchCondition);
        BhDttBHXHChiTiet FindById(Guid id);
        bool ExistBHXHChiTiet(Guid bhxhId);
        IEnumerable<BhDttBHXHChiTiet> FindByParentId(DttBHXHChiTietCriteria searchCondition);
        IEnumerable<BhDttBHXHChiTiet> FindByIdDTT(Guid Id);
    }
}
