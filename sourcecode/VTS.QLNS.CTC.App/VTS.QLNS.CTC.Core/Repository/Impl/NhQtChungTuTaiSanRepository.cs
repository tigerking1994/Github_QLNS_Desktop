using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhQtChungTuTaiSanRepository : Repository<NhQtChungTuTaiSan>, INhQtChungTuTaiSanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhQtChungTuTaiSanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
