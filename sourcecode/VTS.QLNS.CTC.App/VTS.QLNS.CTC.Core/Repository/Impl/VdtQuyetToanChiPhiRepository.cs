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
    public class VdtQuyetToanChiPhiRepository : Repository<VdtQtQuyetToanChiPhi>, IVdtQuyetToanChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQuyetToanChiPhiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<PheDuyetQuyetToanDetailQuery> FindAllPheDuyetQuyetToanDetailByCondition(string idDuAn, DateTime ngay)
        {
            try
            {
                using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
                {
                    SqlParameter idDuAnParam = new SqlParameter("@DuAnId", idDuAn);
                    SqlParameter ngayParam = new SqlParameter("@Ngay", ngay);
                    return ctx.FromSqlRaw<PheDuyetQuyetToanDetailQuery>("EXECUTE dbo.sp_vdt_get_phe_duyet_quyet_toan_detail @DuAnId, @Ngay",
                        idDuAnParam, ngayParam).ToList();
                }

            }
            catch (Exception ex)
            {
                return new List<PheDuyetQuyetToanDetailQuery>();
            }
        }

        public VdtQtQuyetToan FindQuyetToanByIdQt(Guid quyetToanId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtQtQuyetToans.Where(n => n.Id == quyetToanId).FirstOrDefault();
            }
        }
    }
}
