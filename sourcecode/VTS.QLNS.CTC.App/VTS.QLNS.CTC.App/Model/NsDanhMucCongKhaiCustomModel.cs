using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{

    public class NsDanhMucCongKhaiCustomModel : ModelBase
    {
        public string sMoTa { get; set; }

        public int? iNamLamViec { get; set; } = 0;

        public DateTime dNgayTao { get; set; }


        public string sNguoiTao { get; set; }
        public string sNguoiSua { get; set; }
        public string Tag { get; set; }

        public string Log { get; set; }

        public Guid? iID_DMCongKhai_Cha { get; set; }
        public bool bHangCha { get; set; }
        public string sMa { get; set; }
        public string sMaCha { get; set; }
        public string STT { get; set; }
        public string sSpace { get; set; }

    }

}
