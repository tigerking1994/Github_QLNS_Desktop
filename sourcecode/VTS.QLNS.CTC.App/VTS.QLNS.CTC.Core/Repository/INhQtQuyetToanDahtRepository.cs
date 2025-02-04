using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhQtQuyetToanDahtRepository : IRepository<NhQtQuyetToanDaht>
    {
        void SaveNhQtQuyetToanDahtChiTiet(List<NhQtQuyetToanDahtChiTiet> entities, Guid nhQtQuyetToanDahtId);
        void Save(NhQtQuyetToanDaht entity);
        List<NhQtQuyetToanDaht> FindAllByNamLamViec(int yearOfWork);
        List<NhQtQuyetToanDaht> FindAllNhQtDaht();
        List<NhQtQuyetToanDaht> FindAllTongHopByNamLamViec(int yearOfWork);
        IEnumerable<NHDAQDDauTuChiPhiHangMuc> GetChiPhiHangMucByDuAnId(Guid duAnId, Guid DeNghiQTDAHTId);
        void LockUnlockItem(Guid id);
        void TongHopQTDAHT(NhQtQuyetToanDaht nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds);
        void UpdateAggregateStatus(string voucherIds);
    }
}
