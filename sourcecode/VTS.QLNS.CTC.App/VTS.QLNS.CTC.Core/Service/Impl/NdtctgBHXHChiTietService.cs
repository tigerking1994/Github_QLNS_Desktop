using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NdtctgBHXHChiTietService : INdtctgBHXHChiTietService
    {
        private readonly INdtctgBHXHChiTietRepository _iNdtctgchitietBHXHRepository;
        public NdtctgBHXHChiTietService(INdtctgBHXHChiTietRepository iNdtctgchitietBHXHRepository)
        {
            _iNdtctgchitietBHXHRepository = iNdtctgchitietBHXHRepository;
        }

        public IEnumerable<BhDtctgBHXHChiTiet> FindByCondition(Guid Id)
        {
            return _iNdtctgchitietBHXHRepository.FindByCondition(Id);
        }
        public IEnumerable<BhDtctgBHXHChiTietQuery> GetListNhanDuToanChiTrenGiaoChiTiet(Guid idNdtctg, string sLNs, int iNamlamViec, string idMaDonVi, int loaiDotNhanPhanBo)
        {
            return _iNdtctgchitietBHXHRepository.GetListNhanDuToanChiTrenGiaoChiTiet(idNdtctg, sLNs, iNamlamViec, idMaDonVi, loaiDotNhanPhanBo);
        }

        public int Add(BhDtctgBHXHChiTiet item)
        {
            return _iNdtctgchitietBHXHRepository.Add(item);
        }

        public int Update(BhDtctgBHXHChiTiet item)
        {
            return _iNdtctgchitietBHXHRepository.Update(item);
        }
        public int Delete(BhDtctgBHXHChiTiet item)
        {
            return _iNdtctgchitietBHXHRepository.Delete(item);
        }
        public int AddRange(IEnumerable<BhDtctgBHXHChiTiet> lstItems)
        {
            return _iNdtctgchitietBHXHRepository.AddRange(lstItems);
        }
        public int RemoveRange(IEnumerable<BhDtctgBHXHChiTiet> lstItems)
        {
            return _iNdtctgchitietBHXHRepository.RemoveRange(lstItems);
        }
        public IEnumerable<BhDtctgBHXHChiTietQuery> GetBaoCaoChiTieuNganSach(string idDonVi, int iNamLamViec, string sLNS, int dotNhan, string sMaLoaiChi, int donViTinh)
        {
            return _iNdtctgchitietBHXHRepository.GetBaoCaoChiTieuNganSach(idDonVi, iNamLamViec, sLNS, dotNhan, sMaLoaiChi, donViTinh);
        }

        public List<BhDtctgBHXHChiTietQuery> GetListDataAgregateChiTiet(Guid idChungTu, string sLNS, int yearOfWork, string sMaDonVi, Guid? IDLoaiChi)
        {
            return _iNdtctgchitietBHXHRepository.GetListDataAgregateChiTiet(idChungTu, sLNS, yearOfWork, sMaDonVi, IDLoaiChi);
        }

        public bool ExistChungTu(Guid id)
        {
            return _iNdtctgchitietBHXHRepository.ExistChungTu(id);
        }

        public List<BhDtctgBHXHChiTietQuery> GetListDataAgregateAdjustChiTiet(Guid idChungTu, int namLamViec, string sMaDonVi, DateTime? dNgayChungTu, string sLNS)
        {
            return _iNdtctgchitietBHXHRepository.GetListDataAgregateAdjustChiTiet(idChungTu, namLamViec, sMaDonVi, dNgayChungTu, sLNS);
        }

        public List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int namLamViec, DateTime dNgayChungTu)
        {
            return _iNdtctgchitietBHXHRepository.FindGiaTriDieuChinhThuBHXH(iID_MaDonVi, namLamViec, dNgayChungTu);
        }
        public List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string iID_MaDonVi, int namLamViec)
        {
            return _iNdtctgchitietBHXHRepository.FindGiaTriDieuChinhThuBHXHChangeRequest(iID_MaDonVi, namLamViec);
        }
    }
}
