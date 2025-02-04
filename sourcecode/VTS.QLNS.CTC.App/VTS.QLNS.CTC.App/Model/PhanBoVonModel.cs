using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class PhanBoVonModel : BindableBase
    {
        public int iRowIndex { get; set; }
        public Guid Id { get; set; }
        public string sSoQuyetDinh { get; set; }
        public DateTime? dNgayQuyetDinh { get; set; }
        public int? iNamKeHoach { get; set; }
        public Guid? iId_LoaiNguonVonId { get; set; }
        public Guid? iId_DonViQuanLyId { get; set; }
        public string iID_MaDonViQuanLy { get; set; }
        public Guid? iId_NhomQuanLyId { get; set; }
        public Guid? iId_LoaiNganSachId { get; set; }
        public Guid? iId_KhoanNganSachId { get; set; }
        public string sLoaiDieuChinh { get; set; }
        public Guid? iId_ParentId { get; set; }
        public bool? bActive { get; set; }
        public bool? bIsGoc { get; set; }
        private bool _bKhoa;
        public bool BKhoa
        {
            get => _bKhoa;
            set => SetProperty(ref _bKhoa, value);
        }
        public bool? bLaThayThe { get; set; }
        public double? fGiaTrDeNghi { get; set; }
        public double? fGiaTrPhanBo { get; set; }
        public double? fGiaTriThuHoi { get; set; }
        public Guid? iId_DonViTienTeId { get; set; }
        public Guid? iId_TienTeId { get; set; }
        public double? fTiGiaDonVi { get; set; }
        public double? fTiGia { get; set; }
        public string sUserCreate { get; set; }
        public DateTime? dDateCreate { get; set; }
        public string sUserUpdate { get; set; }
        public DateTime? dDateUpdate { get; set; }
        public string sUserDelete { get; set; }
        public DateTime? dDateDelete { get; set; }
        public bool? bIsCanBoDuyet { get; set; }
        public bool? bIsDuyet { get; set; }
        public int? iId_NguonVonId { get; set; }
        public string sNguonVon { get; set; }
        public string sLoaiNguonVon { get; set; }
        public string sTenDonVi { get; set; }
        public double fChiTieuDauNam { get; set; }
        public double fChiTieuBoXung { get; set; }
        public string sL { get; set; }
        public string sK { get; set; }
        public string sM { get; set; }
        public string sTM { get; set; }
        public string sTTM { get; set; }
        public string sNG { get; set; }
        public int iSoLanDieuChinh { get; set; }
        public Guid? IIdPhanBoGocID { get; set; }
        public string sSoLanDieuChinh
        {
            get
            {
                return string.Format("({0})", this.iSoLanDieuChinh);
            }
        }
        public string sCanBoDuyet
        {
            get => this.bIsCanBoDuyet ?? false ? ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.DA_DUYET) : ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.CHUA_DUYET);
        }
        public string sDuyet
        {
            get => this.bIsDuyet ?? false ? ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.DA_DUYET) : ApproveTypeEnum.GetApproveTypeName((int)ApproveTypeEnum.Type.CHUA_DUYET);
        }
        public string sSoQuyetDinhOld { get; set; }
        public DateTime? dNgayQuyetDinhOld { get; set; }
        public bool IsEdit { get; set; }
        public bool IsAdjust { get; set; }
        public Guid? IdAdjust { get; set; }
        public int ActionState => IsEdit ? (int)TypeExecute.Update : IsAdjust ? (int)TypeExecute.Adjust : (int)TypeExecute.Insert;
        public bool BActive { get; set; }
        public bool IsViewDetail { get; set; }
        public string DieuChinhTu { get; set; }
        public double? FCapPhatTaiKhoBac { get; set; }
        public double? FCapPhatBangLenhChi { get; set; }
        public double? FTonKhoanTaiDonVi { get; set; }
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private int _iLoaiDuToan;
        public int ILoaiDuToan
        {
            get => _iLoaiDuToan;
            set => SetProperty(ref _iLoaiDuToan, value);
        }
    }
}
