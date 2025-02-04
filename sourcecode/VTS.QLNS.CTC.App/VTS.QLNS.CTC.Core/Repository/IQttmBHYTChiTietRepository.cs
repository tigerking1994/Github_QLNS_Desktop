using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQttmBHYTChiTietRepository : IRepository<BhQttmBHYTChiTiet>
    {
        IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailById(BhQttmBHYTChiTietCriteria searchCondition);
        IEnumerable<BhQttmBHYTChiTiet> FindVoucherDetailByCondition(BhQttmBHYTChiTietCriteria searchCondition);
        IEnumerable<string> GetLnsHasData(List<Guid> chungTuIds);
        void AddAggregateVoucherDetail(BhQttmBHYTChiTietCriteria creation);
        BhQttmBHYTChiTiet FindById(Guid id);
        bool ExistVoucherDetail(Guid voucherID);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanNLD(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhan(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytThanNhanTongHop(int namLamViec, bool isTongHop, string lstDonvi, string thanNhanQuanNhan, string thanNhanCNVQP, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSV(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHop(int iNamLamViec, string sIdDonVi, int donViTinh, bool isTongHop, int selectedQuarter);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQuyetToanThuMuaBhytHSSVTongHop(int namLamViec, bool isTongHop, string lstDonvi, string hSSV, string luuHS, string hVSQ, string sQDB, int dvt);
        IEnumerable<BhQttmBHYTChiTietQuery> ExportQTTMBhytThanNhanNLDTongHopDonVi(int iNamLamViec, string sIdDonVis, int donViTinh, int selectedQuarter);
    }
}
