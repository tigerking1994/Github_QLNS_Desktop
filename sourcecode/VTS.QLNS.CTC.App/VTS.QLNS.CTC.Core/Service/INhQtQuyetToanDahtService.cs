using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhQtQuyetToanDahtService
    {
        void SaveNhQtQuyetToanDahtChiTiet(List<NhQtQuyetToanDahtChiTiet> entities, Guid NhQtQuyetToanDahtId);
        void Save(NhQtQuyetToanDaht entity);
        List<NhQtQuyetToanDaht> FindAllByNamLamViec(int yearOfWork);
        List<NhQtQuyetToanDaht> FindAllNhQtDaht();
        List<NhQtQuyetToanDaht> FindAllTongHopByNamLamViec(int yearOfWork);
        IEnumerable<NHDAQDDauTuChiPhiHangMuc> GetChiPhiHangMucByDuAnId(Guid duanId, Guid NHDAQDDauTuId);
        void LockUnlockItem(Guid id);
        void Delete(Guid id);
        void TongHopQTDAHT(NhQtQuyetToanDaht nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds);
        NhQtQuyetToanDaht FindById(Guid id);
        void UpdateAggregateStatus(string voucherIds);
    }   
}
