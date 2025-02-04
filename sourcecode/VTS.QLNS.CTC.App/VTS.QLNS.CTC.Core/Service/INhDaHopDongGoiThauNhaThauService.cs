using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaHopDongGoiThauNhaThauService
    {
        void AddRange(List<NhDaHopDongGoiThauNhaThau> entities);
        void UpDateRange(List<NhDaHopDongGoiThauNhaThau> entities);
        void UpDate(NhDaHopDongGoiThauNhaThau entity);
        void Delete(NhDaHopDongGoiThauNhaThau entity);
        IEnumerable<NhDaHopDongGoiThauNhaThauQuery> FindByIdHopDong(Guid? idHopDong);
    }
}
