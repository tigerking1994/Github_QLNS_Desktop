using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QttBHXHChiTietService : IQttBHXHChiTietService
    {
        private readonly IQttBHXHChiTietRepository _repository;
        public QttBHXHChiTietService(IQttBHXHChiTietRepository iQttBHXHChiTietRepository)
        {
            _repository = iQttBHXHChiTietRepository;
        }

        public void AddAggregateVoucherDetail(BhQttBHXHChiTietCriteria creation)
        {
            _repository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<BhQttBHXHChiTiet> chungTuChiTiets)
        {
            return _repository.AddRange(chungTuChiTiets);
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            return _repository.ExistVoucherDetail(voucherID);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopDonViQuy(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopQuy(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBhxhBhtn(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop)
        {
            return _repository.ExportQuyetToanThuBhxhBhtn(namLamViec, lstDonvi, khoiDuToan, khoiHachToan, dvt, isTongHop);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuBHYT(int namLamViec, string lstDonvi, string khoiDuToan, string khoiHachToan, int dvt, bool isTongHop, string sm)
        {
            return _repository.ExportQuyetToanThuBHYT(namLamViec, lstDonvi, khoiDuToan, khoiHachToan, dvt, isTongHop, sm);
        }
        public IEnumerable<BhQttBHXHChiTietQuery> ExportTongHopQuyetToanThuChi(int namLamViec, string lstDonvi, int dvt, bool isTongHop)
        {
            return _repository.ExportTongHopQuyetToanThuChi(namLamViec, lstDonvi, dvt, isTongHop);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            return _repository.ExportQuyetToanThuNopBhxhBhytBhtnNam(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnQuy(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQuyetToanThuNopBhxhBhytBhtnQuy(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected)
        {
            return _repository.ExportQuyetToanThuNopBhxhBhytBhtnTheoThoiGian(iNamLamViec, sIdDonVi, donViTinh, lstMonthSelected);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopTheoThoiGian(int iNamLamViec, string sIdDonVi, int donViTinh, string lstMonthSelected)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopTheoThoiGian(iNamLamViec, sIdDonVi, donViTinh, lstMonthSelected);
        }
        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViTheoThoiGian(int iNamLamViec, string sIdDonVis, int donViTinh, string lstMonthSelected)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViTheoThoiGian(iNamLamViec, sIdDonVis, donViTinh, lstMonthSelected);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopTongHopNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            return _repository.ExportQuyetToanThuNopTongHopNam(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter);
        }

        public IEnumerable<BhQttBHXHChiTiet> FindAllVouchers()
        {
            return _repository.FindAll();
        }

        public IEnumerable<BhQttBHXHChiTiet> FindByCondition(Expression<Func<BhQttBHXHChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public BhQttBHXHChiTiet FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailByCondition(BhQttBHXHChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailByCondition(searchModel);
        }
        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailsByCondition(BhQttBHXHChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailsByCondition(searchModel);
        }
        public List<string> FindListDonViExistSettlement(Guid id, int iNamLamViec, string userName, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindListDonViExistSettlement(id, iNamLamViec, userName, selectedQuarter, loaiQuy);
        }
        public List<string> FindChiTietDonViThangQuy(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindChiTietDonViThangQuy(iNamLamViec, loaiTongHop, bDaTongHop, selectedQuarter, loaiQuy);
        }
        public List<string> FindListChiTietDonViByListMonth(int iNamLamViec, int loaiTongHop, bool bDaTongHop, string selectedQuarterList)
        {
            return _repository.FindListChiTietDonViByListMonth(iNamLamViec, loaiTongHop, bDaTongHop, selectedQuarterList);
        }
        public List<string> FindChiTietDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindChiTietDonVi(iNamLamViec, loaiTongHop, bDaTongHop, selectedQuarter, loaiQuy);
        }
        public List<string> FindChiTietDonViTongHopThangQuy(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindChiTietDonViTongHopThangQuy(iNamLamViec, loaiTongHop, userName, selectedQuarter, loaiQuy);
        }
        public List<string> FindChiTietDonViTongHop(int iNamLamViec, int loaiTongHop, string userName, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindChiTietDonViTongHop(iNamLamViec, loaiTongHop, userName, selectedQuarter, loaiQuy);
        }
        public List<string> FindAllDonVi(int iNamLamViec, int loaiTongHop, bool bDaTongHop, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindAllDonVi(iNamLamViec, loaiTongHop, bDaTongHop, selectedQuarter, loaiQuy);
        }
        public IEnumerable<BhQttBHXHChiTiet> FindDetailsQT(string idDonVi, int iNamLamViec, int iNamNganSach, int iNguonNganSach, int selectedQuarter, int loaiQuy)
        {
            return _repository.FindDetailsQT(idDonVi, iNamLamViec, iNamNganSach, iNguonNganSach, selectedQuarter, loaiQuy);
        }
        public IEnumerable<BhQttBHXHChiTiet> FindVoucherDetailById(BhQttBHXHChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailById(searchModel);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetDataTongHopSoSanhNam(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            return _repository.GetDataTongHopSoSanhNam(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter);
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _repository.GetLnsHasData(chungTuIds);
        }

        public int RemoveRange(IEnumerable<BhQttBHXHChiTiet> chungTuChiTiets)
        {
            return _repository.RemoveRange(chungTuChiTiets);
        }

        public int Update(BhQttBHXHChiTiet item)
        {
            return _repository.Update(item);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopDonViNam(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter);
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> ExportQTTBhxhBhytBhtnTongHopDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int type)
        {
            return _repository.ExportQTTBhxhBhytBhtnTongHopDonViNam(iNamLamViec, sIdDonVis, donViTinh, type);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViQuy(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isLuyKe, bool isTongHop = false)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViQuy(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter, iLoaiQuy, isLuyKe, isTongHop);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, bool isTongHop = false)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViNam(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter, isTongHop);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetDataLuongCanCu(string maDonVi, int? namLamViec, string months, int loaiQuyNam)
        {
            return _repository.GetDataLuongCanCu(maDonVi, namLamViec, months, loaiQuyNam);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQuyetToanThuNopBhxhBhytBhtnThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQuyetToanThuNopBhxhBhytBhtnThang(iNamLamViec, sIdDonVi, donViTinh, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopThang(int iNamLamViec, string sIdDonVi, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopThang(iNamLamViec, sIdDonVi, donViTinh, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int loaiQuy, bool isLuyKe)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopDonViThang(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter, loaiQuy, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViThang(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter, int iLoaiQuy, bool isTongHop, bool isLuyKe)
        {
            return _repository.ExportQTTNopBhxhBhytBhtnTongHopChiTietDonViThang(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter, iLoaiQuy, isTongHop, isLuyKe);
        }

        public IEnumerable<BhQttBHXHChiTietQuery> GetChiTietChungTuThangQuy(int? iNamLamViec, string sIdDonVis)
        {
            return _repository.GetChiTietChungTuThangQuy(iNamLamViec, sIdDonVis);
        }

        public IEnumerable<BhReportQttBHXHChiTietQuery> FindVoucherDetailsThongTri(BhQttBHXHChiTietCriteria searchCondition)
        {
            return _repository.FindVoucherDetailsThongTri(searchCondition);
        }
    }
}
