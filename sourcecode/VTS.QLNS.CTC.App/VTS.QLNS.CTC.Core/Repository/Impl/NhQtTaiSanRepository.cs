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
    public class NhQtTaiSanRepository : Repository<NhQtTaiSan>, INhQtTaiSanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtTaiSanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhQtTaiSanQuery> FindAllThongKeTaiSan()
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NhQtTaiSanQuery>("EXECUTE sp_nh_qt_taisan_thongketaisan");
            }
        }

        public IEnumerable<NhQtTaiSanQuery> GetTaiSanByIdChungTuTaiSan(Guid idChungTuTaiSan)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.FromSqlRaw<NhQtTaiSanQuery>("EXECUTE sp_get_taisan_by_id_chungtutaisan @iID_ChungTuTaiSan",
                    new SqlParameter("@iID_ChungTuTaiSan", idChungTuTaiSan)).ToList();
            }
        }

    }
}
