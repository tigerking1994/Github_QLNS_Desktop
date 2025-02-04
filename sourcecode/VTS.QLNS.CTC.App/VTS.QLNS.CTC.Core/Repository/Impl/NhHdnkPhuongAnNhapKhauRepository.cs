using System.Linq;
using System.Data.SqlClient;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhHdnkPhuongAnNhapKhauRepository : Repository<NhHdnkPhuongAnNhapKhau>, INhHdnkPhuongAnNhapKhauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHdnkPhuongAnNhapKhauRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhHdnkPhuongAnNhapKhau> FindIndex(int? iLoai = null)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_nh_phuongannhapkhau_index @iLoai";
                var parameters = new[] { new SqlParameter("@iLoai", iLoai) };
                return ctx.FromSqlRaw<NhHdnkPhuongAnNhapKhau>(sql, parameters).ToList();
            }
        }
    }
}
