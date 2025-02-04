using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service
{
   public class VdtFtpRootService : IService<VdtFtpRoot>, IVdtFtpRootService
    {
        private readonly IVdtFtpRootRepository _repository;

        public VdtFtpRootService(IVdtFtpRootRepository repository)
        {
            _repository = repository;
        }

        public void Add(VdtFtpRoot item)
        {
            _repository.Add(item);
        }
        public VdtFtpRoot GetVdtFtpRoot(string sMaDonVi, string sIpAddress, string sFolderRoot)
        {
           return  _repository.GetVdtFtpRoot(sMaDonVi, sIpAddress, sFolderRoot);
        }
    }
}
