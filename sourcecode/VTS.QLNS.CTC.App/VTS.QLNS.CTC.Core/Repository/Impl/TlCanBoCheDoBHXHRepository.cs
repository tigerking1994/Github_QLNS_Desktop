using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlCanBoCheDoBHXHRepository : Repository<TlCanBoCheDoBHXH>, ITlCanBoCheDoBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoCheDoBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> GetDataCheDoBHXH(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_luong_get_che_do @MaCanBo";

                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlCanBoCheDoBHXHQuery>(sql, parameters).ToList();
            }
        }

        public bool ExistCanBoCheDo(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoCheDoBHXHs.Any(t => t.SMaCanBo.Equals(maCanBo));
            }
        }

        public IEnumerable<TlCanBoCheDoBHXH> GetSoNgayHuongBHXH(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoCheDoBHXHs.Where(x => x.SMaCanBo == maCanBo).ToList();
            }
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> ExportCanBoCheDo(int year, string months)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_bhxh_export_can_bo_che_do @YearOfWork, @Months";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", year),
                    new SqlParameter("@Months", months)
                };
                return ctx.FromSqlRaw<TlCanBoCheDoBHXHQuery>(sql, parameters).ToList();
            }
        }

        public TlCanBoCheDoBHXH FindByCondition(string maCanBo, string maCheDo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoCheDoBHXHs.FirstOrDefault(x => x.SMaCanBo == maCanBo && x.SMaCheDo == maCheDo);
            }
        }

        public IEnumerable<TlCanBoCheDoBHXHQuery> GetCanBoCheDoIndex(string maCanBo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_luong_get_can_bo_che_do_index @MaCanBo";

                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo)
                };
                return ctx.FromSqlRaw<TlCanBoCheDoBHXHQuery>(sql, parameters).ToList();
            }
        }
    }
}
