using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlBaoCaoModel : DetailModelBase
    {
        public string MaBaoCao { get; set; }
        private string _tenBaoCao;

        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        public string MaParent { get; set; }
        public bool? IsParent { get; set; }

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public bool BHangCha => IsHangCha;

        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
    }
}
