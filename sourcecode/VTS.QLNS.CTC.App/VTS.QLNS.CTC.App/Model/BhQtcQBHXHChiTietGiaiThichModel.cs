using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhQtcQBHXHChiTietGiaiThichModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid IID_QTC_QChungTu { get; set; }
        public int INamLamViec { get; set; }
        public int IQuy { get; set; }
        public string SMoTa { get; set; }
        private string _sMoTa_KienNghi;

        public string SMoTa_KienNghi
        {
            get => _sMoTa_KienNghi;
            set
            {
                SetProperty(ref _sMoTa_KienNghi, value);
            }
        }

        private string _sMoTa_TinhHinh;
        public string SMoTa_TinhHinh
        {
            get => _sMoTa_TinhHinh;
            set
            {
                SetProperty(ref _sMoTa_TinhHinh, value);
            }
        }

        public string SLNS { get; set; }
        public string SMaLoaiChi { get; set; }
        public DateTime? DNgaySua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public string SNguoiSua { get; set; }
        public string SNguoiTao { get; set; }
    }
}
