using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtTtThanhToanQuaKhoBacChiTietRepository : IRepository<VdtTtThanhToanQuaKhoBacChiTiet>
    {
        IEnumerable<VdtTtThanhToanQuaKhoBacChiTiet> GetDetailDataByParentId(Guid iId);
        void DeleteDetailData(IEnumerable<VdtTtThanhToanQuaKhoBacChiTiet> lstData);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByThanhToanKhoBac(int iNamKeHoach, DateTime dNgayQuyetDinh, string sLNS, string iIdMaDonViQuanLy);
        IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetail(int iNamKeHoach, DateTime dNgayQuyetDinh, Guid iIdLoaiNguonVon, string iIdMaDonViQuanLy);
        IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetailByParentId(Guid iIdParentId);
    }
}
