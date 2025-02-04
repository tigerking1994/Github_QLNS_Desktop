using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtDaDuAnService : IVdtDaDuAnService
    {
        private readonly IVdtDaDuAnRepository _iVdtDaDuAnRepository;

        public VdtDaDuAnService(IVdtDaDuAnRepository iVdtDaDuAnRepository)
        {
            _iVdtDaDuAnRepository = iVdtDaDuAnRepository;
        }

        public VdtDaDuAn Find(params object[] keyValues)
        {
            return _iVdtDaDuAnRepository.Find(keyValues);
        }

        public IEnumerable<DeNghiQuyetToanQuery> FindAllDeNghiQuyetToan(int namLamViec, string userName)
        {
            return _iVdtDaDuAnRepository.FindAllDeNghiQuyetToan(namLamViec, userName);
        }

        public IEnumerable<VdtDaDuAnQuery> FindByIdDonViAndNgayQuyetDinh(string idDonVi, DateTime ngayQuyetDinh)
        {
            return _iVdtDaDuAnRepository.FindByIdDonViAndNgayQuyetDinh(idDonVi, ngayQuyetDinh);
        }

        public IEnumerable<VdtDaDuAnQuery> FindByIdDonVi(string idDonVi)
        {
            return _iVdtDaDuAnRepository.FindByIdDonVi(idDonVi);
        }

        public IEnumerable<VdtDaDuAn> FindByIdDonViQuanLy(string idDonVi)
        {
            return _iVdtDaDuAnRepository.FindByIdDonViQuanLy(idDonVi);
        }

        public List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHan()
        {
            return _iVdtDaDuAnRepository.GetDuAnChooseInKeHoachTrungHan();
        }

        public void Insert(List<VdtDaDuAn> lstData)
        {
            _iVdtDaDuAnRepository.AddRange(lstData);
        }

        public IEnumerable<VdtDaDuAnReportQuery> FindDuAnInfoByIdDonVi(string idDonVi)
        {
            return _iVdtDaDuAnRepository.FindDuAnInfoByIdDonVi(idDonVi);
        }

        public IEnumerable<NganSachDuAnInfoQuery> FindNganSachDuAnInfoByIdDuAn(string idDuAn)
        {
            return _iVdtDaDuAnRepository.FindNganSachDuAnInfoByIdDuAn(idDuAn);
        }

        public IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAn(string idDuAn, DateTime ngayBaoCao)
        {
            return _iVdtDaDuAnRepository.GetDataReportTinhHinhDuAn(idDuAn, ngayBaoCao);
        }

        public IEnumerable<ReportTinhHinhDuAnQuery> GetDataReportTinhHinhDuAnV1(string idDuAn, DateTime ngayBaoCao)
        {
            return _iVdtDaDuAnRepository.GetDataReportTinhHinhDuAnV1(idDuAn, ngayBaoCao);
        }

        public int Update(VdtDaDuAn entity)
        {
            return _iVdtDaDuAnRepository.Update(entity);
        }

        public int FindNextSoChungTuIndex()
        {
            return _iVdtDaDuAnRepository.FindNextSoChungTuIndex();
        }

        public VdtDaDuAn FindById(Guid id)
        {
            return _iVdtDaDuAnRepository.Find(id);
        }

        public VdtDaDuAn FindByMaDuAn(string sMaDuAn)
        {
            return _iVdtDaDuAnRepository.FindByMaDuAn(sMaDuAn);
        }

        public IEnumerable<VdtDaDuAn> FindAll()
        {
            return _iVdtDaDuAnRepository.FindAll();
        }

        public IEnumerable<VdtDaDuAn> FindByIdDuAnKhthDeXuat(Guid id)
        {
            return _iVdtDaDuAnRepository.FindByIdDuAnKhthDeXuat(id);
        }

        public VdtDaDuAn Add(VdtDaDuAn entity)
        {
            _iVdtDaDuAnRepository.Add(entity);
            return entity;
        }

        public List<VdtDaDuAn> FindByChuDauTuId(Guid chuDauTuId)
        {
            return _iVdtDaDuAnRepository.FindByChuDauTuId(chuDauTuId);
        }

        public List<VdtDaDuAn> FindByChuDauTuByMaChuDauTu(string maChuDauTu)
        {
            return _iVdtDaDuAnRepository.FindByChuDauTuByMaChuDauTu(maChuDauTu);
        }

        public List<DuAnKeHoachTrungHanQuery> GetDuAnChooseInKeHoachTrungHanDeXuat(string iIdDuAn, int type)
        {
            return _iVdtDaDuAnRepository.GetDuAnChooseInKeHoachTrungHanDeXuat(iIdDuAn, type);
        }

        public IEnumerable<VdtDaDuAn> GetDuAnInQuyetToanDuAnHoanThanh(string iIdMaDonViQuanLy, Guid iIdQuyetToanId)
        {
            return _iVdtDaDuAnRepository.GetDuAnInQuyetToanDuAnHoanThanh(iIdMaDonViQuanLy, iIdQuyetToanId);
        }

        public IEnumerable<VdtDaDuAn> FindByMaDonVi(string maDonVi)
        {
            return _iVdtDaDuAnRepository.FindByDonvi(maDonVi);
        }
        
        public IEnumerable<VdtDaDuAn> FindDuanCreatedKHLCNT(string maDonVi)
        {
            return _iVdtDaDuAnRepository.FindDuanCreatedKHLCNT(maDonVi);
        }
    }
}
