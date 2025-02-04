using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapNq104Service
    {
        TlCanBoPhuCapNq104 FirstOrDefault(Expression<Func<TlCanBoPhuCapNq104, bool>> predicate);
        int AddRange(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        int Add(TlCanBoPhuCapNq104 tlCanBoPhuCap);
        IEnumerable<TlCanBoPhuCapNq104> FindByMaCanBo(string maCanBo);
        int DeleteByMaCanBo(string maCanBo);
        int UpdateRange(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        IEnumerable<TlCanBoPhuCapNq104> FindAll();
        int UpdateRang(List<TlCanBoPhuCapNq104> entities);
        IEnumerable<TlCanBoPhuCapNq104> FindAll(Expression<Func<TlCanBoPhuCapNq104, bool>> predicate);
        int Update(TlCanBoPhuCapNq104 tlCanBoPhuCap);
        void BulkInsert(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        void BulkUpdate(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps);
        IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo);
        int DeleteCanBo(string maCanBo);
        TlCanBoPhuCapNq104 FindByMaCanBoAndMaPhuCap(string maCanBo, string maPhuCap);
        IEnumerable<TLCanBoPhuCapNq104Query> Copy(string maCanBo, int fromYear, int fromMonth, int toYear, int toMonth, bool isCopyValue);
        void UpdateMulti(IEnumerable<TlCanBoPhuCapNq104> tlCanBoPhuCaps, IEnumerable<TlCanBoPhuCapBridgeNq104> tlCanBoPhuCapBridges);
        IEnumerable<TlCanBoPhuCapNq104> FindByMaCbo(string maCanbo);
        IEnumerable<TlCanBoPhuCapNq104> FindDataCanBoPhuCap(string lstCBo);
    }
}
