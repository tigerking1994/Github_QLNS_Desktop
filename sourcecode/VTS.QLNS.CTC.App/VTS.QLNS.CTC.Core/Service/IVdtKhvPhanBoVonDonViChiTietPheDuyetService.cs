using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.Core.Service
{
    public interface IVdtKhvPhanBoVonDonViChiTietPheDuyetService
    {
        /// <summary>
        /// Get all list DuAn in PhanBoVonChiTietInsert View
        /// </summary>
        /// <param name="dNgayLap"></param>
        /// <param name="iIdDonViQuanLyId"></param>
        /// <param name="iIdLoaiCongTrinhId"></param>
        /// <param name="iNguonVonId"></param>
        /// <returns></returns>
        IEnumerable<PhanBoVonDonViChiTietPheDuyetQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId);
        /// <summary>
        /// Insert VDT_KH_PhanBoVonChiTiet
        /// </summary>
        /// <param name="lstDetail">list data detail</param>
        /// <param name="sUserLogin">userLogin</param>
        /// <returns></returns>
        bool CreatePhanBoVonChiTiet(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonDonViChiTietPheDuyetInsertQuery> lstDetail, string sUserLogin, bool bIsEdit);//, int iTypeInsert, Guid? iIdPhanBoVon = null);

        /// <summary>
        /// Delete Vdt_Kh_PhanBoVonChiTiet by parent_id
        /// </summary>
        /// <param name="iIdPhanBoVonId">Vdt_Kh_PhanBoVon</param>
        /// <returns></returns>
        int RemovePhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId);
        List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh = null);
        List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentIdClone(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh = null);
        IEnumerable<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon);
        IEnumerable<VdtKhvVonNamDonViPheDuyetDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh);
    }
}
