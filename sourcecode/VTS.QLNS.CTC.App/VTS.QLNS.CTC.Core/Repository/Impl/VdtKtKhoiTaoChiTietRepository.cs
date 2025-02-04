using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtKtKhoiTaoChiTietRepository : Repository<VdtKtKhoiTaoChiTiet>, IVdtKtKhoiTaoChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtKtKhoiTaoChiTietRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<KhoiTaoChiTietQuery> FindDataKhoiTaoChiTiet(string idKhoiTao, string idDuAn)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter khoiTaoIdParam = new SqlParameter("@KhoiTaoId", idKhoiTao);
                    SqlParameter duAnParam = new SqlParameter("@LNS", idDuAn);
                    return ctx.FromSqlRaw<KhoiTaoChiTietQuery>("EXECUTE dbo.sp_vdt_get_khoi_tao_chitiet @KhoiTaoId, @LNS", khoiTaoIdParam, duAnParam).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<KhoiTaoChiTietQuery>();
            }
        }
    }
}