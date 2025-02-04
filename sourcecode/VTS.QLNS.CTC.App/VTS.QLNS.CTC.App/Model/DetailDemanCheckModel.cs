namespace VTS.QLNS.CTC.App.Model
{
    public class DetailDemandCheckModel : BindableBase
    {
        private string? _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }
        
        private string? _tm;
        public string? TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }
        
        private string? _ng;
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }
        
        private string? _stt;
        public string STT
        {
            get => _stt;
            set => SetProperty(ref _stt, value);
        }
        
        private string? _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        private int? _value;
        public int? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
        
        private int _index;
        
        private string? _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public int Index
        {
            get => _index;
            set => SetProperty(ref _index, value);
        }
    }
}