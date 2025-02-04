using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhDmThamDinhQuyetToanService : IService<BhDmThamDinhQuyetToan>, IBhDmThamDinhQuyetToanService
    {
        private readonly IBhDmThamDinhQuyetToanRepository _bhDmThamDinhQuyetToanRepository;

        public BhDmThamDinhQuyetToanService(IBhDmThamDinhQuyetToanRepository bhDmThamDinhQuyetToanRepository)
        {
            _bhDmThamDinhQuyetToanRepository = bhDmThamDinhQuyetToanRepository;
        }

        public IEnumerable<BhDmThamDinhQuyetToan> FindAll(Expression<Func<BhDmThamDinhQuyetToan, bool>> predicate)
        {
            return _bhDmThamDinhQuyetToanRepository.FindAll(predicate);
        }

        public override IEnumerable<BhDmThamDinhQuyetToan> FindAll(AuthenticationInfo authentication)
        {
            return _bhDmThamDinhQuyetToanRepository.FindAll(authentication);
        }
        public override void AddOrUpdateRange(IEnumerable<BhDmThamDinhQuyetToan> listEntities, AuthenticationInfo authenticationInfo)
        {
            _bhDmThamDinhQuyetToanRepository.AddOrUpdateRange(listEntities, authenticationInfo);
        }
    }
}
