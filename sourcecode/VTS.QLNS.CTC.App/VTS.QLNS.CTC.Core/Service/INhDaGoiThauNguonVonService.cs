using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaGoiThauNguonVonService 
    {
        IEnumerable<NhDaGoiThauNguonVon> FindAll();
        int AddRange(List<NhDaGoiThauNguonVon> entitis);
        int Add(NhDaGoiThauNguonVon entitis);
        int UpdateRange(List<NhDaGoiThauNguonVon> entitis);
        int Update(NhDaGoiThauNguonVon entity);
        int DeleteNguonVon(Guid idNguonVon);
        IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVon(Guid idGoiThau);
        IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauNguonVon> FindByListNguonVonGoiThauId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauNguonVon> GetListNguonVonByIdGoiThau(Guid idGoiThau);
        IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByKhlcntId(Guid iIdKhlcnt);
        IEnumerable<NhDaDetailNguonVonQuery> GetGoiThauNguonVonByGoiThauId(Guid iIdKhlcnt);
        IEnumerable<NhDaGoiThauThongTinNguonVonQuery> FindByIdGoiThau(Guid idGoiThau);
        NhDaGoiThauThongTinNguonVonQuery FindById(Guid id, Guid idGoiThau);
        IEnumerable<NhDaCacQuyetDinhNguonVonGoiThauQuery> FindCacQuyetDinhNguonVonByIdGoiThau(Guid idGoiThau);
    }
}
