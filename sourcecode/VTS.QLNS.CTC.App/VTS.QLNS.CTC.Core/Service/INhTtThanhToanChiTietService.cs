using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhTtThanhToanChiTietService
    {
        void Add(NhTtThanhToanChiTiet nhTtThanhToanChiTiet);
        void Update(NhTtThanhToanChiTiet nhTtThanhToanChiTiet);
        void Delete(Guid id);
        void DeleteByDeNghiThanhToan(Guid deNghiThanhToan);
        NhTtThanhToanChiTiet FindById(Guid id);
        IEnumerable<NhTtThanhToanChiTiet> FindByCondition(Expression<Func<NhTtThanhToanChiTiet, bool>> predicate);
        IEnumerable<NhTtThanhToanChiTiet> FindByIdThanhToan(Guid id);
    }
}
