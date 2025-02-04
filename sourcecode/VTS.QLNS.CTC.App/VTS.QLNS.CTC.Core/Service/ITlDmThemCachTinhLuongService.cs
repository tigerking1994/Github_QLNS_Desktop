using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmThemCachTinhLuongService
    {
        IEnumerable<TlDmThemCachTinhLuong> FindAll();
        TlDmThemCachTinhLuong FindByMaCachTinhLuong(string maCachTinhLuong);
    }
}
