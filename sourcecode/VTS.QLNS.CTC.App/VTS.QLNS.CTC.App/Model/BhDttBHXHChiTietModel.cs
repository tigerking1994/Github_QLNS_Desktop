using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDttBHXHChiTietModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid DttBHXHId { get; set; }
        public Guid? IIDLoaiDoiTuong { get; set; }
        public string SLoaiDoiTuong { get; set; }
        private double? _fTongThuBHXH;
        public double? FTongThuBHXH
        {
            get => _fTongThuBHXH = FThuBHXHNguoiLaoDong + FThuBHXHNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fTongThuBHXH, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fTongThuBHYT;
        public double? FTongThuBHYT
        {
            get => _fTongThuBHYT = FThuBHYTNguoiLaoDong + FThuBHYTNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fTongThuBHYT, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fTongThuBHTN;
        public double? FTongThuBHTN
        {
            get => _fTongThuBHTN = FThuBHTNNguoiLaoDong + FThuBHTNNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fTongThuBHTN, value);
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public double? FTongCong => FTongThuBHXH + FTongThuBHYT + FTongThuBHTN;

        private double? _fThuBHXHNguoiLaoDong;
        public double? FThuBHXHNguoiLaoDong
        {
            get => _fThuBHXHNguoiLaoDong;
            set
            {
                SetProperty(ref _fThuBHXHNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fThuBHXHNguoiSuDungLaoDong;
        public double? FThuBHXHNguoiSuDungLaoDong
        {
            get => _fThuBHXHNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fThuBHXHNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHXH));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _ThuBHYTNguoiLaoDong;
        public double? FThuBHYTNguoiLaoDong
        {
            get => _ThuBHYTNguoiLaoDong;
            set
            {
                SetProperty(ref _ThuBHYTNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fThuBHYTNguoiSuDungLaoDong;
        public double? FThuBHYTNguoiSuDungLaoDong
        {
            get => _fThuBHYTNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fThuBHYTNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHYT));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fThuBHTNNguoiLaoDong;
        public double? FThuBHTNNguoiLaoDong
        {
            get => _fThuBHTNNguoiLaoDong;
            set
            {
                SetProperty(ref _fThuBHTNNguoiLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        private double? _fThuBHTNNguoiSuDungLaoDong;
        public double? FThuBHTNNguoiSuDungLaoDong
        {
            get => _fThuBHTNNguoiSuDungLaoDong;
            set
            {
                SetProperty(ref _fThuBHTNNguoiSuDungLaoDong, value);
                OnPropertyChanged(nameof(FTongThuBHTN));
                OnPropertyChanged(nameof(FTongCong));
            }
        }
        public string IIDMaDonVi { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public Guid IdParent { get; set; }
        public string SKhoiDonVi { get; set; }
        public string Stt { get; set; }
        public bool IsFirstParentRow { get; set; }
        public bool IsRemainRow { get; set; }
        public int ILoai { get; set; }
        public int INamLamViec { get; set; }
        public int INamNganSach { get; set; }
        public int IIdMaNguonNganSach { get; set; }
        public int Level { get; set; }
        public string SGhiChu { get; set; }
        public int? ILoaiChungTu { get; set; }
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public string STenBhMLNS { get; set; }
        public bool? BHangCha { get; set; }
        private Guid _iIdMlns;
        public Guid IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private Guid? _iIdMlnsCha;
        public Guid? IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }
        private string _sLns;
        [ColumnAttribute("LNS", 2, MLNSFiled.LNS)]
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        [ColumnAttribute("L", 3, MLNSFiled.L)]
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        [ColumnAttribute("M", 4, MLNSFiled.M)]
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        [ColumnAttribute("TM", 5, MLNSFiled.TM)]
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        [ColumnAttribute("TTM", 6, MLNSFiled.TTM)]
        public string STtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        [ColumnAttribute("NG", 7, MLNSFiled.NG)]
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sTng;
        [ColumnAttribute("TNG", 8, MLNSFiled.TNG)]
        public string STng
        {
            get => _sTng;
            set => SetProperty(ref _sTng, value);
        }

        private string _sTng1;
        [ColumnAttribute("TNG1", 9, MLNSFiled.TNG1)]
        public string STng1
        {
            get => _sTng1;
            set => SetProperty(ref _sTng1, value);
        }

        private string _sTng2;
        [ColumnAttribute("TNG2", 10, MLNSFiled.TNG2)]
        public string STng2
        {
            get => _sTng2;
            set => SetProperty(ref _sTng2, value);
        }

        private string _sTng3;
        [ColumnAttribute("TNG3", 11, MLNSFiled.TNG3)]
        public string STng3
        {
            get => _sTng3;
            set => SetProperty(ref _sTng3, value);
        }

        private string _sMota;
        public string SMoTa
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }

    }
}
