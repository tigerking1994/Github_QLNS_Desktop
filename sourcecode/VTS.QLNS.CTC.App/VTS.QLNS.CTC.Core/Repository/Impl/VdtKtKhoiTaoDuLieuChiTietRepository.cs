using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKtKhoiTaoDuLieuChiTietRepository : Repository<VdtKtKhoiTaoDuLieuChiTiet>, IVdtKtKhoiTaoDuLieuChiTietRepository
    {
        private ApplicationDbContextFactory _contextFactory;
        public VdtKtKhoiTaoDuLieuChiTietRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<KhoiTaoDuLieuChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
					string sql = "EXECUTE dbo.sp_vdt_get_khoi_tao_dulieu_chitiet @KhoiTaoId";
					var parameters = new[]
					{
						new SqlParameter("@KhoiTaoId", idKhoiTao)
					};
                    return ctx.FromSqlRaw<KhoiTaoDuLieuChiTietQuery>(sql, parameters).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<KhoiTaoDuLieuChiTietQuery>();
            }
        }
    }
}
