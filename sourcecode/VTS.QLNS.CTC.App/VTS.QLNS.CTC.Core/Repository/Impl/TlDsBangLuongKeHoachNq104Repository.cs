using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDsBangLuongKeHoachNq104Repository : Repository<TlDsBangLuongKeHoachNq104>, ITlDsBangLuongKeHoachNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDsBangLuongKeHoachNq104Repository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlDsBangLuongKeHoachNq104 FindByCondition(string cACH0, string maDonVi, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDsBangLuongKeHoachNq104s.FirstOrDefault(x => x.MaCachTl.Equals(cACH0) && x.MaDonVi.Equals(maDonVi) && x.Nam == nam);
            }
        }
    }
}
