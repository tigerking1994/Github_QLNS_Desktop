using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDmNhaThauNguoiNhanModel : ModelBase
    {
        public Guid? IIdNhaThauId { get; set; }

        private string _stenNguonVon;
        public string STenNguoiNhan 
        {
            get => _stenNguonVon;
            set => SetProperty(ref _stenNguonVon, value);
        }

        private string _sSoCmnd;
        public string SSoCmnd
        {
            get => _sSoCmnd;
            set => SetProperty(ref _sSoCmnd, value);
        }

        private string _sNoiCapCmnd;
        public string SNoiCapCmnd
        {
            get => _sNoiCapCmnd;
            set => SetProperty(ref _sNoiCapCmnd, value);
        }

        private DateTime? _dNgayCapCmnd;
        public DateTime? DNgayCapCmnd
        {
            get => _dNgayCapCmnd;
            set => SetProperty(ref _dNgayCapCmnd, value);
        }
        public string SChucVu { get; set; }
        public string SDienThoai { get; set; }
        public string SFax { get; set; }
        public string SEmail { get; set; }
    }
}
