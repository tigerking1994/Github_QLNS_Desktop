using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnDtChungTuRepository : Repository<TnDtChungTu>, ITnDtChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnDtChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public TnDtChungTu FindAggregateVoucher(string voucherNoes)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTus.Where(x => x.ITongHop == voucherNoes).FirstOrDefault();
            }
        }

        public IEnumerable<TnDtChungTu> FindByType(int iLoai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTus.Where(x => x.ILoai == iLoai).ToList();
            }
        }

        public int FindNextSoChungTuIndex(Expression<Func<TnDtChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var currentIndex = ctx.TnDtChungTus
                 .Where(predicate)
                 .Max(x => x.SoChungTuIndex);
                return currentIndex != null ? (int)currentIndex + 1 : 1;
            }
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTuChiTiets.Where(x => x.IdChungTu.HasValue && chungTuIds.Contains(x.IdChungTu.Value) && (x.TuChi != 0)).Select(x => x.Lns).Distinct().ToList();
            }
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                TnDtChungTu entity = ctx.TnDtChungTus.Find(id);
                entity.IsLocked = lockStatus;
                return Update(entity);
            }
        }

        public bool CheckDeletePhanBo(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<TnDtChungTu> items = ctx.TnDtChungTus.Where(x => x.ILoai == 1 && !string.IsNullOrEmpty(x.IdDotNhan) && x.IdDotNhan == id.ToString()).ToList();
                return items.Count() > 0;
            }
        }

        public TnDtChungTu FindByIdDotNhan(string sid)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDtChungTus.FirstOrDefault(x => x.IdDotNhan == sid);
            }
        }
        public List<string> GetAgencyCodeByVoucherDetail(Expression<Func<TnDtChungTu, bool>> predicate)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var data = from chungtu in ctx.TnDtChungTus.Where(predicate)
                           join ct in ctx.TnDtChungTuChiTiets on chungtu.Id equals ct.IdChungTu
                           select ct.IdDonVi;
                return data.Distinct().ToList();
            }
        }
    }
}
