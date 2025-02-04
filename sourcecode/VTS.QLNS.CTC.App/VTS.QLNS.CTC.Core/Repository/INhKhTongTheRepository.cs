using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using System.Linq.Expressions;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhKhTongTheRepository : IRepository<NhKhTongThe>
    {
        IEnumerable<NhKhTongThe> FindAllOrdered();
        NhKhTongThe FindByPredicate(Expression<Func<NhKhTongThe, bool>> predicate);
        IEnumerable<NhKhTongTheQuery> FindAllOverview();
        NhKhTongThe FindByParentIdAndNamKeHoach(Guid idParent, int iNamKeHoach);
        IEnumerable<NhKhTongThe> FindByParentId(Guid idParent);
        IEnumerable<NhKhTongThe> FindByDonViId(Guid idDonVi);

    }
}
