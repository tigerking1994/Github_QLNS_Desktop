using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhTtThanhToanRepository : IRepository<NhTtThanhToan>
    {
        IEnumerable<NhTtThanhToanQuery> FindIndex(int yearOfWork, int iTrangThai, bool bIsDeNghi);
        void TongHopDeNghiThanhToan(NhTtThanhToan nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds);
        List<NhTtThanhToan> FindDeNghiTongHop();
        void RemoveParentIdOfChildren(Guid id);
    }
}
