using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnQtChungTuRepository : Repository<TnQtChungTu>, ITnQtChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnQtChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TnQtChungTu FindAggregateVoucher(string voucherNoes)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTus.Where(x => x.ITongHop == voucherNoes).FirstOrDefault();
            }
        }

        public IEnumerable<TnQtChungTu> FindByIdDonVi(string idDonVi, int iThangQuyLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnQtChungTus.Where(x => x.IdDonVi == idDonVi && x.IThangQuyLoai == iThangQuyLoai).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnQtChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.TnQtChungTus
                 .Where(predicate)
                 .Max(x => x.SoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                TnQtChungTu entity = ctx.TnQtChungTus.Find(id);
                entity.IsLocked = lockStatus;
                return Update(entity);
            }
        }
    }
}
