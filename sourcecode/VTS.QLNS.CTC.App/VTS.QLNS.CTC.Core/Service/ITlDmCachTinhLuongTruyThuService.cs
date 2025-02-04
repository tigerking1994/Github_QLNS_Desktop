using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongTruyThuService
    {
        IEnumerable<TlDmCachTinhLuongTruyThu> FindAll();
        TlDmCachTinhLuongTruyThu Find(Guid id);
        TlDmCachTinhLuongTruyThu FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongTruyThu entity);
        int Add(TlDmCachTinhLuongTruyThu entity);
        int Delete(Guid id);
    }
}
