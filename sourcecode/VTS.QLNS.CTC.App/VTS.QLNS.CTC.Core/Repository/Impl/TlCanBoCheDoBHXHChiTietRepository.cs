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
    public class TlCanBoCheDoBHXHChiTietRepository : Repository<TlCanBoCheDoBHXHChiTiet>, ITlCanBoCheDoBHXHChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlCanBoCheDoBHXHChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTiet(string maCanBo, string maCheDo, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoCheDoBHXHChiTiets.Where(x => x.SMaCanBo == maCanBo && x.SMaCheDo == maCheDo && x.IThang == thang 
                && x.INam == nam).ToList();
            }
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTiet> GetCanBoCheDoChiTietInactive(int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlCanBoCheDoBHXHChiTiets.Where(x => x.IThang == thang && x.INam == nam && x.BTrangThai == false).ToList();
            }
        }

        public IEnumerable<TlCanBoCheDoBHXHChiTietQuery> GetCanBoCheDoChiTietIndex(string maCanBo, string maCheDo, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_luong_get_can_bo_che_do_chi_tiet @MaCanBo, @MaCheDo, @Thang, @Nam";

                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo),
                    new SqlParameter("@MaCheDo", maCheDo),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlRaw<TlCanBoCheDoBHXHChiTietQuery>(sql, parameters).ToList();
            }
        }

        public TlCanBoCheDoBHXHChiTietQuery GetTongSoNgayHuong(string maCanBo, string maCheDo, int thang, int nam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_bhxh_luong_get_tong_so_ngay_huong  @MaCanBo, @MaCheDo, @Thang, @Nam";

                var parameters = new[]
                {
                    new SqlParameter("@MaCanBo", maCanBo),
                    new SqlParameter("@MaCheDo", maCheDo),
                    new SqlParameter("@Thang", thang),
                    new SqlParameter("@Nam", nam)
                };
                return ctx.FromSqlRaw<TlCanBoCheDoBHXHChiTietQuery>(sql, parameters).FirstOrDefault();
            }
        }

        public int ExistSoNgayHuong(string sMaCanBo, string sMaCheDo, int? iThang, int? iNam)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return (int)ctx.TlCanBoCheDoBHXHChiTiets.Where(t => t.SMaCanBo.Equals(sMaCanBo) && t.SMaCheDo.Equals(sMaCheDo) && t.IThang.Equals(iThang)
                && t.INam.Equals(iNam)).Sum(x => x.FSoNgayHuongBHXH.GetValueOrDefault());
            }
        }
    }
}
