using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Text;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCapBacModel : ModelBase
    {
        private string _maCb;
        [DisplayName("Mã cấp bậc")]
        [DisplayDetailInfo("Mã cấp bậc")]
        public string MaCb
        {
            get => _maCb;
            set => SetProperty(ref _maCb, value);
        }

        private string _tenCb;
        [DisplayName("Tên cấp bậc")]
        [DisplayDetailInfo("Tên cấp bậc")]
        public string TenCb
        {
            get => _tenCb;
            set => SetProperty(ref _tenCb, value);
        }

        private bool? _splits;
        public bool? Splits
        {
            get => _splits;
            set => SetProperty(ref _splits, value);
        }

        private string _parent;
        [DisplayName("Cấp bậc cha")]
        public string Parent
        {
            get => _parent;
            set => SetProperty(ref _parent, value);
        }

        private bool? _readonly;
        public bool? Readonly
        {
            get => _readonly;
            set => SetProperty(ref _readonly, value);
        }

        private string _note;
        [DisplayName("Ghi chú")]
        [DisplayDetailInfo("Ghi chú")]
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        private decimal? _bhxhCq;
        [DisplayName("BHXH (Hệ số đơn vị đóng bảo hiểm)")]
        [DisplayDetailInfo("BHXH (Hệ số đơn vị đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? BhxhCq
        {
            get => _bhxhCq;
            set => SetProperty(ref _bhxhCq, value);
        }

        private decimal? _hsBhxh;
        [DisplayName("BHXH (Hệ số cá nhân đóng bảo hiểm)")]
        [DisplayDetailInfo("BHXH (Hệ số cá nhân đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? HsBhxh
        {
            get => _hsBhxh;
            set => SetProperty(ref _hsBhxh, value);
        }

        private decimal? _bhytCq;
        [DisplayName("BHYT (Hệ số đơn vị đóng bảo hiểm)")]
        [DisplayDetailInfo("BHYT (Hệ số đơn vị đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? BhytCq
        {
            get => _bhytCq;
            set => SetProperty(ref _bhytCq, value);
        }

        private decimal? _hsBhyt;
        [DisplayName("BHYT (Hệ số ca nhân đóng bảo hiểm)")]
        [DisplayDetailInfo("BHYT (Hệ số cá nhân đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? HsBhyt
        {
            get => _hsBhyt;
            set => SetProperty(ref _hsBhyt, value);
        }

        private decimal? _bhtnCq;
        [DisplayName("BHTN (Hệ số đơn vị đóng bảo hiểm)")]
        [DisplayDetailInfo("BHTN (Hệ số đơn vị đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? BhtnCq
        {
            get => _bhtnCq;
            set => SetProperty(ref _bhtnCq, value);
        }

        private decimal? _hsBhtn;
        [DisplayName("BHTN (Hệ số ca nhân đóng bảo hiểm)")]
        [DisplayDetailInfo("BHTN (Hệ số cá nhân đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? HsBhtn
        {
            get => _hsBhtn;
            set => SetProperty(ref _hsBhtn, value);
        }

        private decimal? _kpcdCq;
        [DisplayName("KPCD (Hệ số đơn vị đóng bảo hiểm)")]
        [DisplayDetailInfo("KPCD (Hệ số đơn vị đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? KpcdCq
        {
            get => _kpcdCq;
            set => SetProperty(ref _kpcdCq, value);
        }

        private decimal? _hsKpcd;
        [DisplayName("KPCD (Hệ số cá nhân đóng bảo hiểm)")]
        [DisplayDetailInfo("KPCD (Hệ số cá nhân đóng bảo hiểm)")]
        [FormatAttribute("{0:N4}")]
        public decimal? HsKpcd
        {
            get => _hsKpcd;
            set => SetProperty(ref _hsKpcd, value);
        }

        public decimal? BhcsCq { get; set; }
        public decimal? HsBhcs { get; set; }

        private decimal? _lhtHs;
        [DisplayName("Hệ số")]
        [DisplayDetailInfo("Hệ số")]
        [FormatAttribute("{0:N4}")]
        public decimal? LhtHs
        {
            get => _lhtHs;
            set => SetProperty(ref _lhtHs, value);
        }

        private decimal? _phuCapRaQuan;
        [DisplayName("Phụ cấp ra quân")]
        [DisplayDetailInfo("Phụ cấp ra quân")]
        [FormatAttribute("{0:N0}")]
        public decimal? PhuCapRaQuan
        {
            get => _phuCapRaQuan;
            set => SetProperty(ref _phuCapRaQuan, value);
        }

        private decimal? _tiLeHuong;
        [DisplayName("Tỉ lệ hưởng")]
        [DisplayDetailInfo("Tỉ lệ hưởng")]
        [FormatAttribute("{0:N4}")]
        public decimal? TiLeHuong
        {
            get => _tiLeHuong;
            set => SetProperty(ref _tiLeHuong, value);
        }

        private decimal? _hsTroCapOmDau;
        [DisplayName("Hệ số trợ cấp ốm đau")]
        [DisplayDetailInfo("Hệ số trợ cấp ốm đau")]
        [FormatAttribute("{0:N4}")]
        public decimal? HsTroCapOmDau
        {
            get => _hsTroCapOmDau;
            set => SetProperty(ref _hsTroCapOmDau, value);
        }

        public override bool IsHangCha
        {
            get => (Parent == "" || Parent == null);
        }

        public string XauNoiMa { get; set; }

        public string CapBacDisplay => string.Format("{0} - {1} - {2}", MaCb, Note, LhtHs);
        public string CapBacDisplay1 => string.Format("{0} - {1}", Note, LhtHs);
        public string CapBacDisplay2 => string.Format("{0} - {1}", MaCb, Note);
    }
}
