using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmTaiKhoanRepository : Repository<NhDmTaiKhoan>, INhDmTaiKhoanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmTaiKhoanRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDmTaiKhoan> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }
    }
}
