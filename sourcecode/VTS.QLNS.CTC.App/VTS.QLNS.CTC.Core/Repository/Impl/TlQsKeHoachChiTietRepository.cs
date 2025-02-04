using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQsKeHoachChiTietRepository : Repository<TlQsKeHoachChiTiet>, ITlQsKeHoachChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQsKeHoachChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_QS_KeHoach_ChiTiet WHERE Nam = {nam}");
            }
        }

        public TlQsKeHoachChiTiet FindByCondition(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQsKeHoachChiTiets.FirstOrDefault(x => maDonVi.Equals(x.MaDonVi) && x.Thang == thang && x.Nam == nam);
            }
        }
    }
}
