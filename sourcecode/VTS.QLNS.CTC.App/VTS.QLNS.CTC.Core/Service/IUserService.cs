using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IUserService
    {
        IEnumerable<HtNguoiDung> FindAll(int namLamViec);
        IEnumerable<HtNguoiDung> FindAll();
        IEnumerable<HtNguoiDung> FindAll(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        HtNguoiDung Add(HtNguoiDung entity, int namLamViec);
        void Update(HtNguoiDung entity, int namLamViec);
        void Update(HtNguoiDung entity);
        HtNguoiDung FindById(Guid id);
        HtNguoiDung FindById(Guid id, int namLamViec);
        HtNguoiDung FindByLogin(string login);
        HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate);
        HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        string CalculateHash(string clearTextPassword, string salt);
        void RemoveRange(IEnumerable<HtNguoiDung> entities);
        void UpdateRange(IEnumerable<HtNguoiDung> entities);
        void Delete(Guid id);
        void LockOrUnLock(Guid id, bool isActivated);
        IEnumerable<HtNguoiDung> FindAllUsersWithLns(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec);
        public void SaveUserLns(string userLogin, List<NsMucLucNganSach> lns, int namLamViec);
        HtNguoiDung FindUserWithGroupAndLns(Guid userId, int namLamViec);
        void ResetPassword(string userName);
    }
}
