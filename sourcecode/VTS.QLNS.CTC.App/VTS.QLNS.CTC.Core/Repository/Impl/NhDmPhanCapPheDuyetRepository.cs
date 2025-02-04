using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDmPhanCapPheDuyetRepository : Repository<NhDmPhanCapPheDuyet>, INhDmPhanCapPheDuyetRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDmPhanCapPheDuyetRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
