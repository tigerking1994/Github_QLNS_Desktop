using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INsNguoiDungDonViService
    {
        bool CheckParentAgencyByUser(string userName, int yearOfWork);
        IEnumerable<NguoiDungDonVi> FindAll(Expression<Func<NguoiDungDonVi, bool>> predicate);
    }
}
