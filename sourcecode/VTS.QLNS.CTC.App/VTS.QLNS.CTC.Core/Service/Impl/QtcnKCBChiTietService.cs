using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcnKCBChiTietService : IQtcnKCBChiTietService
    {
        private readonly IQtcnKCBChiTietRepository _iQtcnKCBChiTietRepository;
        public QtcnKCBChiTietService(IQtcnKCBChiTietRepository iQtcnKCBChiTietRepository)
        {
            _iQtcnKCBChiTietRepository = iQtcnKCBChiTietRepository;
        }
        public IEnumerable<BhQtcnKCBChiTiet> FindByCondition(Expression<Func<BhQtcnKCBChiTiet, bool>> predicate)
        {
            return _iQtcnKCBChiTietRepository.FindByCondition(predicate);
        }
        public IEnumerable<BhQtcnKCBChiTietQuery> GetChiTietQuyetToanChiNamKCB(Guid idChungTu, string sLNS, int iNamLamViec, bool isTongHop4Quy, string maDonVi, bool loai)
        {
            return _iQtcnKCBChiTietRepository.GetChiTietQuyetToanChiNamKCB(idChungTu, sLNS, iNamLamViec, isTongHop4Quy, maDonVi, loai);
        }

        public int AddRange(IEnumerable<BhQtcnKCBChiTiet> items)
        {
            return _iQtcnKCBChiTietRepository.AddRange(items);
        }

        public int Update(BhQtcnKCBChiTiet item)
        {
            return _iQtcnKCBChiTietRepository.Update(item);
        }

        public int RemoveRange(IEnumerable<BhQtcnKCBChiTiet> items)
        {
            return _iQtcnKCBChiTietRepository.RemoveRange(items);
        }

        public int UpdateRange(IEnumerable<BhQtcnKCBChiTiet> items)
        {
            return _iQtcnKCBChiTietRepository.UpdateRange(items);
        }

        public void CreateVoudcherSummary(string idChungTu, string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            _iQtcnKCBChiTietRepository.CreateVoudcherSummary(idChungTu, idMaDonVi, nguoiTao, namLamViec, idChungTuSummary);
        }

        public void CreateChungTuChiTietTheoQuy(Guid idChungTu, string idMaDonVi, int iNamLamViec, string user, bool isTongHop)
        {
            _iQtcnKCBChiTietRepository.CreateChungTuChiTietTheoQuy(idChungTu, idMaDonVi, iNamLamViec, user, isTongHop);
        }
        public IEnumerable<BhQtcnKCBChiTietQuery> ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            return _iQtcnKCBChiTietRepository.ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(iNamLamViec, sIdDonVi, sLns, donViTinh, isTongHop);
        }
        public IEnumerable<BhQtcnKCBChiTietQuery> ExportPhuLucQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            return _iQtcnKCBChiTietRepository.ExportPhuLucQuyetToanKhamChuaBenhTaiQuanYDonVi(iNamLamViec, sIdDonVi, sLns, donViTinh, isTongHop);
        }

        public bool ExistVoucherDetail(Guid id, int namLamViec)
        {
            return _iQtcnKCBChiTietRepository.ExistVoucherDetail(id, namLamViec);
        }

        public List<BhQtcnKCBChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string idMaDonVi, string sDSLNS, DateTime dNgayChungTu)
        {
            return _iQtcnKCBChiTietRepository.GetTienPhanBoChiTietDuToanChi(iNamLamViec, sMaLoaiChi, id, idMaDonVi, sDSLNS, dNgayChungTu);
        }
    }
}
