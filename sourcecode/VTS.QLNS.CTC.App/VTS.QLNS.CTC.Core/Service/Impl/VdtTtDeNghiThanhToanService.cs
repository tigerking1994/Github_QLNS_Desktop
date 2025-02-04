using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtTtDeNghiThanhToanService : IVdtTtDeNghiThanhToanService
    {
        private readonly IVdtTtDeNghiThanhToanRepository _vdtTtDeNghiThanhToanRepository;
        private readonly IVdtTtPheDuyetThanhToanChiTietRepository _vdtTtPheDuyetThanhToanChiTietRepository;
        private readonly IVdtTtDeNghiThanhToanChiTietRepository _vdtTtDeNghiThanhToanChiTietRepository;

        public VdtTtDeNghiThanhToanService(
            IVdtTtDeNghiThanhToanRepository vdtTtDeNghiThanhToanRepository,
            IVdtTtPheDuyetThanhToanChiTietRepository vdtTtPheDuyetThanhToanChiTietRepository,
            IVdtTtDeNghiThanhToanChiTietRepository vdtTtDeNghiThanhToanChiTietRepository)
        {
            _vdtTtDeNghiThanhToanRepository = vdtTtDeNghiThanhToanRepository;
            _vdtTtPheDuyetThanhToanChiTietRepository = vdtTtPheDuyetThanhToanChiTietRepository;
            _vdtTtDeNghiThanhToanChiTietRepository = vdtTtDeNghiThanhToanChiTietRepository;
        }

        public IEnumerable<VdtTtDeNghiThanhToanQuery> GetDataDeNghiThanhToanIndex(int namLamViec, string userName)
        {
            return _vdtTtDeNghiThanhToanRepository.GetDataDeNghiThanhToanIndex(namLamViec, userName);
        }

        public IEnumerable<VdtTtDeNghiThanhToanChiPhiQuery> GetChiPhiInDenghiThanhToanScreen(DateTime dNgayDeNghi, Guid iIdDuAnId)
        {
            return _vdtTtDeNghiThanhToanRepository.GetChiPhiInDenghiThanhToanScreen(dNgayDeNghi, iIdDuAnId);
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdVon, string type, DateTime dNgayLap, int iNamKeHoach)
        {
            return _vdtTtDeNghiThanhToanRepository.GetDuAnByDeNghiThanhToan(iIdDonViQuanLy, iNguonVonId, iIdVon, type, dNgayLap, iNamKeHoach);
        }

        public IEnumerable<DuAnDeNghiThanhToanQuery> GetDetailDuAnByDeNghiThanhToan(string iIdDonViQuanLy, int iNguonVonId, Guid iIdLoaiNguonVon, DateTime dNgayLap, int iNamKeHoach)
        {
            return _vdtTtDeNghiThanhToanRepository.GetDetailDuAnByDeNghiThanhToan(iIdDonViQuanLy, iNguonVonId, iIdLoaiNguonVon, dNgayLap, iNamKeHoach);
        }

        public bool Insert(VdtTtDeNghiThanhToan dataInsert, string sUserLogin)
        {
            dataInsert.Id = Guid.NewGuid();
            dataInsert.DDateCreate = DateTime.Now;
            dataInsert.SUserCreate = sUserLogin;
            return _vdtTtDeNghiThanhToanRepository.Add(dataInsert) == 1;
        }

        public bool InsertRange(List<VdtTtDeNghiThanhToan> datas, string sUserLogin)
        {
            datas = datas.Select(n => { n.SUserCreate = sUserLogin; n.DDateCreate = DateTime.Now; return n; }).ToList();
            return _vdtTtDeNghiThanhToanRepository.AddRange(datas) > 0;
        }

        public bool Update(VdtTtDeNghiThanhToan dataUpdate, string sUserLogin)
        {
            if (dataUpdate.Id == Guid.Empty) return false;
            var data = _vdtTtDeNghiThanhToanRepository.Find(dataUpdate.Id);
            data.DDateUpdate = DateTime.Now;
            data.SUserUpdate = sUserLogin;
            data.SSoDeNghi = dataUpdate.SSoDeNghi;
            return _vdtTtDeNghiThanhToanRepository.Update(data) == 1;
        }

        public bool DeleteDeNghiThanhToan(VdtTtDeNghiThanhToan data, string sUserLogin)
        {
            if (data.Id == Guid.Empty) return false;
            var objDataDelete = _vdtTtDeNghiThanhToanRepository.Find(data.Id);
            _vdtTtPheDuyetThanhToanChiTietRepository.DeletePheDuyetThanhToanChiTietByParentId(data.Id);
            //var lstDenghiThanhToanChiTiet = _vdtTtDeNghiThanhToanChiTietRepository.DeleteByThanhToanId(data.Id);
            // update de nghi thanh toan con
            var children = _vdtTtDeNghiThanhToanRepository.FindAll(t => data.Id.Equals(t.ParentId)).ToList();
            foreach (var c in children)
            {
                c.ParentId = null;
            }
            _vdtTtDeNghiThanhToanRepository.UpdateRange(children);
            return _vdtTtDeNghiThanhToanRepository.Delete(objDataDelete) == 1;
        }

        public VdtTtDeNghiThanhToan Find(params object[] keyValues)
        {
            return _vdtTtDeNghiThanhToanRepository.Find(keyValues);
        }

        public int Update(VdtTtDeNghiThanhToan entity)
        {
            return _vdtTtDeNghiThanhToanRepository.Update(entity);
        }

        public void LoadGiaTriThanhToan(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, int? loaiKeHoachVon)
        {
            _vdtTtDeNghiThanhToanRepository.LoadGiaTriThanhToan(iCoQuanThanhToan, ngayDeNghi, bThanhToanTheoHopDong, iIdChungTu, nguonVonId, namKeHoach, thanhToanId, loaiCoQuanTaiChinh, ref thanhToanTN, ref thanhToanNN, ref tamUngTN, ref tamUngNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, loaiKeHoachVon);
        }
        
        public void LoadGiaTriThanhToanBaoCao(int iCoQuanThanhToan, DateTime ngayDeNghi, bool bThanhToanTheoHopDong, string iIdChungTu, int nguonVonId, int namKeHoach, Guid? thanhToanId, int? loaiCoQuanTaiChinh, ref double thanhToanTN, ref double thanhToanNN, ref double tamUngTN, ref double tamUngNN, ref double luyKeTUUngTruocTN, ref double luyKeTUUngTruocNN, ref double sumTN, ref double sumNN, ref double sumThuHoiTN, ref double sumThuHoiNN, int? loaiKeHoachVon)
        {
            _vdtTtDeNghiThanhToanRepository.LoadGiaTriThanhToanBaoCao(iCoQuanThanhToan, ngayDeNghi, bThanhToanTheoHopDong, iIdChungTu, nguonVonId, namKeHoach, thanhToanId, loaiCoQuanTaiChinh, ref thanhToanTN, ref thanhToanNN, ref tamUngTN, ref tamUngNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, ref sumThuHoiTN, ref sumThuHoiNN, loaiKeHoachVon);
        }

        public DeNghiThanhToanValueQuery LoadGiaTriPheDuyetThanhToanByParentId(Guid iIdPheDuyetThanhToanId)
        {
            return _vdtTtDeNghiThanhToanRepository.LoadGiaTriPheDuyetThanhToanByParentId(iIdPheDuyetThanhToanId);
        }

        public bool CheckExistSoQuyetDinh(Guid id, string soQuyetDinh)
        {
            return _vdtTtDeNghiThanhToanRepository.CheckExistSoQuyetDinh(id, soQuyetDinh);
        }

        public List<CapPhatThanhToanReportQuery> GetDataReport(string id, int namLamViec, int donViTinh)
        {
            return _vdtTtDeNghiThanhToanRepository.GetDataReport(id, namLamViec, donViTinh );
        }

        public VdtTtDeNghiThanhToan FindByHopDongId(Guid hopdongId)
        {
            return _vdtTtDeNghiThanhToanRepository.FindByHopDongId(hopdongId);
        }

        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _vdtTtDeNghiThanhToanRepository.LockOrUnLock(id, lockStatus);
        }

        public void UpdateThongTriThanhToan(Guid iIdThongTriId, List<Guid> lstThanhToanId)
        {
            _vdtTtDeNghiThanhToanRepository.UpdateThongTriThanhToan(iIdThongTriId, lstThanhToanId);
        }

        public void TongHopDeNghiThanhToan(VdtTtDeNghiThanhToan vdtTtDeNghiThanhToan, List<Guid> childrenIds)
        {
            _vdtTtDeNghiThanhToanRepository.TongHopDeNghiThanhToan(vdtTtDeNghiThanhToan, childrenIds);
        }

        public List<VdtTtDeNghiThanhToan> FindDeNghiTongHop()
        {
            return _vdtTtDeNghiThanhToanRepository.FindDeNghiTongHop();
        }

        public VdtTtDeNghiThanhToan getLastRowBySoDeNghi(int? namLamViec)
        {
            return _vdtTtDeNghiThanhToanRepository.FindLastRowBySoDeNghi(namLamViec);
        }

        public List<VdtTtDeNghiThanhToanQuery> getListDeNghiThanhToanByThongtriId(Guid? thongTriId, int namLamViec, string userName)
        {
            return _vdtTtDeNghiThanhToanRepository.getListDeNghiThanhToanByThongtriId(thongTriId, namLamViec, userName);
        }

        public IEnumerable<VdtTtDeNghiThanhToan> FindByCondition(Expression<Func<VdtTtDeNghiThanhToan, bool>> predicate)
        {
            return _vdtTtDeNghiThanhToanRepository.FindAll(predicate);
        }
    }
}
