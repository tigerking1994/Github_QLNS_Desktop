using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtPheDuyetThanhToanChiTietService : IVdtTtPheDuyetThanhToanChiTietService
    {
        private readonly IVdtTtPheDuyetThanhToanChiTietRepository _detailRepository;

        public VdtTtPheDuyetThanhToanChiTietService(IVdtTtPheDuyetThanhToanChiTietRepository detailRepository)
        {
            _detailRepository = detailRepository;
        }

        public List<VdtTtPheDuyetThanhToanChiTiet> FindByDeNghiThanhToanId(Guid deNghiThanhToanId)
        {
            return _detailRepository.FindByDeNghiThanhToanId(deNghiThanhToanId);
        }
    }
}
