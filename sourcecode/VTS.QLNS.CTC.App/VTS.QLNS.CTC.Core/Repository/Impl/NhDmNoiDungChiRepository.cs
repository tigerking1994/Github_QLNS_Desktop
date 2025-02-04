using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmNoiDungChiRepository : Repository<NhDmNoiDungChi>, INhDmNoiDungChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmNoiDungChiRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDmNoiDungChi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

    }
}
