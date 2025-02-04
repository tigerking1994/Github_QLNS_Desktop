using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface ISktSoLieuChungTuService
    {
        NsDtdauNamChungTu Add(NsDtdauNamChungTu entity);
        NsDtdauNamChungTu Find(params object[] keyValues);
        int Update(NsDtdauNamChungTu entity);
        int Delete(Guid id);
        public IEnumerable<NsDtdauNamChungTu> FindByCondition(Expression<Func<NsDtdauNamChungTu, bool>> predicate);
        void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int loaiNNS, int nguonNganSach);
        void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, string chungTuId);
        void UpdateChildChungTu(int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, string chungTuId);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach, int loaiChungTu);
        List<NsDtdauNamChungTu> GetDataExportJson(List<Guid> lstId);
        void BulkInsertNsDtdauNamChungTu(List<NsDtdauNamChungTu> lstData);
        void DeleteCtdnctByDeleteMlns(Guid iID_CTDTDauNam, string sMLNS, int iNamLamViec);
    }
}
