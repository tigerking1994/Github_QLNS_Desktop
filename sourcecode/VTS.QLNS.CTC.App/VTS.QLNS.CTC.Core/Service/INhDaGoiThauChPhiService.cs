using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaGoiThauChiPhiService
    {
        IEnumerable<NhDaGoiThauChiPhi> FindAll();
        IEnumerable<NhDaGoiThauChiPhi> FindAll(Expression<Func<NhDaGoiThauChiPhi, bool>> predicate);
        int AddRange(List<NhDaGoiThauChiPhi> entitis);
        int Add(NhDaGoiThauChiPhi entity);
        int UpdateRange(List<NhDaGoiThauChiPhi> entitis);
        int Update(NhDaGoiThauChiPhi entity);
        int Delete(Guid idGoiThau);
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhi(Guid idGoiThau);
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByNguonVon(Guid idNguonVon);
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByKhlcnt(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauChiPhi> FindListChiPhiByGT(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailChiPhiQuery> GetGoiThauChiPhiByGoiThauId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauChiPhiQuery> FindByGoiThauId(Guid idGoiThau);
        IEnumerable<NhDaCacQuyetDinhChiPhiGoiThauQuery> FindByCacQuyetDinhChiPhiByGoiThauId(Guid idGoiThau);
    }
}
