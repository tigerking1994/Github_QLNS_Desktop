using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongBaoHiemService
    {
        IEnumerable<TlDmCachTinhLuongBaoHiem> FindAll();
        TlDmCachTinhLuongBaoHiem Find(Guid id);
        TlDmCachTinhLuongBaoHiem FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongBaoHiem entity);
        int Add(TlDmCachTinhLuongBaoHiem entity);
        int Delete(Guid id);
    }
}
