using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvKeHoachVonUngChiTietRepository : IRepository<VdtKhvKeHoachVonUngChiTiet>
    {
        IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetDuAnInKeHoachVonUngDetail(Guid? iIdKhvuDxId);
        IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        void DeleteKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        double GetkeHoachUng(Guid iIdDuAnId, DateTime dNgayBaoCao);
    }
}
