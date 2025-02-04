using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhDmMucLucNganSachRepository : IRepository<BhDmMucLucNganSach>
    {
        IEnumerable<BhDmMucLucNganSach> FindAll(AuthenticationInfo authenticationInfo);
        List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, string khoiDuToanBHXH, string khoiHachToanBHXH);
        List<BhDmMucLucNganSach> GetListBhMucLucNsForQLKP(int inamLamViec, string khoi);
        List<BhDmMucLucNganSach> GetListBhytMucLucNs(int namLamViec, string loaiNS);
        IEnumerable<BhDmMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec);
        IEnumerable<BhDmMucLucNganSachQuery> FindByMLNSNamLamViec(int namLamViec);
        int AddOrUpdateRange(IEnumerable<BhDmMucLucNganSach> entities, int iNamLamViec);
        bool IsUsedMLNS(Guid mlnsId, int namLamViec);
        IEnumerable<ReportMLNSQuery> ReportMLNS(int namLamViec, Guid mlnsId);
        IEnumerable<BhDmMucLucNganSach> FindForDieuChinh(int namLamViec, string donVi, Guid loaiDanhMucCapChi, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindForDieuChinhDTT(int namLamViec, string donVi, DateTime ngayChungTu, string userName);
        List<BhDmMucLucNganSach> GetListMucLucForDanhMucLoaiChi(int namLamViec, string sLNS);
        IEnumerable<BhDmMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNSExcept);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPQL(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPK(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName, Guid idLoaiChi);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQBHXH(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKCB(int yearOfWork, string agencyIds, int iQuy, DateTime dt, string principal);
        List<BhDmMucLucNganSach> FindSLNSForQTCNBHXH(int yearOfWork, string agencyIds, DateTime dtime, string principal);
        List<BhDmMucLucNganSach> FindSLNSForQTCNKCB(int yearOfWork, string agencyIds, DateTime dtime, string principal);
        List<BhDmMucLucNganSach> FindSLNSForQTCNKPQL(int yearOfWork, string agencyIds, DateTime dtime, string principal);
        List<BhDmMucLucNganSach> FindSLNSForQTCNKPK(int yearOfWork, string agencyIds, DateTime dt, string principal, Guid idLoaiChi);
        List<BhDmMucLucNganSachQuery> GetMLNSCheDoBHXH(int yearOfWork);
        List<BhDmMucLucNganSach> GetByLnsDieuChinhDuToan(int yearOfWork, string sLNS);
    }
}
