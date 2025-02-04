using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktMucLucService
    {
        IEnumerable<NsSktMucLuc> FindByCondition(Expression<Func<NsSktMucLuc, bool>> predicate);
        IEnumerable<SktMucLucQuery> FindByCondition(int namLamViec, int loai, string idDonVi, int loaiChungTu);
        IEnumerable<NsSktMucLuc> FindByKyHieu(int namLamViec, List<string> kyHieu);
        IEnumerable<NsSktMucLuc> FindByNganh(int namLamViec, List<string> nganh);
        int AddRange(IEnumerable<NsSktMucLuc> sktMucLucs);
        int CountSktMucLuc(int namLamViec);
        bool IsUsedMLSKT(Guid iidMlskt, int namLamViec);
        IEnumerable<SktMucLucDtQuery> FindByCondition(int namLamViec, int namNganSach, int nguonNganSach, int loai, string idDonVi, int loaiChungTu, string chungTuId);
        IEnumerable<SktMucLucDtQuery> FindByConditionBVTC(int namLamViec, int namNganSach, int nguonNganSach, string loai, string idDonVi, int loaiChungTu, int? iLoaiNNS, string chungTuId);
        IEnumerable<NsMlsktMlns> FindAllMapMlsktMlns(int namLamViec);
        IEnumerable<NsMlsktMlns> FindByConditionMlsktMlns(Expression<Func<NsMlsktMlns, bool>> predicate);
        int CountNsMlsktMlns(int namLamViec);
        void UpdateNSMlsktMlnsMapping();
    }
}