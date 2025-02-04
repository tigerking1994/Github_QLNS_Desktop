using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IUserRepository : IRepository<HtNguoiDung>
    {
        IEnumerable<HtNguoiDung> FindAllUsers(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate);
        HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        IEnumerable<HtNguoiDung> FindAllUsersWithLns(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        void UpdateNguoiDung(HtNguoiDung entity);
        HtNguoiDung FindUserWithGroupAndLns(Guid userId, int namLamViec);
        void DeleteUser(Guid userId);
        void ResetPassword(string userName, string pwd);
    }
}
