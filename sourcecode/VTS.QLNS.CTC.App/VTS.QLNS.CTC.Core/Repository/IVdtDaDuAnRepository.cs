using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtDaDuAnRepository : IRepository<VdtDaDuAn>
    {
        IEnumerable<DeNghiQuyetToanQuery> FindAllDeNghiQuyetToan(int namLamViec, string userName);
        IEnumerable<VdtDaDuAn> FindByIdDonViQuanLy(string idDonVi);
        IEnumerable<VdtDaDuAnQuery> FindByIdDonViAndNgayQuyetDinh(string idDonVi, DateTime ngayQuyetDinh);
        IEnumerable<VdtDaDuAnQuery> FindByIdDonVi(string idDonVi);
        List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHan();
        IEnumerable<VdtDaDuAnReportQuery> FindDuAnInfoByIdDonVi(string idDonVi);
        IEnumerable<NganSachDuAnInfoQuery> FindNganSachDuAnInfoByIdDuAn(string idDuAn);
        IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAn(string idDuAn, DateTime ngayBaoCao);
        IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAnV1(string idDuAn, DateTime ngayBaoCao);
        int FindNextSoChungTuIndex();
        VdtDaDuAn FindByMaDuAn(string sMaDuAn);
        IEnumerable<VdtDaDuAn> FindByIdDuAnKhthDeXuat(Guid id);
        void CreateDuAn(string lstId);
        List<VdtDaDuAn> FindByChuDauTuId(Guid chuDauTuId);
        List<VdtDaDuAn> FindByChuDauTuByMaChuDauTu(string maChuDauTu);
        List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHanDeXuat(string iIdDuAn, int type);
        IEnumerable<VdtDaDuAn> GetDuAnInQuyetToanDuAnHoanThanh(string iIdMaDonViQuanLy, Guid iIdQuyetToanId);
        IEnumerable<VdtDaDuAn> FindByDonvi(string maDonVi);
        IEnumerable<VdtDaDuAn> FindDuanCreatedKHLCNT(string maDonVi);
    }
}
