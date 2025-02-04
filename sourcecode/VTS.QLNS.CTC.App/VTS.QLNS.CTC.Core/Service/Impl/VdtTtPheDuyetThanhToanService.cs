using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtPheDuyetThanhToanService : IVdtTtPheDuyetThanhToanService
    {
        private readonly IVdtTtDeNghiThanhToanRepository _thanhtoanService;
        private readonly IVdtTtPheDuyetThanhToanRepository _repository;
        private readonly IVdtTtPheDuyetThanhToanChiTietRepository _detailRepository;

        public VdtTtPheDuyetThanhToanService(
            IVdtTtDeNghiThanhToanRepository thanhtoanService,
            IVdtTtPheDuyetThanhToanRepository repository,
            IVdtTtPheDuyetThanhToanChiTietRepository detailRepository)
        {
            _thanhtoanService = thanhtoanService;
            _repository = repository;
            _detailRepository = detailRepository;
        }

        public List<VdtTtPheDuyetThanhToanQuery> GetAllPheDuyetThanhToan()
        {
            return _repository.GetAllPheDuyetThanhToan();
        }

        public void Insert(VdtTtPheDuyetThanhToan item, string userLogin)
        {
            item.Id = Guid.NewGuid();
            item.SUserCreate = userLogin;
            item.DDateCreate = DateTime.Now;
            _repository.Add(item);
        }

        public void Update(VdtTtPheDuyetThanhToan item, string userLogin)
        {
            var data = _repository.Find(item.Id);
            if (data == null) return;
            data.SSoQuyetDinh = item.SSoQuyetDinh;
            data.DNgayQuyetDinh = item.DNgayQuyetDinh;
            data.SNguoiLap = item.SNguoiLap;
            _repository.Update(item);
        }

        public void InsertDetailData(Guid parentId, List<VdtTtPheDuyetThanhToanChiTiet> lstData)
        {
            _detailRepository.DeletePheDuyetThanhToanChiTietByParentId(parentId);
            if (lstData == null) return;
            _detailRepository.AddRange(lstData);
            var parentData = _thanhtoanService.Find(parentId);
        }

        public void DeletePheDuyetThanhToan(Guid iIdData, string sUserLogin)
        {
            var data = _repository.Find(iIdData);
            if (data == null) return;
            _detailRepository.DeletePheDuyetThanhToanChiTietByParentId(iIdData);
            _repository.Delete(data);
        }

        public List<VdtTtPheDuyetThanhToanChiTietQuery> GetAllPheDuyetThanhToanChiTiet(Guid iIdParentId)
        {
            return _detailRepository.GetAllPheDuyetThanhToanChiTiet(iIdParentId);
        }

        public List<PheDuyetThanhToanChiTietQuery> GetAllVdtTTPheDuyetThanhToanChiTiet(Guid iIdParentId)
        {
            return _detailRepository.GetAllVdtTTPheDuyetThanhToanChiTiet(iIdParentId);
        }
    }
}
