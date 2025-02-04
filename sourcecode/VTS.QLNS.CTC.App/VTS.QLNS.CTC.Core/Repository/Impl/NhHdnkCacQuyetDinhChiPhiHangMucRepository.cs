using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhHdnkCacQuyetDinhChiPhiHangMucRepository : Repository<NhHdnkCacQuyetDinhChiPhiHangMuc>, INhHdnkCacQuyetDinhChiPhiHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhHdnkCacQuyetDinhChiPhiHangMucRepository(ApplicationDbContextFactory contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<NhHdnkCacQuyetDinhChiPhiHangMuc> FindByIdQuyetDinhChiPhi(Guid IdQuyetDinhChiPhi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhHdnkCacQuyetDinhChiPhiHangMucs.Where(n => n.IIdCacQuyetDinhChiPhiId == IdQuyetDinhChiPhi).ToList();
            }
        }
    }
}
