using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCanBoKeHoachNq104Service
    {
        IEnumerable<TlDmCanBoKeHoachNq104> FindAll();
        IEnumerable<TlDmCanBoKeHoachNq104> FindLoadIndex();
        IEnumerable<TlDmCanBoKeHoachNq104> FindByCondition(Expression<Func<TlDmCanBoKeHoachNq104, bool>> predicate);
        int Add(TlDmCanBoKeHoachNq104 tlDmCanBoKeHoach);
        int AddRange(IEnumerable<TlDmCanBoKeHoachNq104> tlDmCanBoKeHoach);
        IEnumerable<TlDmCanBoKeHoachNq104> FindByYear(int year);
        int Delete(Guid id);
        int DeleteByYear(int year);
        TlDmCanBoKeHoachNq104 Find(Guid id);
        int Update(TlDmCanBoKeHoachNq104 tlDmCanBo);
        IEnumerable<TlDmCanBoKeHoachNq104> FindAllCanBo();
        TlDmCanBoKeHoachNq104 FindByMaCanBo(string maCanbo);
    }
}
