using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class ImpDuToanRepository : Repository<ImpDuToan>, IImpDuToanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public ImpDuToanRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
