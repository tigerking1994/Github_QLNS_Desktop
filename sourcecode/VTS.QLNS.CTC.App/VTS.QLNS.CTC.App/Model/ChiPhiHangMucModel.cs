using System;

namespace VTS.QLNS.CTC.App.Model
{
    public class ChiPhiHangMucModel : ModelBase
    {
        public Guid? IdCreate { get; set; }
        public int ILoai { get; set; }
        public string sNoiChuoi { get; set; }
        public Guid? IId_HangMuc { get; set; }
        public string SLoaiCongTrinh { get; set; }
        public Guid? IID_ChiPhi { get; set; }

        private int? _iId_NguonVon;
        public int? IId_NguonVon
        {
            get => _iId_NguonVon;
            set => SetProperty(ref _iId_NguonVon, value);
        }

        public string SChiPhi { get; set; }
        public string SNoiDung { get; set; }
        public Guid? IID_ParentID { get; set; }

        private double? _fTienPheDuyet;
        public double? FTienPheDuyet
        {
            get => _fTienPheDuyet;
            set
            {
                SetProperty(ref _fTienPheDuyet, value);
            }
        }

        private double? _fTienGoiThau;
        public double? FTienGoiThau
        {
            get => _fTienGoiThau;
            set => SetProperty(ref _fTienGoiThau, value);
        }

        private double? _fGiaTriConLai;
        public double? FGiaTriConLai
        {
            get => _fGiaTriConLai;
            set => SetProperty(ref _fGiaTriConLai, value);
        }

        private bool _IsReadOnly;
        public bool IsReadOnly
        {
            get => _IsReadOnly;
            set => SetProperty(ref _IsReadOnly, value);
        }

        private bool _isEditHangMuc;
        public bool IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        public bool IsConLai { get; set; }
        public string MaOrDer { get; set; }
        public int? IThuTu { get; set; }


        private bool? _isChecked;
        public new bool? IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private bool? _isEdit;
        public bool? IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
    }
}
