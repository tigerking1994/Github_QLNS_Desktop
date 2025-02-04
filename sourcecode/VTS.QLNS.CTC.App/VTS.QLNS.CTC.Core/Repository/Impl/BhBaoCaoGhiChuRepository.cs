using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class BhBaoCaoGhiChuRepository : Repository<BhCauHinhBaoCao>, IBhBaoCaoGhiChuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public BhBaoCaoGhiChuRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
