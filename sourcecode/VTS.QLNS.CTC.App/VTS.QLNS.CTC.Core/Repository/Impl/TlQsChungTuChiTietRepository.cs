using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQsChungTuChiTietRepository : Repository<TlQsChungTuChiTiet>, ITlQsChungTuChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQsChungTuChiTietRepository(ApplicationDbContextFactory contextFactory)
            :base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteParent(Guid ChungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlQsChungTuChiTiets.RemoveRange(ctx.TlQsChungTuChiTiets.Where(x => x.IdChungTu == ChungTuId.ToString()));
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<TlQsChungTuChiTiet> FindQuyetToanQuanSo(string idDonVi, string thang, int nam, string thangTruoc, int namTruoc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_qt_chutuchitiet @strIdDonVi, @strThang, @strNam, @strThangTruoc, @strNamTruoc";
                var parameters = new[]
                {
                    new SqlParameter("@strIdDonVi", idDonVi),
                    new SqlParameter("@strThang", thang),
                    new SqlParameter("@strNam", nam),
                    new SqlParameter("@strThangTruoc", thangTruoc),
                    new SqlParameter("@strNamTruoc", namTruoc)
                };
                return ctx.FromSqlRaw<TlQsChungTuChiTiet>(sql, parameters).ToList();
            }
        }
    }
}
