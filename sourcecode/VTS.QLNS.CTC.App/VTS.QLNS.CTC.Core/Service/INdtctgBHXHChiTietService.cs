using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INdtctgBHXHChiTietService
    {
        IEnumerable<BhDtctgBHXHChiTiet> FindByCondition(Guid Id);
        IEnumerable<BhDtctgBHXHChiTietQuery> GetListNhanDuToanChiTrenGiaoChiTiet(Guid idNdtctg, string sLNs, int iNamlamViec, string IIDDonVi, int loaiDotNhanPhanBo);

        int Add(BhDtctgBHXHChiTiet item);
        int Update(BhDtctgBHXHChiTiet item);

        int AddRange(IEnumerable<BhDtctgBHXHChiTiet> lstItems);
        int RemoveRange(IEnumerable<BhDtctgBHXHChiTiet> lstItems);
        int Delete(BhDtctgBHXHChiTiet item);
        IEnumerable<BhDtctgBHXHChiTietQuery> GetBaoCaoChiTieuNganSach(string idDonVi, int iNamLamViec, string sLNS, int dotNhan, string sMaLoaiChi, int donViTinh);
        List<BhDtctgBHXHChiTietQuery> GetListDataAgregateChiTiet(Guid idChungTu, string sLNS, int yearOfWork, string sMaDonVi, Guid? IDLoaiChi);

        bool ExistChungTu(Guid id);
        List<BhDtctgBHXHChiTietQuery> GetListDataAgregateAdjustChiTiet(Guid idChungTu, int namLamViec, string sMaDonVi, DateTime? dNgayChungTu, string sLNS);
        List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXH(string iID_MaDonVi, int namLamViec,DateTime dNgayChungTu);
        List<BhDtctgBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string iID_MaDonVi, int namLamViec);
    }
}
