using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class GroupUserRepository : Repository<HtNhomNguoiDung>, IGroupUserRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public GroupUserRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
