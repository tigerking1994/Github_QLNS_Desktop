using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PheDuyetQuyetToanService : IPheDuyetQuyetToanService
    {
        private IVdtQuyetToanChiPhiRepository _vdtQuyetToanChiPhiRepository;
        private IVdtQuyetToanNguonVonRepository _vdtQuyetToanNguonVonRepository;

        public PheDuyetQuyetToanService(IVdtQuyetToanChiPhiRepository vdtQuyetToanChiPhiRepository,
                                        IVdtQuyetToanNguonVonRepository vdtQuyetToanNguonVonRepository)
        {
            _vdtQuyetToanChiPhiRepository = vdtQuyetToanChiPhiRepository;
            _vdtQuyetToanNguonVonRepository = vdtQuyetToanNguonVonRepository;
        }

        public int AddQuyetToanChiPhi(VdtQtQuyetToanChiPhi entity)
        {
            return _vdtQuyetToanChiPhiRepository.Add(entity);
        }

        public int AddRangeQuyetToanChiPhi(IEnumerable<VdtQtQuyetToanChiPhi> entities)
        {
            return _vdtQuyetToanChiPhiRepository.AddRange(entities);
        }

        public int AddRangeQuyetToanNguonVon(IEnumerable<VdtQtQuyetToanNguonvon> entities)
        {
            return _vdtQuyetToanNguonVonRepository.AddRange(entities);
        }

        public int DeleteChiPhi(Guid id)
        {
            return _vdtQuyetToanChiPhiRepository.Delete(id);
        }

        public int DeleteNguonVon(Guid id)
        {
            return _vdtQuyetToanNguonVonRepository.Delete(id);
        }

        public void DeleteQuyetToanNguonVonByQuyetToanId(Guid quyetToanId)
        {
            List<VdtQtQuyetToanNguonvon> list = _vdtQuyetToanNguonVonRepository.FindByQuyetToanId(quyetToanId);
            foreach (VdtQtQuyetToanNguonvon item in list)
            {
                _vdtQuyetToanNguonVonRepository.Delete(item);
            }
        }

        public IEnumerable<PheDuyetQuyetToanDetailQuery> FindAllPheDuyetQuyetToanDetailByCondition(string idDuAn, DateTime ngay)
        {
            return _vdtQuyetToanChiPhiRepository.FindAllPheDuyetQuyetToanDetailByCondition(idDuAn, ngay);
        }

        public List<VdtQtQuyetToanNguonvon> FindByQuyetToanId(Guid quyetToanId)
        {
            return _vdtQuyetToanNguonVonRepository.FindByQuyetToanId(quyetToanId);
        }

        public VdtQtQuyetToan FindQuyetToanByIdQt(Guid quyetToanId)
        {
            return _vdtQuyetToanChiPhiRepository.FindQuyetToanByIdQt(quyetToanId);
        }

        public VdtQtQuyetToanChiPhi FindQuyetToanChiPhi(params object[] keyValues)
        {
            return _vdtQuyetToanChiPhiRepository.Find(keyValues);
        }

        public VdtQtQuyetToanNguonvon FindQuyetToanNguonVon(params object[] keyValues)
        {
            return _vdtQuyetToanNguonVonRepository.Find(keyValues);
        }

        public int UpdateChiPhi(VdtQtQuyetToanChiPhi entity)
        {
            return _vdtQuyetToanChiPhiRepository.Update(entity);
        }

        public int UpdateNguonVon(VdtQtQuyetToanNguonvon entity)
        {
            return _vdtQuyetToanNguonVonRepository.Update(entity);
        }
    }
}
