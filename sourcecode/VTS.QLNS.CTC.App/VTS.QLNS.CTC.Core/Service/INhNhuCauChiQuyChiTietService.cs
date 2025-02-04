using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhNhuCauChiQuyChiTietService 
    {
        IEnumerable<NhuCauChiQuyNhiemVuChiQuery> FindNhiemVuChiByIdDonVi(Guid? idDonVi);
        void Save(IEnumerable<NhNhuCauChiQuyChiTiet> entities);
        void Delete(IEnumerable<NhNhuCauChiQuyChiTiet> entities);
        IEnumerable<NhNhuCauChiQuyChiTiet> FindByIdChiQuy(Guid idChiQuy);
        NhNhuCauChiQuyChiTiet FindByIdHopDong(Guid? idHopDong);
        void AddRange(List<NhNhuCauChiQuyChiTiet> entities);
        
        IEnumerable<NhNhuCauChiQuyKinhPhiDaChiQuery> KinhPhiDaChi(Guid idHopDong, int nam);
    }
}
