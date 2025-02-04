using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhKtKhoiTaoCapPhatChiTietService
    {
        void AddOrUpdate(IEnumerable<NhKtKhoiTaoCapPhatChiTiet> entities);
        IEnumerable<NhKtKhoiTaoCapPhatChiTiet> FindByKhoiTaoCapPhatId(Guid khoiTaoCapPhatId);
        IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindById(Guid khoiTaoCapPhatId);
        IEnumerable<NhKtKhoiTaoCapPhatChiTietQuery> FindDetailById(Guid khoiTaoCapPhatId);
        NhKtKhoiTaoCapPhatChiTiet FetchData(Guid khoiTaoCapPhatId, Guid hopDongId);
    }
}
