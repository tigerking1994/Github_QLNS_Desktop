using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmCongThucOutputRepository : Repository<NhDmCongThucOutput>, INhDmCongThucOutputRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmCongThucOutputRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDmCongThucOutput> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }
    }
}
