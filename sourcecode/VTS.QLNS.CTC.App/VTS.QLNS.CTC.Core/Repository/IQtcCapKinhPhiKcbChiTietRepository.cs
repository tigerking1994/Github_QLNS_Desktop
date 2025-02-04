using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcCapKinhPhiKcbChiTietRepository : IRepository<BhQtCapKinhPhiKcbChiTiet>
    {
        IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindChungTuChiTietByChungTuId(BhQtCapKinhPhiKcbChiTietCriteria searchCondition);
        IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindVoucherDetailByCondition(BhQtCapKinhPhiKcbChiTietCriteria searchCondition);
        BhQtCapKinhPhiKcbChiTiet FindById(Guid id);
        bool ExistVoucherDetail(Guid chungTuId);
    }
}
