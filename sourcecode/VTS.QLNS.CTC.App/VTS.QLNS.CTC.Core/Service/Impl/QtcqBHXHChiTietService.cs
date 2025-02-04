using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcqBHXHChiTietService : IQtcqBHXHChiTietService
    {
        private readonly IQtcqBHXHChiTietRepository _iQtcqBHXHChiTietRepository;
        public QtcqBHXHChiTietService(IQtcqBHXHChiTietRepository iQtcqBHXHChiTietRepository)
        {
            _iQtcqBHXHChiTietRepository = iQtcqBHXHChiTietRepository;
        }
        public IEnumerable<BhQtcqBHXHChiTiet> FindByCondition(Expression<Func<BhQtcqBHXHChiTiet, bool>> predicate)
        {
            return _iQtcqBHXHChiTietRepository.FindByCondition(predicate);
        }

        public int AddRange(IEnumerable<BhQtcqBHXHChiTiet> items)
        {
            return _iQtcqBHXHChiTietRepository.AddRange(items);
        }

        public int Update(BhQtcqBHXHChiTiet item)
        {
            return _iQtcqBHXHChiTietRepository.Update(item);
        }

        public int RemoveRange(IEnumerable<BhQtcqBHXHChiTiet> items)
        {
            return _iQtcqBHXHChiTietRepository.RemoveRange(items);
        }

        public int UpdateRange(IEnumerable<BhQtcqBHXHChiTiet> items)
        {
            return _iQtcqBHXHChiTietRepository.UpdateRange(items);
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary, string sMaDonVi)
        {
            _iQtcqBHXHChiTietRepository.CreateVoudcherSummary(idChungTu, nguoiTao, namLamViec, idChungTuSummary, sMaDonVi);
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> GetChiTietQuyetToanChiQuyBHXH(Guid idChungTu, string sSLNS, int iNamLamViec, string iIDMaDonVi, bool isPhanBo, DateTime? dNgayChungTu)
        {
            return _iQtcqBHXHChiTietRepository.GetChiTietQuyetToanChiQuyBHXH(idChungTu, sSLNS, iNamLamViec, iIDMaDonVi, isPhanBo, dNgayChungTu);
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> BaoCaoQuyetToanChiQuyBHXH(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoQuyetToanChiQuyBHXH(iNamLamViec, idMaDonVi, sLNS, isTongHop, iQuy, donViTinh);
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDau(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapOmDau(iNamLamViec, idMaDonVi, sLNS, isTongHop, iQuy, donViTinh);
        }
        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSan(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapThaiSan(iNamLamViec, idMaDonVi, sLNS, isTongHop, iQuy, donViTinh);
        }

        public IEnumerable<BhQtcqBHXHChiTietQuery> FindSoTienQuyetToanDaDuocDuyetTheoQuy(string idMaDonVi, string sLNS, int namLamViec, int quyChungTu)
        {
            return _iQtcqBHXHChiTietRepository.FindSoTienQuyetToanDaDuocDuyetTheoQuy(idMaDonVi, sLNS, namLamViec, quyChungTu);
        }

        public void CreateVoudcherForQuaterBefore(BhQtcqBHXH entity)
        {
            _iQtcqBHXHChiTietRepository.CreateVoudcherForQuaterBefore(entity);
        }

        public List<ReportBHQTCQBHXHThongTriQuery> GetDataThongTriForDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.GetDataThongTriForDonVi(yearOfWork, quy, donVi, principal, iLoaiChungTu, donViTinh);
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            return _iQtcqBHXHChiTietRepository.ExistVoucherDetail(voucherID);
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindAllVouchers()
        {
            return _iQtcqBHXHChiTietRepository.FindAll();
        }

        public IEnumerable<BhQtcqBHXHChiTiet> FindByVoucherID(Guid voucherID)
        {
            return _iQtcqBHXHChiTietRepository.FindByVoucherID(voucherID);
        }

        public List<BhQtcqGiaiThichTroCapQuery> ExportDanhSachNguoiLaoDongNghiViec(int yearOfWork, int donViTinh, int quy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachNguoiLaoDongNghiViec(yearOfWork, donViTinh, quy, donVi);
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNan(int yearOfWork, int donViTinh, int quy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapTaiNan(yearOfWork, donViTinh, quy, donVi);
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanTruyLinh(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapTaiNanTruyLinh(yearOfWork, donViTinh, iQuy, donVi);
        }
        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTT(yearOfWork, donViTinh, iQuy, donVi);
        }

        public List<BhQtcqBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu)
        {
            return _iQtcqBHXHChiTietRepository.GetTienPhanBoChiTietDuToanChi(iNamLamViec, sMaLoaiChi, id, sMaDonVi, sLNS, dNgayChungTu);
        }

        public List<BhQtcqBHXHChiTietQuery> GetDataSummaryBefore(BhQtcqBHXH entity)
        {
            return _iQtcqBHXHChiTietRepository.GetDataSummaryBefore(entity);
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoi(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapTaiNanNNTheoKhoi(yearOfWork, donViTinh, iQuy, donVi);
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoi(int yearOfWork, string donVi, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapThaiSanFollowKhoi(yearOfWork, donVi, iQuy, donViTinh);
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTDetailXN(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTTDetailXN(yearOfWork, donViTinh, iQuy, donVi);
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoi(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTTKhoi(yearOfWork, donViTinh, iQuy, donVi);
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiDetailXN(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiDetailXN(yearOfWork, donViTinh, iQuy, donVi);
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoi(int yearOfWork, string donVi, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapOmDauKhoi(yearOfWork, donVi, lNS_9010001_9010002, isTongHop, iQuy, donViTinh);
        }
        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapOmDauKhoiNhomDT(int yearOfWork, string donVi, string lNS_9010001_9010002, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapOmDauKhoiNhomDT(yearOfWork, donVi, lNS_9010001_9010002, isTongHop, iQuy, donViTinh);
        }

        public IEnumerable<BhBaoCaoQuyetToanChiQuyQuery> BaoCaoGiaiThichTroCapThaiSanFollowKhoiNhomDT(int yearOfWork, string donVi, int iQuy, int donViTinh)
        {
            return _iQtcqBHXHChiTietRepository.BaoCaoGiaiThichTroCapThaiSanFollowKhoiNhomDT(yearOfWork, donVi, iQuy, donViTinh);
        }

        public List<BhQtcqGiaiThichTroCapTaiNanQuery> ExportDanhSachHuongTroCapTaiNanNNTheoKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapTaiNanNNTheoKhoiNhomDT(yearOfWork, donViTinh, iQuy, donVi);
    
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDTDetail(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDTDetail(yearOfWork, donViTinh, iQuy, donVi);
        }

        public List<BhQtcqGiaiThichTroCapHTPVXNTVTTQuery> ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDT(int yearOfWork, int donViTinh, int iQuy, string donVi)
        {
            return _iQtcqBHXHChiTietRepository.ExportDanhSachHuongTroCapHTPVXNTVTTKhoiNhomDT(yearOfWork, donViTinh, iQuy, donVi);
        }
    }
}
