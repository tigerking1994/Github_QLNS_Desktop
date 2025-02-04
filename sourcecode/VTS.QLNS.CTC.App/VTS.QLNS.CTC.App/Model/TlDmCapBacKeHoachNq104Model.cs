using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCapBacKeHoachNq104Model : ModelBase
    {
        private string _maCb;
        public string MaCb
        {
            get => _maCb;
            set => SetProperty(ref _maCb, value);
        }

        private string _tenCb;
        public string TenCb
        {
            get => _tenCb;
            set => SetProperty(ref _tenCb, value);
        }
        public bool? Splits { get; set; }

        private string _parent;
        public string Parent
        {
            get => _parent;
            set
            {
                SetProperty(ref _parent, value);
                OnPropertyChanged(nameof(IsNhomEnabled));
            }
        }
        public bool? Readonly { get; set; }

        private string _moTa;
        public string MoTa
        {
            get => _moTa;
            set => SetProperty(ref _moTa, value);
        }

        private decimal? _kpcdCq;
        public decimal? KpcdCq
        {
            get => _kpcdCq;
            set => SetProperty(ref _kpcdCq, value);
        }

        private decimal? _kpcdCn;
        public decimal? KpcdCn
        {
            get => _kpcdCn;
            set => SetProperty(ref _kpcdCn, value);
        }

        private int? _thoiHanTang;
        public int? ThoiHanTang
        {
            get => _thoiHanTang;
            set => SetProperty(ref _thoiHanTang, value);
        }

        private string _maCbKeHoach;
        public string MaCbKeHoach
        {
            get => _maCbKeHoach;
            set => SetProperty(ref _maCbKeHoach, value);
        }

        private string _tenCbKeHoach;
        public string TenCbKeHoach
        {
            get => _tenCbKeHoach;
            set => SetProperty(ref _tenCbKeHoach, value);
        }

        private string _moTaKeHoach;
        public string MoTaKeHoach
        {
            get => _moTaKeHoach;
            set => SetProperty(ref _moTaKeHoach, value);
        }

        private List<ComboboxItem> _listCapBac;
        public List<ComboboxItem> ListCapBac
        {
            get => _listCapBac;
            set => SetProperty(ref _listCapBac, value);
        }

        private List<ComboboxItem> _listLoaiNhom;
        public List<ComboboxItem> ListLoaiNhom
        {
            get => _listLoaiNhom;
            set => SetProperty(ref _listLoaiNhom, value);
        }

        private int? _tuoiHuuNam;
        public int? TuoiHuuNam
        {
            get => _tuoiHuuNam;
            set => SetProperty(ref _tuoiHuuNam, value);
        }

        private int? _tuoiHuuNu;
        public int? TuoiHuuNu
        {
            get => _tuoiHuuNu;
            set => SetProperty(ref _tuoiHuuNu, value);
        }

        private List<ComboboxItem> _listCapBacKeHoach;
        public List<ComboboxItem> ListCapBacKeHoach
        {
            get => _listCapBacKeHoach;
            set => SetProperty(ref _listCapBacKeHoach, value);
        }

        private double? _pcrqTt;
        public double? PcrqTt
        {
            get => _pcrqTt;
            set => SetProperty(ref _pcrqTt, value);
        }

        private List<ComboboxItem> _lstBacLuongHienTai;
        public List<ComboboxItem> LstBacLuongHienTai
        {
            get => _lstBacLuongHienTai;
            set => SetProperty(ref _lstBacLuongHienTai, value);
        }

        private List<ComboboxItem> _lstBacLuongKeHoach;
        public List<ComboboxItem> LstBacLuongKeHoach
        {
            get => _lstBacLuongKeHoach;
            set => SetProperty(ref _lstBacLuongKeHoach, value);
        }

        private List<ComboboxItem> _lstBacLuongTran;
        public List<ComboboxItem> LstBacLuongTran
        {
            get => _lstBacLuongTran;
            set => SetProperty(ref _lstBacLuongTran, value);
        }

        public DateTime? NgayNhanQh { get; set; }

        public bool BHangCha => IsHangCha;

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private string _loai;
        public string Loai
        {
            get => _loai;
            set => SetProperty(ref _loai, value);
        }

        private string _nhom;
        public string Nhom
        {
            get => _nhom;
            set => SetProperty(ref _nhom, value);
        }

        public bool IsNhomEnabled => !string.IsNullOrEmpty(Parent) && (Parent.StartsWith("2") || Parent.StartsWith("3"));

        private string _maBacLuongTran;
        public string MaBacLuongTran
        {
            get => _maBacLuongTran;
            set => SetProperty(ref _maBacLuongTran, value);
        }

        private decimal? _hsVk;
        public decimal? HsVk
        {
            get => _hsVk;
            set => SetProperty(ref _hsVk, value);
        }

        private string _maBacLuong;
        public string MaBacLuong
        {
            get => _maBacLuong;
            set => SetProperty(ref _maBacLuong, value);
        }

        private string _maBacLuongKeHoach;
        public string MaBacLuongKeHoach
        {
            get => _maBacLuongKeHoach;
            set => SetProperty(ref _maBacLuongKeHoach, value);
        }

        private string _loaiNhom;
        public string LoaiNhom
        {
            get => _loaiNhom;
            set => SetProperty(ref _loaiNhom, value);
        }
    }
}
