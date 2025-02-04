using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCachTinhLuongTruyLinhService
    {
        IEnumerable<TlDmCachTinhLuongTruyLinh> FindAll();
        TlDmCachTinhLuongTruyLinh Find(Guid id);
        TlDmCachTinhLuongTruyLinh FindByMaCot(string maCot);
        int Update(TlDmCachTinhLuongTruyLinh entity);
        int Add(TlDmCachTinhLuongTruyLinh entity);
        int Delete(Guid id);
    }
}
