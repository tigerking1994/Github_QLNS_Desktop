using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmButToanInputRepository : Repository<NhDmButToanInput>, INhDmButToanInputRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmButToanInputRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDmButToanInput> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }
    }
}
