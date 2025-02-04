using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmNhaThauNganHangRepository : Repository<NhDmNhaThauNganHang>, INhDmNhaThauNganHangRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmNhaThauNganHangRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
