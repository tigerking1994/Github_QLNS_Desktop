using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDmMucDongBHXHRepository : Repository<BhDmMucDongBHXH>, IBhDmMucDongBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDmMucDongBHXHRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<BhDmMucDongBHXH> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach(var entity in listEntities.OrderBy(s => s.SNoiDung))
                {
                    var tracked = ctx.BhDmMucDongBHXHs.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
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
                            tracked.SMaMucDong=entity.SMaMucDong;
                            tracked.SNoiDung = entity.SNoiDung;
                            tracked.SBH_XauNoiMa = entity.SBH_XauNoiMa;
                            tracked.FTyLe_BHXH_NLD = entity.FTyLe_BHXH_NLD;
                            tracked.FTyLe_BHXH_NSD = entity.FTyLe_BHXH_NSD;
                            tracked.FTyLe_BHYT_NLD = entity.FTyLe_BHYT_NLD;
                            tracked.FTyLe_BHYT_NSD = entity.FTyLe_BHYT_NSD;
                            tracked.FTyLe_BHTN_NLD = entity.FTyLe_BHTN_NLD;
                            tracked.FTyLe_BHTN_NSD = entity.FTyLe_BHTN_NSD;
                            tracked.INamLamViec = authenticationInfo.YearOfWork;
                            tracked.ITrangThai = entity.ITrangThai;
                            tracked.SNguoiSua = authenticationInfo.Principal;
                            tracked.DNgaySua = time;
                            ctx.Update(tracked);
                        }
                        else
                        {
                            entity.Id = Guid.NewGuid();
                            entity.INamLamViec = authenticationInfo.YearOfWork;
                            entity.SNguoiTao = authenticationInfo.Principal;
                            entity.DNgayTao = time;
                            ctx.Set<BhDmMucDongBHXH>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<BhDmMucDongBHXH> FindAll(AuthenticationInfo authenticationInfo)
        {
            var result = FindAll().OrderBy(s => s.SNoiDung);
            return result.ToList();
        }
    }
}
