using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ILbChungTuChiTietPhanCapRepository : IRepository<NsNganhChungTuChiTietPhanCap>
    {
        IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCap(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        NsNganhChungTuChiTietPhanCap FindByCondition(string idDonVi, string idChungTuChiTiet, int namLamViec);
        IEnumerable<LbChiTietPhanCapQuery> GetSoLieuChiTietPhanCapExport(int namLamViec, string xauNoiMa, string listXauNoiMa, string idChiTiet);
        List<NsNganhChungTuChiTietPhanCap> FindByChiTietId(string chitietId, string idDonVi, int namLamViec);
        void DeleteByNganhChiTiet(Guid idNganhChiTiet);
    }
}

