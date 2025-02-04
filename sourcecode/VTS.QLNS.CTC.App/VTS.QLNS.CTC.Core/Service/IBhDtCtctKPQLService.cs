using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDtCtctKPQLService
    {
        void Add(BhDtCtctKPQL entity);
        void Delete(Guid id);
        void Update(BhDtCtctKPQL entity);
        IEnumerable<BhDtCtctKPQLQuery> FindIndex(int iNamChungTu, Guid? iDChungTuChiTiet, Guid? iDChungTu, string sMaDonVi);
        int AddRange(IEnumerable<BhDtCtctKPQL> lstItems);
        List<BhDtCtctKPQLQuery> FindDuToanTrenGiaoKPQL(Guid? iDChungTu, string sMaDonVi, int NamLamViec);
        List<BhDtCtctKPQLQuery> FindPhanBoDuToanTrenGiaoKPQL(Guid id, string sMaDonVi, int yearOfWork);
        IEnumerable<BhDtCtctKPQL> FindByCondition(Guid id);
        int RemoveRange(IEnumerable<BhDtCtctKPQL> lstItems);
        IEnumerable<ReportBhDtCtctKPQLQuery> ExportBaoCaoChiTietDonViKQPL(string maDonVi, int yearOfWork, Guid id, int donViTinh,bool IsMillionRound);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCKPQL(int year);
    }
}
