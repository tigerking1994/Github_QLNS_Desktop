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
    public class VdtKhvPhanBoVonChiPhiChiTietService : IVdtKhvPhanBoVonChiPhiChiTietService
    {
        private readonly IVdtKhvPhanBoVonChiPhiChiTietRepository _repository;

        public VdtKhvPhanBoVonChiPhiChiTietService(IVdtKhvPhanBoVonChiPhiChiTietRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhiChiTiet> FindAll()
        {
            return _repository.FindAll();
        }

        public void Add(VdtKhvPhanBoVonChiPhiChiTiet entity)
        {
            _repository.Add(entity);    
        }

        public void Update(VdtKhvPhanBoVonChiPhiChiTiet entity)
        {
            _repository.Update(entity);
        }

        public void Delete(VdtKhvPhanBoVonChiPhiChiTiet entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<VdtKhvPhanBoVonChiPhiChiTietQuery> FindByIdChiPhi(Guid idChiPhi)
        {
            return _repository.FindByIdChiPhi(idChiPhi);
        }
    }
}
