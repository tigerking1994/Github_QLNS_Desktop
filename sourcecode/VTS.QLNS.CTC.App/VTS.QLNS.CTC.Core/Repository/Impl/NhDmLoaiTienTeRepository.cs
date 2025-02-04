using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmLoaiTienTeRepository : Repository<NhDmLoaiTienTe>, INhDmLoaiTienTeRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmLoaiTienTeRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

    }
}
