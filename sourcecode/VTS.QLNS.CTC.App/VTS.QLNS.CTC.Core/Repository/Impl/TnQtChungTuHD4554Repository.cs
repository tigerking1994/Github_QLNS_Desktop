using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnQtChungTuHD4554Repository : Repository<TnQtChungTuHD4554>, ITnQtChungTuHD4554Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public TnQtChungTuHD4554Repository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TnQtChungTuHD4554 FindAggregateVoucher(string voucherNoes)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTuHD4554s.Where(x => x.STongHop == voucherNoes).FirstOrDefault();
            }
        }

        public IEnumerable<TnQtChungTuHD4554> FindByIdDonVi(string idDonVi, int iThangQuyLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTuHD4554s.Where(x => x.IIdMaDonVi == idDonVi && x.IThangQuyLoai == iThangQuyLoai).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnQtChungTuHD4554, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.TnQtChungTuHD4554s
                 .Where(predicate)
                 .Max(x => x.ISoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public IEnumerable<TnQtChungTuHD4554Query> GetChungTuHD4554(int iNamLamViec)
        {
            //using (var ctx = _contextFactory.CreateDbContext())
            //{
            //    return ctx.TnQtChungTuHD4554s.Where(x => x.INamLamViec == iNamLamViec).ToList();
            //}
            return new List<TnQtChungTuHD4554Query>();
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                TnQtChungTuHD4554 entity = ctx.TnQtChungTuHD4554s.Find(id);
                entity.BKhoa = lockStatus;
                return Update(entity);
            }
        }
    }
}
