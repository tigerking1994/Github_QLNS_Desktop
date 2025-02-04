using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhPbdttmBHYTChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IID_DTTM_BHYT_ThanNhan_PhanBo { get; set; }
        public Guid IID_DTTM_BHYT_ThanNhan { get; set; }
        public Guid? IID_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet { get; set; }
        public string SSoChungTu { get; set; }
        public DateTime DNgayChungTu { get; set; }
        public string SXauNoiMa { get; set; }
        public string SSoQuyetDinh { get; set; }
        public DateTime DNgayQuyetDinh { get; set; }
        public string IID_MaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public string SLNS { get; set; }
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
        public string STNG1 { get; set; }
        public string STNG2 { get; set; }
        public string STNG3 { get; set; }
        public string SNoiDung { get; set; }

        public Double? _fDuToanTruocDieuChinh;
        public Double? FDuToanTruocDieuChinh { get => _fDuToanTruocDieuChinh; set => SetProperty(ref _fDuToanTruocDieuChinh, value); }

        public Double? _fDuToan;
        public Double? FDuToan { get => _fDuToan; set => SetProperty(ref _fDuToan, value); }

        public Double? _fDuToanSauDieuChinh;
        public Double? FDuToanSauDieuChinh { get => _fDuToanSauDieuChinh; set => SetProperty(ref _fDuToanSauDieuChinh, value); }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime DNgayTao { get; set; }
        public DateTime DNgaySua { get; set; }
        public bool BIsKhoa { get; set; }
        public bool BHangCha { get; set; }
        public string SGhiChu { get; set; }
        public string SMoTa { get; set; }
        public int Type { get; set; }
        public bool IsRemainRow { get; set; }
        public string STenDonVi { get; set; }
        public bool BEmty { get;set; }

        private ObservableCollection<ComboboxItem> _cbxDonVi;
        public ObservableCollection<ComboboxItem> CbxDonVi { get => _cbxDonVi; set => SetProperty(ref _cbxDonVi, value); }

        private ObservableCollection<ComboboxItem> _cbxNhanPhanBos;
        public ObservableCollection<ComboboxItem> CbxNhanPhanBos { get => _cbxNhanPhanBos; set => SetProperty(ref _cbxNhanPhanBos, value); }
        public override bool IsEditable => !BHangCha && !IsDeleted;
        public bool IsEmptyPlanData => FDuToan.GetValueOrDefault() == 0;
        public bool HasData => !IsHangCha && (FDuToan.GetValueOrDefault() != 0 || !string.IsNullOrEmpty(SGhiChu));
    }
}
