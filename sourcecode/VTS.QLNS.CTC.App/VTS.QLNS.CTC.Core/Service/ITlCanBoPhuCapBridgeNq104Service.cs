using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapBridgeNq104Service
    {
        void DeleteAll();
        void BulkInsert(IEnumerable<TlCanBoPhuCapBridgeNq104> lstData);
        void DataPreprocess(int? thang = null, int? nam = null);
        IEnumerable<TlCanBoPhuCapBridgeNq104> FindByMaCanBo(string maCanBo);
        int UpdateRang(List<TlCanBoPhuCapBridgeNq104> entities);
        IEnumerable<TlCanBoPhuCapBridgeNq104> FindAll(Expression<Func<TlCanBoPhuCapBridgeNq104, bool>> predicate);

    }
}
