using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlDmCachTinhLuongBaoHiemNq104Repository : IRepository<TlDmCachTinhLuongBaoHiemNq104>
    {
        TlDmCachTinhLuongBaoHiemNq104 FindByMaCot(string maCot);
    }
}
