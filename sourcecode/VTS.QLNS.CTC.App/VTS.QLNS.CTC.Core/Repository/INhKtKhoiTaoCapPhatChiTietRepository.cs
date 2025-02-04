using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhKtKhoiTaoCapPhatChiTietRepository : IRepository<NhKtKhoiTaoCapPhatChiTiet>
    {
        IEnumerable<NhKtKhoiTaoCapPhatChiTiet> FetchData(Guid khoiTaoCapPhatId, Guid hopDongId);
        IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindById(Guid khoiTaoCapPhatId);
        IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindDetailById(Guid khoiTaoCapPhatId);

    }
}
