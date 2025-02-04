using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoachVonUngChiTietService
    {
        IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetDuAnInKeHoachVonUngDetail(Guid? iIdKhvuDxId);
        IEnumerable<VdtKhvKeHoachVonUngChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        bool Insert(Guid parentId, List<VdtKhvKeHoachVonUngChiTiet> lstChild);
        double GetkeHoachUng(Guid iIdDuAnId, DateTime dNgayBaoCao);

        VdtKhvKeHoachVonUngChiTiet FindById(Guid id);
    }
}
