using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaChenhLechTiGiaRepository : INhDaChenhLechTiGiaRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaChenhLechTiGiaRepository(ApplicationDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDaChenhLechTiGiaQuery> FindAllExchangeRateDifference(ExchangeRateDifferenceCriteria condition)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                //return ctx.FromSqlRaw<NhDaChenhLechTiGiaQuery>("EXECUTE dbo.sp_nh_chenhlechtigia_index @iDDonVi, @iDChuongTrinh, @iDHopDong, @iIDDuAn",
                //    new SqlParameter("@iDDonVi", condition.iID_DonVi),
                //    new SqlParameter("@iDChuongTrinh", condition.iID_KHTongThe_Nvc_ID),
                //    new SqlParameter("@iDHopDong", condition.iID_HopDongID),
                //    new SqlParameter("@iIDDuAn", condition.iID_DuAnID));
                return ctx.FromSqlRaw<NhDaChenhLechTiGiaQuery>("EXECUTE dbo.sp_nh_chenhlechtigia_index_New_TH @iDDonVi, @iDChuongTrinh, @iDHopDong, @iIDDuAn, @iNamKeHoach",
                    new SqlParameter("@iDDonVi", condition.IID_DonVi),
                    new SqlParameter("@iDChuongTrinh", condition.IID_KHTongThe_Nvc_ID),
                    new SqlParameter("@iDHopDong", condition.IID_HopDongID),
                    new SqlParameter("@iIDDuAn", condition.IID_DuAnID),
                    new SqlParameter("@iNamKeHoach", condition.INamKeHoach));
            }
        }
    }
}
