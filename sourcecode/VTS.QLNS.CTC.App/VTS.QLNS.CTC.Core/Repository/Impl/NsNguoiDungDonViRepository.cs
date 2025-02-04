using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NsNguoiDungDonViRepository : Repository<NguoiDungDonVi>, INsNguoiDungDonViRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NsNguoiDungDonViRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckParentAgencyByUser(string userName, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNguoiDungDonVis.Any(x => x.IIDMaNguoiDung == userName && x.INamLamViec == yearOfWork && x.ITrangThai == 1);
            }
        }

        public void RemoveListNguoiDungDonvi(IEnumerable<NguoiDungDonVi> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.NsNguoiDungDonVis.RemoveRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
