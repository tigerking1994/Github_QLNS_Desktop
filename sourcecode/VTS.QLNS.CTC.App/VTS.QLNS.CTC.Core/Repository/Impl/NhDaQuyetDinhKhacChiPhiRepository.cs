using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaQuyetDinhKhacChiPhiRepository : Repository<NhDaQuyetDinhKhacChiPhi>, INhDaQuyetDinhKhacChiPhiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaQuyetDinhKhacChiPhiRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindAll(AuthenticationInfo authenticationInfo)
        {
            return FindAll();
        }

        public IEnumerable<NhDaQuyetDinhKhacChiPhi> FindByQuyetDinhKhacId(Guid iIdQuyetDinhKhacId)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                return ctx.NhDaQuyetDinhKhacChiPhis.Where(x => x.IIdQuyetDinhKhacId == iIdQuyetDinhKhacId).ToList();
            }
        }
    }
}
