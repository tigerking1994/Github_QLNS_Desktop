using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcnBHXHChiTietService : IQtcnBHXHChiTietService
    {
        private readonly IQtcnBHXHChiTietRepository _iQtcnBHXHChiTietRepository;
        public QtcnBHXHChiTietService(IQtcnBHXHChiTietRepository iQtcnBHXHChiTietRepository)
        {
            _iQtcnBHXHChiTietRepository = iQtcnBHXHChiTietRepository;
        }
        public IEnumerable<BhQtcnBHXHChiTiet> FindByCondition(Expression<Func<BhQtcnBHXHChiTiet, bool>> predicate)
        {
            return _iQtcnBHXHChiTietRepository.FindByCondition(predicate);
        }
        public IEnumerable<BhQtcnBHXHChiTietQuery> GetChiTietQuyetToanChiNamBHXH(Guid idChungTu, int iNamLamViec, bool isTongHop4Quy, int iLoaiTongHop, string sMaDonVi)
        {
            return _iQtcnBHXHChiTietRepository.GetChiTietQuyetToanChiNamBHXH(idChungTu, iNamLamViec, isTongHop4Quy, iLoaiTongHop, sMaDonVi);
        }

        public int AddRange(IEnumerable<BhQtcnBHXHChiTiet> items)
        {
            return _iQtcnBHXHChiTietRepository.AddRange(items);
        }

        public int Update(BhQtcnBHXHChiTiet item)
        {
            return _iQtcnBHXHChiTietRepository.Update(item);
        }

        public int RemoveRange(IEnumerable<BhQtcnBHXHChiTiet> items)
        {
            return _iQtcnBHXHChiTietRepository.RemoveRange(items);
        }

        public int UpdateRange(IEnumerable<BhQtcnBHXHChiTiet> items)
        {
            return _iQtcnBHXHChiTietRepository.UpdateRange(items);
        }

        public void CreateVoudcherSummary(string idChungTu,string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            _iQtcnBHXHChiTietRepository.CreateVoudcherSummary(idChungTu, idMaDonVi, nguoiTao, namLamViec, idChungTuSummary);
        }

        public void CreateChungTuChiTietTheoQuy(Guid idChungTu, string idMaDonVi, int iNamLamViec, string user, bool isTongHop)
        {
            _iQtcnBHXHChiTietRepository.CreateChungTuChiTietTheoQuy(idChungTu, idMaDonVi, iNamLamViec, user, isTongHop);
        }

        public IEnumerable<BhQtcnBHXHChiTietQuery> ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            return _iQtcnBHXHChiTietRepository.ExportBaoCaoQuyetToanChiNamCacCheDoBHXH(iNamLamViec, sIdDonVi, sLns, donViTinh, isTongHop);
        }

        public IEnumerable<BhBaoCaoQuyetToanChiNamQuery> ExportQuyetToanChiNamCacCheDoBHXH(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop)
        {
            return _iQtcnBHXHChiTietRepository.ExportQuyetToanChiNamCacCheDoBHXH(iNamLamViec, sIdDonVi, sLns, donViTinh, isTongHop);
        }

        public List<BhQtcnBHXHChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, string sMaDonVi, string sLNS, DateTime dNgayChungTu)
        {
            return _iQtcnBHXHChiTietRepository.GetTienPhanBoChiTietDuToanChi(iNamLamViec, sMaLoaiChi, sMaDonVi, sLNS, dNgayChungTu);
        }

        public bool ExistVoucherDetail(Guid id, int? namLamViec)
        {
            return _iQtcnBHXHChiTietRepository.ExistVoucherDetail(id, namLamViec);
        }

        public void  CreateVoudcherDetailSummary(int iNamLamViec, string sMaLoaiChi, Guid id, string sMaDonVi, string sLNS, DateTime dNgayChungTu,Guid idChungTu)
        {
            _iQtcnBHXHChiTietRepository.CreateVoudcherDetailSummary(iNamLamViec, sMaLoaiChi, id, sMaDonVi, sLNS, dNgayChungTu, idChungTu);
        }
    }
}
