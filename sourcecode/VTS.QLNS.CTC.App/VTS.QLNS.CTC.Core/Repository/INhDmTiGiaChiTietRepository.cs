using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDmTiGiaChiTietRepository : IRepository<NhDmTiGiaChiTiet>
    {
        IEnumerable<NhDmTiGiaChiTiet> FindByTiGia(Guid idTiGia);
        IEnumerable<NhDmTiGiaChiTiet> FindAll();
    }
}
