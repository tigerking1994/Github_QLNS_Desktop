using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IQttmBHYTChiTietService
    {
        IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailById(BhQttmBHYTChiTietCriteria searchModel);
        int RemoveRange(IEnumerable<BhQttmBHYTChiTiet> chungTuChiTiets);
        IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailByCondition(BhQttmBHYTChiTietCriteria searchModel);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        IEnumerable<BhQttmBHYTChiTiet> FindByCondition(Expression<Func<BhQttmBHYTChiTiet, bool>> predicate);
        void AddAggregateVoucherDetail(BhQttmBHYTChiTietCriteria creation);
        int AddRange(IEnumerable<BhQttmBHYTChiTiet> chungTuChiTiets);
        BhQttmBHYTChiTiet FindById(Guid id);
        int Update(BhQttmBHYTChiTiet item);
        bool ExistVoucherDetail(Guid voucherID);
        IEnumerable<BhQttmBHYTChiTiet> FindAllVouchers();
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanNLD(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHop(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhan(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanTongHop(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSV(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSVTongHop(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHopDonVi(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter);
    }
}
