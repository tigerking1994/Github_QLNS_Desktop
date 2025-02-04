using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DttBHXHPhanBoChiTietService : IDttBHXHPhanBoChiTietService
    {
        private readonly IDttBHXHPhanBoChiTietRepository _dtChungTuChiTietRepository;

        public DttBHXHPhanBoChiTietService(IDttBHXHPhanBoChiTietRepository dtChungTuChiTietRepository)
        {
            _dtChungTuChiTietRepository = dtChungTuChiTietRepository;
        }

        public int AddRange(IEnumerable<BhDtPhanBoChungTuChiTiet> entities)
        {
            return _dtChungTuChiTietRepository.AddRange(entities);
        }

        public void DeleteByIdChungTu(Guid id)
        {
            _dtChungTuChiTietRepository.DeleteByIdChungTu(id);
        }

        public void DeleteByIdChungTuDuToanNhan(Guid id, string idDuToanNhan)
        {
            _dtChungTuChiTietRepository.DeleteByIdChungTuDuToanNhan(id, idDuToanNhan);
        }

        public void DeleteByIds(IEnumerable<string> ids)
        {
            _dtChungTuChiTietRepository.DeleteByIds(ids);
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHXH(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi)
        {
            return _dtChungTuChiTietRepository.ExportKhtDuToanBHXH(namLamViec, lstDonvi, khoiDuToan, khoiHachToan, soQuyetDinh, ngayQuyetDinh, dvt, isMillionRound, IsKhoi);
        }

        public IEnumerable<ReportKhtDuToanBHXHBHYTBHTNQuery> ExportKhtDuToanBHXHBHYTBHTN(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound)
        {
            return _dtChungTuChiTietRepository.ExportKhtDuToanBHXHBHYTBHTN(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, isMillionRound);
        }

        public IEnumerable<ReportKhtDuToanBHXHQuery> ExportKhtDuToanBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, string sm, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound, bool IsKhoi)
        {
            return _dtChungTuChiTietRepository.ExportKhtDuToanBHYT(namLamViec, lstDonvi, khoiDuToan, khoiHachToan, sm, soQuyetDinh, ngayQuyetDinh, dvt, isMillionRound, IsKhoi);
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportTongHopDuToanThuChi(int iNamLamViec, string sIdDonVis, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound)
        {
            return _dtChungTuChiTietRepository.ExportTongHopDuToanThuChi(iNamLamViec, sIdDonVis, soQuyetDinh, ngayQuyetDinh, donViTinh, isMillionRound);
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindByCondition(DuToanThuChungTuChiTietCriteria searchCondition)
        {
            return _dtChungTuChiTietRepository.FindByCondition(searchCondition);
        }

        public IEnumerable<BhDtPhanBoChungTuChiTiet> FindByCondition(Expression<Func<BhDtPhanBoChungTuChiTiet, bool>> predicate)
        {
            return _dtChungTuChiTietRepository.FindByCondition(predicate);
        }

        public BhDtPhanBoChungTuChiTiet FindById(Guid id)
        {
            return _dtChungTuChiTietRepository.Find(id);
        }

        public IEnumerable<BhDtPhanBoChungTuChiTiet> FindByIdChungTu(string idChungTu)
        {
            return _dtChungTuChiTietRepository.FindByIdChungTu(idChungTu);
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTiet(Guid chungTuPhanBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName)
        {
            return _dtChungTuChiTietRepository.FindChungTuChiTiet(chungTuPhanBoId, sLNS, sIdDonVi, iNamLamViec, userName);
        }

        public IEnumerable<BhDtPhanBoChungTuChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName)
        {
            return _dtChungTuChiTietRepository.FindChungTuChiTietDieuChinh(chungTuPhanBoId, sLNS, iNamLamViec, userName);
        }

        public bool isExistEstimate(Guid id, Guid estimateId)
        {
            return _dtChungTuChiTietRepository.IsExistEstimate(id, estimateId);
        }

        public int RemoveRange(IEnumerable<BhDtPhanBoChungTuChiTiet> entities)
        {
            return _dtChungTuChiTietRepository.RemoveRange(entities);
        }

        public int Update(BhDtPhanBoChungTuChiTiet entity)
        {
            return _dtChungTuChiTietRepository.Update(entity);
        }
    }
}
