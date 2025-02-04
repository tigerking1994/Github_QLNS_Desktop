using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmLoaiTaiSanRepository : Repository<NhDmLoaiTaiSan>, INhDmLoaiTaiSanRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmLoaiTaiSanRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhDmLoaiTaiSan> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }
    }
}
