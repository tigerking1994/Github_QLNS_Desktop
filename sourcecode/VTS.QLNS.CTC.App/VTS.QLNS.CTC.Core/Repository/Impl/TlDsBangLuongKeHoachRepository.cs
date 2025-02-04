using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDsBangLuongKeHoachRepository : Repository<TlDsBangLuongKeHoach>, ITlDsBangLuongKeHoachRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDsBangLuongKeHoachRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDsBangLuongKeHoach FindByCondition(string cACH0, string maDonVi, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsBangLuongKeHoaches.FirstOrDefault(x => x.MaCachTl.Equals(cACH0) && x.MaDonVi.Equals(maDonVi) && x.Nam == nam);
            }
        }
    }
}
