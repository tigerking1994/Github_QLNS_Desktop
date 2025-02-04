using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmThueThuNhapCaNhanNq104Repository : Repository<TlDmThueThuNhapCaNhanNq104>, ITlDmThueThuNhapCaNhanNq104Repository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmThueThuNhapCaNhanNq104Repository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
