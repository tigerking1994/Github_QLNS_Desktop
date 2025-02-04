using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtFtpRootService
    {
        void Add(VdtFtpRoot vdtFtpRoot);
        public VdtFtpRoot GetVdtFtpRoot(string sMaDonVi, string sIpAddress, string sFolderRoot);
    }
}
