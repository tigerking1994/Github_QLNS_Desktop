using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsNguonNganSachRepository : Repository<NsNguonNganSach>, INsNguonNgansachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsNguonNganSachRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int AddOrUpdateNsNguonNganSach(IEnumerable<NsNguonNganSach> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        if (entity.IIdMaNguonNganSach.HasValue)
                        {
                            var tracked = ctx.Set<NsNguonNganSach>().Find(entity.IIdMaNguonNganSach);
                            ctx.Remove<NsNguonNganSach>(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (entity.IIdMaNguonNganSach.HasValue)
                        {
                            var tracked = ctx.Set<NsNguonNganSach>().Find(entity.IIdMaNguonNganSach);
                            ctx.Entry(tracked).CurrentValues.SetValues(entity);
                        }
                        else
                        {
                            ctx.Set<NsNguonNganSach>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }

        public List<NsNguonNganSach> FindByDuAnId(string duAnId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_vdt_get_nguon_ngan_sach_by_du_an_id @DuAnId";
                var parameters = new[]
                {
                    new SqlParameter("@DuAnId", duAnId)
                };
                return ctx.Set<NsNguonNganSach>().FromSql(sql, parameters).ToList();
            }
        }

        public IEnumerable<NsNguonNganSach> FindNguonNganSach()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NguonNganSaches.OrderBy(x => x.IStt).ToList();
            }
        }

        public NsNguonNganSach FindNguonNganSachById(int idMaNguon)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NguonNganSaches.Where(x => x.IIdMaNguonNganSach == idMaNguon).FirstOrDefault();
            }
        }
    }
}
