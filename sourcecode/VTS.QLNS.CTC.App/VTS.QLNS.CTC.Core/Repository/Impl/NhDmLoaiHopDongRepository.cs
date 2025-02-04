using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmLoaiHopDongRepository : Repository<NhDmLoaiHopDong>, INhDmLoaiHopDongRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmLoaiHopDongRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
