using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhMstnKeHoachDatHangDanhMucRepository : IRepository<NhMSTNKeHoachDatHangDanhMuc>
    {
        void DeleteByKHDatHangId(Guid khDatHangId);
        void AddOrUpdate(Guid khDatHangId, IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities);
    }
}
