using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcQBHXHChiTietGiaiThichRepository : IRepository<BhQtcQBHXHChiTietGiaiThich>
    {
        void RemoveGiaiThichBangLoiTheoChungTu(Guid id);
        BhQtcQBHXHChiTietGiaiThich FindByCondition(BhQtcQBHXHChiTietGiaiThichCriteria condition);
        BhQtcQBHXHChiTietGiaiThich FindById(Guid id);
        List<BhQtcQBHXHChiTietGiaiThichQuery> GetGiaiThichBangLoiTheoDonVi(int yearOfWork, string sMaDonVi, int iQuy, string sMABHXH);
    }
}
