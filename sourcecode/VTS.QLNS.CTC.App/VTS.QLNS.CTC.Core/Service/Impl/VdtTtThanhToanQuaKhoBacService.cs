using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtThanhToanQuaKhoBacService : IVdtTtThanhToanQuaKhoBacService
    {
        private readonly IVdtTtThanhToanQuaKhoBacRepository _thanhToanKhoBacRepository;
        private readonly IVdtTtThanhToanQuaKhoBacChiTietRepository _thanhToanKhoBacChiTietRepository;

        public VdtTtThanhToanQuaKhoBacService(IVdtTtThanhToanQuaKhoBacRepository thanhToanKhoBacRepository,
            IVdtTtThanhToanQuaKhoBacChiTietRepository thanhToanKhoBacChiTietRepository)
        {
            _thanhToanKhoBacRepository = thanhToanKhoBacRepository;
            _thanhToanKhoBacChiTietRepository = thanhToanKhoBacChiTietRepository;
        }

        public IEnumerable<VdtTtThanhToanQuaKhoBacQuery> GetDataIndex()
        {
            return _thanhToanKhoBacRepository.GetDataIndex();
        }

        public void DeleteThanhToanKhoBac(Guid iId)
        {
            VdtTtThanhToanQuaKhoBac data = _thanhToanKhoBacRepository.Find(iId);
            if (data == null) return;
            var dataDetails = _thanhToanKhoBacChiTietRepository.GetDetailDataByParentId(iId);
            if (dataDetails != null && dataDetails.Any())
            {
                _thanhToanKhoBacChiTietRepository.DeleteDetailData(dataDetails);
            }
            _thanhToanKhoBacRepository.Delete(data);
        }

        public void Insert(VdtTtThanhToanQuaKhoBac data, string sUserLogin)
        {
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            _thanhToanKhoBacRepository.Add(data);
        }

        public void Update(VdtTtThanhToanQuaKhoBac data, string sUserLogin)
        {
            var dataUpdate = _thanhToanKhoBacRepository.Find(data.Id);
            if (dataUpdate == null) return;
            dataUpdate.SSoThanhToan = data.SSoThanhToan;
            dataUpdate.DNgayThanhToan = data.DNgayThanhToan;
            dataUpdate.SNguoiLap = data.SNguoiLap;
            dataUpdate.IIdDonViNhanThanhToanId = data.IIdDonViNhanThanhToanId;
            dataUpdate.IIdMaDonViNhanThanhToanID = data.IIdMaDonViNhanThanhToanID;
            dataUpdate.DDateUpdate = DateTime.Now;
            dataUpdate.SUserUpdate = sUserLogin;
            _thanhToanKhoBacRepository.Update(dataUpdate);
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByThanhToanKhoBac(int iNamKeHoach, DateTime dNgayQuyetDinh, string sLNS, string iIdMaDonViQuanLy)
        {
            return _thanhToanKhoBacChiTietRepository.GetDuAnByThanhToanKhoBac(iNamKeHoach, dNgayQuyetDinh, sLNS, iIdMaDonViQuanLy);
        }

        public IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetail(int iNamKeHoach, DateTime dNgayQuyetDinh, Guid iIdLoaiNguonVon, string iIdMaDonViQuanLy)
        {
            return _thanhToanKhoBacChiTietRepository.GetThanhToanKhoBacDetail(iNamKeHoach, dNgayQuyetDinh, iIdLoaiNguonVon, iIdMaDonViQuanLy);
        }

        public IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetailByParentId(Guid iIdParentId)
        {
            return _thanhToanKhoBacChiTietRepository.GetThanhToanKhoBacDetailByParentId(iIdParentId);
        }

        public bool InsertDetail(Guid iIdParentId, List<VdtTtThanhToanQuaKhoBacChiTiet> lstData)
        {
            var lstDataOld = _thanhToanKhoBacChiTietRepository.GetDetailDataByParentId(iIdParentId);
            if (lstDataOld != null)
            {
                _thanhToanKhoBacChiTietRepository.DeleteDetailData(lstDataOld);
            }
            lstData = lstData.Select(n => { n.IIdThanhToanId = iIdParentId; return n; }).ToList();
            int iCount = _thanhToanKhoBacChiTietRepository.AddRange(lstData);
            var parentData = _thanhToanKhoBacRepository.Find(iIdParentId);
            if (parentData == null) return false;
            parentData.FGiaTriThanhToan = lstData.Sum(n => n.FGiaTriThanhToan ?? 0);
            parentData.FGiaTriTamUng = lstData.Sum(n => n.FGiaTriTamUng ?? 0);
            _thanhToanKhoBacRepository.Update(parentData);
            return iCount != 0;
        }
    }
}
