using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmNangLuongService
    {
        IEnumerable<TlDmNangLuong> FindAll();
    }
}
