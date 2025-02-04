using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcCapKinhPhiKcbChiTietService
    {
        IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindChungTuChiTietByChungTuId(BhQtCapKinhPhiKcbChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhQtCapKinhPhiKcbChiTiet> chungTuChiTiets);
        IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindVoucherDetailByCondition(BhQtCapKinhPhiKcbChiTietCriteria searchModel);
        int AddRange(IEnumerable<BhQtCapKinhPhiKcbChiTiet> chungTuChiTiets);
        BhQtCapKinhPhiKcbChiTiet FindById(Guid id);
        int Update(BhQtCapKinhPhiKcbChiTiet item);
        bool ExistVoucherDetail(Guid chungTuId);
        IEnumerable<BhQtCapKinhPhiKcbChiTiet> FindAllVouchers();
    }
}
