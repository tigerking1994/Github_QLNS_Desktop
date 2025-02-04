using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDmChuDauTuRepository : IRepository<DmChuDauTu>
    {
        IEnumerable<DmChuDauTu> FindDmChuDauTuByByNamLamViec(int namLamViec);
        void UpdateBHangChaToTrue(IEnumerable<Guid> ids);
        IEnumerable<DmChuDauTu> FindByNamLamViec(int yearOfWork);
        DmChuDauTu FindByMaDonVi(string iIDMaDonVi, int namLamViec);
        DmChuDauTu FindAllByMaDonVi(string maDonVi);
        IEnumerable<DmChuDauTu> FindByAllDataDonVi();
        DmChuDauTu FindByParentId(Guid id, int namLamViec);
        DmChuDauTu FindAllByParentId(Guid id);
        List<DmChuDauTu> FindByIdDonViCha(Guid id, int namLamViec);
        List<DmChuDauTu> FindByAllIdDonViCha(Guid id);
        List<DmChuDauTu> FindByDuAnId(Guid id);
        List<DmChuDauTu> FindByDuAnId(List<Guid> ids);
        DmChuDauTu GetChuDauTuByVdtDuAnId(Guid iIdDuAnId);
        DmChuDauTu FindByIdDuAn(Guid idDuAn);
        IEnumerable<DmChuDauTu> FindByAll();
    }
}
