using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtQuyetToanDahtService : INhQtQuyetToanDahtService
    {
        private readonly INhQtQuyetToanDahtRepository _nhQtQuyetToanDahtRepository;

        public NhQtQuyetToanDahtService(INhQtQuyetToanDahtRepository nhQtQuyetToanDahtRepository)
        {
            _nhQtQuyetToanDahtRepository = nhQtQuyetToanDahtRepository;
        }

        public void Delete(Guid id)
        {
            _nhQtQuyetToanDahtRepository.Delete(id);
        }

        public List<NhQtQuyetToanDaht> FindAllByNamLamViec(int yearOfWork)
        {
            return _nhQtQuyetToanDahtRepository.FindAllByNamLamViec(yearOfWork);
        }

        public List<NhQtQuyetToanDaht> FindAllNhQtDaht()
        {
            return _nhQtQuyetToanDahtRepository.FindAllNhQtDaht();
        }
        public List<NhQtQuyetToanDaht> FindAllTongHopByNamLamViec(int yearOfWork)
        {
            return _nhQtQuyetToanDahtRepository.FindAllTongHopByNamLamViec(yearOfWork);
        }

        public NhQtQuyetToanDaht FindById(Guid id)
        {
            return _nhQtQuyetToanDahtRepository.Find(id);
        }

        public void UpdateAggregateStatus(string voucherIds)
        {
            _nhQtQuyetToanDahtRepository.UpdateAggregateStatus(voucherIds);
        }

        public IEnumerable<NHDAQDDauTuChiPhiHangMuc> GetChiPhiHangMucByDuAnId(Guid duanId, Guid NHDAQDDauTuId)
        {
            return _nhQtQuyetToanDahtRepository.GetChiPhiHangMucByDuAnId(duanId, NHDAQDDauTuId);
        }

        public void LockUnlockItem(Guid id)
        {
            _nhQtQuyetToanDahtRepository.LockUnlockItem(id);
        }

        public void Save(NhQtQuyetToanDaht entity)
        {
            _nhQtQuyetToanDahtRepository.Save(entity);
        }

        public void SaveNhQtQuyetToanDahtChiTiet(List<NhQtQuyetToanDahtChiTiet> entities, Guid NhQtQuyetToanDahtId)
        {
            _nhQtQuyetToanDahtRepository.SaveNhQtQuyetToanDahtChiTiet(entities, NhQtQuyetToanDahtId);
        }

        public void TongHopQTDAHT(NhQtQuyetToanDaht nhTtDeNghiThanhToan, List<Guid> voucherAgregatesIds)
        {
            _nhQtQuyetToanDahtRepository.TongHopQTDAHT(nhTtDeNghiThanhToan, voucherAgregatesIds);
        }
    }
}
