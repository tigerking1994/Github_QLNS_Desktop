using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhDanhMucLoaiChiRepository : Repository<BhDanhMucLoaiChi>, IBhDanhMucLoaiChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDanhMucLoaiChiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<BhDanhMucLoaiChi> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities.OrderBy(s => s.STenDanhMucLoaiChi))
                {
                    var tracked = ctx.BhDanhMucLoaiChis.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
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
                            tracked.SMaLoaiChi = entity.SMaLoaiChi;
                            tracked.STenDanhMucLoaiChi = entity.STenDanhMucLoaiChi;
                            tracked.SMoTa = entity.SMoTa;
                            tracked.ITrangThai = entity.ITrangThai;
                            tracked.SDSXauNoiMa = entity.SDSXauNoiMa;
                            tracked.SLNS = entity.SLNS;
                            tracked.INamLamViec = authenticationInfo.YearOfWork;
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
                            ctx.Set<BhDanhMucLoaiChi>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<BhDanhMucLoaiChi> FindAll(AuthenticationInfo authenticationInfo)
        {
            var result = FindAll().Where(x => x.INamLamViec == authenticationInfo.YearOfWork).OrderBy(s => s.SMaLoaiChi);
            return result.ToList();
        }

        public IEnumerable<BhDanhMucLoaiChi> FindByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.BhDanhMucLoaiChis.Where(n => n.INamLamViec == namLamViec && n.ITrangThai == NSEntityStatus.ACTIVED).OrderBy(s=>s.SMaLoaiChi).ToList();
            }
        }
    }
}
