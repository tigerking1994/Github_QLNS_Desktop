using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtThongTriChiTietModel : DetailModelBase
    {
        public string SMaKieuThongTri { get; set; }
        public string SSoThongTri { get; set; }
        public string STenDuAn { get; set; }

        private Guid? _iIdDuAnId;
        public Guid? IIdDuAnId
        {
            get => _iIdDuAnId;
            set => SetProperty(ref _iIdDuAnId, value);
        }

        public Guid? IIdNhaThauId { get; set; }
        public double FSoTien { get; set; }
        public Guid? IIdMucId { get; set; }
        public Guid? IIdTieuMucId { get; set; }
        public Guid? IIdTietMucId { get; set; }
        public Guid? IIdNganhId { get; set; }
        public Guid? IIdLoaiCongTrinhId { get; set; }
        public Guid? IIdLoaiNguonVonId { get; set; }
        public Guid? IIdCapPheDuyetId { get; set; }
        public Guid? IIdDeNghiThanhToanId { get; set; }

        private string _sLns;
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private string _sL;
        public string SL
        {
            get => _sL;
            set => SetProperty(ref _sL, value);
        }

        private string _sK;
        public string SK
        {
            get => _sK;
            set => SetProperty(ref _sK, value);
        }

        private string _sM;
        public string SM
        {
            get => _sM;
            set => SetProperty(ref _sM, value);
        }

        private string _sTm;
        public string STm
        {
            get => _sTm;
            set => SetProperty(ref _sTm, value);
        }

        private string _sTtm;
        public string STtm
        {
            get => _sTtm;
            set => SetProperty(ref _sTtm, value);
        }

        private string _sNg;
        public string SNg
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sDonViThuHuong;
        public string SDonViThuHuong
        {
            get => _sDonViThuHuong;
            set => SetProperty(ref _sDonViThuHuong, value);
        }

        public string SDonViThuHuongFormat
        {
            get
            {
                return string.IsNullOrEmpty(_sDonViThuHuong) ? string.Empty : $"{_sDonViThuHuong} : ";
            }
        }
    }
}
