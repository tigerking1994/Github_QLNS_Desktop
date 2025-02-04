using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISoLieuChiTietPhanCapService
    {
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);

        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDTDN(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet, Guid iID_CTDTDauNam, string XauNoiMaGoc);
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0_1(int namLamViec, Guid iID_CTDTDauNam);
        int AddRange(IEnumerable<NsDtdauNamPhanCap> entities);
        int Add(NsDtdauNamPhanCap entity);
        NsDtdauNamPhanCap Find(params object[] keyValues);
        int Update(NsDtdauNamPhanCap entity);
        int Delete(NsDtdauNamPhanCap entity);
        NsDtdauNamPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec);
        IEnumerable<NsDtdauNamPhanCap> FindDonViTongHop(string idDonVi, string mlnsId, int namLamViec);
        void DeleteByVoucherId(Guid voucherId);
        IEnumerable<NsDtdauNamPhanCap> FindAll();
        int RemoveRange(IEnumerable<NsDtdauNamPhanCap> entities);
        void BulkInsertNsDtdauNamPhanCap(List<NsDtdauNamPhanCap> lstData);
    }
}
