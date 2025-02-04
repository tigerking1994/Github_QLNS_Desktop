using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtDeNghiThanhToanUngChiTietService : IVdtTtDeNghiThanhToanUngChiTietService
    {
        private readonly IVdtTtDeNghiThanhToanUngRepository _thanhToanUngRepository;
        private readonly IVdtTtDeNghiThanhToanUngChiTietRepository _thanhToanUngChiTietRepository;

        public VdtTtDeNghiThanhToanUngChiTietService(
            IVdtTtDeNghiThanhToanUngRepository thanhToanUngRepository,
            IVdtTtDeNghiThanhToanUngChiTietRepository thanhToanUngChiTietRepository)
        {
            _thanhToanUngRepository = thanhToanUngRepository;
            _thanhToanUngChiTietRepository = thanhToanUngChiTietRepository;
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByDeNghiThanhToanUng(string iIdDonVi, DateTime dNgayLap)
        {
            return _thanhToanUngChiTietRepository.GetDuAnByDeNghiThanhToanUng(iIdDonVi, dNgayLap);
        }

        public IEnumerable<VdtTtDeNghiThanhToanUngChiTietQuery> GetDuAnByIdThanhToan(Guid iIdParent, string iIdDonViQuanLyId, DateTime dNgayDeNghi)
        {
            return _thanhToanUngChiTietRepository.GetDuAnByIdThanhToan(iIdParent, iIdDonViQuanLyId, dNgayDeNghi);
        }

        public VdtTtDeNghiThanhToanUngChiTietQuery GetLuyKeThanhToan(Guid iIdDuAn, Guid? iIdHopDong, string sMaDonViQuanLy, DateTime dNgayPheDuyet)
        {
            return _thanhToanUngChiTietRepository.GetLuyKeThanhToan(iIdDuAn, iIdHopDong, sMaDonViQuanLy, dNgayPheDuyet);
        }

        public bool Insert(Guid iIdThanhToan, List<VdtTtDeNghiThanhToanUngChiTiet> data)
        {
            _thanhToanUngChiTietRepository.DeleteByThanhToanId(iIdThanhToan);
            if (!_thanhToanUngChiTietRepository.Insert(data)) return false;
            double fGiaTriThanhToan = data.Sum(n => n.FGiaTriThanhToan ?? 0);
            double fGiaTriTamUng = data.Sum(n => n.FGiaTriTamUng ?? 0);
            double fGiaTriThuHoiUngNgoaiChiTieu = data.Sum(n => n.FGiaTriThuHoiUngNgoaiChiTieu ?? 0);
            double fGiaTriThuHoi = data.Sum(n => n.FGiaTriThuHoi ?? 0);
            VdtTtDeNghiThanhToanUng dataThanhToan = _thanhToanUngRepository.Find(iIdThanhToan);
            if (dataThanhToan == null) return false;
            dataThanhToan.FGiaTriThanhToan = fGiaTriThanhToan;
            dataThanhToan.FGiaTriTamUng = fGiaTriTamUng;
            dataThanhToan.FGiaTriThuHoiUngNgoaiChiTieu = fGiaTriThuHoiUngNgoaiChiTieu;
            dataThanhToan.FGiaTriThuHoi = fGiaTriThuHoi;
            return _thanhToanUngRepository.Update(dataThanhToan) != 0;
        }
    }
}
