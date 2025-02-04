using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{ 
    public class CpDanhMucRepository : Repository<CpDanhMuc>, ICpDanhMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CpDanhMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory; 
        }

        public int CountDanhMucCP(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.CpDanhMucs.Where(n => n.INamLamViec == namLamViec && n.ITrangThai == 1).Count();
            }
        }

        public List<CpDanhMuc> FindByCondition(string maDanhMuc,string tenDanhMuc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.CpDanhMucs.Where(n => (string.IsNullOrEmpty(maDanhMuc) || (!string.IsNullOrEmpty(maDanhMuc) && n.IIDMaDMCapPhat.ToLower().Contains(maDanhMuc.ToLower())))
                                                && (string.IsNullOrEmpty(tenDanhMuc) || (!string.IsNullOrEmpty(tenDanhMuc) && n.STen.ToLower().Contains(tenDanhMuc.ToLower())))).OrderBy(n => n.OrderIndex).ToList();
            }
        }

        public List<CpDanhMuc> FindByNamLamViec(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.CpDanhMucs.Where(n => n.INamLamViec == namLamViec && n.ITrangThai == 1).ToList();
            }
        }
    }
}
