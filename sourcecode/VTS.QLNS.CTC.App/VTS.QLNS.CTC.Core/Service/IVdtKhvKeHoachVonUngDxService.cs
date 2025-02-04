using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvKeHoachVonUngDxService
    {
        IEnumerable<VdtKhvKeHoachVonUngDxQuery> GetKeHoachVonUngIndex();
        IEnumerable<VdtKhvKeHoachVonUngDx> GetKHVUDeXuatInKHVUDuocDuyet(string iIdMaDonVi, int iNamKeHoach, DateTime dNgayLap);
        bool Insert(VdtKhvKeHoachVonUngDx dataInsert, string sUserLogin);
        bool Update(VdtKhvKeHoachVonUngDx dataUpdate, string sUserLogin);
        bool LogItem(Guid iId, string sUserLogin);
        bool DeleteKeHoachVonUng(VdtKhvKeHoachVonUngDx data);
        IEnumerable<VdtKhvKeHoachVonUngDx> FindAll(Expression<Func<VdtKhvKeHoachVonUngDx, bool>> predicate);
        IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetDuAnInKeHoachVonUngDetail(string iIdDonVi, DateTime dNgayLap, string sTongHop);
        IEnumerable<VdtKhvKeHoachVonUngDxChiTietQuery> GetKeHoachVonUngChiTietByParentId(Guid iIdKeHoachVonUng);
        bool InsertDetail(Guid parentId, List<VdtKhvKeHoachVonUngDxChiTiet> lstChild);
        void InsertKhVonUngDeXuatTongHop(Guid iIdKeHoachTongHop, List<Guid> lstIdChild);
        int Adjust(VdtKhvKeHoachVonUngDx entity, List<VdtKhvKeHoachVonUngDxChiTiet> lstData);
        IEnumerable<VdtKhvKeHoachVonUngDxChiTiet> FindAllCT(Expression<Func<VdtKhvKeHoachVonUngDxChiTiet, bool>> predicate);
        bool CheckTrungSoDeNghi(string sSoDeNghi, Guid id);
        IEnumerable<ExportVonUngDonViQuery> GetKeHoachVonUngDonViExport(List<Guid> lstPhanboVonId);
        bool CheckExistSoKeHoach(string sSoQuyetDinh, int iNamLamViec, Guid? iId);
    }
}
