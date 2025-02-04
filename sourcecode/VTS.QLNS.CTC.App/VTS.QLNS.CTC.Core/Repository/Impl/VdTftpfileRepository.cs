using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdTftpfileRepository : Repository<VdtFtpFile>, IVdTftpfileRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdTftpfileRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

    }
}
