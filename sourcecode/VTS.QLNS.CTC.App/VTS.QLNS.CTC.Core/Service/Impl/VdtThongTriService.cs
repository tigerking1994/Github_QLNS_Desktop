using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtThongTriService : IVdtThongTriService
    {
        private readonly IVdtThongTriRepository _thongTriRepository;
        private readonly IVdtThongTriChiTietRepository _thongTriChiTietRepository;
        private readonly IVdtTtDeNghiThanhToanRepository _thanhtoanRepository;

        public VdtThongTriService(IVdtThongTriRepository thongTriRepository,
            IVdtThongTriChiTietRepository thongTriChiTietRepository,
            IVdtTtDeNghiThanhToanRepository thanhtoanRepository)
        {
            _thongTriRepository = thongTriRepository;
            _thongTriChiTietRepository = thongTriChiTietRepository;
            _thanhtoanRepository = thanhtoanRepository;
        }

        public IEnumerable<VdtThongTriQuery> GetVdtThongTriIndex(Guid iIdLoaiThongTri, int openFromPheDuyetThanhToan)
        {
            return _thongTriRepository.GetVdtThongTriIndex(iIdLoaiThongTri, openFromPheDuyetThanhToan);
        }

        public void DeleteThongTriThanhToan(VdtThongTri data)
        {
            _thongTriChiTietRepository.DeleteThongTriChiTietByParentId(data.Id);
            var dataDelete = _thongTriRepository.Find(data.Id);
            _thanhtoanRepository.UpdateThongTriThanhToan(data.Id, null);
            _thongTriRepository.Delete(dataDelete);
        }

        public void Insert(VdtThongTri data, string sUserLogin)
        {
            data.Id = Guid.NewGuid();
            data.DDateCreate = DateTime.Now;
            data.SUserCreate = sUserLogin;
            data.BIsCanBoDuyet = false;
            data.BIsDuyet = false;
            _thongTriRepository.Add(data);
        }

        public void Update(VdtThongTri data, string sUserLogin)
        {
            var dataUpdate = _thongTriRepository.Find(data.Id);
            dataUpdate.DDateUpdate = DateTime.Now;
            dataUpdate.SUserUpdate = sUserLogin;
            dataUpdate.SMaThongTri = data.SMaThongTri;
            dataUpdate.SNguoiLap = data.SNguoiLap;
            dataUpdate.SThuTruongDonVi = data.SThuTruongDonVi;
            dataUpdate.STruongPhong = data.STruongPhong;
            _thongTriRepository.Update(dataUpdate);
        }

        public IEnumerable<VdtDmLoaiThongTri> GetAllDmLoaiThongTri()
        {
            return _thongTriRepository.GetAllDmLoaiThongTri();
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTiet(Guid iIdThongTri, string sMaDonVi, int iLoaiThongTri, int iNamKeHoach, DateTime dNgayThongTri,
            string sMaNguonVon, DateTime? dNgayLapGanNhat, string sMaLoaiCongTrinh)
        {
            if (string.IsNullOrEmpty(sMaLoaiCongTrinh))
            {
                return _thongTriChiTietRepository.GetVdtThongTriChiTiet(iIdThongTri, sMaDonVi, iLoaiThongTri, iNamKeHoach, dNgayThongTri, sMaNguonVon);
            }
            else
            {
                return _thongTriChiTietRepository.GetVdtThongTriQuyetToanChiTiet(sMaDonVi, iNamKeHoach, dNgayThongTri, sMaNguonVon, dNgayLapGanNhat, sMaLoaiCongTrinh);
            }
        }

        public IEnumerable<VdtDmKieuThongTri> GetAllKieuThongTri()
        {
            return _thongTriRepository.GetAllKieuThongTri();
        }

        public void InsertThongTriChiTiet(List<VdtThongTriChiTiet> lstData)
        {
            _thongTriChiTietRepository.AddRange(lstData);
        }

        public void DeleteThongTriChiTietByParentId(Guid iIdThongTriId)
        {
            _thongTriChiTietRepository.DeleteThongTriChiTietByParentId(iIdThongTriId);
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByParentId(Guid iId)
        {
            return _thongTriChiTietRepository.GetVdtThongTriChiTietByParentId(iId);
        }

        public IEnumerable<VdtCanCuThanhToanQuery> GetCanCuThanhToanByThongTri(Guid iIdThongTri, bool bIsThanhToan, string sMaDonVi, int iNamLamViec, int iNguonVon, DateTime dNgayLap)
        {
            return _thongTriChiTietRepository.GetCanCuThanhToanByThongTri(iIdThongTri, bIsThanhToan, sMaDonVi, iNamLamViec, iNguonVon, dNgayLap);
        }

        public IEnumerable<VdtThongTriChiTietQuery> GetVdtThongTriChiTietByPheDuyet()
        {
            return _thongTriChiTietRepository.GetVdtThongTriChiTietByPheDuyet();
        }

        public IEnumerable<VdtThongTriChiTietQuery> FindByIdThongTri(Guid idThongTriId)
        {
            return _thongTriChiTietRepository.FindByIdThongTri(idThongTriId);
        }

        public List<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanById(Guid iIdThongTri)
        {
            var datas = _thongTriChiTietRepository.GetVdtThongTriQuyetToanById(iIdThongTri);
            if (datas == null) return new List<VdtThongTriQuyetToanQuery>();
            return datas.ToList();
        }

        public List<VdtThongTriQuyetToanQuery> GetVdtThongTriQuyetToanChiTiet(Guid iIdQuyetToanId)
        {
            var datas = _thongTriChiTietRepository.GetVdtThongTriQuyetToan(iIdQuyetToanId);
            if (datas == null) return new List<VdtThongTriQuyetToanQuery>();
            return datas.ToList();
        }
    }
}
