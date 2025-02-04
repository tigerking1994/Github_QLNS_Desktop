using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtFtpRootRepository : Repository<VdtFtpRoot>, IVdtFtpRootRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtFtpRootRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public VdtFtpRoot GetVdtFtpRoot(string sMaDonVi, string sIpAddress, string sFolderRoot)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VDTFtpRoot.Where(x => x.SMaDonVi == sMaDonVi && x.SIpAddress == sIpAddress && x.SFolderRoot == sFolderRoot).FirstOrDefault();
            }
        }
    }
}
