using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtDuAnHangMucService
    {
        int AddRange(IEnumerable<VdtDaDuAnHangMuc> entities);
        IEnumerable<VdtDaDuAnHangMuc> FindAll(Expression<Func<VdtDaDuAnHangMuc, bool>> predicate);
        int Update(VdtDaDuAnHangMuc entity);
        VdtDaDuAnHangMuc Add(VdtDaDuAnHangMuc entity);
        int FindNextSoChungTuIndex();
        VdtDaDuAnHangMuc FindById(Guid id);
        IEnumerable<VdtDaDuAnHangMuc> FindByDuAnHangMuc(Guid idDuAn, int? nguonVon, Guid? idLoaiCongTrinh);
        IEnumerable<VdtDaDuAnHangMuc> FindByIdDuAn(Guid idDuAn);
        void DeleteByDuAnId(Guid duanId);
    }
}
