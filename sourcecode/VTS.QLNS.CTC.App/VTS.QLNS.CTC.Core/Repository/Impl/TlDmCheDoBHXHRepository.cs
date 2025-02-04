using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmCheDoBHXHRepository : Repository<TlDmCheDoBHXH>, ITlDmCheDoBHXHRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmCheDoBHXHRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public bool CheckPhuCapExist(string maCheDo, Guid iId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCheDoBHXHs.Where(n => n.Id != iId && n.SMaCheDo.Equals(maCheDo)).Any();
            }
        }

        public TlDmCheDoBHXH GetCheDoBHXHByMaCheDo(string maCheDo)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCheDoBHXHs.FirstOrDefault(n => n.SMaCheDo.Equals(maCheDo));
            }
        }

        public IEnumerable<TlDmCheDoBHXH> GetAllCheDoBHXH()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_luong_bhxh_get_all_che_do";
                return ctx.FromSqlRaw<TlDmCheDoBHXH>(sql).ToList();
            }
        }

        public IEnumerable<TlDmCheDoBHXHQuery> GetCheDoBHXHMapping()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_tl_export_danh_muc_che_do_bhxh";
                return ctx.FromSqlRaw<TlDmCheDoBHXHQuery>(sql).ToList();
            }
        }

        public IEnumerable<TlDmCheDoBHXH> GetCheDoByParent(string maCheDoCha)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCheDoBHXHs.Where(n => n.SMaCheDoCha == maCheDoCha).ToList();
            }
        }

        public TlDmCachTinhLuongBaoHiem GetCachTinhLuong(string congThuc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongBaoHiems.FirstOrDefault(n => n.CongThuc.Contains(congThuc));
            }
        }

        public TlDmCachTinhLuongBaoHiemNq104 GetCachTinhLuongNq104(string congThuc)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.TlDmCachTinhLuongBaoHiemsNq104.FirstOrDefault(n => n.CongThuc.Contains(congThuc));
            }
        }
    }
}
