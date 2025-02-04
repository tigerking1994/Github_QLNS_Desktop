using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IBhDmMucLucNganSachService
    {
        IEnumerable<BhDmMucLucNganSach> FindAll(int namLamViec);
        IEnumerable<BhDmMucLucNganSach> FindByCondition(Expression<Func<BhDmMucLucNganSach, bool>> predicate);
        List<BhDmMucLucNganSach> GetListBhMucLucNs(int namLamViec, string khoiDuToanBHXH, string khoiHachToanBHXH);
        List<BhDmMucLucNganSach> GetListBhytMucLucNs(int namLamViec, string loaiNS);
        DanhMuc FindMLNSChiTietToi(int namLamViec);
        List<BhDmMucLucNganSach> GetListBhMucLucNsForQLKP(int inamLamViec, string khoi);
        IEnumerable<BhDmMucLucNganSach> FindByListLnsDonVi(string lns, int namLamViec);
        bool IsUsedMLNS(Guid mlnsId, int namLamViec);
        int Update(BhDmMucLucNganSach item);
        IEnumerable<BhDmMucLucNganSach> FindByMLNS(int namLamViec, int status);
        List<BhDmMucLucNganSach> FindByUser(string userName, int yearOfWork, string LNSExcept);
        IEnumerable<BhDmMucLucNganSach> FindForDieuChinh(int namLamViec, string donVi, Guid loaiDanhMucCapChi, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindForDieuChinhDTT(int namLamViec, string donVi, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindAllByYear(int namLamViec);
        List<BhDmMucLucNganSach> GetListMucLucForDanhMucLoaiChi(int namLamViec, string sLNS);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPQL(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName);
        IEnumerable<BhDmMucLucNganSach> FindSLNSForQTCQKPK(int namLamViec, string donVi, int iQuy, DateTime ngayChungTu, string userName, Guid IdLoaiChi);
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
