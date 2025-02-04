using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Repository
{
    public interface IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository : IRepository<VdtKhvPhanBoVonDonViChiTietPheDuyet>
    {
        /// <summary>
        /// Get all list DuAn in PhanBoVonChiTietInsert View
        /// </summary>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdDonViQuanLyId"></param>
        /// <param name="iNguonVonId"></param>
        /// <returns></returns>
        IEnumerable<PhanBoVonDonViChiTietPheDuyetQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId);

        /// <summary>
        /// Get Vdt_Kh_PhanBoVonChiTiet by PhanBoVonID
        /// </summary>
        /// <param name="iIdPhanBoVonChiTiet"></param>
        /// <param name="dNgayQuyetDinh">if is adjust , have value</param>
        /// <returns></returns>
        List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh);
        
        /// <summary>
        /// Get Vdt_Kh_PhanBoVonChiTiet by PhanBoVonID
        /// </summary>
        /// <param name="iIdPhanBoVonChiTiet"></param>
        /// <param name="dNgayQuyetDinh">if is adjust , have value</param>
        /// <returns></returns>
        List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentIdClone(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh);

        /// <summary>
        /// Insert PhanBoVonChiTiet
        /// </summary>
        /// <param name="dt">VDT_KH_PhanBoVonChiTiet</param>
        /// <param name="sUserLogin">user login</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTiet(int iLoaiKeHoach, DataTable dt, string sUserLogin, bool bIsEdit);
        /// <summary>
        /// Get VDT_KHV_PhanBoVonChiTiet by VDT_KHV_PhanBoVon
        /// </summary>
        /// <param name="iIdPhanBoVonId">VDT_KHV_PhanBoVon.iIdPhanBoVon_Id</param>
        /// <returns></returns>
        IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> GetPhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId);

        /// <summary>
        /// Remove VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="data">VDT_KHV_PhanBoVonChiTiet</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTiet(VdtKhvPhanBoVonDonViChiTietPheDuyet data);

        /// <summary>
        /// Update list VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="datas">VDT_KHV_PhanBoVonChiTiets</param>
        /// <returns></returns>
        int Update(IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> datas);

        /// <summary>
        /// Remove list VDT_KHV_PhanBoVonChiTiet
        /// </summary>
        /// <param name="datas">VDT_KHV_PhanBoVonChiTiets</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTiet(IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> datas);
        IEnumerable<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon);
        IEnumerable<VdtKhvVonNamDonViPheDuyetDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh);
        IEnumerable<PhanBoVonDonViPheDuyetDieuChinhReportQuery> GetPhanBoVonDonViPheDuyetDieuChinhReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe);
        IEnumerable<PhanBoVonDonViPheDuyetReportQuery> GetPhanBoVonDonViPheDuyetReport(string lstId, string lstLct, int yearPlan, int type, string lstDonVi, double donViTienTe);
        IEnumerable<long> GetVonBoTri5Nam(string lstId, int yearPlan);
        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonQuery> GetPhanBoVonDonViPheDuyetNguonVon(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe);
        public IEnumerable<VdtKhvVonNamDonViPheDuyetNguonVonDieuChinhQuery> GetPhanBoVonDonViPheDuyetNguonVonDieuChinh(int type, string lstId, string lstLct, string lstNguonVon, string lstDonVi, double donViTienTe);

    }
}
