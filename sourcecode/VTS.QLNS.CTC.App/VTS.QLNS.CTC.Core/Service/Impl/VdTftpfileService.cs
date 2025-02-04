using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdTftpfileService : IService<VdtFtpFile>, IVdTftpfileService
    {
        private readonly IVdTftpfileRepository _repository;

        public VdTftpfileService(IVdTftpfileRepository repository)
        {
            _repository = repository;
        }

        public void Add(VdtFtpFile item)
        {
            _repository.Add(item);
        }
    }
}
