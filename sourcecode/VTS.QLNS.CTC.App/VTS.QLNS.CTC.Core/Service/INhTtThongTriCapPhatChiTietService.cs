using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhTtThongTriCapPhatChiTietService
    {
        IEnumerable<NhTtThongTriCapPhatChiTietQuery> FindAllChiTiet();
        IEnumerable<NhTtThongTriCapPhatChiTiet> FindAll();
        void Save(IEnumerable<NhTtThongTriCapPhatChiTiet> entities, NhTtThongTriCapPhat entity);
        int Delete(IEnumerable<NhTtThongTriCapPhatChiTiet> entities);
        IEnumerable<NhTtThongTriCapPhatChiTiet> FindByIdThongTriCapPhat(Guid Id);
    }
}
