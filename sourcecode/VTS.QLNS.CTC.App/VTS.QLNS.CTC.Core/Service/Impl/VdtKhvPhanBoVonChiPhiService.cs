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
    public class VdtKhvPhanBoVonChiPhiService : IVdtKhvPhanBoVonChiPhiService
    {
        private readonly IVdtKhvPhanBoVonChiPhiRepository _repository;

        public VdtKhvPhanBoVonChiPhiService(IVdtKhvPhanBoVonChiPhiRepository repository)
        {
            _repository = repository;
        }

        public List<VdtKhvPhanBoVonChiPhi> GetVdtKhvPhanBoVonChiPhiInThanhToanChiPhiDialog(string sMaDonVi, Guid iIdDuAnId, int iNamKeHoach)
        {
            return _repository.GetVdtKhvPhanBoVonChiPhiInThanhToanChiPhiDialog(sMaDonVi, iIdDuAnId, iNamKeHoach);
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhi> FindAll()
        {
            return _repository.FindAll();
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhiQuery> FindGiaoDuToanIndex()
        {
            return _repository.FindGiaoDuToanIndex();
        }

        public VdtKhvPhanBoVonChiPhi FindById(Guid idChiPhi)
        {
            return _repository.Find(idChiPhi);
        }
        public void Delete(VdtKhvPhanBoVonChiPhi chiphi)
        {
            _repository.Delete(chiphi);
        }

        public void Add(VdtKhvPhanBoVonChiPhi chiphi)
        {
            _repository.Add(chiphi);
        }
        public void Update(VdtKhvPhanBoVonChiPhi chiphi)
        {
            _repository.Update(chiphi);
        }
        public void Adjust(VdtKhvPhanBoVonChiPhi entity)
        {
            _repository.Add(entity);

            // Update BActive = false của chứng từ gốc
            VdtKhvPhanBoVonChiPhi parentEntity = _repository.Find(entity.IIdParentId);
            if (parentEntity != null)
            {
                parentEntity.BActive = false;
            }
            _repository.Update(parentEntity);
        }
        public bool LogItem(Guid iId, string sUserLogin)
        {
            var data = _repository.Find(iId);
            if (data == null || data.Id == Guid.Empty) return false;
            data.BKhoa = !data.BKhoa;
            data.dDateUpdate = DateTime.Now;
            data.sUserUpdate = sUserLogin;
            return _repository.Update(data) != 0;
        }

        public bool IsExistSoQuyetDinh(string soQuyetDinh, Guid id)
        {
            return _repository.IsExistSoQuyetDinh(soQuyetDinh, id);
        }
    }
}
