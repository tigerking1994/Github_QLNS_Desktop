using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class AuthorityRepository : Repository<HtQuyen>, IAuthorityRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public AuthorityRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
