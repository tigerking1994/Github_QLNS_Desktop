using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
   public class NhQtPheDuyetQuyetToanDAHTChiTietRepository : Repository<NhQtPheDuyetQuyetToanDAHTChiTiet>, INhQtPheDuyetQuyetToanDAHTChiTietRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtPheDuyetQuyetToanDAHTChiTietRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

    }
}
