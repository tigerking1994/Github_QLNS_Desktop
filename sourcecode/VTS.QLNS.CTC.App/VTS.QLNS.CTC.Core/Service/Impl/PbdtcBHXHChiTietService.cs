using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PbdtcBHXHChiTietService : IPbdtcBHXHChiTietService
    {
        private readonly IPbdtcBHXHChiTietRepository _ipbdtcBHXHChiTietRepository;
        public PbdtcBHXHChiTietService(IPbdtcBHXHChiTietRepository ipbdtcBHXHChiTietRepository)
        {
            _ipbdtcBHXHChiTietRepository = ipbdtcBHXHChiTietRepository;
        }
        public IEnumerable<BhPbdtcBHXHChiTiet> FindByCondition(Expression<Func<BhPbdtcBHXHChiTiet, bool>> predicate)
        {
            return _ipbdtcBHXHChiTietRepository.FindByCondition(predicate);
        }
        public int Add(BhPbdtcBHXHChiTiet item)
        {
            return _ipbdtcBHXHChiTietRepository.Add(item);
        }

        public int Update(BhPbdtcBHXHChiTiet item)
        {
            return _ipbdtcBHXHChiTietRepository.Update(item);
        }

        public int Delete(BhPbdtcBHXHChiTiet item)
        {
            return _ipbdtcBHXHChiTietRepository.Delete(item);
        }

        public int AddRange(IEnumerable<BhPbdtcBHXHChiTiet> items)
        {
            return _ipbdtcBHXHChiTietRepository.AddRange(items);
        }
        public int RemoveRange(IEnumerable<BhPbdtcBHXHChiTiet> items)
        {
            return _ipbdtcBHXHChiTietRepository.RemoveRange(items);
        }
        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTiet(Guid chungTuPhaBoId, string sLNS, string sIdDonVi, int iNamLamViec, string userName, int? loaiDotNhanPhanBo)
        {
            return _ipbdtcBHXHChiTietRepository.FindChungTuChiTiet(chungTuPhaBoId, sLNS, sIdDonVi, iNamLamViec, userName, loaiDotNhanPhanBo);
        }
        public IEnumerable<BhPbdtcBHXHChiTietQuery> FindChungTuChiTietDieuChinh(Guid chungTuPhanBoId, string sLNS, int iNamLamViec, string userName)
        {
            return _ipbdtcBHXHChiTietRepository.FindChungTuChiTietDieuChinh(chungTuPhanBoId, sLNS, iNamLamViec, userName);
        }
        public IEnumerable<BhPbdtcBHXHChiTietQuery> ExportExcelPhanBoDuToanChi(Guid chungTuId, string sLNS, int iNamLamViec, string sMaDonVi)
        {
            return _ipbdtcBHXHChiTietRepository.ExportExcelPhanBoDuToanChi(chungTuId, sLNS, iNamLamViec, sMaDonVi);
        }

        public IEnumerable<BhPbdtcBHXHChiTietQuery> GetSoChuaPhanBo(Guid iD_Ndtctg, Guid iD_Mlns, Guid? idChungTuEdit)
        {
            return _ipbdtcBHXHChiTietRepository.GetSoChuaPhanBo(iD_Ndtctg, iD_Mlns, idChungTuEdit);
        }

        public List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiBHXH(int yearOfWork, string selectedUnits, Guid? IdLoaiChi, string sMaLoaiChi, string lstIDChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound)
        {
            return _ipbdtcBHXHChiTietRepository.GetListDataPhanBoLoaiChiBHXH(yearOfWork, selectedUnits, IdLoaiChi, sMaLoaiChi, lstIDChungTu, donViTinh, IsTongHopDonViKhoi, dotNhan, isMillionRound);
        }

        public List<BhPbdtcBHXHChiBHXHReportQuery> GetListDataPhanBoLoaiChiKPQLKCBKHAC(int yearOfWork, string selectedUnits, Guid? iDLoaiChi, string sMaLoaiChi, string lstIDChungTu, int donViTinh, bool IsTongHopDonViKhoi, int dotNhan, bool isMillionRound)
        {
            return _ipbdtcBHXHChiTietRepository.GetListDataPhanBoLoaiChiKPQLKCBKHAC(yearOfWork, selectedUnits, iDLoaiChi, sMaLoaiChi, lstIDChungTu, donViTinh, IsTongHopDonViKhoi, dotNhan, isMillionRound);
        }

        public List<BhPbdtcBHXHChiTietQuery> FindGiaTriDieuChinhThuBHXHChangeRequest(string sIdDonVi, int iNamLamViec)
        {
            return _ipbdtcBHXHChiTietRepository.FindGiaTriDieuChinhThuBHXHChangeRequest(sIdDonVi, iNamLamViec);
        }

        public List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoGopChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound, string lstMaLoaiChi)
        {
            return _ipbdtcBHXHChiTietRepository.ExportBaoCaoGopChiKQPL(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, isMillionRound, lstMaLoaiChi);
        }

        public List<ReportDuToanChiBHXHBHYTBHTNQuery> ExportBaoCaoTachChiKQPL(int yearOfWork, string selectedUnits, string soQuyetDinh, string ngayQuyetDinh, int donViTinh, bool isMillionRound , string lstMaLoaiChi)
        {
            return _ipbdtcBHXHChiTietRepository.ExportBaoCaoTachChiKQPL(yearOfWork, selectedUnits, soQuyetDinh, ngayQuyetDinh, donViTinh, isMillionRound, lstMaLoaiChi);
        }
    }
}
