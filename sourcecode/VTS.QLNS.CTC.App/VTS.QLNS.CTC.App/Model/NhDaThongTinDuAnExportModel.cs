using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhDaThongTinDuAnExportModel : ModelBase
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

        private string _sTenHangMuc;
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }

        private string _sMaLoaiCongTrinh;
        public string SMaLoaiCongTrinh
        {
            get => _sMaLoaiCongTrinh;
            set => SetProperty(ref _sMaLoaiCongTrinh, value);
        }

        private string _iIdNguonVonId;
        public string IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private string _sDiaDiem;
        public string SDiaDiem
        {
            get => _sDiaDiem;
            set => SetProperty(ref _sDiaDiem, value);
        }

        private string _sMucTieu;
        public string SMucTieu
        {
            get => _sMucTieu;
            set => SetProperty(ref _sMucTieu, value);
        }

        private string _fGiTriUsd;
        public string FGiaTriUsd
        {
            get => _fGiTriUsd;
            set => SetProperty(ref _fGiTriUsd, value);
        }

        private string _fGiTriVnd;
        public string FGiaTriVnd
        {
            get => _fGiTriVnd;
            set => SetProperty(ref _fGiTriVnd, value);
        }
    }
}
