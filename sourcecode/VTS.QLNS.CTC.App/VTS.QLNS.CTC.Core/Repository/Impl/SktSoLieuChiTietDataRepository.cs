using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class SktSoLieuChiTietDataRepository : Repository<NsDtdauNamChungTuChiTietCanCu>, ISktSoLieuChiTietDataRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public SktSoLieuChiTietDataRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<DuToanDauNamCanCuQuery> FindCanCuSoNhuCau(string lstChungTu, string lstIdMucLuc, string idDonVi, int loaiCanCu, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE dbo.sp_skt_get_can_cu_du_toan_dau_nam @LstIdChungTu, @LstIdMucLuc, @IdDonVi, @LoaiCanCu, @NamLamViec";
                var parameters = new[]
                {
                    new SqlParameter("@LstIdChungTu", lstChungTu),
                    new SqlParameter("@LstIdMucLuc", lstIdMucLuc),
                    new SqlParameter("@IdDonVi", idDonVi),
                    new SqlParameter("@LoaiCanCu", loaiCanCu),
                    new SqlParameter("@NamLamViec", namLamViec)
                };
                return ctx.FromSqlRaw<DuToanDauNamCanCuQuery>(sql, parameters).ToList();
            }
        }
    }
}
