using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsNguoiDungLnsService
    {
        IEnumerable<NsNguoiDungLns> FindAll(Expression<Func<NsNguoiDungLns, bool>> predicate);
    }
}
