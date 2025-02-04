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
using static VTS.QLNS.CTC.Utility.Enum.BaoHiemDuToanTypeEnum;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QtcqKCBChiTietService : IQtcqKCBChiTietService
    {
        private readonly IQtcqKCBChiTietRepository _iQtcqKCBChiTietRepository;
        public QtcqKCBChiTietService(IQtcqKCBChiTietRepository iQtcqKCBChiTietRepository)
        {
            _iQtcqKCBChiTietRepository = iQtcqKCBChiTietRepository;
        }
        public IEnumerable<BhQtcqKCBChiTiet> FindByCondition(Expression<Func<BhQtcqKCBChiTiet, bool>> predicate)
        {
            return _iQtcqKCBChiTietRepository.FindByCondition(predicate);
        }

        public int AddRange(IEnumerable<BhQtcqKCBChiTiet> items)
        {
            return _iQtcqKCBChiTietRepository.AddRange(items);
        }

        public int Update(BhQtcqKCBChiTiet item)
        {
            return _iQtcqKCBChiTietRepository.Update(item);
        }

        public int RemoveRange(IEnumerable<BhQtcqKCBChiTiet> items)
        {
            return _iQtcqKCBChiTietRepository.RemoveRange(items);
        }

        public int UpdateRange(IEnumerable<BhQtcqKCBChiTiet> items)
        {
            return _iQtcqKCBChiTietRepository.UpdateRange(items);
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary, string sMaDonVi)
        {
            _iQtcqKCBChiTietRepository.CreateVoudcherSummary(idChungTu, nguoiTao, namLamViec, idChungTuSummary, sMaDonVi);
        }
        public IEnumerable<BhQtcqKCBChiTietQuery> GetChiTietQuyetToanChiQuyKCB(Guid idChungTu, Guid idLoaiChi, string sLNS, string sMaLoaiChi, string sMaDonVi, DateTime dNgayChungTu, int iQuy, int iNamLamViec, int loai)
        {
            return _iQtcqKCBChiTietRepository.GetChiTietQuyetToanChiQuyKCB(idChungTu, idLoaiChi, sLNS, sMaLoaiChi, sMaDonVi, dNgayChungTu, iQuy, iNamLamViec, loai);
        }

        public IEnumerable<BhQtcqKCBChiTietQuery> BaoCaoKCBQuanYDonVi(int iNamLamViec, string idMaDonVi, string sLNS, bool isTongHop, int iQuy, int donViTinh)
        {
            return _iQtcqKCBChiTietRepository.BaoCaoKCBQuanYDonVi(iNamLamViec, idMaDonVi, sLNS, isTongHop, iQuy, donViTinh);
        }

        public void CreateVoudcherForQuaterBefore(BhQtcqKCB entity)
        {
            _iQtcqKCBChiTietRepository.CreateVoudcherForQuaterBefore(entity);
        }

        public List<ReportBHQTCQKCBThongTriQuery> GetDataThongTriDonVi(int yearOfWork, string quy, string donVi, string principal, int iLoaiChungTu, int donViTinh)
        {
            return _iQtcqKCBChiTietRepository.GetDataThongTriDonVi(yearOfWork, quy, donVi, principal, iLoaiChungTu, donViTinh);
        }

        public List<BhQtcqKCBChiTietQuery> GetDataTienDuToanPhanBoChi(int namChungTu, string sDSLNS, string idMaDonVi, DateTime dNgayChungTu, Guid idLoaiChi, int quyChungTu)
        {
            return _iQtcqKCBChiTietRepository.GetDataTienDuToanPhanBoChi(namChungTu, sDSLNS, idMaDonVi, dNgayChungTu, idLoaiChi, quyChungTu);
        }

        public bool ExitChungTuChiTiet(Guid id)
        {
            return _iQtcqKCBChiTietRepository.ExitChungTuChiTiet(id);
        }

        public List<ReportBhQtcQKCBTongHopChi> BaoCaoKCBQuanYDonViTongHopChi(int yearOfWork, int donViTinh, string lstIdDonVi, int iQuy)
        {
            return _iQtcqKCBChiTietRepository.BaoCaoKCBQuanYDonViTongHopChi(yearOfWork, donViTinh, lstIdDonVi, iQuy);
        }
    }
}
