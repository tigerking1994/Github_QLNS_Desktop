using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IDmChuDauTuService
    {
        IEnumerable<DmChuDauTu> FindByNamLamViec(int yearOfWork);
        IEnumerable<DmChuDauTu> FindByAllDataDonVi();
        IEnumerable<DmChuDauTu> FindByAll();
        DmChuDauTu FindById(Guid id);
        DmChuDauTu FindByParentId(Guid id, int namLamViec);
        DmChuDauTu FindByAllParentId(Guid id);
        DmChuDauTu FindByMaDonVi(string iIDMaDonVi, int namLamViec);
        DmChuDauTu FindAllByMaDonVi(string maDonVi);
        IEnumerable<DmChuDauTu> FindByCondition(Expression<Func<DmChuDauTu, bool>> predicate);
        List<DmChuDauTu> FindByDuAnId(Guid id);
        DmChuDauTu FindByIdDuAn(Guid idDuAn);
        DmChuDauTu GetChuDauTuByVdtDuAnId(Guid iIdDuAnId);
        List<DmChuDauTu> FindByIdDonViCha(Guid id, int namLamViec);
        List<DmChuDauTu> FindByAllIdDonViCha(Guid id);

    }
}
