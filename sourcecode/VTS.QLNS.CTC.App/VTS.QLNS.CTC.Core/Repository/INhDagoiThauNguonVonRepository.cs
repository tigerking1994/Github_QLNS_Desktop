using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDagoiThauNguonVonRepository : IRepository<NhDaGoiThauNguonVon>
    {
        IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByGoiThauId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauThongTinNguonVonQuery> FindByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaCacQuyetDinhNguonVonGoiThauQuery> FindCacQuyetDinhNguonVonByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaGoiThauNguonVon> GetListNguonVonByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonGoiThauId(Guid iIdKhlcnt);

        
    }
}
