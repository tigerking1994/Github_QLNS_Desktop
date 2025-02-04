using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;

namespace VTS.QLNS.CTC.App.Model
{
    public class DmDeTaiModel : ModelBase
    {
        private string _sMa;
        [DisplayName("Mã đề tài")]
        [DisplayDetailInfo("Mã đề tài")]
        public string SMa
        {
            get => _sMa;
            set => SetProperty(ref _sMa, value);
        }

        private string _sMota;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMota 
        {
            get => _sMota;
            set => SetProperty(ref _sMota, value);
        }
    }
}
