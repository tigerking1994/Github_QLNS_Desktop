using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class SysFunctionAuthorityService : ISysFunctionAuthorityService
    {
        private readonly ISysFunctionAuthorityRepository _repository;

        public SysFunctionAuthorityService(ISysFunctionAuthorityRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<HtQuyenChucNang> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
