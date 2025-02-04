using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Criteria;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class VdtKhvPhanBoVonDonViChiTietPheDuyetService : IVdtKhvPhanBoVonDonViChiTietPheDuyetService
    {
        private readonly IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository;
        private readonly ITongHopNguonNSDauTuRepository _tonghopRepository;

        public VdtKhvPhanBoVonDonViChiTietPheDuyetService(
            IVdtKhvPhanBoVonDonViChiTietPheDuyetRepository vdtKhvPhanBoVonDonViChiTietPheDuyetRepository,
            ITongHopNguonNSDauTuRepository tonghopRepository)
        {
            _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository = vdtKhvPhanBoVonDonViChiTietPheDuyetRepository;
            _tonghopRepository = tonghopRepository;
        }

        public IEnumerable<PhanBoVonDonViChiTietPheDuyetQuery> GetAllDuAnInPhanBoVon(string idPhanBoVonDeXuat, int iNguonVonId)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetAllDuAnInPhanBoVon(idPhanBoVonDeXuat, iNguonVonId);
        }

        public List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentId(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietByParentId(iIdPhanBoVonChiTiet, dNgayQuyetDinh);
        }
        
        public List<PhanBoVonDonViChiTietPheDuyetQuery> GetPhanBoVonChiTietByParentIdClone(Guid iIdPhanBoVonChiTiet, DateTime? dNgayQuyetDinh)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietByParentIdClone(iIdPhanBoVonChiTiet, dNgayQuyetDinh);
        }

        public bool CreatePhanBoVonChiTiet(Guid iIdPhanBoVon, int iLoaiKeHoach, List<PhanBoVonDonViChiTietPheDuyetInsertQuery> lstDetail, string sUserLogin, bool bIsEdit)//, int iTypeInsert, Guid? iIdPhanBoVon = null)
        {
            if (iLoaiKeHoach == (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet)
                _tonghopRepository.DeleteTongHopNguonDauTu_Giam(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, iIdPhanBoVon);
            DataTable dt = DBExtension.ConvertDataToTableDefined("t_tbl_phanbovonchitiet6", lstDetail);
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.CreatePhanBoVonChiTiet(iLoaiKeHoach, dt, sUserLogin, bIsEdit);
        }

        public int RemovePhanBoVonChiTietByIidPhanBoVonID(Guid iIdPhanBoVonId)
        {
            IEnumerable<VdtKhvPhanBoVonDonViChiTietPheDuyet> datas = _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietByIidPhanBoVonID(iIdPhanBoVonId);
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.RemovePhanBoVonChiTiet(datas);
        }

        public IEnumerable<VdtKhvVonNamDonViPheDuyetDuocDuyetQuery> GetKeHoachVonNamDuocDuyetReport(string listId, string lct, int type, int loaiDuAn, string lstDonVi, double donViTinh)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetKeHoachVonNamDuocDuyetReport(listId, lct, type, loaiDuAn, lstDonVi, donViTinh);
        }

        public IEnumerable<PhanBoVonDonViPheDuyetDuocDuyetChiTietQuery> GetPhanBoVonChiTietDieuChinhByParentId(Guid idPhanBoVon)
        {
            return _vdtKhvPhanBoVonDonViChiTietPheDuyetRepository.GetPhanBoVonChiTietDieuChinhByParentId(idPhanBoVon);
        }
    }
}
