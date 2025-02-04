using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaDuAnNguonVonRepository : Repository<NhDaDuAnNguonVon>, INhDaDuAnNguonVonRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaDuAnNguonVonRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdate(Guid duAnId, IEnumerable<NhDaDuAnNguonVon> entities)
        {
            List<NhDaDuAnNguonVon> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted && x.IIdNguonVonId.HasValue).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdDuAnId = duAnId;
                }
                this.AddRange(lstAdded);
            }

            List<NhDaDuAnNguonVon> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted && x.IIdNguonVonId.HasValue).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdDuAnId = duAnId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhDaDuAnNguonVon> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
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
