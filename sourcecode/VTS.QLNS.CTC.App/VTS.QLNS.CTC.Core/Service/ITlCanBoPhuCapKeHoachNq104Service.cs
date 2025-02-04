using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapKeHoachNq104Service
    {
        int AddRange(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches);
        int DeleteByMaCanBo(string maCanBo);
        int DeleteByYear(int year);
        int DeleteManyMaCanBo(string lstMaCanBo);
        IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoachNq104, bool>> predicate);
        IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByMaCanBo(string maCanBo);
        void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches);
        void BulkUpdate(IEnumerable<TlCanBoPhuCapKeHoachNq104> tlCanBoPhuCapKeHoaches);
        IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo);
    }
}
