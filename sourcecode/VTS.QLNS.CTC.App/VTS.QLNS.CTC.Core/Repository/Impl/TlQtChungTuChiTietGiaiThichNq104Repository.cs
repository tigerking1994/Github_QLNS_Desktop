using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuChiTietGiaiThichNq104Repository : Repository<TlQtChungTuChiTietGiaiThichNq104>, ITlQtChungTuChiTietGiaiThichNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuChiTietGiaiThichNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlQtChungTuChiTietGiaiThichNq104 FindByChungTuId(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietGiaiThichesNq104.FirstOrDefault(n => n.IIdQtchungTu == chungTuId);
            }
        }

        public TlQtChungTuChiTietGiaiThichNq104 FindByCondition(string thang, int nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietGiaiThichesNq104.FirstOrDefault(x => thang.Split(',').Contains(x.IThang.ToString()) && x.INam == nam && x.IMaDonVi.Equals(maDonVi));
            }
        }
    }
}
