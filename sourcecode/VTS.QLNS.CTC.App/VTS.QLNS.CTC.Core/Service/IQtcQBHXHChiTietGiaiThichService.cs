using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQtcQBHXHChiTietGiaiThichService
    {
        void Add(BhQtcQBHXHChiTietGiaiThich chungTuChiTietGiaiThich);
        BhQtcQBHXHChiTietGiaiThich FindById(Guid id);
        void Update(BhQtcQBHXHChiTietGiaiThich chungTuChiTietGiaiThich);
        BhQtcQBHXHChiTietGiaiThich FindByCondition(BhQtcQBHXHChiTietGiaiThichCriteria condition);
        int Delete(Guid id);
        List<BhQtcQBHXHChiTietGiaiThichQuery> GetGiaiThichBangLoiTheoDonVi(int yearOfWork, string sMaDonVi, int iQuy, string sMABHXH);
        void RemoveGiaiThichBangLoiTheoChungTu(Guid id);
    }
}
