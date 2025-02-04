using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public interface IVdtDaDuAnService
    {
        IEnumerable<DeNghiQuyetToanQuery> FindAllDeNghiQuyetToan(int namLamViec, string userName);
        IEnumerable<VdtDaDuAn> FindByIdDonViQuanLy(string idDonVi);
        IEnumerable<VdtDaDuAnQuery> FindByIdDonViAndNgayQuyetDinh(string idDonVi, DateTime ngayQuyetDinh);
        VdtDaDuAn Find(params object[] keyValues);
        IEnumerable<VdtDaDuAnQuery> FindByIdDonVi(string idDonVi);
        List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHan();
        List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHanDeXuat(string iIdDuAn, int type);
        void Insert(List<VdtDaDuAn> lstData);
        IEnumerable<VdtDaDuAnReportQuery> FindDuAnInfoByIdDonVi(string idDonVi);
        IEnumerable<NganSachDuAnInfoQuery> FindNganSachDuAnInfoByIdDuAn(string idDuAn);
        IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAn(string idDuAn, DateTime ngayBaoCao);
        IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAnV1(string idDuAn, DateTime ngayBaoCao);
        int Update(VdtDaDuAn entity);
        int FindNextSoChungTuIndex();
        VdtDaDuAn FindById(Guid id);
        VdtDaDuAn FindByMaDuAn(string sMaDuAn);
        IEnumerable<VdtDaDuAn> FindAll();
        List<VdtDaDuAn> FindByChuDauTuId(Guid chuDauTuId);
        List<VdtDaDuAn> FindByChuDauTuByMaChuDauTu(string maChuDauTu);
        IEnumerable<VdtDaDuAn> FindByIdDuAnKhthDeXuat(Guid id);
        VdtDaDuAn Add(VdtDaDuAn entity);
        IEnumerable<VdtDaDuAn> GetDuAnInQuyetToanDuAnHoanThanh(string iIdMaDonViQuanLy, Guid iIdQuyetToanId);
        IEnumerable<VdtDaDuAn> FindByMaDonVi(string maDonVi);
        IEnumerable<VdtDaDuAn> FindDuanCreatedKHLCNT(string maDonVi);
    }
}
