using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ILoaiHopDongService
    {
        IEnumerable<VdtDmLoaiHopDong> FindAll();
    }
}
