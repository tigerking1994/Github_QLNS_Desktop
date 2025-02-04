using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtQuyetToanService : IVdtQtQuyetToanService
    {
        private IVdtQtQuyetToanRepository _vdtQtQuyetToanRepository;
        public VdtQtQuyetToanService(IVdtQtQuyetToanRepository vdtQtQuyetToanRepository)
        {
            _vdtQtQuyetToanRepository = vdtQtQuyetToanRepository;
        }

        public int Add(VdtQtQuyetToan entity)
        {
            return _vdtQtQuyetToanRepository.Add(entity);
        }

        public int AddRange(IEnumerable<VdtQtQuyetToan> entities)
        {
            return _vdtQtQuyetToanRepository.AddRange(entities);
        }

        public int Delete(Guid Id)
        {
            VdtQtQuyetToan itemDelete = _vdtQtQuyetToanRepository.Find(Id);
            if (itemDelete != null)
            {
                return _vdtQtQuyetToanRepository.Delete(itemDelete);
            }
            return 0;
        }

        public VdtQtQuyetToan Find(params object[] keyValues)
        {
            return _vdtQtQuyetToanRepository.Find(keyValues);
        }

        public IEnumerable<VdtQtQuyetToanQuery> FindAllPheDuyetQuyetToan(int yearOfWork, string userName)
        {
            return _vdtQtQuyetToanRepository.FindAllPheDuyetQuyetToan(yearOfWork, userName);
        }

        public List<NguonVonQuyetToanQuery> GetNguonVonByDuToanIdDeNghiQuyetToanId(string duToanId, string deNghiQuyetToanId)
        {
            return _vdtQtQuyetToanRepository.GetNguonVonByDuToanIdDeNghiQuyetToanId(duToanId, deNghiQuyetToanId);
        }

        public int Update(VdtQtQuyetToan entity)
        {
            return _vdtQtQuyetToanRepository.Update(entity);
        }

        public void UpdateTienQuyetToan(string quyetToanId)
        {
            _vdtQtQuyetToanRepository.UpdateTienQuyetToan(quyetToanId);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _vdtQtQuyetToanRepository.LockOrUnLock(id, lockStatus);
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            return _vdtQtQuyetToanRepository.IsExistSoQuyetDinh(soQuyetDinh, id);
        }
    }
}
