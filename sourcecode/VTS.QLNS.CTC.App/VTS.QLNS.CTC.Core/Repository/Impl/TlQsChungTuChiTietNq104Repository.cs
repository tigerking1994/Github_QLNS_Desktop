using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlQsChungTuChiTietNq104Repository : Repository<TlQsChungTuChiTietNq104>, ITlQsChungTuChiTietNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlQsChungTuChiTietNq104Repository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteParent(Guid ChungTuId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                ctx.TlQsChungTuChiTietNq104s.RemoveRange(ctx.TlQsChungTuChiTietNq104s.Where(x => x.IdChungTu == ChungTuId.ToString()));
                return ctx.SaveChanges();
            }
        }

        public IEnumerable<TlQsChungTuChiTietNq104> FindQuyetToanQuanSo(string idDonVi, string thang, int nam, string thangTruoc, int namTruoc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_qt_qt_chutuchitiet_nq104 @strIdDonVi, @strThang, @strNam, @strThangTruoc, @strNamTruoc";
                var parameters = new[]
                {
                    new SqlParameter("@strIdDonVi", idDonVi),
                    new SqlParameter("@strThang", thang),
                    new SqlParameter("@strNam", nam),
                    new SqlParameter("@strThangTruoc", thangTruoc),
                    new SqlParameter("@strNamTruoc", namTruoc)
                };
                return ctx.FromSqlRaw<TlQsChungTuChiTietNq104>(sql, parameters).ToList();
            }
        }
    }
}
