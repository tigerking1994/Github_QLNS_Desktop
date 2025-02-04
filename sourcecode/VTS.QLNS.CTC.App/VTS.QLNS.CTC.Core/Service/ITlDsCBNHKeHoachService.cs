using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDsCBNHKeHoachService
    {
        IEnumerable<TlDsCBNHKeHoach> FindAll();
        IEnumerable<TlDsCBNHKeHoach> FindLoadIndex();
        IEnumerable<TlDsCBNHKeHoach> FindByCondition(Expression<Func<TlDsCBNHKeHoach, bool>> predicate);
        int Add(TlDsCBNHKeHoach tlDmCanBoKeHoach);
        int AddRange(IEnumerable<TlDsCBNHKeHoach> tlDmCanBoKeHoach);
        IEnumerable<TlDsCBNHKeHoach> FindByYear(int year);
        int Delete(Guid id);
        int DeleteByYear(int year);
        TlDsCBNHKeHoach Find(Guid id);
        int Update(TlDsCBNHKeHoach tlDmCanBo);
        IEnumerable<TlDsCBNHKeHoach> FindAllCanBoNghiHuu();
        IEnumerable<TlDsCBNHKeHoach> FinAllCanBoNghiHuu();
        TlDsCBNHKeHoach FindByMaCanBo(string maCanbo);
    }
}
