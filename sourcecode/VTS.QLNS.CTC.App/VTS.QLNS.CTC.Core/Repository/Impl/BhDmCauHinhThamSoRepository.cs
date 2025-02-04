using Microsoft.AspNetCore.Http.Authentication;
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
    public class BhDmCauHinhThamSoRepository : Repository<BhDmCauHinhThamSo>, IBhDmCauHinhThamSoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDmCauHinhThamSoRepository(ApplicationDbContextFactory contextFactory) 
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<BhDmCauHinhThamSo> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities.OrderBy(s=>s.SMa))
                {
                    var tracked = ctx.BhDmCauHinhThamSo.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
                    if (entity.IsDeleted)
                    {
                        if (tracked!=null)
                        {
                            ctx.Remove(tracked);
                        }                        
                    }
                    else if (entity.IsModified)
                    {
                        if (tracked != null)
                        {
                            entity.SNguoiSua = authenticationInfo.Principal;
                            entity.DNgaySua = time;
                            ctx.Update(entity);
                        }
                        else
                        {
                            entity.SNguoiTao = authenticationInfo.Principal;
                            entity.DNgayTao = time;
                            ctx.Set<BhDmCauHinhThamSo>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
