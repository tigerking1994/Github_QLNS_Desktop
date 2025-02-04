using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmLoaiCongTrinhRepository : Repository<VdtDmLoaiCongTrinh>, IDMLoaiCongTrinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmLoaiCongTrinhRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VdtDmLoaiCongTrinh> GetAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiCongTrinhs.ToList();
            }
        }

        public IEnumerable<VdtDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiCongTrinhs.Include(u => u.Parent).ToList();
            }
        }

        public int UpdateDmLoaiCongTrinh(IEnumerable<VdtDmLoaiCongTrinh> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var tracked = ctx.Set<VdtDmLoaiCongTrinh>().Find(entity.IIdLoaiCongTrinh);
                    if (entity.IsDeleted)
                    {
                        if (!entity.IIdLoaiCongTrinh.Equals(Guid.Empty))
                        {
                            if (tracked != null)
                                ctx.Remove<VdtDmLoaiCongTrinh>(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        if (tracked != null)
                        {
                            ctx.Entry(tracked).CurrentValues.SetValues(entity);
                        }
                        else
                        {
                            ctx.Set<VdtDmLoaiCongTrinh>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
