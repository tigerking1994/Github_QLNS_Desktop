using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhQtcqCtctGtTroCapService
    {
        void AddRange(List<BhQtcqCtctGtTroCap> listGiaiThichLuongTrus);
        void Delete(Guid id);
        BhQtcqCtctGtTroCap FindById(Guid id);
        List<BhQtcqCtctGtTroCap> FindByForIdChungTu(Guid id);
        List<BhQtcqCtctGtTroCapQuery> GetDataExplainSubtracts(int yearWork, string idQTCQuyCheDoBHXH, string sXauNoiMa);
        void Update(BhQtcqCtctGtTroCap giaiThichLuongTru);
        bool IsExistExplain(Guid voucherQTCQID);
        IEnumerable<BhQtcqCtctGtTroCap> FindByVoucherID(Guid voucherID);
        int RemoveRange(IEnumerable<BhQtcqCtctGtTroCap> items);
        IEnumerable<BhSalaryDataQuery> FindSalaryDataByXauNoiMaAnQuarte(string sXauNoiMa, string sQuarte, string sMaDonVi, int namLamViec, string sLNS);
        int DeleteDupItem(Guid voucherID);
        IEnumerable<BhQtcqBHXHChiTiet> FindDataBhxhByIdQtcqAndXauNoiMa(Guid voucherID, string sXauNoiMa, int iNamLamViec);

        List<BhQtcqCtctGtTroCap> FindExplainSubtracts(int namLamViec);

    }
}
