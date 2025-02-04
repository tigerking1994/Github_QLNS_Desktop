using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQtChungTuRepository : Repository<TlQtChungTu>, ITlQtChungTuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQtChungTuRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlQtChungTu> FindChungTuExist(int yearOfWork, int thang, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlQtChungTus.Where(x => x.Nam == yearOfWork && x.Thang == thang && x.MaDonVi == maDonVi).ToList();
            }
        }

        public int GetSoChungTuIndexByCondition(int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                var result = ctx.TlQtChungTus.Where(x => x.Nam == namLamViec).OrderByDescending(n => n.SoChungTu).ToList();
                if (result.Count <= 0) return 1;
                var indexString = result.FirstOrDefault().SoChungTu.Substring(3, 3);
                try
                {
                    var index = int.Parse(indexString) + 1;
                    return index;
                }
                catch (Exception e)
                {
                    return result.Count + 1;
                }
            }
        }
    }
}
