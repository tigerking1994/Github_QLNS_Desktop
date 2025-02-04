using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtNhaThauRepository : IRepository<VdtDmNhaThau>
    {
        IEnumerable<VdtDmNhaThau> GetNhaThauByHopDong(Guid iIdHopDongId);
        IEnumerable<string> GetListSTKByNhaThau(Guid iIdNhaThau);
    }
}
