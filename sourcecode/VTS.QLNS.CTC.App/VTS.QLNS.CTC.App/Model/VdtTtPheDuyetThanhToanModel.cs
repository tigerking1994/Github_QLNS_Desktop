using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtPheDuyetThanhToanModel : BindableBase
    {
        public int iRowIndex { get; set; }
        public Guid Id { get; set; }
        public string sSoDeNghi { get; set; }

        private Guid _iID_DeNghiThanhToan;
        public Guid iID_DeNghiThanhToan 
        { 
            get => _iID_DeNghiThanhToan; 
            set => SetProperty(ref _iID_DeNghiThanhToan, value); 
        }

        private string _sSoQuyetDinh;
        public string sSoQuyetDinh 
        { 
            get => _sSoQuyetDinh; 
            set => SetProperty(ref _sSoQuyetDinh, value); 
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? dNgayQuyetDinh 
        { 
            get => _dNgayQuyetDinh; 
            set => SetProperty(ref _dNgayQuyetDinh, value); 
        }

        private Guid? _iID_DonViQuanLyID;
        public Guid? iID_DonViQuanLyID 
        { 
            get => _iID_DonViQuanLyID; 
            set => SetProperty(ref _iID_DonViQuanLyID, value); 
        }

        private string _iID_MaDonViQuanLy;
        public string iID_MaDonViQuanLy 
        { 
            get => _iID_MaDonViQuanLy; 
            set => SetProperty(ref _iID_MaDonViQuanLy, value); 
        }

        public string sTenDonVi { get; set; }

        private Guid? _iID_NhomQuanLyID;
        public Guid? iID_NhomQuanLyID 
        { 
            get => _iID_NhomQuanLyID; 
            set => SetProperty(ref _iID_NhomQuanLyID, value); 
        }

        private string _sNguoiLap;
        public string sNguoiLap 
        { 
            get => _sNguoiLap; 
            set => SetProperty(ref _sNguoiLap, value); 
        }

        private string _sGhiChu;
        public string sGhiChu 
        { 
            get => _sGhiChu; 
            set => SetProperty(ref _sGhiChu, value); 
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public int? iNamLamViec { get; set; }
    }
}
