using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmLoaiCongTrinhRepository : Repository<NhDmLoaiCongTrinh>, INhDmLoaiCongTrinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmLoaiCongTrinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDmLoaiCongTrinh> FindAll(AuthenticationInfo authenticationInfo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDmLoaiCongTrinh.Include(u => u.Parent).ToList();
            }
        }

        public List<NhDmLoaiCongTrinh> GetAll()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDmLoaiCongTrinh.ToList();
            }
        }

        public int UpdateNhDmLoaiCongTrinhRepository(IEnumerable<NhDmLoaiCongTrinh> entities)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    var tracked = ctx.Set<NhDmLoaiCongTrinh>().Find(entity.Id);
                    if (entity.IsDeleted)
                    {
                        if (!entity.Id.Equals(Guid.Empty))
                        {
                            if (tracked != null)
                                ctx.Remove<NhDmLoaiCongTrinh>(tracked);
                        }
                    }
                    else if (entity.IsModified)
                    {
                        //if (!entity.Id.Equals(Guid.Empty))
                        if(tracked!=null)
                        {
                            // ctx.Update(entity);
                            ctx.Entry(tracked).CurrentValues.SetValues(entity);
                        }
                        else
                        {
                            ctx.Set<NhDmLoaiCongTrinh>().Add(entity);
                        }
                    }
                }
                return ctx.SaveChanges();
            }
        }
    }
}
