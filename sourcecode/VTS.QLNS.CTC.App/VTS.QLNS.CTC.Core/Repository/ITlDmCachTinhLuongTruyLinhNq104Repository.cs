using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCachTinhLuongTruyLinhNq104Repository : IRepository<TlDmCachTinhLuongTruyLinhNq104>
    {
        TlDmCachTinhLuongTruyLinhNq104 FindByMaCot(string maCot);
    }
}
