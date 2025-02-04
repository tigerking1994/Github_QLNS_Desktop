using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class DmDeTaiRepository : Repository<DmDeTai>, IDmDeTaiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public DmDeTaiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
