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

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class QttmBHYTChiTietService : IQttmBHYTChiTietService
    {
        private readonly IQttmBHYTChiTietRepository _repository;
        public QttmBHYTChiTietService(IQttmBHYTChiTietRepository iQttmBHYTChiTietRepository)
        {
            _repository = iQttmBHYTChiTietRepository;
        }

        public void AddAggregateVoucherDetail(BhQttmBHYTChiTietCriteria creation)
        {
            _repository.AddAggregateVoucherDetail(creation);
        }

        public int AddRange(IEnumerable<BhQttmBHYTChiTiet> chungTuChiTiets)
        {
            return _repository.AddRange(chungTuChiTiets);
        }

        public bool ExistVoucherDetail(Guid voucherID)
        {
            return _repository.ExistVoucherDetail(voucherID);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHop(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            return _repository.ExportQTTMBhytThanNhanNLDTongHop(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHopDonVi(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter)
        {
            return _repository.ExportQTTMBhytThanNhanNLDTongHopDonVi(iNamLamViec, sIdDonVis, donViTinh, selectedQuarter);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSV(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt)
        {
            return _repository.ExportQuyetToanThuMuaBhytHSSV(namLamViec, isTongHop, lstDonvi, hSSV, luuHS, hVSQ, sQDB, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSVTongHop(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt)
        {
            return _repository.ExportQuyetToanThuMuaBhytHSSVTongHop(namLamViec, isTongHop, lstDonvi, hSSV, luuHS, hVSQ, sQDB, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhan(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt)
        {
            return _repository.ExportQuyetToanThuMuaBhytThanNhan(namLamViec, isTongHop, lstDonvi, thanNhanQuanNhan, thanNhanCNVQP, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanNLD(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter)
        {
            return _repository.ExportQuyetToanThuMuaBhytThanNhanNLD(iNamLamViec, sIdDonVi, donViTinh, isTongHop, selectedQuarter);
        }

        public IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanTongHop(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt)
        {
            return _repository.ExportQuyetToanThuMuaBhytThanNhanTongHop(namLamViec, isTongHop, lstDonvi, thanNhanQuanNhan, thanNhanCNVQP, dvt);
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindAllVouchers()
        {
            return _repository.FindAll();
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindByCondition(Expression<Func<BhQttmBHYTChiTiet, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public BhQttmBHYTChiTiet FindById(Guid id)
        {
            return _repository.FindById(id);
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailByCondition(BhQttmBHYTChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailByCondition(searchModel);
        }

        public IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailById(BhQttmBHYTChiTietCriteria searchModel)
        {
            return _repository.FindVoucherDetailById(searchModel);
        }

        public IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds)
        {
            return _repository.GetLnsHasData(chungTuIds);
        }

        public int RemoveRange(IEnumerable<BhQttmBHYTChiTiet> chungTuChiTiets)
        {
            return _repository.RemoveRange(chungTuChiTiets);
        }

        public int Update(BhQttmBHYTChiTiet item)
        {
            return _repository.Update(item);
        }
    }
}
