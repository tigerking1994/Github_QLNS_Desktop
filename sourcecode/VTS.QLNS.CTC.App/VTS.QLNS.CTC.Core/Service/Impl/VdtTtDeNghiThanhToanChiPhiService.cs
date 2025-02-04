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
    public class VdtTtDeNghiThanhToanChiPhiService : IVdtTtDeNghiThanhToanChiPhiService
    {
        private readonly IVdtTtDeNghiThanhToanChiPhiRepository _repository;
        private readonly IVdtTtDeNghiThanhToanChiPhiChiTietRepository _detailRepository;

        public VdtTtDeNghiThanhToanChiPhiService(IVdtTtDeNghiThanhToanChiPhiRepository repository,
            IVdtTtDeNghiThanhToanChiPhiChiTietRepository detailRepository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
        }

        public VdtTtDeNghiThanhToanChiPhi Find(Guid id)
        {
            return _repository.Find(id);
        }

        public void Insert(VdtTtDeNghiThanhToanChiPhi obj)
        {
            _repository.Add(obj);
        }

        public void Update(VdtTtDeNghiThanhToanChiPhi obj)
        {
            _repository.Update(obj);
        }

        public void Delete(Guid iId)
        {
            var data =_repository.Find(iId);
            _detailRepository.DeleteByParentId(iId);
            if (data != null)
                _repository.Delete(iId);
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiIndexQuery> GetDeNghiThanhToanChiPhiIndex()
        {
            return _repository.GetDeNghiThanhToanChiPhiIndex();
        }

        public void AddDetail(Guid iId, List<VdtTtDeNghiThanhToanChiPhiChiTiet> datas)
        {
            _detailRepository.DeleteByParentId(iId);
            _detailRepository.AddRange(datas);
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetail(Guid iIdDuToanId)
        {
            return _detailRepository.GetDeNghiThanhToanChiPhiDetail(iIdDuToanId);
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiChiTietQuery> GetDeNghiThanhToanChiPhiDetailById(Guid iIdChungTu, Guid iIdDuToan)
        {
            return _detailRepository.GetDeNghiThanhToanChiPhiDetailById(iIdChungTu, iIdDuToan);
        }
    }
}
