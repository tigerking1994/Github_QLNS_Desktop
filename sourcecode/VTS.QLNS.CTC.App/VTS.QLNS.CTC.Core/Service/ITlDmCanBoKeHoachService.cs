using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ITlDmCanBoKeHoachService
    {
        IEnumerable<TlDmCanBoKeHoach> FindAll();
        IEnumerable<TlDmCanBoKeHoach> FindLoadIndex();
        IEnumerable<TlDmCanBoKeHoach> FindByCondition(Expression<Func<TlDmCanBoKeHoach, bool>> predicate);
        int Add(TlDmCanBoKeHoach tlDmCanBoKeHoach);
        int AddRange(IEnumerable<TlDmCanBoKeHoach> tlDmCanBoKeHoach);
        IEnumerable<TlDmCanBoKeHoach> FindByYear(int year);
        int Delete(Guid id);
        int DeleteByYear(int year);
        TlDmCanBoKeHoach Find(Guid id);
        int Update(TlDmCanBoKeHoach tlDmCanBo);
        IEnumerable<TlDmCanBoKeHoach> FindAllCanBo();
        TlDmCanBoKeHoach FindByMaCanBo(string maCanbo);
    }
}
