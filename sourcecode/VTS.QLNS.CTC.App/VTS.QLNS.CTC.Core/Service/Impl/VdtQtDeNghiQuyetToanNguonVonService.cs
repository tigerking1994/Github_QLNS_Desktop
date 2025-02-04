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
    public class VdtQtDeNghiQuyetToanNguonVonService : IVdtQtDeNghiQuyetToanNguonVonService
    {
        private readonly IVdtQtDeNghiQuyetToanNguonVonRepository _cpChungTuChiTietRepository;

        public VdtQtDeNghiQuyetToanNguonVonService(IVdtQtDeNghiQuyetToanNguonVonRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public VdtQtDeNghiQuyetToanNguonvon Add(VdtQtDeNghiQuyetToanNguonvon entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtQtDeNghiQuyetToanNguonvon entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtQtDeNghiQuyetToanNguonvon Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtQtDeNghiQuyetToanNguonvon entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtQtDeNghiQuyetToanNguonvon> FindByCondition(Expression<Func<VdtQtDeNghiQuyetToanNguonvon, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public int AddRange(IEnumerable<VdtQtDeNghiQuyetToanNguonvon> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }

        public void DeleteByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            List<VdtQtDeNghiQuyetToanNguonvon> list = _cpChungTuChiTietRepository.FindByDeNghiQuyetToanId(deNghiQuyetToanId);
            foreach (VdtQtDeNghiQuyetToanNguonvon item in list)
            {
                _cpChungTuChiTietRepository.Delete(item);
            }
        }

        public List<VdtQtDeNghiQuyetToanNguonvon> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            return _cpChungTuChiTietRepository.FindByDeNghiQuyetToanId(deNghiQuyetToanId);
        }

        public List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByDuToanId(string duToanId)
        {
            return _cpChungTuChiTietRepository.GetNguonVonByDuToanId(duToanId);
        }
        public List<NguonVonQuyetToanKeHoachQuery> GetNguonVonByQDDTId(string duToanId)
        {
            return _cpChungTuChiTietRepository.GetNguonVonByQDDTId(duToanId);
        }
    }
}
