using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtDeNghiThanhToanChiTietService : IVdtTtDeNghiThanhToanChiTietService
    {
        private readonly IVdtTtDeNghiThanhToanChiTietRepository _vdtTtDeNghiThanhToanChiTietRepository;
        private readonly IVdtTtDeNghiThanhToanRepository _vdtTtDeNghiThanhToanRepository;

        public VdtTtDeNghiThanhToanChiTietService(
            IVdtTtDeNghiThanhToanChiTietRepository vdtTtDeNghiThanhToanChiTietRepository,
            IVdtTtDeNghiThanhToanRepository vdtTtDeNghiThanhToanRepository)
        {
            _vdtTtDeNghiThanhToanChiTietRepository = vdtTtDeNghiThanhToanChiTietRepository;
            _vdtTtDeNghiThanhToanRepository = vdtTtDeNghiThanhToanRepository;
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiTietQuery> GetDuAnByIdThanhToan(Guid iIdThanhToanId)
        {
            return _vdtTtDeNghiThanhToanChiTietRepository.GetDuAnByIdThanhToan(iIdThanhToanId);
        }

        public HopDongInfoQuery GetHopDongInfo(Guid iIdHopDong, DateTime dNgayPheDuyet, int iIdNguonVonId)
        {
            return _vdtTtDeNghiThanhToanChiTietRepository.GetHopDongInfo(iIdHopDong, dNgayPheDuyet, iIdNguonVonId);
        }

        public bool Insert(Guid iIdThanhToan, List<VdtTtDeNghiThanhToanChiTiet> data)
        {
            //var parentData = _vdtTtDeNghiThanhToanRepository.Find(iIdThanhToan);
            //if (parentData == null) return false;
            //_vdtTtDeNghiThanhToanChiTietRepository.DeleteByThanhToanId(iIdThanhToan);
            //if (!_vdtTtDeNghiThanhToanChiTietRepository.Insert(data)) return false;
            //if (parentData.ILoaiThanhToan == (int)PaymentType.Type.THANH_TOAN)
            //{
            //    parentData.FGiaTriThanhToan = data.Sum(n => (n.FGiaTriThanhToanTN ?? 0) + (n.FGiaTriThanhToanNN ?? 0));
            //}
            //else
            //{
            //    parentData.FGiaTriTamUng = data.Sum(n => (n.FGiaTriThanhToanTN ?? 0) + (n.FGiaTriThanhToanNN ?? 0));
            //}

            //parentData.FGiaTriThuHoi = data.Sum(
            //    n =>
            //    (n.FGiaTriThuHoiNamTruocTN ?? 0)
            //    + (n.FGiaTriThuHoiNamTruocNN ?? 0)
            //    + (n.FGiaTriThuHoiNamNayTN ?? 0)
            //    + (n.FGiaTriThuHoiNamNayNN ?? 0));
            //return _vdtTtDeNghiThanhToanRepository.Update(parentData) == 1;
            return true;
        }
    }
}
