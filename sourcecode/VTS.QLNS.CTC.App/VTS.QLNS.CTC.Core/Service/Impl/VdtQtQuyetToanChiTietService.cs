using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtQtQuyetToanChiTietService : IVdtQtQuyetToanChiTietService
    {
        private readonly IVdtQtQuyetToanChiTietRepository _cpChungTuChiTietRepository;

        public VdtQtQuyetToanChiTietService(IVdtQtQuyetToanChiTietRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public VdtQtQuyetToanChiTiet Add(VdtQtQuyetToanChiTiet entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtQtQuyetToanChiTiet entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtQtQuyetToanChiTiet Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtQtQuyetToanChiTiet entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtQtQuyetToanChiTiet> FindByCondition(Expression<Func<VdtQtQuyetToanChiTiet, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public void DeleteByQuyetToanId(Guid deNghiQuyetToanId)
        {
            List<VdtQtQuyetToanChiTiet> list = _cpChungTuChiTietRepository.FindByQuyetToanId(deNghiQuyetToanId);
            foreach (VdtQtQuyetToanChiTiet item in list)
            {
                _cpChungTuChiTietRepository.Delete(item);
            }
        }

        public int AddRange(IEnumerable<VdtQtQuyetToanChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }

        public List<VdtQtQuyetToanChiTiet> FindByQuyetToanId(Guid quyetToanId)
        {
            return _cpChungTuChiTietRepository.FindByQuyetToanId(quyetToanId);
        }

        public void UpdateTotal(string voucherId)
        {
            _cpChungTuChiTietRepository.UpdateTotal(voucherId);
        }
    }
}
