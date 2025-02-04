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
    public class CptuBHYTChiTietService : ICptuBHYTChiTietService
    {
        private readonly ICptuBHYTChiTietRepository _iCptuBHYTChiTietRepository;
        public CptuBHYTChiTietService(ICptuBHYTChiTietRepository iCptuBHYTChiTietRepository)
        {
            _iCptuBHYTChiTietRepository = iCptuBHYTChiTietRepository;
        }

        public IEnumerable<BhCptuBHYTChiTiet> FindByCondition(Expression<Func<BhCptuBHYTChiTiet, bool>> predicate)
        {
            return _iCptuBHYTChiTietRepository.FindByCondition(predicate);
        }

        public int Add(BhCptuBHYTChiTiet item)
        {
            return _iCptuBHYTChiTietRepository.Add(item);
        }

        public int UpdateRange(IEnumerable<BhCptuBHYTChiTiet> items)
        {
            return _iCptuBHYTChiTietRepository.UpdateRange(items);
        }

        public int AddRange(IEnumerable<BhCptuBHYTChiTiet> items)
        {
            return _iCptuBHYTChiTietRepository.AddRange(items);
        }

        public int RemoveRange(IEnumerable<BhCptuBHYTChiTiet> items)
        {
            return _iCptuBHYTChiTietRepository.RemoveRange(items);
        }

        public void CreateVoudcherSummary(string idChungTu, string nguoiTao, int namLamViec, string idChungTuSummary)
        {
             _iCptuBHYTChiTietRepository.CreateVoudcherSummary(idChungTu, nguoiTao, namLamViec, idChungTuSummary);
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> FinChungTuChiTiet(Guid idChungTu, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName)
        {
            return _iCptuBHYTChiTietRepository.FinChungTuChiTiet(idChungTu, sLNS, idCsYTe, iNamLamViec, iQuyKyTruoc, iNamKyTruoc, userName);
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> FindChungTuImport(int iQuy, string sLNS, string idCsYTe, int iNamLamViec, int iQuyKyTruoc, int iNamKyTruoc, string userName)
        {
            return _iCptuBHYTChiTietRepository.FindChungTuImport(iQuy, sLNS, idCsYTe, iNamLamViec, iQuyKyTruoc, iNamKyTruoc, userName);
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportKeHoachCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion)
        {
            return _iCptuBHYTChiTietRepository.ExportKeHoachCapPhatTamUngKCBBHYT(sIdCsYTe, iLoai, iQuy, iNamLamViec, userName, donViTinh, sLns, isRoundMillion);
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportTongHopCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, int iQuy, int iNamLamViec, string userName, int donViTinh, string sLns, bool isRoundMillion)
        {
            return _iCptuBHYTChiTietRepository.ExportTongHopCapPhatTamUngKCBBHYT(sIdCsYTe, iLoai, iQuy, iNamLamViec, userName, donViTinh, sLns, isRoundMillion);
        }

        public IEnumerable<BhCptuBHYTChiTietQuery> ExportThongTriCapPhatTamUngKCBBHYT(string sIdCsYTe, int iLoai, string sLns, int iQuy, int iNamLamViec, string userName, int donViTinh, bool isRoundMillion)
        {
            return _iCptuBHYTChiTietRepository.ExportThongTriCapPhatTamUngKCBBHYT(sIdCsYTe, iLoai, sLns,iQuy, iNamLamViec, userName, donViTinh, isRoundMillion);
        }

        public IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTChiTiet(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe)
        {
            return _iCptuBHYTChiTietRepository.GetDataReportQuyetToanKPKCBBHYTChiTiet(quy, namlamviec, lns, donvitinh, idMaCoSoYTe);
        }

        public IEnumerable<ReportQuyetToanKCBBHYTQuery> GetDataReportQuyetToanKPKCBBHYTTongHop(int quy, int namlamviec, string lns, int donvitinh, string idMaCoSoYTe)
        {
            return _iCptuBHYTChiTietRepository.GetDataReportQuyetToanKPKCBBHYTTongHop(quy, namlamviec, lns, donvitinh, idMaCoSoYTe);
        }

        public IEnumerable<BhCptuBHYTChiTiet> FindChungTuChiTietByChungTuId(BhCpTUChungTuChiTietCriteria searchModel)
        {
            return _iCptuBHYTChiTietRepository.FindChungTuChiTietByChungTuId(searchModel);
        }
    }
}
