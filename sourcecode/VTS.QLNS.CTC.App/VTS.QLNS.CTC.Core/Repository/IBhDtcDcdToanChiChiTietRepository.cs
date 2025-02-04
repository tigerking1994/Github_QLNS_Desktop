using System.Collections.Generic;
using System;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDtcDcdToanChiChiTietRepository : IRepository<BhDtcDcdToanChiChiTiet>
    {
        void AddAggregate(BhDtcDcdToanChiChiTietCriteria creation);
        bool ExistDTChiTiet(Guid bhxhId);
        IEnumerable<BhDtcDcdToanChiChiTietQuery> FindByConditionForChildUnit(BhDtcDcdToanChiChiTietCriteria searchCondition);
        IEnumerable<BhDtcDcdToanChiChiTiet> FindByIdChiTiet(Guid id);
        IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition);
        IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgency(BhDtcDcdToanChiChiTietCriteria searchCondition);
        IEnumerable<BhDtcDcdToanChiChiTietQuery> GetDataAggregateForAgencyKPQLKCBQYKCBTSKPK(BhDtcDcdToanChiChiTietCriteria searchCondition);
        List<BhDtcDcdToanChiChiTietQuery> ExportDataChiTiet(BhDtcDcdToanChiChiTietCriteria searchCondition);
        List<BhDtcDcdToanChiChiTietQuery> GetAdjustData(int namLamViec, string vouchers, string sLNS, string sMaLoaiChi, string sID_MaDonVi);
        List<BhDtcDcdToanChiChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int? namLamViec, DateTime dNgayChungTu,int iLoaiTongHop);
        IEnumerable<BhPbdtcBHXHChiTietQuery> FindPbDtChiChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, DateTime? ngayChungTu, string sLNS);
        IEnumerable<BhDtctgBHXHChiTietQuery> FindNPBChiChiTiet(BhDtcDcdToanChiChiTietCriteria chiTietCriteria);
        List<BhDtcDcdToanChiChiTietQuery> FindData6ThangDauNamChiTiet(int namLamViec, string idDonVi, Guid loaiDanhMucCapChi, string sMaLoaiChi, string sLNS, DateTime? dNgayChungTu);
        IEnumerable<BhDtcDcdToanChiChiTietQuery> GetSettlementData(int namLamViec, string idDonVi, int quy, string sLns);
    }
}
