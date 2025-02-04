using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhMstnKeHoachDatHangDanhMucRepository : Repository<NhMSTNKeHoachDatHangDanhMuc>, INhMstnKeHoachDatHangDanhMucRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhMstnKeHoachDatHangDanhMucRepository(ApplicationDbContextFactory contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        //public void AddOrUpdate(Guid khDatHangId, IEnumerable<NhMSTNKeHoachDatHangDanhMuc> entities)
        //{
        //    List<NhMSTNKeHoachDatHangDanhMuc> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
        //    if (lstAdded.Any())
        //    {
        //        foreach (var item in lstAdded)
        //        {
        //            item.IID_KeHoachDatHang = khDatHangId;
        //        }
        //        this.AddRange(lstAdded);
        //    }

        //    List<NhMSTNKeHoachDatHangDanhMuc> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
        //    if (lstModified.Any())
        //    {
        //        foreach (var item in lstModified)
        //        {
        //            item.IID_KeHoachDatHang = khDatHangId;
        //        }
        //        this.UpdateRange(lstModified);
        //    }

        //    List<NhMSTNKeHoachDatHangDanhMuc> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
        //    if (lstDeleted.Any())
        //    {
        //        this.RemoveRange(lstDeleted);
        //    }
        //}
        public void AddOrUpdate(Guid khDatHangId, IEnumerable<NhMSTNKeHoachDatHangDanhMuc> items)
        {
            if (!items.Any()) return;

            var listAdded = items.Where(s => s.IsAdded && !s.IsDeleted).ToList();
            if (!listAdded.IsEmpty())
            {
                foreach (var item in listAdded)
                {
                    item.IID_KeHoachDatHang = khDatHangId;
                }
                this.AddRange(listAdded);
            }

            var listModified = items.Where(s => s.IsModified && !s.IsAdded && !s.IsDeleted).ToList();
            if (!listModified.IsEmpty())
            {
                foreach (var item in listModified)
                {
                    item.IID_KeHoachDatHang = khDatHangId;
                }
                this.UpdateRange(listModified);
            }

            var listDeleted = items.Where(s => s.IsDeleted).ToList();
            if (!listDeleted.IsEmpty())
            {
                this.RemoveRange(listDeleted);
            }
        }

        public void DeleteByKHDatHangId(Guid khDatHangId)
        {
            var lstDeleted = this.FindAll(x => x.IID_KeHoachDatHang == khDatHangId);
            if (!lstDeleted.IsEmpty())
            {
                this.RemoveRange(lstDeleted);
            }
        }
    }
}
