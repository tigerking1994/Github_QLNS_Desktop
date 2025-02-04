using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface INhDaDuAnService
    {
        void AddRange(IEnumerable<NhDaDuAn> data);
        void Add(NhDaDuAn entity);
        void Delete(NhDaDuAn entity);
        void Update(NhDaDuAn entity);
		NhDaDuAn FindById(Guid id);
        IEnumerable<NhDaDuAn> FindAll();
        IEnumerable<NhDaDuAn> FindAll(Expression<Func<NhDaDuAn, bool>> predicate);
        IEnumerable<NhDaDuAnQuery> FindIndex(int iLoai);
        IEnumerable<NhDaDuAnQuery> FindFromChuTruongDauTu(int yearOfWork, string maDonVi, Guid? chuTruongDauTuId = null);
        IEnumerable<NhDaDuAnQuery> FindFromQdDauTu(int yearOfWork, string maDonVi, int iLoai, Guid? qdDauTuId = null);
        IEnumerable<NhDaDuAnQuery> FindFromDuToan(int yearOfWork, string maDonVi, Guid? qdDauTuId = null);
        IEnumerable<NhDaDuAnTrongNuocQuery> FindDuAnTrongNuoc();
        IEnumerable<NhDaDuAnTinhHinhDuAnQuery> GetInfoDuAnTinhHinhDuAnReport(int yearOfWork, string maDonVi);
        IEnumerable<NganSachNhDuAnInfoQuery> FindNganSachNgoaiHoiDuAnInfoByIdDuAn(string idDuAn);
        IEnumerable<NhBaoCaoTinhHinhThucHienDuAnQuery> GetDataReportTinhHinhThucHienDuAn(string idDuAn, DateTime? ngayBatDau, DateTime? ngayKetThuc, string idHopDong, string idKHTongThe); 
        IEnumerable<ReportNHTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria searchCondition);
        IEnumerable<NhDaDuAn> FindAllDuAnByQDDT();
        void AddOrUpdateRange(IEnumerable<NhDaDuAn> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaDuAnExportCTCQuery> GetDuAnExportCTC(int iLoai);
    }
}
