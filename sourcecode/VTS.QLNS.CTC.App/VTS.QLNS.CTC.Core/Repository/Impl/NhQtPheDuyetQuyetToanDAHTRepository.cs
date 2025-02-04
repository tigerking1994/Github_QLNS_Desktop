using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtPheDuyetQuyetToanDAHTRepository : Repository<NhQtPheDuyetQuyetToanDAHT>, INhQtPheDuyetQuyetToanDAHTRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;
        public NhQtPheDuyetQuyetToanDAHTRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHTQuery> FindIndex(int yearOfWork)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_pheduyet_quyettoandaht_index @YearOfWork";
                var parameters = new[]
                {
                    new SqlParameter("@YearOfWork", yearOfWork)
                };
                return ctx.FromSqlRaw<NhQtPheDuyetQuyetToanDAHTQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataCreate(int iNamBaoCaoTu, int iNamBaoCaoDen, Guid? iIDDonVi, int? slbDonViUSD, int? slbDonViVND)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_pheduyet_quyettoandaht_create  @iIDDonVi, @iNamBaoCaoTu , @iNamBaoCaoDen, @devideDonViUSD, @devideDonViVND";
                var parameters = new[]
                {
                    new SqlParameter("@iIDDonVi", iIDDonVi),
                    new SqlParameter("@iNamBaoCaoTu", iNamBaoCaoTu),
                    new SqlParameter("@iNamBaoCaoDen", iNamBaoCaoDen),
                    new SqlParameter("@devideDonViUSD", slbDonViUSD),
                    new SqlParameter("@devideDonViVND", slbDonViVND)
                };
                return ctx.FromSqlRaw<NhQtPheDuyetQuyetToanDAHTChiTietQuery>(executeSql, parameters).ToList();
            }
        }
        public IEnumerable<NhTtThucHienNganSachGiaiDoanQuery> GetGiaiDoan()
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "select distinct iGiaiDoanTu, iGiaiDoanDen , CONCAT(N'Giai đoạn ', iGiaiDoanTu , ' - ' , iGiaiDoanDen) as sGiaiDoan from NH_KHTongThe where CONCAT(iGiaiDoanDen , '') != '' order by iGiaiDoanTu;";
                return ctx.FromSqlRaw<NhTtThucHienNganSachGiaiDoanQuery>(sql).ToList();
            }
        }
        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, Guid? iDPheDuyetQuyetToan)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_nh_pheduyet_quyettoandaht_detail  @iIDDonVi, @iDPheDuyetQuyetToan, @devideDonViUSD, @devideDonViVND";
                var parameters = new[]
                {                    
                    new SqlParameter("@iIDDonVi", iIDDonVi),
                    new SqlParameter("@iDPheDuyetQuyetToan", iDPheDuyetQuyetToan),
                    new SqlParameter("@devideDonViUSD", devideDonViUSD),
                    new SqlParameter("@devideDonViVND", devideDonViVND),

                };
                return ctx.FromSqlRaw<NhQtPheDuyetQuyetToanDAHTChiTietQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhQtPheDuyetQuyetToanDAHTChiTietQuery> GetDataBaoCaoKetLuanDetail(Guid? iIDDonVi, int? devideDonViUSD, int? devideDonViVND, DateTime? dNgayPheDuyetTu, DateTime? dNgayPheDuyetDen)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.proc_get_all_nh_baocao_ketluanbaocao  @iIDDonVi, @dNgayPheDuyetTu,@dNgayPheDuyetDen,@devideDonViUSD,@devideDonViVND";
                var parameters = new[]
                {
                    new SqlParameter("@iIDDonVi", iIDDonVi),
                    new SqlParameter("@dNgayPheDuyetTu", dNgayPheDuyetTu),
                    new SqlParameter("@dNgayPheDuyetDen", dNgayPheDuyetDen),
                    new SqlParameter("@devideDonViUSD", devideDonViUSD),
                    new SqlParameter("@devideDonViVND", devideDonViVND),
                };
                return ctx.FromSqlRaw<NhQtPheDuyetQuyetToanDAHTChiTietQuery>(executeSql, parameters).ToList();
            }
        }

    }
}
