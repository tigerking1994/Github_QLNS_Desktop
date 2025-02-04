using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TnDanhMucLoaiHinhRepository : Repository<TnDanhMucLoaiHinh>, ITnDanhMucLoaiHinhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TnDanhMucLoaiHinhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TnDanhMucLoaiHinh> FindByLoaiHinh(int yearOfWork, int iTrangThai)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TnDanhMucLoaiHinhs.Where(x => x.INamLamViec == yearOfWork && x.ITrangThai == iTrangThai).OrderBy(x => x.Lns).ToList();
            }
        }
    }
}
