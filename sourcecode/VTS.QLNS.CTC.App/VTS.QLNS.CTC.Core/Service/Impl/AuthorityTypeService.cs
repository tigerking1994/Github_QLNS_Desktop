using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class AuthorityTypeService : IAuthorityTypeService
    {
        private readonly IAuthorityTypeRepository _authorityTypeRepository;

        public AuthorityTypeService(IAuthorityTypeRepository authorityTypeRepository)
        {
            _authorityTypeRepository = authorityTypeRepository;
        }

        public IEnumerable<HtLoaiQuyen> FindAll(Expression<Func<HtLoaiQuyen, bool>> predicate)
        {
            return _authorityTypeRepository.LoadEagerAuthorityTypes(predicate);
        }

        public IEnumerable<HtLoaiQuyen> FindAll()
        {
            return _authorityTypeRepository.LoadEagerAuthorityTypes(a => true);
        }
    }
}
