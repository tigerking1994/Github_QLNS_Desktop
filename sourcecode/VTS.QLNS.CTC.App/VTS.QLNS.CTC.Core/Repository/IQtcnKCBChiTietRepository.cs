using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IQtcnKCBChiTietRepository : IRepository<BhQtcnKCBChiTiet>
    {
        IEnumerable<BhQtcnKCBChiTiet> FindByCondition(Expression<Func<BhQtcnKCBChiTiet, bool>> predicate);
        IEnumerable<BhQtcnKCBChiTietQuery> GetChiTietQuyetToanChiNamKCB(Guid idChungTu, string sLNS, int iNamLamViec, bool isTongHop4Quy, string maDonVi, bool loai);
        void CreateVoudcherSummary(string idChungTu, string idMaDonVi, string nguoiTao, int namLamViec, string idChungTuSummary);
        void CreateChungTuChiTietTheoQuy(Guid idChungTu, string idMaDonVi, int iNamLamViec, string user, bool isTongHop);
        IEnumerable<BhQtcnKCBChiTietQuery> ExportBaoCaoQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop);
        IEnumerable<BhQtcnKCBChiTietQuery> ExportPhuLucQuyetToanKhamChuaBenhTaiQuanYDonVi(int iNamLamViec, string sIdDonVi, string sLns, int donViTinh, bool isTongHop);
        bool ExistVoucherDetail(Guid id, int namLamViec);
        List<BhQtcnKCBChiTietQuery> GetTienPhanBoChiTietDuToanChi(int iNamLamViec, string sMaLoaiChi, Guid id, string idMaDonVi, string sDSLNS, DateTime dNgayChungTu);
    }
}
