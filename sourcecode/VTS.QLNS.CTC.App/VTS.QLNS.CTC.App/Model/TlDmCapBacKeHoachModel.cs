using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCapBacKeHoachModel : ModelBase
    {
        public Guid Id { get; set; }

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

        private decimal? _lhtHs;
        public decimal? LhtHs
        {
            get => _lhtHs;
            set => SetProperty(ref _lhtHs, value);
        }

        private decimal? _bhxhCq;
        public decimal? BhxhCq
        {
            get => _bhxhCq;
            set => SetProperty(ref _bhxhCq, value);
        }

        private decimal? _bhxhCn;
        public decimal? BhxhCn
        {
            get => _bhxhCn;
            set => SetProperty(ref _bhxhCn, value);
        }

        private decimal? _bhytCq;
        public decimal? BhytCq
        {
            get => _bhytCq;
            set => SetProperty(ref _bhytCq, value);
        }

        private decimal? _bhytCn;
        public decimal? BhytCn
        {
            get => _bhytCn;
            set => SetProperty(ref _bhytCn, value);
        }

        private decimal? _bhtnCq;
        public decimal? BhtnCq
        {
            get => _bhtnCq;
            set => SetProperty(ref _bhtnCq, value);
        }

        private decimal? _bhtnCn;
        public decimal? BhtnCn
        {
            get => _bhtnCn;
            set => SetProperty(ref _bhtnCn, value);
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

        private decimal? _hsLuongKeHoach;
        public decimal? HsLuongKeHoach
        {
            get => _hsLuongKeHoach;
            set => SetProperty(ref _hsLuongKeHoach, value);
        }

        private decimal? _hsLuongTran;
        public decimal? HsLuongTran
        {
            get => _hsLuongTran;
            set => SetProperty(ref _hsLuongTran, value);
        }

        private string _moTaLuongTran;
        public string MoTaLuongTran
        {
            get => _moTaLuongTran;
            set => SetProperty(ref _moTaLuongTran, value);
        }

        private List<ComboboxItem> _lstHslKeHoach;
        public List<ComboboxItem> LstHslKeHoach
        {
            get => _lstHslKeHoach;
            set => SetProperty(ref _lstHslKeHoach, value);
        }

        private List<ComboboxItem> _lstHslHienTai;
        public List<ComboboxItem> LstHslHienTai
        {
            get => _lstHslHienTai;
            set => SetProperty(ref _lstHslHienTai, value);
        }

        private List<ComboboxItem> _lstHslTran;
        public List<ComboboxItem> LstHslTran
        {
            get => _lstHslTran;
            set => SetProperty(ref _lstHslTran, value);
        }

        private Guid? _idHslKeHoach;
        public Guid? IdHslKeHoach
        {
            get => _idHslKeHoach;
            set => SetProperty(ref _idHslKeHoach, value);
        }

        private Guid? _idHslHienTai;
        public Guid? IdHslHienTai
        {
            get => _idHslHienTai;
            set => SetProperty(ref _idHslHienTai, value);
        }

        public DateTime? NgayNhanQh { get; set; }

        public string Display => string.Format("{0} - {1} - {2}", HsLuongKeHoach, MoTaKeHoach.Trim(), MaCbKeHoach);
        public string DislayTran => IdHslTran == null ? string.Empty : string.Format("{0} - {1} - {2}", HsLuongTran, MoTaLuongTran, MaCbTran);

        public bool BHangCha => IsHangCha;

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private string _nhom;
        public string Nhom
        {
            get => _nhom;
            set => SetProperty(ref _nhom, value);
        }

        public bool IsNhomEnabled => !string.IsNullOrEmpty(Parent) && (Parent.StartsWith("2") || Parent.StartsWith("3"));

        private Guid? _idHslTran;
        public Guid? IdHslTran
        {
            get => _idHslTran;
            set => SetProperty(ref _idHslTran, value);
        }

        private string _maCbTran;
        public string MaCbTran
        {
            get => _maCbTran;
            set => SetProperty(ref _maCbTran, value);
        }

        private decimal? _hsVk;
        public decimal? HsVk
        {
            get => _hsVk;
            set => SetProperty(ref _hsVk, value);
        }
    }
}
