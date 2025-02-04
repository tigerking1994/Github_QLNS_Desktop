using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDeNghiQuyetToanService : IVdtDeNghiQuyetToanService
    {
        private IVdtDeNghiQuyetToanRepository _vdtDeNghiQuyetToanRepository;

        public VdtDeNghiQuyetToanService(IVdtDeNghiQuyetToanRepository vdtDeNghiQuyetToanRepository)
        {
            _vdtDeNghiQuyetToanRepository = vdtDeNghiQuyetToanRepository;
        }

        public int Add(VdtQtDeNghiQuyetToan entity)
        {
            return _vdtDeNghiQuyetToanRepository.Add(entity);
        }

        public int AddRange(IEnumerable<VdtQtDeNghiQuyetToan> entities)
        {
            return _vdtDeNghiQuyetToanRepository.AddRange(entities);
        }

        public int Delete(Guid Id)
        {
            VdtQtDeNghiQuyetToan item = _vdtDeNghiQuyetToanRepository.Find(Id);
            if (item != null)
            {
                return _vdtDeNghiQuyetToanRepository.Delete(item);
            }
            return 0;
        }

        public VdtQtDeNghiQuyetToan Find(params object[] keyValues)
        {
            return _vdtDeNghiQuyetToanRepository.Find(keyValues);
        }

        public VdtQtDeNghiQuyetToan FindByDuAnId(Guid duAnId)
        {
            return _vdtDeNghiQuyetToanRepository.FindByDuAnId(duAnId);
        }
        public IEnumerable<VdtQtDeNghiQuyetToan> FindLstDeNghiQTByDuAnId(Guid duAnId)
        {
            return _vdtDeNghiQuyetToanRepository.FindLstDeNghiQTByDuAnId(duAnId);
        }
        public List<VdtQtDeNghiQuyetToan> FindDeNghiTongHop()
        {
            return _vdtDeNghiQuyetToanRepository.FindDeNghiTongHop();
        }

        public List<ReportQuyetToanHoanThanhQuery> GetDataReportQuyetToanHoanThanh(int namLamViec)
        {
            return _vdtDeNghiQuyetToanRepository.GetDataReportQuyetToanHoanThanh(namLamViec);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _vdtDeNghiQuyetToanRepository.LockOrUnLock(id, lockStatus);
        }

        public void TongHopDeNghiQuyetToan(VdtQtDeNghiQuyetToan vdtTtDeNghiQT, List<Guid> voucherAgregatesIds)
        {
            _vdtDeNghiQuyetToanRepository.TongHopDeNghiQuyetToan(vdtTtDeNghiQT, voucherAgregatesIds);
        }

        public int Update(VdtQtDeNghiQuyetToan entity)
        {
            return _vdtDeNghiQuyetToanRepository.Update(entity);
        }

        public void UpdateTotal(string voucherId)
        {
            _vdtDeNghiQuyetToanRepository.UpdateTotal(voucherId);
        }
    }
}
