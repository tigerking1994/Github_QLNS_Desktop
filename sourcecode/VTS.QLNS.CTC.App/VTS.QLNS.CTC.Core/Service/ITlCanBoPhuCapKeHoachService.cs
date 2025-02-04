using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlCanBoPhuCapKeHoachService
    {
        int AddRange(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches);
        int DeleteByMaCanBo(string maCanBo);
        int DeleteByYear(int year);
        int DeleteManyMaCanBo(string lstMaCanBo);
        IEnumerable<TlCanBoPhuCapKeHoach> FindByCondition(Expression<Func<TlCanBoPhuCapKeHoach, bool>> predicate);
        IEnumerable<TlCanBoPhuCapKeHoach> FindByMaCanBo(string maCanBo);
        void BulkInsert(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches);
        void BulkUpdate(IEnumerable<TlCanBoPhuCapKeHoach> tlCanBoPhuCapKeHoaches);
        IEnumerable<TlPhuCapQuery> FindCanBoPhuCap(string maCanBo);
        IEnumerable<TlPhuCapNq104Query> FindCanBoPhuCapNq104(string maCanBo);
        int DeleteByUnitYear(int year, string unit);
        IEnumerable<TlCanBoPhuCapKeHoachQuery> FindCanBoPhuCapKeHoachByMaCanBo(string maCanBo);
    }
}
