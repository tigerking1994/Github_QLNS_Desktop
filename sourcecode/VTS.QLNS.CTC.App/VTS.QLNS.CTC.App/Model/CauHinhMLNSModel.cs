using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class CauHinhMLNSModel : ModelBase
    {
        private Guid _mlnsId;
        public Guid MlnsId
        {
            get => _mlnsId;
            set => SetProperty(ref _mlnsId, value);
        }

        private Guid? _mlnsIdParent;
        public Guid? MlnsIdParent
        {
            get => _mlnsIdParent;
            set => SetProperty(ref _mlnsIdParent, value);
        }

        private string _lns;
        [DisplayName("LNS")]
        [DisplayDetailInfo("LNS")]
        public string Lns
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        [DisplayDetailInfo("L")]
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        [DisplayDetailInfo("K")]
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        private string _m;
        [DisplayDetailInfo("M")]
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        [DisplayDetailInfo("TM")]
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        [DisplayDetailInfo("TTM")]
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        [DisplayDetailInfo("NG")]
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        private string _tng;
        [DisplayDetailInfo("TNG")]
        public string TNG
        {
            get => _tng;
            set => SetProperty(ref _tng, value);
        }

        private string _tng1;
        [DisplayDetailInfo("TNG1")]
        public string TNG1
        {
            get => _tng1;
            set => SetProperty(ref _tng1, value);
        }

        private string _tng2;
        [DisplayDetailInfo("TNG2")]
        public string TNG2
        {
            get => _tng2;
            set => SetProperty(ref _tng2, value);
        }

        private string _tng3;
        [DisplayDetailInfo("TNG3")]
        public string TNG3
        {
            get => _tng3;
            set => SetProperty(ref _tng3, value);
        }

        private string _mota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private string _xauNoima;
        [DisplayDetailInfo("Xâu nối mã")]
        public string XauNoiMa
        {
            get => _xauNoima;
            set => SetProperty(ref _xauNoima, value);
        }

        public string LNSDisplay => String.Format("{0} - {1}", Lns, MoTa);

        public string XNMDisplay => String.Format("{0} - {1}", XauNoiMa, MoTa);

        private int? _iTrangThai;
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        [DisplayDetailInfo("Năm làm việc")]
        public int? NamLamViec { get; set; }
        public string Chuong { get; set; }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _idPhongBan;
        public string IdPhongBan
        {
            get => _idPhongBan;
            set => SetProperty(ref _idPhongBan, value);
        }

        private string _tenPhongBan;
        [TypeOfDialogAttribute(typeof(NSPhongBanModel), typeof(DmBQuanLy), typeof(NsPhongBanService), typeof(INsPhongBanService))]
        [MapperMethodAttribute("ConvertPhongBanToMLNS")]
        [InitSelectedItemsMethodAttribute("SetSelectedPhongBan")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(false)]
        [DisplayDetailInfo("B quản lý")]
        [DisplayName("B quản lý (Chọn F6/ Hủy chọn Ctrl + F6)")]
        public string TenPhongBan
        {
            get => _tenPhongBan;
            set => SetProperty(ref _tenPhongBan, value);
        }

        public DateTime? DateCreated { get; set; }
        public string UserCreator { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModifier { get; set; }
        public string Tag { get; set; }
        public string Log { get; set; }
        public bool? ILock { get; set; }
        public string IdDonViTao { get; set; }

        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override string DetailInfoModalTitle => "Chi tiết MLNS " + XauNoiMa;
        public override bool IsEditable => !IsDeleted;

        private string _chiTietToi;
        [DisplayName("Chi tiết đến")]
        [DisplayDetailInfo("Chi tiết đến")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllTrangNgNganSach")]
        public string ChiTietToi
        {
            get => _chiTietToi;
            set
            {
                SetProperty(ref _chiTietToi, value);
                OnPropertyChanged(nameof(MLNSReportSetting));
                OnPropertyChanged(nameof(SelectedMLNSReportSetting));
            }
        }

        private int? _iLoaiNganSach;
        [DisplayName("Loại")]
        [DisplayDetailInfo("Loại")]
        [ColumnTypeAttribute(ColumnType.Combobox, "LoadAllLoaiNganSach")]
        public int? ILoaiNganSach
        {
            get => _iLoaiNganSach;
            set
            {
                SetProperty(ref _iLoaiNganSach, value);
            }
        }


        private bool _isUnableEditBQuanlyChiTietToi;
        public bool IsUnableEditBQuanlyChiTietToi
        {
            get => _isUnableEditBQuanlyChiTietToi;
            set => SetProperty(ref _isUnableEditBQuanlyChiTietToi, value);
        }

        private bool _isUnableEditTextField;
        public bool IsUnableEditTextField
        {
            get => _isUnableEditTextField;
            set => SetProperty(ref _isUnableEditTextField, value);
        }

        public ObservableCollection<ComboboxItem> MLNSReportSetting
        {
            get => new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
        }

        private string _selectedMLNSReportSetting;
        public string SelectedMLNSReportSetting
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedMLNSReportSetting) && MLNSReportSetting.Count > 0)
                    return MLNSReportSetting.Last().ValueItem;
                return _selectedMLNSReportSetting;
            }
            set => SetProperty(ref _selectedMLNSReportSetting, value);
        }

        private bool _bNgay;
        public bool BNgay
        {
            get => _bNgay;
            set => SetProperty(ref _bNgay, value);
        }

        private bool _bSoNguoi;
        public bool BSoNguoi
        {
            get => _bSoNguoi;
            set => SetProperty(ref _bSoNguoi, value);
        }

        private bool _bTonKho;
        [DisplayName("Tồn kho")]
        [DisplayDetailInfo("Tồn kho")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BTonKho
        {
            get => _bTonKho;
            set => SetProperty(ref _bTonKho, value);
        }

        private bool _bTuChi;
        [DisplayName("Tự chi")]
        [DisplayDetailInfo("Tự chi")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BTuChi
        {
            get => _bTuChi;
            set => SetProperty(ref _bTuChi, value);
        }

        private bool _bHangNhap;
        [DisplayName("Hàng nhập")]
        [DisplayDetailInfo("Hàng nhập")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BHangNhap
        {
            get => _bHangNhap;
            set => SetProperty(ref _bHangNhap, value);
        }

        private bool _bHangMua;
        [DisplayName("Hàng mua")]
        [DisplayDetailInfo("Hàng mua")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BHangMua
        {
            get => _bHangMua;
            set => SetProperty(ref _bHangMua, value);
        }

        private bool _bHienVat;
        [DisplayName("Hiện vật")]
        [DisplayDetailInfo("Hiện vật")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BHienVat
        {
            get => _bHienVat;
            set => SetProperty(ref _bHienVat, value);
        }

        private bool _bDuPhong;
        [DisplayName("Dự phòng")]
        [DisplayDetailInfo("Dự phòng")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BDuPhong
        {
            get => _bDuPhong;
            set => SetProperty(ref _bDuPhong, value);
        }

        private bool _bPhanCap;
        [DisplayName("Phân cấp")]
        [DisplayDetailInfo("Phân cấp")]
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        public bool BPhanCap
        {
            get => _bPhanCap;
            set => SetProperty(ref _bPhanCap, value);
        }
        public string SNhapTheoTruong { get; set; }
        public string IdMaDonVi { get; set; }
        public string SCPChiTietToi { get; set; }
        public bool? BHangChaDuToan { get; set; }
        public bool? BHangChaQuyetToan { get; set; }
        public string SQuyetToanChiTietToi { get; set; }
        public string SDuToanChiTietToi { get; set; }

    }
}
