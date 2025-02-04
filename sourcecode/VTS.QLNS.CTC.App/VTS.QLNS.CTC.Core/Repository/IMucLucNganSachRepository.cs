using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IMucLucNganSachRepository : IRepository<NsMucLucNganSach>
    {
        List<NsMucLucNganSach> FindByLNS(string lns);
        IEnumerable<NsMucLucNganSach> FindAllMlnsByLnsAndNamLamViec(List<string> lns, int namLamViec);
        IEnumerable<NsMucLucNganSach> GetLoaiNganSachByNamLamViec(int iNamLamViec);
        IEnumerable<ReportMLNSQuery> ReportMLNS(int namLamViec, Guid mlnsId);
        IEnumerable<NsMucLucNganSach> FindBySktMucLucNotIn(string sktKyHieu, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByBHXHMucLucNotIn(int namLamViec, string mlnsLoai, string mlnsBhxh, string mlnsChosen);
        string FindByBHXHMucLucIn(int namLamViec, string mlnsLoai, string mlnsBhxh);
        int countMLNS(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindLNSStartWith2ByNsDonVi(IEnumerable<string> excludeMLNS, int namLamViec);
        IEnumerable<NsMucLucNganSach> FindAllLnsStartWith2(AuthenticationInfo authenticationInfo);
        void UpdateDonViOfMLNS(IEnumerable<NsMucLucNganSach> entities, string idMaDonVi);
        IEnumerable<NsMucLucNganSach> FindByNamLamViec(int namLamViec);
        IEnumerable<NsMucLucNganSach> FindByNamLamViec(IEnumerable<int> namLamViecs);
        bool IsUsedMLNS(Guid mlnsId, int namLamViec);
        int AddOrUpdateRange(IEnumerable<NsMucLucNganSach> entities, int iNamLamViec);
        void AddRangeWithMLSKT(List<NsMucLucNganSach> listMlns);
        bool IsLNS(NsMucLucNganSach mlns, IEnumerable<NsMucLucNganSach> ListOfMlns);
        IEnumerable<string> GetAllUsedDuToanMlns(int namLamViec);
        IEnumerable<string> GetAllUsedQuyetToanMlns(int namLamViec);
        IEnumerable<string> GetAllUsedMlns(int namLamViec);
        IEnumerable<NsMucLucNganSachQuery> FindByMLNSNamLamViec(int namLamViec);
        List<NsMucLucNganSach> FindAllNotIn(List<string> xnms, int yearOfWork);
        void UpdateIsHangChaMucLucNganSach(List<Guid> lstParentId, int iNamLamViec);
        IEnumerable<MucLucNganSachCheckDataQuery> FindHasDataMLNS(int yearOfWork, string sXauNoiMa, int loai);
        void DeleteHasDataMLNS(string sXauNoiMa, string loai, Guid uniqueidentifier);
        NsMucLucNganSach FindByMLNS(string sXauNoiMa, int yearOfWork);
    }
}
