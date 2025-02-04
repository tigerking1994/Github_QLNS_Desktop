using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaThongTinDuAnCTCExportModel : ModelBase
    {
        private string _iStt;
        public string IStt
        {
            get => _iStt;
            set => SetProperty(ref _iStt, value);
        }

        private string _sTenDuAn;
        public string STenDuAn
        {
            get => _sTenDuAn;
            set => SetProperty(ref _sTenDuAn, value);
        }

        private string _iIdMaChuDauTu;
        public string IIdMaChuDauTu
        {
            get => _iIdMaChuDauTu;
            set => SetProperty(ref _iIdMaChuDauTu, value);
        }

        private string _sMaPhanCapPheDuyet;
        public string SMaPhanCapPheDuyet
        {
            get => _sMaPhanCapPheDuyet;
            set => SetProperty(ref _sMaPhanCapPheDuyet, value);
        }

        private string _sMaDuAn;
        public string SMaDuAn
        {
            get => _sMaDuAn;
            set => SetProperty(ref _sMaDuAn, value);
        }

        private string _sKhoiCong;
        public string SKhoiCong
        {
            get => _sKhoiCong;
            set => SetProperty(ref _sKhoiCong, value);
        }

        private string _sKetThuc;
        public string SKetThuc
        {
            get => _sKetThuc;
            set => SetProperty(ref _sKetThuc, value);
        }

        private string _sSoDauTu;
        public string SSoDauTu
        {
            get => _sSoDauTu;
            set => SetProperty(ref _sSoDauTu, value);
        }

        private string _dNgayDauTu;
        public string DNgayDauTu
        {
            get => _dNgayDauTu;
            set => SetProperty(ref _dNgayDauTu, value);
        }

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private string _dNgayQuyetDinh;
        public string DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set => SetProperty(ref _dNgayQuyetDinh, value);
        }
    }
}
