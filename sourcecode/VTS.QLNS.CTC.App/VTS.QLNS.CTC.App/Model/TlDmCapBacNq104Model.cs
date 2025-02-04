namespace VTS.QLNS.CTC.App.Model
{
    public class TlDmCapBacNq104Model : ModelBase
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
        public string _parent;
        public string Parent
        {
            get => _parent;
            set => SetProperty(ref _parent, value);
        }
        public bool? IsReadonly { get; set; }
        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }
        public string XauNoiMa { get; set; }
        public override bool IsHangCha => string.IsNullOrEmpty(Parent);
        public string CapBac => $"{MaCb} - {TenCb}";
        private int? _nam;
        public int? Nam
        {
            get => _nam;
            set => SetProperty(ref _nam, value);
        }
        public string TenCha { get; set; }

        public string CapBacDisplay => string.Format("{0} - {1}", MaCb, TenCb);

    }
}
