using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDmNhaThauService : IService<VdtDmNhaThau>, IVdtDmNhaThauService
    {
        private readonly IVdtDmNhaThauRepository _vdtDmNhaThauRepository;

        public VdtDmNhaThauService(IVdtDmNhaThauRepository vdtDmNhaThauRepository)
        {
            _vdtDmNhaThauRepository = vdtDmNhaThauRepository;
        }

        public override void AddOrUpdateRange(IEnumerable<VdtDmNhaThau> listEntities, AuthenticationInfo authenticationInfo)
        {
            _vdtDmNhaThauRepository.UpdateVdtDmNhaThauRepository(listEntities);
        }

        public VdtDmNhaThau Find(params object[] keyValues)
        {
            return _vdtDmNhaThauRepository.Find(keyValues);
        }

        public override IEnumerable<VdtDmNhaThau> FindAll(AuthenticationInfo authenticationInfo)
        {
            return _vdtDmNhaThauRepository.FindAll(authenticationInfo);
        }

        public IEnumerable<VdtDmNhaThau> FindAll(Expression<Func<VdtDmNhaThau, bool>> predicate)
        {
            return _vdtDmNhaThauRepository.FindAll(predicate);
        }
    }
}
