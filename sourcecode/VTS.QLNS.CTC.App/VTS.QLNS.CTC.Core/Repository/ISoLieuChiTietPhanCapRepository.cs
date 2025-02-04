using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISoLieuChiTietPhanCapRepository : IRepository<NsDtdauNamPhanCap>
    {
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDTDN(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet, Guid iID_CTDTDauNam, string XauNoiMaGoc);
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        IEnumerable<SoLieuChiTietPhanCapQuery> GetSoLieuChiTietPhanCapDonVi0_1(int namLamViec, Guid iID_CTDTDauNam);
        NsDtdauNamPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec);
        IEnumerable<NsDtdauNamPhanCap> FindDonViTongHop(string idDonVi, string mlnsId, int namLamViec);
        void DeleteByVoucherId(Guid voucherId);
    }
}
