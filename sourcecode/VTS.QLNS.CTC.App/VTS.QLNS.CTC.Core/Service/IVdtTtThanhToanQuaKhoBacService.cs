using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtTtThanhToanQuaKhoBacService
    {
        IEnumerable<VdtTtThanhToanQuaKhoBacQuery> GetDataIndex();
        void DeleteThanhToanKhoBac(Guid iId);
        void Insert(VdtTtThanhToanQuaKhoBac data, string sUserLogin);
        void Update(VdtTtThanhToanQuaKhoBac data, string sUserLogin);
        IEnumerable<DuAnDeNghiThanhToanQuery> GetDuAnByThanhToanKhoBac(int iNamKeHoach, DateTime dNgayQuyetDinh, string sLNS, string iIdMaDonViQuanLy);
        IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetail(int iNamKeHoach, DateTime dNgayQuyetDinh, Guid iIdLoaiNguonVon, string iIdMaDonViQuanLy);
        IEnumerable<ThanhToanQuaKhoBacChiTietQuery> GetThanhToanKhoBacDetailByParentId(Guid iIdParentId);
        bool InsertDetail(Guid iIdParentId, List<VdtTtThanhToanQuaKhoBacChiTiet> lstData);
    }
}
