using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtQddtHangmucRepository : Repository<VdtDaQddauTuHangMuc>, IVdtQddtHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtQddtHangmucRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public int DeleteQDHangMucAndDuAnHangMuc(Guid qdHangMucId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                SqlParameter qDHangMucIdParam = new SqlParameter("@qdHangMucId", qdHangMucId);
                return ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_qdhangmuc_duanhangmuc @qdHangMucId", qDHangMucIdParam);
            }
        }
    }
}
