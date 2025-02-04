using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class TlRptLuongKeHoachModel
    {
        public decimal? SaiSo { get; set; }
        public decimal? TongCong { get; set; }
        public decimal? PhanTram { get; set; }
        public decimal? PhuCapRaQuan { get; set; }
        public decimal? ChiPhiVaoQuan { get; set; }
        public decimal? TongQuyetToan { get; set; }
        public ObservableCollection<TlQtChungTuChiTietKeHoachModel> ItemsChungTuChiTiet { get; set; }
    }
}
