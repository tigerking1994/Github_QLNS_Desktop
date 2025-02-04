using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ITlCanBoPhuCapKeHoachNq104Repository : IRepository<TlCanBoPhuCapKeHoachNq104>
    {
        int DeleteByMaCanBo(string maCanBo);
        int DeleteByYear(int year);
        int DeleteManyMaCanBo(string lstMaCanBo);
        IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoachNq104, bool>> predicate);
        IEnumerable<TlCanBoPhuCapKeHoachNq104> FindByMaCanBo(string maCanBo);
        IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCap(string maCanBo);
    }
}
