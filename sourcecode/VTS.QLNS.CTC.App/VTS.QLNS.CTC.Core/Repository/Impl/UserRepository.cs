using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class UserRepository : Repository<HtNguoiDung>, IUserRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public UserRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<HtNguoiDung> FindAllUsers(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<HtNguoiDung> result = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).ThenInclude(s => s.HTNhom).Where(predicate).ToList();
                foreach (HtNguoiDung sysUser in result)
                {
                    List<NguoiDungDonVi> nsNguoiDungDonVis = ctx.NsNguoiDungDonVis.Where(s => s.IIDMaNguoiDung.Equals(sysUser.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                    sysUser.NsNguoiDungDonVis = nsNguoiDungDonVis;
                }
                return result.ToList();
            }
        }

        public HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                HtNguoiDung result = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).ThenInclude(s => s.HTNhom)
                    .SingleOrDefault(predicate);
                if (result == null)
                {
                    return new HtNguoiDung();
                }
                List<NguoiDungDonVi> nsNguoiDungDonVis = ctx.NsNguoiDungDonVis.Where(s => s.IIDMaNguoiDung.Equals(result.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                result.NsNguoiDungDonVis = nsNguoiDungDonVis;
                List<NguoiDungPhanHo> tlNguoiDungPhanHo = ctx.TlNguoiDungPhanHos.Where(s => s.IIDMaNguoiDung.Equals(result.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                result.TlNguoiDungPhanHos = tlNguoiDungPhanHo;
                return result;
            }
        }

        public HtNguoiDung FindByPredicate(Expression<Func<HtNguoiDung, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).ThenInclude(s => s.HTNhom)
                .SingleOrDefault(predicate);
            }
        }

        public IEnumerable<HtNguoiDung> FindAllUsersWithLns(Expression<Func<HtNguoiDung, bool>> predicate, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                IEnumerable<HtNguoiDung> result = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).ThenInclude(s => s.HTNhom).Where(predicate).ToList();
                foreach (HtNguoiDung sysUser in result)
                {
                    List<NsNguoiDungLns> NsNguoiDungLns = ctx.NsNguoiDungLns.Where(s => s.SMaNguoiDung.Equals(sysUser.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                    sysUser.NguoiDungLns = NsNguoiDungLns;
                }
                return result.ToList();
            }
        }

        public void UpdateNguoiDung(HtNguoiDung entity)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var tracked = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).FirstOrDefault(t => t.STaiKhoan.Equals(entity.STaiKhoan));
                tracked.SysGroupUsers.Clear();
                tracked.STaiKhoan = entity.STaiKhoan;
                tracked.SHo = entity.SHo;
                tracked.STen = entity.STen;
                tracked.SEmail = entity.SEmail;
                //ctx.Entry(tracked).CurrentValues.SetValues(entity);
                ctx.SaveChanges();
            }
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var tracked = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).FirstOrDefault(t => t.STaiKhoan.Equals(entity.STaiKhoan));
                tracked.SysGroupUsers = entity.SysGroupUsers;
                //ctx.Entry(tracked).CurrentValues.SetValues(entity);
                ctx.SaveChanges();
            }
        }

        public HtNguoiDung FindUserWithGroupAndLns(Guid userId, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                HtNguoiDung sysUser = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).ThenInclude(s => s.HTNhom)
                   .FirstOrDefault(user => user.Id == userId);
                List<NguoiDungDonVi> nsNguoiDungDonVis = ctx.NsNguoiDungDonVis.Where(s => s.IIDMaNguoiDung.Equals(sysUser.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                sysUser.NsNguoiDungDonVis = nsNguoiDungDonVis;
                List<NguoiDungPhanHo> tlNguoiDungPhanHo = ctx.TlNguoiDungPhanHos.Where(s => s.IIDMaNguoiDung.Equals(sysUser.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                sysUser.TlNguoiDungPhanHos = tlNguoiDungPhanHo;
                List<NsNguoiDungLns> NsNguoiDungLns = ctx.NsNguoiDungLns.Where(s => s.SMaNguoiDung.Equals(sysUser.STaiKhoan) && s.INamLamViec == namLamViec).ToList();
                sysUser.NguoiDungLns = NsNguoiDungLns;
                return sysUser;
            }
        }

        public void DeleteUser(Guid userId)
        {
            using(var ctx = _contextFactory.CreateDbContext())
            {
                HtNguoiDung sysUser = ctx.HtNguoiDungs.Include(g => g.SysGroupUsers).FirstOrDefault(user => user.Id == userId);
                List<NguoiDungDonVi> nsNguoiDungDonVis = ctx.NsNguoiDungDonVis.Where(s => s.IIDMaNguoiDung.Equals(sysUser.STaiKhoan)).ToList();
                sysUser.NsNguoiDungDonVis = nsNguoiDungDonVis;
                List<NguoiDungPhanHo> tlNguoiDungPhanHos = ctx.TlNguoiDungPhanHos.Where(s => s.IIDMaNguoiDung.Equals(sysUser.STaiKhoan)).ToList();
                sysUser.TlNguoiDungPhanHos = tlNguoiDungPhanHos;
                List<NsNguoiDungLns> NsNguoiDungLns = ctx.NsNguoiDungLns.Where(s => s.SMaNguoiDung.Equals(sysUser.STaiKhoan)).ToList();
                sysUser.NguoiDungLns = NsNguoiDungLns;
                ctx.NsNguoiDungLns.RemoveRange(NsNguoiDungLns);
                ctx.NsNguoiDungDonVis.RemoveRange(nsNguoiDungDonVis);
                ctx.TlNguoiDungPhanHos.RemoveRange(tlNguoiDungPhanHos);
                ctx.Remove(sysUser);
                ctx.SaveChanges();
            }
        }

        public void ResetPassword(string userName, string pwd)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                HtNguoiDung sysUser = ctx.HtNguoiDungs.FirstOrDefault(t => t.STaiKhoan.Equals(userName));
                sysUser.SMatKhau = pwd;
                ctx.SaveChanges();
            }
        }
    }
}
