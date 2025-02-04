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
    public class VdtQtDeNghiQuyetToanChiTietService : IVdtQtDeNghiQuyetToanChiTietService
    {
        private readonly IVdtQtDeNghiQuyetToanChiTietRepository _cpChungTuChiTietRepository;

        public VdtQtDeNghiQuyetToanChiTietService(IVdtQtDeNghiQuyetToanChiTietRepository chungTuChiTietRepository)
        {
            _cpChungTuChiTietRepository = chungTuChiTietRepository;
        }

        public VdtQtDeNghiQuyetToanChiTiet Add(VdtQtDeNghiQuyetToanChiTiet entity)
        {
            _cpChungTuChiTietRepository.Add(entity);
            return entity;
        }

        public int Delete(Guid id)
        {
            VdtQtDeNghiQuyetToanChiTiet entity = _cpChungTuChiTietRepository.Find(id);
            if (entity != null)
            {
                return _cpChungTuChiTietRepository.Delete(entity);
            }
            return 0;
        }

        public VdtQtDeNghiQuyetToanChiTiet Find(params object[] keyValues)
        {
            return _cpChungTuChiTietRepository.Find(keyValues);
        }

        public int Update(VdtQtDeNghiQuyetToanChiTiet entity)
        {
            return _cpChungTuChiTietRepository.Update(entity);
        }

        public IEnumerable<VdtQtDeNghiQuyetToanChiTiet> FindByCondition(Expression<Func<VdtQtDeNghiQuyetToanChiTiet, bool>> predicate)
        {
            return _cpChungTuChiTietRepository.FindAll(predicate);
        }

        public List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAn(Guid duAnId)
        {
            return _cpChungTuChiTietRepository.FindListDuToanChiPhiByDuAn(duAnId);
        }

        public int AddRange(IEnumerable<VdtQtDeNghiQuyetToanChiTiet> entities)
        {
            return _cpChungTuChiTietRepository.AddRange(entities);
        }

        public void DeleteByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            List<VdtQtDeNghiQuyetToanChiTiet> list = _cpChungTuChiTietRepository.FindByDeNghiQuyetToanId(deNghiQuyetToanId);
            foreach (VdtQtDeNghiQuyetToanChiTiet item in list)
            {
                _cpChungTuChiTietRepository.Delete(item);
            }
        }

        public List<VdtQtDeNghiQuyetToanChiTiet> FindByDeNghiQuyetToanId(Guid deNghiQuyetToanId)
        {
            return _cpChungTuChiTietRepository.FindByDeNghiQuyetToanId(deNghiQuyetToanId);
        }

        public List<VdtDaDuToanChiPhiDataQuery> FindListDuToanChiPhiByDuAnNew(Guid duAnId)
        {
            List<VdtDaDuToanChiPhiDataQuery> lstData = _cpChungTuChiTietRepository.FindListDuToanChiPhiByDuAnNew(duAnId);
            List<VdtDaDuToanChiPhiDataQuery> results = new List<VdtDaDuToanChiPhiDataQuery>();
            if (lstData == null) return results;
            Dictionary<Guid?, VdtDaDuToanChiPhiDataQuery> dicData = lstData.ToDictionary(n => n.Id, n => n);

            foreach (var item in lstData.Where(n => !n.IdChiPhiDuAnParent.HasValue))
            {
                results.AddRange(ReciveDuToanChiPhi(item, lstData, dicData));
            }
            return results;
        }

        private List<VdtDaDuToanChiPhiDataQuery> ReciveDuToanChiPhi(VdtDaDuToanChiPhiDataQuery item, List<VdtDaDuToanChiPhiDataQuery> lstData, Dictionary<Guid?, VdtDaDuToanChiPhiDataQuery> dicData, int iPhanCap = 0)
        {
            List<VdtDaDuToanChiPhiDataQuery> results = new List<VdtDaDuToanChiPhiDataQuery>();
            List<VdtDaDuToanChiPhiDataQuery> lstChild = lstData.Where(n => n.IdChiPhiDuAnParent == item.ChiPhiId).ToList();
            var current = dicData[item.Id];

            if (!item.IdChiPhiDuAnParent.HasValue || (lstChild != null && lstChild.Count == 0))
                current.IsHangCha = true;

            if (iPhanCap != 0)
                current.PhanCap = iPhanCap + 1;

            results.Add(current);

            foreach (var child in lstChild)
            {
                results.AddRange(ReciveDuToanChiPhi(child, lstData, dicData, current.PhanCap));
            }
            return results;
        }
    }
}
