using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDuAnHangMucRepository : IRepository<VdtDaDuAnHangMuc>
    {
        int FindNextSoChungTuIndex();
        IEnumerable<VdtDaDuAnHangMuc> FindByDuAnHangMuc(Guid idDuAn, int? nguonVon, Guid? idLoaiCongTrinh);
        void DeleteByDuAnId(Guid duanId);
        IEnumerable<VdtDaDuAnHangMuc> FindByIdDuAn(Guid idDuAn);
    }
}
