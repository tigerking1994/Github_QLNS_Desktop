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
    public class BhDmMucLucBHXHMapRepository : Repository<BhDmMucLucNganSach>, IBhDmMucLucBHXHMapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public BhDmMucLucBHXHMapRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in listEntities.OrderBy(s => s.SXauNoiMa))
                {
                    var tracked = ctx.BhDmMucLucNganSachs.AsNoTracking().FirstOrDefault(i => i.Id.Equals(entity.Id));
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
                            tracked.FTyLeBHTNNSD = entity.FTyLeBHTNNSD;
                            tracked.FTyLeBHTNNLD = entity.FTyLeBHTNNLD;
                            tracked.FTyLeBHXHNLD = entity.FTyLeBHXHNLD;
                            tracked.FTyLeBHXHNSD = entity.FTyLeBHXHNSD;
                            tracked.FTyLeBHYTNLD = entity.FTyLeBHYTNLD;
                            tracked.FTyLeBHYTNSD = entity.FTyLeBHYTNSD;
                            tracked.FHeSoLayQuyLuong = entity.FHeSoLayQuyLuong;
                            tracked.SNS_LuongChinh = entity.SNS_LuongChinh;
                            tracked.SNS_PCCV = entity.SNS_PCCV;
                            tracked.SNS_PCTN = entity.SNS_PCTN;
                            tracked.SNS_PCTNVK = entity.SNS_PCTNVK;
                            tracked.SNS_HSBL = entity.SNS_HSBL;
                            tracked.SLuongChinh = entity.SLuongChinh;
                            tracked.SPCCV = entity.SPCCV;
                            tracked.SPCTN = entity.SPCTN;
                            tracked.SPCTNVK = entity.SPCTNVK;
                            tracked.SNguoiSua = authenticationInfo.Principal;
                            tracked.DNgaySua = time;
                            ctx.Update(tracked);
                        }                        
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
