using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCachTinhLuongChuanNq104Repository : IRepository<TlDmCachTinhLuongChuanNq104>
    {
        TlDmCachTinhLuongChuanNq104 FindByMaCot(string maCot);
    }
}
