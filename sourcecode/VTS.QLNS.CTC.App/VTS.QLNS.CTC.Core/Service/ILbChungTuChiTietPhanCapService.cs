using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ILbChungTuChiTietPhanCapService
    {
        IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        int AddRange(IEnumerable<NsNganhChungTuChiTietPhanCap> entities);
        int Add(NsNganhChungTuChiTietPhanCap entity);
        NsNganhChungTuChiTietPhanCap Find(params object[] keyValues);
        int Update(NsNganhChungTuChiTietPhanCap entity);
        int Delete(NsNganhChungTuChiTietPhanCap entity);
        NsNganhChungTuChiTietPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec);
        IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCapExport(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        List<NsNganhChungTuChiTietPhanCap> FindByChiTietId(string chitietId, string idDonVi, int namLamViec);
        void DeleteByNganhChiTiet(Guid idNganhChiTiet);
    }
}
