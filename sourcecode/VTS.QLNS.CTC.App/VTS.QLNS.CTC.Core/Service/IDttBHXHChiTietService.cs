using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDttBHXHChiTietService
    {
        IEnumerable<BhDttBHXHChiTiet> FindDttBHXHChiTietByIdBhxh(DttBHXHChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhDttBHXHChiTiet> bhxhChungTuChiTiets);
        int AddRange(IEnumerable<BhDttBHXHChiTiet> dttBhxhChiTiets);
        BhDttBHXHChiTiet FindById(Guid id);
        int Update(BhDttBHXHChiTiet item);
        bool ExistBHXHChiTiet(Guid bhxhId);
        IEnumerable<BhDttBHXHChiTiet> FindByParentId(DttBHXHChiTietCriteria searchCondition);
        IEnumerable<BhDttBHXHChiTiet> FindAllChungTuDuToan();
        IEnumerable<BhDttBHXHChiTiet> FindByIdDTT(Guid Id);
    }
}
