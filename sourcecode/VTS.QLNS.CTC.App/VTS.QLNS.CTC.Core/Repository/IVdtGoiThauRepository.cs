using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtGoiThauRepository : IRepository<VdtDaGoiThau>
    {
        IEnumerable<GoiThauQuery> FindGoiThauByDuAnId(string idDuAn, Guid? iIdHopDong);
        IEnumerable<GoiThauQuery> FindGoiThauByHopDong(Guid? iIdHopDong);
        IEnumerable<VdtDaGoiThau> FindGoiThauDieuChinh(Guid khLuaChonNhaThauId);
    }
}
