using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktSoLieuChiTietCanCuService
    {
        IEnumerable<NsDtdauNamChungTuChungTuCanCu> FindByCondition(Expression<Func<NsDtdauNamChungTuChungTuCanCu, bool>> predicate);
        void AddRange(List<NsDtdauNamChungTuChungTuCanCu> entitys);
        int RemoveRange(IEnumerable<NsDtdauNamChungTuChungTuCanCu> sktChungTuChiTiets);
        int Delete(Guid id);
    }
}
