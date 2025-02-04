using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class CauHinhCanCuRepository : Repository<NsCauHinhCanCu>, ICauHinhCanCuRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public CauHinhCanCuRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
