using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlNguoiDungPhanHoRepository : Repository<NguoiDungPhanHo>, ITlNguoiDungPhanHoRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlNguoiDungPhanHoRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckParentAgencyByUser(string userName, int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlNguoiDungPhanHos.Any(x => x.IIDMaNguoiDung == userName && x.INamLamViec == yearOfWork && x.ITrangThai == 1);
            }
        }

        public void RemoveListNguoiDungPhanHo(IEnumerable<NguoiDungPhanHo> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlNguoiDungPhanHos.RemoveRange(entities);
                ctx.SaveChanges();
            }
        }
    }
}
