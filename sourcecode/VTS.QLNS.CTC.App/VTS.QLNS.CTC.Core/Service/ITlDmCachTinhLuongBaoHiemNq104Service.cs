using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongBaoHiemNq104Service
    {
        IEnumerable<TlDmCachTinhLuongBaoHiemNq104> FindAll();
        TlDmCachTinhLuongBaoHiemNq104 Find(Guid id);
        TlDmCachTinhLuongBaoHiemNq104 FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongBaoHiemNq104 entity);
        int Add(TlDmCachTinhLuongBaoHiemNq104 entity);
        int Delete(Guid id);
    }
}
