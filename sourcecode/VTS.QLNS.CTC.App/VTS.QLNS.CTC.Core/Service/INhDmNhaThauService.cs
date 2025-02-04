using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDmNhaThauService
    {
        int Add(NhDmNhaThau entity);
        int AddRange(List<NhDmNhaThau> entities);
        int Update(NhDmNhaThau entity);
        int Delete(Guid id);
        NhDmNhaThau FindById(Guid id);
        IEnumerable<NhDmNhaThau> FindAll();
        object FindByCondition(Func<object, bool> p);
        IEnumerable<NhDmNhaThau> FindAll(Expression<Func<NhDmNhaThau, bool>> predicate);
    }
}