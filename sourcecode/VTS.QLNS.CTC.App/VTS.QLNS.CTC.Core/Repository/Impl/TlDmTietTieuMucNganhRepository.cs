using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class TlDmTietTieuMucNganhRepository : Repository<TlDmTietTieuMucNganh>, ITlDmTietTieuMucNganhRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public TlDmTietTieuMucNganhRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
