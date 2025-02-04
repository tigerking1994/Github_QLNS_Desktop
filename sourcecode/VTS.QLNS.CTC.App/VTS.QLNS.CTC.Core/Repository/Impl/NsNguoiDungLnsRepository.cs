using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsNguoiDungLnsRepository : Repository<NsNguoiDungLns>, INsNguoiDungLnsRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsNguoiDungLnsRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void RemoveListNguoiDungLns(IEnumerable<NsNguoiDungLns> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.NsNguoiDungLns.RemoveRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
