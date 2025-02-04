using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmThamDinhQuyetToanRepository : Repository<BhDmThamDinhQuyetToan>, IBhDmThamDinhQuyetToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhDmThamDinhQuyetToanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<BhDmThamDinhQuyetToan> FindAll(AuthenticationInfo authenticationInfo)
        {

            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDmThamDinhQuyetToans.Where(b => b.INamLamViec == authenticationInfo.YearOfWork).OrderBy(o => o.ISTT).ToList();
            }
        }
        public int AddOrUpdateRange(IEnumerable<BhDmThamDinhQuyetToan> listEntities, AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities)
                {
                    var tracked = ctx.BhDmThamDinhQuyetToans.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (tracked != null)
                        {
                            ctx.Remove(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (tracked != null)
                        {
                            tracked.IMa = entity.IMa;
                            tracked.IMaCha = entity.IMaCha;
                            tracked.SNoiDung = entity.SNoiDung;
                            tracked.ISTT = entity.ISTT;
                            tracked.SSTT = entity.SSTT;
                            tracked.IKieuChu = entity.IKieuChu;
                            tracked.INamLamViec = authenticationInfo.YearOfWork;
                            tracked.ITrangThai = entity.ITrangThai;
                            tracked.SNguoiSua = authenticationInfo.Principal;
                            ctx.Update(tracked);
                        }
                        else
                        {
                            entity.Id = Guid.NewGuid();
                            entity.INamLamViec = authenticationInfo.YearOfWork;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            ctx.Set<BhDmThamDinhQuyetToan>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

    }
}
