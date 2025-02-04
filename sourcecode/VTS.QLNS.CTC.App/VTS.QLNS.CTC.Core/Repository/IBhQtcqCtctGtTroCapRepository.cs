using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IBhQtcqCtctGtTroCapRepository : IRepository<BhQtcqCtctGtTroCap>
    {
        List<BhQtcqCtctGtTroCapQuery> GetDataExplainSubtracts(int yearWork, string idQTCQuyCheDoBHXH, string sXauNoiMa);
        bool IsExistExplain(Guid voucherQTCQID);
        IEnumerable<BhQtcqCtctGtTroCap> FindByVoucherID(Guid voucherID);
        IEnumerable<BhSalaryDataQuery> FindSalaryDataByXauNoiMaAnQuarte(string sXauNoiMa, string sQuarte, string sMaDonVi, int namLamViec,string sLNS);
        int DeleteDupItem(Guid voucherID);
        IEnumerable<BhQtcqBHXHChiTiet> FindDataBhxhByIdQtcqAndXauNoiMa(Guid voucherID, string sXauNoiMa, int iNamLamViec);
        List<BhQtcqCtctGtTroCap> FindByForIdChungTu(Guid id);
        List<BhQtcqCtctGtTroCap> FindExplainSubtracts(int namLamViec);
    }
}
