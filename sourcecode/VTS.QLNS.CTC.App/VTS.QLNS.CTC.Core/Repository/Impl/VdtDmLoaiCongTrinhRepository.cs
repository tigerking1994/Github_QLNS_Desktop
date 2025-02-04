using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDmLoaiCongTrinhRepository : Repository<VdtDmLoaiCongTrinh>, IVdtDmLoaiCongTrinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDmLoaiCongTrinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtDmLoaiCongTrinh FindById(Guid id)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiCongTrinhs.Where(x => x.IIdLoaiCongTrinh == id).FirstOrDefault();
            }
        }

        public IEnumerable<VdtDmLoaiCongTrinh> FindByListId(List<Guid?> lstId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDmLoaiCongTrinhs.Where(x => lstId.Contains(x.IIdLoaiCongTrinh) && x.IIdParent == null).ToList();
            }
        }
    }
}
