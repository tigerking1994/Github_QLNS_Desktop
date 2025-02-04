using System;
using System.Collections.Generic;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface INhDaDuAnRepository : IRepository<NhDaDuAn>
    {
        IEnumerable<NhDaDuAn> FindAll(AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaDuAn> GetDuAnHaveQDDauTu(Guid iIdKhlcntId, string sMaDonVi);
        IEnumerable<NhDaDuAn> GetDuAnByDonVi(Guid iIdKhlcntId, Guid Id, int iloai);
        IEnumerable<NhDaDuAnQuery> FindIndex(int iLoai);
        IEnumerable<NhDaDuAnQuery> FindFromChuTruongDauTu(int yearOfWork, string maDonVi, Guid? chuTruongDauTuId = null);
        IEnumerable<NhDaDuAnQuery> FindFromQdDauTu(int yearOfWork, string maDonVi, int iLoai, Guid? qdDauTuId = null);
        IEnumerable<NhDaDuAnQuery> FindFromDuToan(int yearOfWork, string maDonVi, Guid? duToanId = null);
        IEnumerable<NhDaDuAnTrongNuocQuery> FindDuAnTrongNuoc();
        IEnumerable<NhDaDuAnTinhHinhDuAnQuery> GetInfoDuAnTinhHinhDuAnReport(int yearOfWork, string maDonVi);
        IEnumerable<NganSachNhDuAnInfoQuery> FindNganSachNgoaiHoiDuAnInfoByIdDuAn(string idDuAn);
        IEnumerable<NhBaoCaoTinhHinhThucHienDuAnQuery> GetDataReportTinhHinhThucHienDuAn(string idDuAn, DateTime? ngayBatDau, DateTime? ngayKetThuc, string idHopDong, string IdKHTongThe);
        IEnumerable<ReportNHTongHopThongTinDuAnQuery> FindByAggregateProjectInformationReport(AggregateProjectInformationReportCriteria searchCondition);
        IEnumerable<NhDaDuAn> FindAllDuAnByQDDT();
        void AddOrUpdateRange(IEnumerable<NhDaDuAn> entities, AuthenticationInfo authenticationInfo);
        IEnumerable<NhDaDuAnExportCTCQuery> GetDuAnExportCTC(int iLoai);
    }
}
