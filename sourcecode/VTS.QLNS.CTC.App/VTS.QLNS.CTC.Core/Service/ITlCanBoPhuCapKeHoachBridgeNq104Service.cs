using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapKeHoachBridgeNq104Service
    {
        void DeleteAll();
        void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> lstData);
        void DataPreprocess(int? thang = null, int? nam = null);
        IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> FindByMaCanBo(string maCanBo);
        int UpdateRang(List<TlCanBoPhuCapKeHoachBridgeNq104> entities);
        IEnumerable<TlCanBoPhuCapKeHoachBridgeNq104> FindAll(Expression<Func<TlCanBoPhuCapKeHoachBridgeNq104, bool>> predicate);
    }
}
