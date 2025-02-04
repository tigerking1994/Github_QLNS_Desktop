using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDtTmBHYTTNChiTietService
    {
        IEnumerable<BhDtTmBHYTTNChiTiet> FindDttmBHYTChiTietById(DtTmBHYTTNChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhDtTmBHYTTNChiTiet> dttmBhytChiTiets);
        int AddRange(IEnumerable<BhDtTmBHYTTNChiTiet> dttmBhytChiTiets);
        BhDtTmBHYTTNChiTiet FindById(Guid id);
        int Update(BhDtTmBHYTTNChiTiet item);
        bool ExistBHXHChiTiet(Guid bhytId);
        IEnumerable<BhDtTmBHYTTNChiTiet> FindByParentId(DtTmBHYTTNChiTietCriteria searchCondition);
        IEnumerable<BhDtTmBHYTTNChiTiet> FindAllChungTuDuToan();
    }
}
