using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoachVonUngService
    {
        IEnumerable<VdtKhvKeHoachVonUngQuery> GetKeHoachVonUngIndex();
        bool Insert(VdtKhvKeHoachVonUng dataInsert, string sUserLogin);
        bool Update(VdtKhvKeHoachVonUng dataUpdate, string sUserLogin);
        bool DeleteKeHoachVonUng(VdtKhvKeHoachVonUng data);
        int Adjust(VdtKhvKeHoachVonUng entity, List<VdtKhvKeHoachVonUngChiTiet> listDetail);
        IEnumerable<VdtKhvKeHoachVonUng> FindAll(Expression<Func<VdtKhvKeHoachVonUng, bool>> predicate);
        IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<Guid> lstPhanboVonId);
        bool CheckTrungSoQuyetDinh(string sSoQuyetDinh, Guid id);
        VdtKhvKeHoachVonUng FindById(Guid id);
    }
}
