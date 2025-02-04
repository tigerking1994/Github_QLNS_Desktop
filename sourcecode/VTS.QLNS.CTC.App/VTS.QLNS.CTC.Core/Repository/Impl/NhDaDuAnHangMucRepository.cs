using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuAnHangMucRepository : Repository<NhDaDuAnHangMuc>, INhDaDuAnHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuAnHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdate(Guid duAnId, IEnumerable<NhDaDuAnHangMuc> entities)
        {
            List<NhDaDuAnHangMuc> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdDuAnId = duAnId;
                }
                this.AddRange(lstAdded);
            }

            List<NhDaDuAnHangMuc> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdDuAnId = duAnId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhDaDuAnHangMuc> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByDuAnId(Guid duAnId)
        {
            var lstDeleted = this.FindAll(x => x.IIdDuAnId == duAnId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
