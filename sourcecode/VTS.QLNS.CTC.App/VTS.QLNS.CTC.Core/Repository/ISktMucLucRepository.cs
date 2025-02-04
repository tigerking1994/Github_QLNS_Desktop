using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktMucLucRepository : IRepository<NsSktMucLuc>
    {
        IEnumerable<SktMucLucQuery> FindByCondition(int namLamViec, int loai, string idDonVi, int loaiChungTu);
        IEnumerable<NsSktMucLuc> FindByKyHieu(int namLamViec, List<string> kyHieu);
        IEnumerable<NsSktMucLuc> FindByNganh(int namLamViec, List<string> nganh);
        IEnumerable<NsSktMucLuc> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<NsSktMucLuc> FindAllOld(AuthenticationInfo authenticationInfo);
        IEnumerable<NsSktMucLuc> FindAllNew(AuthenticationInfo authenticationInfo);
        int CountSktMucLuc(int namLamViec);
        void UpdateBHangCha(IEnumerable<Guid> sktMucLucs, int namLamViec);
        void UpdateBHangChaToFalse(IEnumerable<Guid> sktMucLucs, int namLamViec);
        bool IsUsedMLSKT(Guid iidMlskt, int namLamViec);
        IEnumerable<SktMucLucDtQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, string idDonVi, int loaiChungTu, string chungTuId);
        IEnumerable<SktMucLucDtQuery> FindByConditionBVTC(int namLamViec, int namNganSach, int nguonNganSach, string loai, string idDonVi, int loaiChungTu, int? iLoaiNNS, string chungTuId);
        IEnumerable<NsMlsktMlns> FindAllMapMlsktMlns(int namLamViec);
        void DeleteSktByNamLamViec(IEnumerable<int> NamLamViecs);
        int CountNsMlsktMlns(int namLamViec);
        void RevertAllMLSKT(int namLamViec);
        void UpdateNSMlsktMlnsMapping();
        void UpdateSKTChungTuChiTiet(Guid voucherID);
    }
}