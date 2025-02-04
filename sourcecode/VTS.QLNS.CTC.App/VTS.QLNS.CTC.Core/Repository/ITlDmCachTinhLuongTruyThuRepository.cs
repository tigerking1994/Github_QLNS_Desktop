using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCachTinhLuongTruyThuRepository : IRepository<TlDmCachTinhLuongTruyThu>
    {
        TlDmCachTinhLuongTruyThu FindByMaCot(string maCot);
    }
}
