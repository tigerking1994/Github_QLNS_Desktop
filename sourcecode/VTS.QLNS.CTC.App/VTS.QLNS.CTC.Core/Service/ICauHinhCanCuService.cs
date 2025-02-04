using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ICauHinhCanCuService
    {
        IEnumerable<NsCauHinhCanCu> FindByCondition(Expression<Func<NsCauHinhCanCu, bool>> predicate);
    }
}
