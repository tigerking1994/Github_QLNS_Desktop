using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktChungTuChiTietCanCuService
    {
        IEnumerable<NsSktChungTuChiTietCanCu> FindByCondition(Expression<Func<NsSktChungTuChiTietCanCu, bool>> predicate);
        void AddRange(List<NsSktChungTuChiTietCanCu> duToans);
        int RemoveRange(IEnumerable<NsSktChungTuChiTietCanCu> sktChungTuChiTiets);
    }
}
