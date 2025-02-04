using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongChuanService
    {
        IEnumerable<TlDmCachTinhLuongChuan> FindAll();
        TlDmCachTinhLuongChuan Find(Guid id);
        TlDmCachTinhLuongChuan FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongChuan entity);
        int Add(TlDmCachTinhLuongChuan entity);
        int Delete(Guid id);
    }
}
