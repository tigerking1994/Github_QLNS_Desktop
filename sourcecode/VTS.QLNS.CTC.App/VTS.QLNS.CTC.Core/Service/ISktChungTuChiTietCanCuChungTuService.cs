using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktChungTuChiTietCanCuChungTuService
    {
        IEnumerable<NsSktChungTuChungTuCanCu> FindByCondition(Expression<Func<NsSktChungTuChungTuCanCu, bool>> predicate);
        void AddRange(List<NsSktChungTuChungTuCanCu> duToans);
        int RemoveRange(IEnumerable<NsSktChungTuChungTuCanCu> sktChungTuChiTiets);
    }
}
