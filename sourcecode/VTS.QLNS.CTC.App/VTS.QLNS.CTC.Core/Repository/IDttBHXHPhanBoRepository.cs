using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IDttBHXHPhanBoRepository : IRepository<BhDtPhanBoChungTu>
    {
        IEnumerable<BhDtPhanBoChungTu> FindByCondition(DuToanThuChungTuCriteria condition);
        int LockOrUnLock(Guid id, bool lockStatus);
        Dictionary<string, string> FindAllDict(int namLamViec, int? loaiDuToan);
        IEnumerable<BhDttChungTuDotNhanQuery> FindChungTuDotNhan(EstimationVoucherCriteria condition, bool isCreate);
        int FindNextSoChungTuIndex(Expression<Func<BhDtPhanBoChungTu, bool>> predicate);
        IEnumerable<BhDttChungTuDotNhanQuery> FindAllChungTuDotNhan(EstimationVoucherCriteria condition);
        IEnumerable<BhDtPhanBoChungTu> FindDotNhanByChungTuPhanBo(Guid idPhanBo);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinh(int year);
        IEnumerable<BhDtPhanBoChungTu> FindBySoQuyetDinh(string soQuyetDinh, int nam);
        IEnumerable<BhDtPhanBoChungTu> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam);        
        IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound);
        IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopDieuChinhChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, bool isMillionRound);
        IEnumerable<BhDuToanThuChiQuery> FindDotDuToan();
        IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopThuDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound);
        IEnumerable<BhDuToanTongHopThuChiQuery> ExportTongHopChiDonVi(int year, string donVis, string soQuyetDinh, int donViTinh, string ngayQuyetDinh, bool isMillionRound);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTT(int year, bool isInTheoChungTu);
        IEnumerable<BhDtPhanBoChungTuQuery> FindBySoQuyetDinhLuyKe(string soQuyetDinh, string ngayQuyetDinh, int nam);
        IEnumerable<BhDtPhanBoChungTu> FindBySoChungTu(string soChungTu, int nam);
        IEnumerable<BhDtPhanBoChungTu> FindByIdNhanDuToan(string id);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTTBHXHBHYTBHTN(int year);
        IEnumerable<BhDuToanThuChiQuery> GetSoQuyetDinhDTCBHXHBHYTBHTN(int year);
        IEnumerable<BhPbdttmBHYTQuery> FindBySoQuyetDinhLuyKeDttmBHYT(string soQuyetDinh, string ngayQuyetDinh, int nam);
        List<string> GetDonViDttDttnDtc(int namLamViec, string soQuyetDinh);
    }
}
