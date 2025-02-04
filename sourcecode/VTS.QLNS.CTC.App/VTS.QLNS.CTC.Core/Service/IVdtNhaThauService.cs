using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtNhaThauService
    {
        IEnumerable<VdtDmNhaThau> FindAll();
        VdtDmNhaThau FindById(Guid iIdNhaThau);
        IEnumerable<VdtDmNhaThau> GetNhaThauByHopDong(Guid iIdHopDongId);
        IEnumerable<string> GetListSTKByNhaThau(Guid iIdNhaThau);
    }
}
