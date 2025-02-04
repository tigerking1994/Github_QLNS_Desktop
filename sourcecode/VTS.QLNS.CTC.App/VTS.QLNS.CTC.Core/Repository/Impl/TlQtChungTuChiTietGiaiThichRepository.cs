using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuChiTietGiaiThichRepository : Repository<TlQtChungTuChiTietGiaiThich>, ITlQtChungTuChiTietGiaiThichRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuChiTietGiaiThichRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TlQtChungTuChiTietGiaiThich FindByChungTuId(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietGiaiThiches.FirstOrDefault(n => n.IIdQtchungTu == chungTuId);
            }
        }

        public TlQtChungTuChiTietGiaiThich FindByCondition(string thang, int nam, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietGiaiThiches.FirstOrDefault(x => thang.Split(',').Contains(x.IThang.ToString()) && x.INam == nam && x.IMaDonVi.Equals(maDonVi));
            }
        }

        public IEnumerable<TlQtChungTuChiTietGiaiThich> FindListByChungTuId(Guid chungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTuChiTietGiaiThiches.Where(n => n.IIdQtchungTu == chungTuId).ToList();
            }
        }
    }
}
