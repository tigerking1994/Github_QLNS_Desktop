using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhTtThanhToanChiTietRepository : IRepository<NhTtThanhToanChiTiet>
    { 
        void AddOrUpdate(Guid thanhToanId, IEnumerable<NhTtThanhToanChiTiet> entities);
        void DeleteByDeNghiThanhToan(Guid deNghiThanhToan);
    }
}
