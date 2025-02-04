using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsDtChungTuCanCuService
    {
        int Update(Guid idChungTu, List<Guid> idsChungTuPhanBo, string userModified);
        int DeleteByIdChungTuDuToan(Guid id);
        IEnumerable<NsDtChungTuCanCu> FindAll(Expression<Func<NsDtChungTuCanCu, bool>> predicate);
    }
}
