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
    public class PbdttmBHYTChiTietService : IPbdttmBHYTChiTietService
    {
        private readonly IPbdttmBHYTChiTietRepository  _iPbdttmBHYTChiTietRepository;
        public PbdttmBHYTChiTietService(IPbdttmBHYTChiTietRepository iPbdttmBHYTChiTietRepository)
        {
            _iPbdttmBHYTChiTietRepository = iPbdttmBHYTChiTietRepository;
        }

        public IEnumerable<BhPbdttmBHYTChiTiet> FindByCondition(Expression<Func<BhPbdttmBHYTChiTiet, bool>> predicate)
        {
            return _iPbdttmBHYTChiTietRepository.FindByCondition(predicate);
        }

        public int Add(BhPbdttmBHYTChiTiet item)
        {
            return _iPbdttmBHYTChiTietRepository.Add(item);
        }
        public int Update(BhPbdttmBHYTChiTiet item)
        {
            return _iPbdttmBHYTChiTietRepository.Update(item);
        }

        public int Delete(BhPbdttmBHYTChiTiet item)
        {
            return _iPbdttmBHYTChiTietRepository.Delete(item);
        }

        public int AddRange(List<BhPbdttmBHYTChiTiet> items)
        {
            return _iPbdttmBHYTChiTietRepository.AddRange(items);
        }
        public int RemoveRange(List<BhPbdttmBHYTChiTiet> items)
        {
            return _iPbdttmBHYTChiTietRepository.RemoveRange(items);
        }

        public IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTiet(Guid chungTuId, string sLNS, string sIDDonVi, int iNamLamViec, string userName)
        {
            return _iPbdttmBHYTChiTietRepository.FindChungTuChiTiet(chungTuId, sLNS, sIDDonVi, iNamLamViec, userName);
        }

        public IEnumerable<BhPbdttmBHYTChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuId, string sLNS, int iNamLamViec, string userName)
        {
            return _iPbdttmBHYTChiTietRepository.FindChungTuChiTietDieuChinh(chungTuId, sLNS, iNamLamViec, userName);
        }
        public IEnumerable<BhPbdttmBHYTChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec)
        {
            return _iPbdttmBHYTChiTietRepository.ExportExcelPhanBoDuToanChi(chungTuId, sLNS, iNamLamViec);
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytThanNhan(int namLamViec, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, string smDuToan, string smHachToan, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound)
        {
            return _iPbdttmBHYTChiTietRepository.ExportDuToanThuBhytThanNhan(namLamViec, lstDonvi, thanNhanQuanNhan, thanNhanCNVQP, smDuToan, smHachToan, soQuyetDinh, ngayQuyetDinh, dvt, isMillionRound);
        }

        public IEnumerable<ReportKhtmDuToanBHYTQuery> ExportDuToanThuBhytHSSV(int namLamViec, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDuBi, string soQuyetDinh, string ngayQuyetDinh, int dvt, bool isMillionRound)
        {
            return _iPbdttmBHYTChiTietRepository.ExportDuToanThuBhytHSSV(namLamViec, lstDonvi, hSSV, luuHS, hVSQ, sQDuBi, soQuyetDinh, ngayQuyetDinh, dvt, isMillionRound);
        }

        public IEnumerable<BhPbdttmBHYTChiTiet> FindByXauNoiMaVaSoQuyetDinh(string sSoQuyetDinh, List<string> sLNS, int iNamLamViec, bool isContains = true)
        {
            return _iPbdttmBHYTChiTietRepository.FindByXauNoiMaVaSoQuyetDinh(sSoQuyetDinh, sLNS, iNamLamViec, isContains);
        }
    }
}
