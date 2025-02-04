using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhTtThanhToanService
    {
        void Add(NhTtThanhToan nhTtThanhToan);
        void Update(NhTtThanhToan nhTtThanhToan);
        void Delete(Guid id);
        void Delete(NhTtThanhToan entity);
        NhTtThanhToan FindById(Guid id);
        IEnumerable<NhTtThanhToan> FindByCondition(Expression<Func<NhTtThanhToan, bool>> predicate);
        IEnumerable<NhTtThanhToanQuery> FindIndex(int yearOfWork, int iTrangThai, bool bIsDeNghi);
        void LockOrUnlock(Guid id, bool status);
        void TongHopDeNghiThanhToan(NhTtThanhToan vdtTtDeNghiThanhToan, List<Guid> voucherAgregatesIds);
        List<NhTtThanhToan> FindDeNghiTongHop();
    }
}
