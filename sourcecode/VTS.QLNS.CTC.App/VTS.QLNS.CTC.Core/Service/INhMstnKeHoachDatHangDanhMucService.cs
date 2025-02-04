using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhMstnKeHoachDatHangDanhMucService
    {
        int AddRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities);
        int UpdateRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities);
        int DeleteRange(IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entitys);
        int Update(NhMSTNKeHoachDatHangDanhMuc entity);
        IEnumerable<NhMSTNKeHoachDatHangDanhMuc> FindByKHDatHangId(Guid? khDatHangId);
        void AddOrUpdate(Guid khDatHangId, IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities);
    }
}
