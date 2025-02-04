using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class DuAnKeHoachTrungHanDeXuatModel : CheckBoxItem
    {
        public Guid? IIdDuAnId { get; set; }
        public string SMaDuAn { get; set; }
        public string STenDuAn { get; set; }
        public Guid? Id_DuAnKhthDeXuat { get; set; }
    }
}
