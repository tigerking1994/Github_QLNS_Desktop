using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhDaChuTruongDauTuHangMucRepository : Repository<NhDaChuTruongDauTuHangMuc>, INhDaChuTruongDauTuHangMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhDaChuTruongDauTuHangMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void AddOrUpdate(Guid chuTruongDauTuId, IEnumerable<NhDaChuTruongDauTuHangMuc> entities)
        {
            List<NhDaChuTruongDauTuHangMuc> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
            if (lstAdded.Any())
            {
                foreach (var item in lstAdded)
                {
                    item.IIdChuTruongDauTuId = chuTruongDauTuId;
                }
                this.AddRange(lstAdded);
            }

            List<NhDaChuTruongDauTuHangMuc> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
            if (lstModified.Any())
            {
                foreach (var item in lstModified)
                {
                    item.IIdChuTruongDauTuId = chuTruongDauTuId;
                }
                this.UpdateRange(lstModified);
            }

            List<NhDaChuTruongDauTuHangMuc> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
            if (lstDeleted.Any())
            {
                this.RemoveRange(lstDeleted);
            }
        }

        public void DeleteByChuTruongDauTuId(Guid chuTruongDauTuId)
        {
            var lstDeleted = this.FindAll(x => x.IIdChuTruongDauTuId == chuTruongDauTuId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
