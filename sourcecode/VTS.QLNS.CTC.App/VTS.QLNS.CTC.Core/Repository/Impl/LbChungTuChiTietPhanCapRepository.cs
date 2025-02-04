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
    public class LbChungTuChiTietPhanCapRepository : Repository<NsNganhChungTuChiTietPhanCap>, ILbChungTuChiTietPhanCapRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public LbChungTuChiTietPhanCapRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NsNganhChungTuChiTietPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNganhChungTuChiTietPhanCaps.Where(n => n.IIdMaDonVi == idDonVi && n.IIdCtnganhChiTiet == Guid.Parse(idChungTuChiTiet) && n.INamLamViec == namLamViec).FirstOrDefault();
            }
        }

        public IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_lb_chungtu_chitiet_phancap @YearOfWork, @XauNoiMaString, @XauNoiMa, @ChiTietId";
                    var parameters = new[]
                    {
                        new SqlParameter("@YearOfWork", namLamViec),
                        new SqlParameter("@XauNoiMaString", listXauNoiMa),
                        new SqlParameter("@XauNoiMa", xauNoiMa),
                        new SqlParameter("@ChiTietId", idChiTiet)
                    };
                    return ctx.FromSqlRaw<LbChiTietPhanCapQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<LbChiTietPhanCapQuery>();
                }
            }
        }

        public IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCapExport(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                try
                {
                    string sql = "EXECUTE dbo.sp_lb_chungtu_chitiet_phancap_export @YearOfWork, @XauNoiMaString, @XauNoiMa, @ChiTietId";
                    var parameters = new[]
                    {
                        new SqlParameter("@YearOfWork", namLamViec),
                        new SqlParameter("@XauNoiMaString", listXauNoiMa),
                        new SqlParameter("@XauNoiMa", xauNoiMa),
                        new SqlParameter("@ChiTietId", idChiTiet)
                    };
                    return ctx.FromSqlRaw<LbChiTietPhanCapQuery>(sql, parameters).ToList();
                }
                catch (Exception ex)
                {
                    return new List<LbChiTietPhanCapQuery>();
                }
            }
        }

        public List<NsNganhChungTuChiTietPhanCap> FindByChiTietId(string chitietId, string idDonVi, int namLamViec)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NsNganhChungTuChiTietPhanCaps.Where(n => n.IIdMaDonVi == idDonVi && n.IIdCtnganhChiTiet == Guid.Parse(chitietId) && n.INamLamViec == namLamViec).ToList();
            }
        }

        public void DeleteByNganhChiTiet(Guid idNganhChiTiet)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = $"DELETE FROM NS_Nganh_ChungTuChiTiet_PhanCap WHERE iID_CTNganhChiTiet = @NganhChiTietId";
                var parameters = new[]
                {
                    new SqlParameter("@NganhChiTietId", idNganhChiTiet.ToString())
                };
                ctx.Database.ExecuteSqlCommand(sql, parameters);
            }
        }
    }
}
