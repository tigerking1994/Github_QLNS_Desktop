using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
namespace VTS.QLNS.CTC.Core.Repository
{
    public interface ISktSoLieuChungTuRepository : IRepository<NsDtdauNamChungTu>
    {
        void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int loaiNNS, int nguonNganSach);
        int GetSoChungTuIndexByCondition(int namLamViec, int nguonNganSach, int namNganSach, int loaiChungTu);
        void UpdateTotalChungTu(string idDonVi, string loaiDonVi, int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, int loaiNNS, string chungTuId);
        void UpdateChildChungTu(int loaiChungTu, int namLamViec, int namNganSach, int nguonNganSach, string chungTuId);
        List<NsDtdauNamChungTu> GetDataExportJson(List<Guid> lstId);
        void DeleteCtdnctByDeleteMlns(Guid iID_CTDTDauNam, string sMLNS, int iNamLamViec);
    }
}
