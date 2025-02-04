using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmChucVuRepository : Repository<TlDmChucVu>, ITlDmChucVuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmChucVuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDmChucVu FindByMaChucVu(string maChucVu)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmChucVus.Where(x => x.MaCv == maChucVu).FirstOrDefault();
            }
        }

        public TlDmChucVu FindByHeSoChucVu(decimal? heSoCv)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmChucVus.Where(x => x.HeSoCv == heSoCv).FirstOrDefault();
            }
        }
    }
}
