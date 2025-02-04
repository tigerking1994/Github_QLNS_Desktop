using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaHangMucRepository : IRepository<VdtDaDuAnHangMuc>
    {
        IEnumerable<VdtDaDuAnHangMuc> GetAllHangMuc();
        IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetail(Guid duAnId);
        IEnumerable<VdtDaChuTruongDauTuNguonVonQuery> FindListChuTruongNguonVon(Guid chuTruongId);
        int DeleteDuAnHangMucById(Guid id);
        IEnumerable<VdtDaHangMucQuery> FindListDAHangMucDetailAfterSaveChuTruong(Guid chuTruongId);
        IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhAdd(Guid chuTruongId);
        IEnumerable<VdtDaHangMucQuery> FindListChuTruongHangMucDieuChinhUpdate(Guid chuTruongId);
    }
}
