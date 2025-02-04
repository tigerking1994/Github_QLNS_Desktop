using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQsKeHoachChiTietNq104Repository : Repository<TlQsKeHoachChiTietNq104>, ITlQsKeHoachChiTietNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQsKeHoachChiTietNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteByNam(int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.Database.ExecuteSqlCommand($"DELETE FROM TL_QS_KeHoach_ChiTiet_NQ104 WHERE Nam = {nam}");
            }
        }

        public TlQsKeHoachChiTietNq104 FindByCondition(string maDonVi, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQsKeHoachChiTietNq104s.FirstOrDefault(x => maDonVi.Equals(x.MaDonVi) && x.Thang == thang && x.Nam == nam);
            }
        }
    }
}
