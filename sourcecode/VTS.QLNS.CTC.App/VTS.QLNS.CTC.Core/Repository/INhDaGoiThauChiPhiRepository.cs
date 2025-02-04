using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaGoiThauChiPhiRepository  : IRepository<NhDaGoiThauChiPhi>
    {
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByKhlcnt(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByGT(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByGoiThauId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauChiPhiQuery> FindByGoiThauId(Guid idGoiThau);
        IEnumerable<NhDaCacQuyetDinhChiPhiGoiThauQuery> FindByCacQuyetDinhChiPhiByGoiThauId(Guid idGoiThau);
    }
}
