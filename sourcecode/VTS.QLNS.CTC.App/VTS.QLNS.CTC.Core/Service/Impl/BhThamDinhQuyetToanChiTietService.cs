using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class BhThamDinhQuyetToanChiTietService : IService<BhThamDinhQuyetToanChiTiet>, IBhThamDinhQuyetToanChiTietService
    {
        private readonly IBhThamDinhQuyetToanChiTietRepository _bhThamDinhQuyetToanChiTietRepository;

        public BhThamDinhQuyetToanChiTietService(IBhThamDinhQuyetToanChiTietRepository bhThamDinhQuyetToanChiTietRepository)
        {
            _bhThamDinhQuyetToanChiTietRepository = bhThamDinhQuyetToanChiTietRepository;
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(Guid iIDChungTu, int yearOfWork, string iIDMaDonVi)
        {
            return _bhThamDinhQuyetToanChiTietRepository.FindAll(iIDChungTu, yearOfWork, iIDMaDonVi);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> FindAll(int yearOfWork, string iIDMaDonVi, int dvt)
        {
            return _bhThamDinhQuyetToanChiTietRepository.FindAll(yearOfWork, iIDMaDonVi, dvt);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTTBYTQuery> GetChiKinhPhiTTBYT(int yearOfWork, string iIDMaDonVi,  int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.GetChiKinhPhiTTBYT(yearOfWork, iIDMaDonVi, donViTinh);
        }

        public IEnumerable<BhThamDinhQuyetToanChiCheDoBHXHQuery> GetChiKinhPhiCheDoBHXH(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.GetChiKinhPhiCheDoBHXH(yearOfWork, iIDMaDonVi, donViTinh);
        }

        public IEnumerable<BhThamDinhQuyetToanChiKCBHSSVNLDQuery> GetChiKinhPhiCSSKHSSVNLD(int yearOfWork, string iIDMaDonVi, int iLoai, int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.GetChiKinhPhiCSSKHSSVNLD(yearOfWork, iIDMaDonVi, iLoai, donViTinh);
        }

        public IEnumerable<BhThamDinhQuyetToanChiKCBQuanYDonViQuery> GetChiKinhPhiKCBQuanYDonVi(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.GetChiKinhPhiKCBQuanYDonVi(yearOfWork, iIDMaDonVi, donViTinh);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTiet> FindAll(Expression<Func<BhThamDinhQuyetToanChiTiet, bool>> predicate)
        {
            return _bhThamDinhQuyetToanChiTietRepository.FindAll(predicate);
        }
        public int AddRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities)
        {
            return _bhThamDinhQuyetToanChiTietRepository.AddRange(entities);
        }

        public int UpdateRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities)
        {
            return _bhThamDinhQuyetToanChiTietRepository.UpdateRange(entities);
        }

        public int RemoveRange(IEnumerable<BhThamDinhQuyetToanChiTiet> entities)
        {
            return _bhThamDinhQuyetToanChiTietRepository.RemoveRange(entities);
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
            _bhThamDinhQuyetToanChiTietRepository.CreateVoudcherSummary(idChungTu, nguoiTao, namLamViec, idChungTuSummary);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, int dvt, bool isBHXH)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportQuyetToanThuBhxhBhtn(namLamViec, lstDonvi, dvt, isBHXH);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhyt(int namLamViec, string lstDonvi, int dvt)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportQuyetToanThuBhyt(namLamViec, lstDonvi, dvt);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhytQuanNhan(int namLamViec, string lstDonvi, int dvt)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportQuyetToanThuBhytQuanNhan(namLamViec, lstDonvi, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytThanNhan(int namLamViec, string lstDonvi, int dvt)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportQuyetToanThuBhytThanNhan(namLamViec, lstDonvi, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuBhytHssvHvqs(int namLamViec, string lstDonvi, int dvt)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportQuyetToanThuBhytHssvHvqs(namLamViec, lstDonvi, dvt);
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportThongBaoPheDuyetThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, int type)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportThongBaoPheDuyetThuChi(iNamLamViec, sIdDonVis, donViTinh, type);
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int iNamLamViec, string sIdDonVis, int donViTinh, bool isTongHop)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportTongHopQuyetToanThuChi(iNamLamViec, sIdDonVis, donViTinh, isTongHop);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportDuToanKinhPhiChuyenNamSau(int iNamLamViec, string sIdDonVis, int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportDuToanKinhPhiChuyenNamSau(iNamLamViec, sIdDonVis, donViTinh);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTiet> FindAllOfLastYear(int yearOfWork, string iIDMaDonVi)
        {
            return _bhThamDinhQuyetToanChiTietRepository.FindAllOfLastYear(yearOfWork, iIDMaDonVi);
        }

        public IEnumerable<BhThamDinhQuyetToanChiTietQuery> ExportCanCuTrichQuyBhxhSangBhyt(int yearOfWork, string iIDMaDonVi, int donViTinh)
        {
            return _bhThamDinhQuyetToanChiTietRepository.ExportCanCuTrichQuyBhxhSangBhyt(yearOfWork, iIDMaDonVi, donViTinh);
        }
    }
}
